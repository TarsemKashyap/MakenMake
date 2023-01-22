using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Pages
{
    public partial class EnginerServiceTickets : System.Web.UI.Page
    {
        BLServiceEngineer objEngineer = new BLServiceEngineer();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTicketData();
            }
        }
        private void BindTicketData(string TicketIDOrName, int inspectionType, int findwhat)
        {
            BLServiceEngineer objEngineer = new BLServiceEngineer();
            DataTable dt = objEngineer.SearchTickets(Convert.ToInt64(Session[Constant.Session.AdminSession]), TicketIDOrName, inspectionType, findwhat);
            if (dt != null && dt.Rows.Count > 0)
            {
                tblPaging.Visible = true;
                lblMsg.Text = string.Empty;
                RptTickets.DataSource = dt;
                RptTickets.DataBind();
            }
            else
            {
                tblPaging.Visible = false;
                RptTickets.DataSource = null;
                RptTickets.DataBind();
                lblMsg.Text = "No Tickets are available according to search criteria";
            }
        }
        protected void RptTickets_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Customer")
            {
                Int64 userID = Convert.ToInt64(e.CommandArgument);
                UserInfo.BindData(userID);
                hdnCustomer.Value = "1";
            }
            else if (e.CommandName == "detail")
            {
                HiddenField hdnStat = (HiddenField)e.Item.FindControl("hdnStat");
                Response.Redirect("TicketDetail.aspx?TicketID=" + Utilities.EncryptDecrypt.Encript(Convert.ToString(e.CommandArgument)) + "&Status=" + hdnStat.Value);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindTicketData((txtIDName.Text.Replace("'", "")).Trim(), Convert.ToInt32(ddlTicketType.SelectedValue), Convert.ToInt32(ddlSearch.SelectedValue));
        }
        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlSearch.SelectedValue == "0" || ddlSearch.SelectedValue == "1")
            {
                dvTicketIDName.Visible = true;
                ddltype.Visible = false;
                DvSubmit.Visible = true;
            }
            else if (ddlSearch.SelectedValue == "2")
            {
                DvSubmit.Visible = true;
                dvTicketIDName.Visible = false;
                ddltype.Visible = true;
            }
            else
            {
                DvSubmit.Visible = false;
                dvTicketIDName.Visible = false;
                ddltype.Visible = false;
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }
        DataTable GetticketData(int currentpage, Int64 EngID)
        {
            DataTable dtable = objEngineer.GetTickets(CurrentPage, EngID);

            return dtable;
        }
        private void BindTicketData()
        {
            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetticketData(CurrentPage, Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];

            if (dt != null && dt.Rows.Count > 0)
            {
                lblMsg.Text = string.Empty;
                RptTickets.DataSource = dt;
                RptTickets.DataBind();
            }
            else
            {
                RptTickets.DataSource = null;
                RptTickets.DataBind();
                lblMsg.Text = "No Tickets for you";
            }

            doPaging();
            RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        }
        private void doPaging()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageText");
            findex = CurrentPage - 5;
            if (CurrentPage > 5)
            {
                lindex = CurrentPage + 5;
            }
            else
            {
                lindex = 10;
            }

            if (lindex > Convert.ToInt32(ViewState["totpage"]))
            {
                lindex = Convert.ToInt32(ViewState["totpage"]);
                findex = lindex - 10;
            }

            if (findex < 0)
            {
                findex = 0;
            }

            for (int i = findex; i < lindex; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            RepeaterPaging.DataSource = dt;
            RepeaterPaging.DataBind();
        }
        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                {
                    return 0;
                }
                else
                {
                    return ((int)ViewState["CurrentPage"]);
                }
            }
            set
            {

                ViewState["CurrentPage"] = value;
            }
        }

        protected void RepeaterPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("newpage"))
            {

                CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
                BindTicketData();
            }
        }

        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            BindTicketData();
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            BindTicketData();
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindTicketData();
            }
            else
            {
                CurrentPage = 0;
                BindTicketData();

            }

        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindTicketData();
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                BindTicketData();
            }
        }

        protected void RepeaterPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            LinkButton lnkPage = (LinkButton)e.Item.FindControl("Pagingbtn");
            if (lnkPage.CommandArgument.ToString() == CurrentPage.ToString())
            {
                lnkPage.Enabled = false;
                lnkPage.BackColor = System.Drawing.Color.FromName("#970915");
            }
        }

        protected void RptTickets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblreasn = (Label)e.Item.FindControl("lblDescription");
                if (lblreasn.Text.Length > 25)
                {
                    lblreasn.Text = lblreasn.Text.Substring(0, 25);
                    LinkButton lnkBtnMore = (LinkButton)e.Item.FindControl("lnkBtnMore");
                    lnkBtnMore.Visible = true;
                    lnkBtnMore.OnClientClick = "ShowMsg(this)";
                }
                else
                {
                    LinkButton lnkBtnMore = (LinkButton)e.Item.FindControl("lnkBtnMore");
                    lnkBtnMore.Visible = false;
                }
                HiddenField hdnStat = (HiddenField)e.Item.FindControl("hdnStat");
                Label lblTicketID = (Label)e.Item.FindControl("lblTicketID");
                LinkButton LinkButton1 = (LinkButton)e.Item.FindControl("LinkButton1");
                if (hdnStat.Value == "3")
                {
                    lblTicketID.Visible = true;
                    LinkButton1.Visible = false;
                }
                else
                {
                    lblTicketID.Visible = false;
                    LinkButton1.Visible = true;
                }

            }
        }

    }
}
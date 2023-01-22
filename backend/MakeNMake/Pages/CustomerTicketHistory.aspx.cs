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
    public partial class CustomerTicketHistory : System.Web.UI.Page
    {

        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTicket();
            }
        }
        private void BindTicket()
        {
            BL.BLConsumer obj = new BL.BLConsumer();
            DataTable dt = obj.GetTicketsByUserID(Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlTickets.DataSource = dt;
                ddlTickets.DataTextField = "TicketID";
                ddlTickets.DataValueField = "TicketID";
                ddlTickets.DataBind();
                ddlTickets.Items.Insert(0, new ListItem("--Select Ticket Id--", "0"));
            }
            else
            {
                ddlTickets.Items.Insert(0, new ListItem("--Select Ticket Id--", "0"));
            }
        }
        DataTable GetAppointmentHistory(int currentpage, Int64 TicketID)
        {
            BLEscalation obj = new BLEscalation();
            DataTable dtable = obj.GetTicketHistoryByTicketID(currentpage, TicketID);
            return dtable;
        }
        private int BindData(Int64 TicketID)
        {


            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetAppointmentHistory(CurrentPage, TicketID);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["Historytotpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount2"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["Historytotpage"];



            if (dt != null && dt.Rows.Count > 0)
            {
                tblticket.Visible = true;
                RptTickets.DataSource = dt;
                RptTickets.DataBind();
                doPaging();
                RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            }
            else
            {
                tblticket.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No Tickets History') ;", true);
            }


            return (Convert.ToInt32(dt.Rows.Count));
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
                BindData(Convert.ToInt64(ddlTickets.SelectedValue));
            }
        }
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindData(Convert.ToInt64(ddlTickets.SelectedValue));
        }
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            BindData(Convert.ToInt64(ddlTickets.SelectedValue));
        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindData(Convert.ToInt64(ddlTickets.SelectedValue));
            }
            else
            {
                CurrentPage = 0;
                BindData(Convert.ToInt64(ddlTickets.SelectedValue));
            }

        }
        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindData(Convert.ToInt64(ddlTickets.SelectedValue));
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                BindData(Convert.ToInt64(ddlTickets.SelectedValue));
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
        protected void ddlTickets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTickets.SelectedValue != "0")
            {
                RptTickets.Visible = true;
                BindData(Convert.ToInt64(ddlTickets.SelectedValue));
            }
            else
            {
                RptTickets.Visible = false;
            }
        }

    }
}
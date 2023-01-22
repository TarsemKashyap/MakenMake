using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.ServiceEngineer
{
    public partial class AppoinmentTickets : System.Web.UI.Page
    {
        BLServiceEngineer objEngineer = new BLServiceEngineer();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAppointmentData();
            }
        }

        protected void RptAppointment_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Customer")
            {
                Int64 userID = Convert.ToInt64(e.CommandArgument);
                UserInfo.BindData(userID);
                hdnCustomer.Value = "1";
            }
            else if (e.CommandName == "statusChange")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string AppointMentID = commandArgs[0];
                string Status = commandArgs[1];
                Response.Redirect("AppointMentDetails.aspx?AppointMentID=" + Utilities.EncryptDecrypt.Encript(Convert.ToString(AppointMentID)) + "&Status=" + Convert.ToString(Status));
            }
        }
        //26-05-2015
        DataTable GetBindAppointmentData(int currentpage, Int64 EngID)
        {
            DataTable dtable = objEngineer.GetAppoinments(currentpage, EngID);
            return dtable;
        }


        private int BindAppointmentData()
        {

            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetBindAppointmentData(CurrentPage, Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if (dt != null && dt.Rows.Count > 0)
            {
                lnkNext.Visible = true;
                lnkLast.Visible = true;
                lnkFirst.Visible = true;
                lnkPrevious.Visible = true;
                lblpage.Visible = true;
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalcount"]) / 10));
            }
            else
            {
                lnkNext.Visible = false;
                lnkLast.Visible = false;
                lnkFirst.Visible = false;
                lnkPrevious.Visible = false;
                lblpage.Visible = false;
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];



            if (dt != null && dt.Rows.Count > 0)
            {
                RptAppointment.DataSource = dt;
                RptAppointment.DataBind();
                doPaging();
                RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            }
            else
            {
                lblMsg.Text = "No Appointments for you";
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
                BindAppointmentData();
            }
        }

        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            BindAppointmentData();
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            BindAppointmentData();
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindAppointmentData();
            }
            else
            {
                CurrentPage = 0;
                BindAppointmentData();

            }

        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindAppointmentData();
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                BindAppointmentData();
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

        protected void RptAppointment_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblreasn = (Label)e.Item.FindControl("lblReason");
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
            }
        }

    }
}
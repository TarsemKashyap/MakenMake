using MakeNMake.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Admin
{
    public partial class Appointments : System.Web.UI.Page
    {
        BL.BLAdmin objAdmin = new BL.BLAdmin();
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
            else if (e.CommandName == "changeStatus")
            {
                Label lblticketType = (Label)e.Item.FindControl("lblTicketType");
                if (lblticketType.Text.ToLower() == "appointment")
                {
                    HiddenField EngineerID = (HiddenField)e.Item.FindControl("EngineerID");
                    HiddenField lblStatus = (HiddenField)e.Item.FindControl("hdnStatus");
                    Response.Redirect("ChangeAppointment.aspx?AppointmentID=" + e.CommandArgument + "&AppoinmentStatus=" + lblStatus.Value + "&EngineerID=" + EngineerID.Value);
                }
                else
                {
                    Response.Redirect("TicketHistoryViaTicketID.aspx?TicketID=" + e.CommandArgument + "&TicketType=1");
                }
            }
        }

        DataTable GetBindData(int currentpage,string clientName)
        {
            DataTable dtable = objAdmin.GetAllAppointments(currentpage,clientName);
            
            return dtable;
        }


        private int BindAppointmentData()
        {

            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetBindData(CurrentPage, txtSearchclient.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                tblPaging.Visible = true;
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
            }
            else
            {
                tblPaging.Visible = false;

            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];



            if (dt != null && dt.Rows.Count > 0)
            {
                RptAppointment.DataSource = dt;
                RptAppointment.DataBind();
                lblMsg.Text = string.Empty;
            }
            else
            {
                lblMsg.Text = "No Appointments";
            }

            doPaging();
            RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
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

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            if (txtSearchclient.Text != "")
            {
                CurrentPage = 0;
                int x = BindAppointmentData();
                if (x != 0)
                {
                    tblPaging.Visible = true;
                    divClientList.Visible = true;
                }
                else
                {
                    tblPaging.Visible = false;
                    divClientList.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "PageReload()", true);
                    txtSearchclient.Text = null;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please Enter the Customer Name,Engineer Name or Status!') ;", true);
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
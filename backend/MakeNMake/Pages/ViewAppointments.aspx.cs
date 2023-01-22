using MakeNMake.BL;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Excalation
{
    public partial class ViewAppointments : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        BLEscalation objEscalation = new BLEscalation();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        int AppoinmentStatus = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ShowAllAppointment();
                }
                catch (Exception ex)
                {
                    logger.Error(logger.Name + ":" + ex.Message);
                }
            }
        }

        DataTable GetAppointment(int currentpage)
        {
            DataTable dtable = objEscalation.GetAllAppoinments(currentpage, AppoinmentStatus, txtSearchclient.Text);
            return dtable;
        }

        private int ShowAllAppointment()
        {


            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetAppointment(CurrentPage);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];



            if (dt != null && dt.Rows.Count > 0)
            {
                divClientList.Visible = true;
                RptTickets.DataSource = dt;
                RptTickets.DataBind();
                doPaging();
                RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            }
            else
            {
                divClientList.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No Appointment') ;", true);
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
                ShowAllAppointment();
            }
        }

        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            ShowAllAppointment();
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            ShowAllAppointment();
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                ShowAllAppointment();
            }
            else
            {
                CurrentPage = 0;
                ShowAllAppointment();

            }

        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                ShowAllAppointment();
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                ShowAllAppointment();
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


        protected void RptTickets_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            //if (e.CommandName == "checkhistory")
            //{
            //    Int64 AppointmentID = Convert.ToInt64(e.CommandArgument);
            //    Response.Redirect("AppointmentHistory.aspx?AppointmentID=" + AppointmentID + "&AppoinmentStatus=" + AppoinmentStatus);
            //}
            if (e.CommandName == "AppointmentID")
            {
                try
                {
                    HiddenField hdnEngineerID = (HiddenField)e.Item.FindControl("hdnEngineerID");
                    Int64 AppointmentID = Convert.ToInt64(e.CommandArgument);
                    Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                    int status = 0;
                    if (lblStatus.Text.ToLower() == "accepted")
                    {
                        status = 5;
                    }
                    else if (lblStatus.Text.ToLower() == "assigned")
                    {
                        status = 1;
                    }

                    else if (lblStatus.Text.ToLower() == "escalated")
                    {
                        status = 4; hdnEngineerID.Value = "0";
                    }
                    else if (lblStatus.Text.ToLower() == "completed")
                    {
                        status = 3;
                    }
                    else
                    {
                        status = 0; hdnEngineerID.Value = "0";
                    }
                    Response.Redirect("AppointmentHistory.aspx?AppointmentID=" + AppointmentID + "&AppoinmentStatus=" + AppoinmentStatus+"&EngineerID="+Convert.ToString(hdnEngineerID.Value));
                  
                }
                catch (Exception te)
                {
                    //do nothing or handle as required
                }


            }

        }

        //protected void btnView_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("ViewAppoinmentNotInProcess.aspx");
        //}
        protected void btnSubmit1_Click(object sender, EventArgs e)
        {
            if (txtSearchclient.Text != "")
            {
                CurrentPage = 0;
                int x = ShowAllAppointment();
                if (x != 0)
                {
                    Table1.Visible = true;
                    divClientList.Visible = true;
                }
                else
                {
                    Table1.Visible = false;
                    divClientList.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No Record Found!') ;", true);
                    txtSearchclient.Text = null;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please Enter the Customer Name.Status,Engineer Name or TicketId') ;", true);
            }
        }
    }
}
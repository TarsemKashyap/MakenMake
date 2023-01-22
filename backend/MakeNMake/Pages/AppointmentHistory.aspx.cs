using MakeNMake.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MakeNMake.CommomFunctions;

namespace MakeNMake.Excalation
{
    public partial class AppointmentHistory : System.Web.UI.Page
    {
        BLEscalation objEscalation = new BLEscalation();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        Int64 AppointmentID;
        Int64 EngineerID;
        int AppoinmentStatus;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["Session_UserID"] = Convert.ToString(Session[Constant.Session.AdminSession]);
            AppointmentID = Convert.ToInt64(Request.QueryString["AppointmentID"]);
            EngineerID = Convert.ToInt64(Request.QueryString["EngineerID"]);
            lblhstryAppointmentID.Text =Convert.ToString(AppointmentID);
            AppoinmentStatus = Convert.ToInt32(Request.QueryString["AppoinmentStatus"]);
            if (!IsPostBack)
            {
                AllAppointmentHistory();
                BindEngineer();
                FindEngineerStatus();
            }
        }


        protected void BindEngineer()
        {
            DataTable dt = objEscalation.BindAllEngineer(AppointmentID,"Appointment");
            ddlEngineer.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlEngineer.DataTextField = "allEngineerName";
                ddlEngineer.DataValueField = "allEngineerID";
                ddlEngineer.DataBind();
                ddlEngineer.Items.Insert(0, new ListItem("--Select Engineer--", "0"));
                if (AppoinmentStatus==1)
                {
                     ddlEngineer.SelectedValue = EngineerID.ToString();
                     //ddlEngineer.Enabled = false;
                }
                if (AppoinmentStatus == 2)
                {
                    ddlEngineer.Items.Insert(0, new ListItem("", "0"));
                    ddlEngineer.Enabled = true;
                }
               
            }
            else
            {
                ddlEngineer.Items.Insert(0, new ListItem("--No Engineer Found--", "0"));
            }

        }

        protected void FindEngineerStatus()
        {
            if (AppointmentID == 0)
            {
                Response.Redirect("AppointmentHistory.aspx");
            }
            DataTable dt = objEscalation.FindEngineerStatus(AppointmentID,"Appointment");
            int Status =Convert.ToInt32(dt.Rows[0]["Status"]);
            if (Status == 0 || Status == 1 || Status == 2 || Status == 4)
            {
                ddlEngineer.Enabled = true;
                ddlStatus.Enabled = true;
            }
            if (Status == 5)
            {
                ddlEngineer.Enabled = false;
               
                ddlStatus.Items.Insert(0, new ListItem("Accepted", "0"));
                ddlStatus.Enabled = false;
                divSubmitButton.Visible=false;
            }
            if (Status == 3)
            {
                ddlEngineer.Enabled = false;
                ddlStatus.Items.Insert(0, new ListItem("Completed", "0"));
                ddlStatus.Enabled = false;
                divSubmitButton.Visible=false;
            }
        }


        DataTable GetAppointmentHistory(int currentpage, Int64 AppointmentID)
        {
            DataTable dtable = objEscalation.AllAppointmentHistory(currentpage, AppointmentID);
            return dtable;
        }

        private int AllAppointmentHistory()
        {


            pgsource.CurrentPageIndex = CurrentPage2;
            DataTable dt = GetAppointmentHistory(CurrentPage2, AppointmentID);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["Historytotpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount2"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage2.Text = "Page " + (CurrentPage2 + 1) + " of " + ViewState["Historytotpage"];



            if (dt != null && dt.Rows.Count > 0)
            {
                DvAppoint_History.Visible = true;
                RptHistory.DataSource = dt;
                RptHistory.DataBind();
                doPaging2();
                RepeaterPaging2.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            }
            else
            {
                DvAppoint_History.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No Appointment History') ;", true);
            }


            return (Convert.ToInt32(dt.Rows.Count));
        }

        private void doPaging2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PageIndex2");
            dt.Columns.Add("PageText2");
            findex = CurrentPage2 - 5;
            if (CurrentPage2 > 5)
            {
                lindex = CurrentPage2 + 5;
            }
            else
            {
                lindex = 10;
            }

            if (lindex > Convert.ToInt32(ViewState["Historytotpage"]))
            {
                lindex = Convert.ToInt32(ViewState["Historytotpage"]);
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

            RepeaterPaging2.DataSource = dt;
            RepeaterPaging2.DataBind();

        }
        private int CurrentPage2
        {
            get
            {
                if (ViewState["CurrentPage2"] == null)
                {
                    return 0;
                }
                else
                {
                    return ((int)ViewState["CurrentPage2"]);
                }
            }
            set
            {

                ViewState["CurrentPage2"] = value;
            }
        }


        protected void RepeaterPaging2_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("newpage2"))
            {

                CurrentPage2 = Convert.ToInt32(e.CommandArgument.ToString());
                AllAppointmentHistory();
            }
        }

        protected void lnkFirst2_Click(object sender, EventArgs e)
        {

            CurrentPage2 = 0;
            AllAppointmentHistory();
        }

        protected void lnkLast2_Click(object sender, EventArgs e)
        {

            CurrentPage2 = (Convert.ToInt32(ViewState["Historytotpage"]) - 1);
            AllAppointmentHistory();
        }

        protected void lnkPrevious2_Click(object sender, EventArgs e)
        {

            CurrentPage2 -= 1;
            if (CurrentPage2 >= 0 && CurrentPage2 < Convert.ToInt16(ViewState["Historytotpage"]))
            {
                AllAppointmentHistory();
            }
            else
            {
                CurrentPage2 = 0;
                AllAppointmentHistory();

            }

        }

        protected void lnkNext2_Click(object sender, EventArgs e)
        {

            CurrentPage2 += 1;

            if (CurrentPage2 < Convert.ToInt16(ViewState["Historytotpage"]))
            {
                AllAppointmentHistory();
            }
            else
            {
                CurrentPage2 = (Convert.ToInt32(ViewState["Historytotpage"]) - 1);
                AllAppointmentHistory();
            }
        }


        protected void RepeaterPaging2_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            LinkButton lnkPage = (LinkButton)e.Item.FindControl("Pagingbtn2");
            if (lnkPage.CommandArgument.ToString() == CurrentPage2.ToString())
            {
                lnkPage.Enabled = false;
                lnkPage.BackColor = System.Drawing.Color.FromName("#970915");
            }
        }

        protected void ddlEngineer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DivComments.Visible = true;
            
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlStatus.SelectedValue) == 1)
            {
                ddlEngineer.Visible = true;
                ddlEngineer.Enabled = true;
            }
            if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
            {
                ddlEngineer.Visible = false;
                ddlEngineer.Enabled = false;
            }
             DivComments.Visible = true;
            
        }
        

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewAppointments.aspx");         
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            objEscalation.AssignAppointmentsToEnginner(Convert.ToInt64(ddlEngineer.SelectedValue), Convert.ToInt64(ViewState["Session_UserID"]), Convert.ToInt64(AppointmentID), txtComments.Text, Convert.ToInt32(ddlStatus.SelectedValue));
            SentMailSms(Convert.ToInt64(AppointmentID), Convert.ToInt64(ddlEngineer.SelectedValue));
            Response.Redirect("ViewAppointments.aspx");
        }
        private void SentMailSms(Int64 ticketID, Int64 EngineerID)
        {
            BLCustomerCare objSend = new BLCustomerCare();
            DataTable dt = objSend.GetUSerByTicketID(ticketID , 2);

            string firstname = Convert.ToString(dt.Rows[0]["name"]);
            string userid = Convert.ToString(dt.Rows[0]["UserID"]);
            string emailid = Convert.ToString(dt.Rows[0]["EmailID"]);
            string mobile = Convert.ToString(dt.Rows[0]["MobileNumber"]);
            string engineerName = string.Empty;

            if (dt != null && dt.Rows.Count > 0)
            {
                BLCustomerCare care = new BLCustomerCare();
                DataTable dtcare = care.GetEngineerByTicketID(ticketID);
                if (dtcare != null && dtcare.Rows.Count > 0)
                {
                    engineerName = Convert.ToString(dtcare.Rows[0]["name"]);
                    string message = "Hi, " + firstname + "! Your Appointment ticket Id: " + ticketID + " has been assigned to Engineer "
                        + engineerName + "He will serve you shortly. Please log in with your account details on our website (www.makenmake.in)/Mobile App to see the status of the ticket.Or call us at Helpline Nos:"+ReadConfig.helpLineNumber;
                    SendSms objSms = new SendSms();
                    try
                    {
                        int i = objSms.SendSmsOnMobile(message, mobile);
                        if (i != 1)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(userid), 0, "Escalation manager Sent to Client when Appointment logs", 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(userid), 0, "Error while Escalation manager Sent to Client when Appoinment logs-Issue:-" + ex.Message, 1);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dtcare.Rows[0]["MobileNumber"])))
                    {
                        message = "Hi," + "" + "! A new Appointment ticket Id: " + ticketID + " has been assigned to you. Please login to see the details.";
                        objSms = new SendSms();
                        try
                        {
                            int i = objSms.SendSmsOnMobile(message, Convert.ToString(dtcare.Rows[0]["MobileNumber"]));
                            if (i != 1)
                            {
                                BL.BLAdmin objAdmin = new BL.BLAdmin();
                                objAdmin.AddNotSendSmsMail(Convert.ToInt64(dtcare.Rows[0]["UserID"]), 0, "Appointmnt Status to Engineer", 1);
                            }
                        }
                        catch (Exception ex)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(dtcare.Rows[0]["UserID"]), 0, "Error while Appointmnt Status to Engineer-Issue:-" + ex.Message, 1);
                        }
                    }
                    else
                    {
                        MEmail.SendGMail(Convert.ToString(dtcare.Rows[0]["EmailID"]), "New Appointment Ticket Make 'N' Make",
                        "Dear " + engineerName + ",<br><br> A new Appointment ticket Id: " + ticketID + " has been assigned to you. Please login to see the details.", "");
                    }
                }
            }
            else
            {
                string message = "Hi," + firstname + "! Your Appointment ticket Id: " + ticketID + " . We will serve you shortly. Please log in with your account details on our website (www.makenmake.in)/Mobile App to see the status of the ticket.Or call us at Helpline Nos:"+ReadConfig.helpLineNumber;
                SendSms objSms = new SendSms();
                try
                {
                    int i = objSms.SendSmsOnMobile(message, mobile);
                    if (i != 1)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(userid), 0, "Appointment Status to Client", 1);
                    }
                }
                catch (Exception ex)
                {
                    BL.BLAdmin objAdmin = new BL.BLAdmin();
                    objAdmin.AddNotSendSmsMail(Convert.ToInt64(userid), 0, "Error while Appointment Status to Client-Issue:-" + ex.Message, 1);
                }
            }
        }
    }
}
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
    public partial class CheckTicketByID : System.Web.UI.Page
    {
        Int64 ticketid;
        Int64 EngineerID;
        int TicketStatus;
        BLEscalation obj = new BLEscalation();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["Session_UserID"] = Convert.ToString(Session[Constant.Session.AdminSession]);
            ViewState["TicketID"] = Convert.ToInt64(Request.QueryString["TicketID"]);
            EngineerID = Convert.ToInt64(Request.QueryString["EngineerID"]);
            lblhstryTicketID.Text = Convert.ToString(ViewState["TicketID"]);
            TicketStatus = Convert.ToInt32(Request.QueryString["TicketStatus"]);
            if (!IsPostBack)
            {
                try
                {
                    ticketid = Convert.ToInt64(Request.QueryString["TicketID"]);
                    BindData(ticketid);
                    BindEngineer();
                    FindEngineerStatus();
                }
                catch
                {
                    string TicketType = Convert.ToString(Request.QueryString["TicketType"]);
                    int roleid = Convert.ToInt32(Session[Constant.Session.Role]);
                    if (string.IsNullOrEmpty(TicketType) || TicketType == "")
                    {
                        if (roleid == 1)
                        {
                            Response.Redirect("Tickethistory.aspx");
                        }
                        else
                        {
                            Response.Redirect("ViewTickets.aspx");
                        }
                    }
                    else
                    {
                        if (roleid == 1)
                        {
                            Response.Redirect("Appointments.aspx");
                        }
                        else
                        {
                            Response.Redirect("Tickethistory.aspx");
                        }
                    }
                }
            }

        }

        DataTable GetAppointmentHistory(int currentpage, Int64 TicketID)
        {
            DataTable dtable = obj.GetTicketHistoryByTicketID(currentpage, TicketID);
            return dtable;
        }

        private int BindData(Int64 TicketID)
        {
            pgsource.CurrentPageIndex = CurrentPage2;
            DataTable dt = GetAppointmentHistory(CurrentPage2, Convert.ToInt64(ViewState["TicketID"]));
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["Historytotpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount2"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;
            lblpage2.Text = "Page " + (CurrentPage2 + 1) + " of " + ViewState["Historytotpage"];
            if (dt != null && dt.Rows.Count > 0)
            {
                DvTicket_History.Visible = true;
                RptTickets.DataSource = dt;
                RptTickets.DataBind();
                doPaging2();
                RepeaterPaging2.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            }
            else
            {
                DvTicket_History.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No Tickets History') ;", true);
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
                BindData(Convert.ToInt64(ViewState["TicketID"]));
            }
        }

        protected void lnkFirst2_Click(object sender, EventArgs e)
        {
            CurrentPage2 = 0;
            BindData(Convert.ToInt64(ViewState["TicketID"]));
        }

        protected void lnkLast2_Click(object sender, EventArgs e)
        {
            CurrentPage2 = (Convert.ToInt32(ViewState["Historytotpage"]) - 1);
            BindData(Convert.ToInt64(ViewState["TicketID"]));
        }

        protected void lnkPrevious2_Click(object sender, EventArgs e)
        {

            CurrentPage2 -= 1;
            if (CurrentPage2 >= 0 && CurrentPage2 < Convert.ToInt16(ViewState["Historytotpage"]))
            {
                BindData(Convert.ToInt64(ViewState["TicketID"]));
            }
            else
            {
                CurrentPage2 = 0;
                BindData(Convert.ToInt64(ViewState["TicketID"]));

            }

        }

        protected void lnkNext2_Click(object sender, EventArgs e)
        {

            CurrentPage2 += 1;

            if (CurrentPage2 < Convert.ToInt16(ViewState["Historytotpage"]))
            {
                BindData(Convert.ToInt64(ViewState["TicketID"]));
            }
            else
            {
                CurrentPage2 = (Convert.ToInt32(ViewState["Historytotpage"]) - 1);
                BindData(Convert.ToInt64(ViewState["TicketID"]));
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
        protected void BindEngineer()
        {
            DataTable dt = obj.BindAllEngineer(Convert.ToInt64(ViewState["TicketID"]), "ServiceTicket");
            ddlEngineer.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlEngineer.DataTextField = "allEngineerName";
                ddlEngineer.DataValueField = "allEngineerID";
                ddlEngineer.DataBind();
                // ddlEngineer.Items.Insert(0, new ListItem("--Select Engineer--", "0"));
                if (TicketStatus == 1 || TicketStatus == 3 || TicketStatus == 5)
                {
                    ddlEngineer.SelectedValue = EngineerID.ToString();
                    ddlEngineer.Items.Insert(0, new ListItem("--Select Engineer--", "0"));
                    ddlEngineer.Enabled = false;
                }
                else if (TicketStatus == 2)
                {
                    ddlEngineer.Items.Insert(0, new ListItem("--Select Engineer--", "0"));
                    ddlEngineer.Enabled = true;
                }
                else if (TicketStatus == 4)
                {
                    ddlEngineer.Items.Insert(0, new ListItem("--Select Engineer--", "0"));
                    ddlEngineer.Enabled = true;
                }
                else
                {
                    ddlEngineer.Items.Insert(0, new ListItem("--Select Engineer--", "0"));
                }
            }
            else
            {
                ddlEngineer.Items.Insert(0, new ListItem("--No Other Engineer Found in this Zone--", "0"));
            }

        }
        protected void FindEngineerStatus()
        {
            DataTable dt = obj.FindEngineerStatus(Convert.ToInt64(ViewState["TicketID"]), "Ticket");
            int Status = Convert.ToInt32(dt.Rows[0]["Status"]);
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
                divSubmitButton.Visible = false;
            }
            if (Status == 3)
            {
                ddlEngineer.Enabled = false;
                ddlStatus.Items.Insert(0, new ListItem("Completed", "0"));
                ddlStatus.Enabled = false;
                divSubmitButton.Visible = false;
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
                dvAssigned.Visible = true;
            }
            if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
            {
                dvAssigned.Visible = false;
            }
            DivComments.Visible = true;

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string TicketType = Convert.ToString(Request.QueryString["TicketType"]);
            int roleid = Convert.ToInt32(Session[Constant.Session.Role]);
            if (string.IsNullOrEmpty(TicketType) || TicketType == "")
            {
                if (roleid == 1)
                {
                    Response.Redirect("Tickethistory.aspx");
                }
                else
                {
                    Response.Redirect("ViewTickets.aspx");
                }
            }
            else
            {
                if (roleid == 1)
                {
                    Response.Redirect("Appointments.aspx");
                }
                else
                {
                    Response.Redirect("Tickethistory.aspx");
                }
            }
        }

        //private void SentMailSms(Int64 UserID, Int64 ticketID)
        //{
        //    Common objSend = new Common();
        //    DataTable dt = objSend.GetUserInfoByID(UserID);
        //    string firstname = Convert.ToString(dt.Rows[0]["firstname"]);
        //    string emailid = Convert.ToString(dt.Rows[0]["Emailid"]);
        //    string mobile = Convert.ToString(dt.Rows[0]["MNumber"]);
        //    string engineerName = string.Empty;

        //    string message1 = "Hi," + firstname + "! Your Service ticket Id: " + ticketID + " has been assigned to Engineer XXXXXXXX. ";

          


        //  SendSms objSms1 = new SendSms();
        //    try
        //    {
        //        int i = objSms1.SendSmsOnMobile(message1, mobile);
        //        if (i != 1)
        //        {
        //            BL.BLAdmin objAdmin = new BL.BLAdmin();
        //            objAdmin.AddNotSendSmsMail(Convert.ToInt64(UserID), 0, "Ticket Status to Client", 1);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BL.BLAdmin objAdmin = new BL.BLAdmin();
        //        objAdmin.AddNotSendSmsMail(Convert.ToInt64(UserID), 0, "Error while Ticket Status to Client-Issue:-" + ex.Message, 1);
        //    }

        //    MEmail.SendGMail(emailid, "Service Ticket Make 'N' Make", message1, "");

        //    if (dt != null && dt.Rows.Count > 0)
        //    {

        //        BLCustomerCare care = new BLCustomerCare();
        //        DataTable dtcare = care.GetEngineerByTicketID(ticketID);
        //        if (dtcare != null && dtcare.Rows.Count > 0)
        //        {
        //            engineerName = Convert.ToString(dtcare.Rows[0]["name"]);

        //            if (!string.IsNullOrEmpty(Convert.ToString(dtcare.Rows[0]["MobileNumber"])))
        //            {
        //                string message = "Hi," + engineerName + "! A new Service ticket Id: " + ticketID + " has been assigned to you. Please login to our website (www.makenmake.co.in) see the details.";
        //                MEmail.SendGMail(Convert.ToString(dtcare.Rows[0]["EmailID"]), "New Service Ticket Make 'N' Make", message, "");

        //                SendSms objSms = new SendSms();
        //                try
        //                {
        //                    int i = objSms.SendSmsOnMobile(message, Convert.ToString(dtcare.Rows[0]["MobileNumber"]));
        //                    if (i != 1)
        //                    {
        //                        BL.BLAdmin objAdmin = new BL.BLAdmin();
        //                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(dtcare.Rows[0]["UserID"]), 0, "Ticket Status to Engineer", 1);
        //                    }
        //                    MEmail.SendGMail(Convert.ToString(dtcare.Rows[0]["EmailID"]), "New Service Ticket Make 'N' Make",
        //                     "Dear " + engineerName + ",<br><br> A new Service ticket Id: " + ticketID + " has been assigned to you. Please login to our website (www.makenmake.co.in) see the details.", "");

        //                }
        //                catch (Exception ex)
        //                {
        //                    BL.BLAdmin objAdmin = new BL.BLAdmin();
        //                    objAdmin.AddNotSendSmsMail(Convert.ToInt64(dtcare.Rows[0]["UserID"]), 0, "Error while Ticket Status to Engineer-Issue:-" + ex.Message, 1);
        //                }
        //            }
        //        }
        //    }
        //}
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            int result = obj.AssignTicketToEnginner(Convert.ToInt64(ddlStatus.SelectedValue == "2" ? "0" : ddlEngineer.SelectedValue), Convert.ToInt64(ViewState["Session_UserID"]), Convert.ToInt64(ViewState["TicketID"]), txtComments.Text, Convert.ToInt32(ddlStatus.SelectedValue));
            if (result > 0)
            {
                if (ddlStatus.SelectedValue != "2")
                {
                    SentMailSms(Convert.ToInt64(ViewState["TicketID"]), Convert.ToInt64(ddlEngineer.SelectedValue));
                }
                string TicketType = Convert.ToString(Request.QueryString["TicketType"]);
                int roleid = Convert.ToInt32(Session[Constant.Session.Role]);
                if (string.IsNullOrEmpty(TicketType) || TicketType == "")
                {
                    if (roleid == 1)
                    {
                        Response.Redirect("Tickethistory.aspx");
                    }
                    else
                    {
                        Response.Redirect("ViewTickets.aspx");
                    }
                }
                else
                {
                    if (roleid == 1)
                    {
                        Response.Redirect("Appointments.aspx");
                    }
                    else
                    {
                        Response.Redirect("Tickethistory.aspx");
                    }
                }
            }
            else if (result == -95)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot change the ticket status as the client has taken new plan') ;", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot assign ticket because he has already more than 5 tickets') ;", true);
            }
        }
        private void SentMailSms(Int64 ticketID, Int64 EngineerID)
        {
            BLCustomerCare objSend = new BLCustomerCare();
            DataTable dt = objSend.GetUSerByTicketID(ticketID, 1);

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
                    string message = "Hi, " + firstname + "! Your Service ticket Id: " + ticketID + " has been assigned to Engineer "
                        + engineerName + "He will serve you shortly. Please log in with your account details on our website (www.makenmake.in)/Mobile App to see the status of the ticket.Or call us at Helpline Nos:"+ReadConfig.helpLineNumber;
                    SendSms objSms = new SendSms();
                    try
                    {
                        int i = objSms.SendSmsOnMobile(message, mobile);
                        if (i != 1)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(userid), 0, "Admin Sent to Client when Ticket logs", 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(userid), 0, "Error while Admin Sent to Client when Ticket logs-Issue:-" + ex.Message, 1);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dtcare.Rows[0]["MobileNumber"])))
                    {
                        message = "Hi," + "" + "! A new Service ticket Id: " + ticketID + " has been assigned to you. Please login to see the details.";
                        objSms = new SendSms();
                        try
                        {
                            int i = objSms.SendSmsOnMobile(message, Convert.ToString(dtcare.Rows[0]["MobileNumber"]));
                            if (i != 1)
                            {
                                BL.BLAdmin objAdmin = new BL.BLAdmin();
                                objAdmin.AddNotSendSmsMail(Convert.ToInt64(dtcare.Rows[0]["UserID"]), 0, "Admin Ticket Status to Engineer", 1);
                            }
                        }
                        catch (Exception ex)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(dtcare.Rows[0]["UserID"]), 0, "Error while Admin Ticket Status to Engineer-Issue:-" + ex.Message, 1);
                        }
                    }
                    else
                    {
                        MEmail.SendGMail(Convert.ToString(dtcare.Rows[0]["EmailID"]), "New Service Ticket Make 'N' Make",
                        "Dear " + engineerName + ",<br><br> A new Service ticket Id: " + ticketID + " has been assigned to you. Please login to see the details.", "");
                    }
                }
            }
            else
            {
                string message = "Hi," + firstname + "! Your Service ticket Id: " + ticketID + " . We will serve you shortly. Please log in with your account details on our website (www.makenmake.in)/Mobile App to see the status of the ticket.Or call us at Helpline Nos:"+ReadConfig.helpLineNumber;
                SendSms objSms = new SendSms();
                try
                {
                    int i = objSms.SendSmsOnMobile(message, mobile);
                    if (i != 1)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(userid), 0, "Admin Ticket Status to Client", 1);
                    }
                }
                catch (Exception ex)
                {
                    BL.BLAdmin objAdmin = new BL.BLAdmin();
                    objAdmin.AddNotSendSmsMail(Convert.ToInt64(userid), 0, "Error while Admin Ticket Status to Client-Issue:-" + ex.Message, 1);
                }
            }
        }

        protected void RptTickets_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            Label LablblEngineer = (Label)e.Item.FindControl("LablblEngineer");
            if (e.CommandName == "WorkHistory")
            {
                Response.Redirect("WorkHistory.aspx?TicketID=" + lblhstryTicketID.Text + "&EngineerName=" + LablblEngineer.Text);
            }
        }

        protected void RptTickets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblreasn = (Label)e.Item.FindControl("lblcreated");
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
﻿using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Customer
{
    public partial class TicketLogging : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCheckOtp_Click(object sender, EventArgs e)
        {
            //string ClientID = Convert.ToString(Request.QueryString[Constant.QueryString.ClientID]);
            //if (!string.IsNullOrEmpty(ClientID))
            //{
            //    ClientID = EncryptDecrypt.DecryptText(ClientID);
            //    BL.BLCustomerCare objClient = new BL.BLCustomerCare();
            //    int result = objClient.CheckUserOtp(Convert.ToInt64(ClientID), txtVOtp.Text.Trim());
            //    if (result == 1)
            //    {
            //        int i = objClient.AddUpdateUserOTP(0, string.Empty, string.Empty, 3);
            //        dvComplaint.Visible = true;
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Incorrect OTP') ;", true);
            //    }
            //}
            //else
            //{
            //    Response.Redirect(Constant.Pages.Client);
            //}
        }

        protected void btnComplaint_Click(object sender, EventArgs e)
        {
            BLCustomerCare objComplaint = new BLCustomerCare();

            StringBuilder services = new StringBuilder();
            bool iSChecked = false;
            bool isEmpty = false;
            HiddenField hdnAgreementID = new HiddenField();
            services.Append("<Root>");
            if (ddlPlan.Visible)
            {
                foreach (RepeaterItem item in RptInspection.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        CheckBox chk = (CheckBox)item.FindControl("chkID");
                        if (chk.Checked)
                        {
                            iSChecked = true;
                            TextBox txtDescription = (TextBox)item.FindControl("txtDesc");
                            if (txtDescription.Text == "")
                            {
                                Label lblmsg = (Label)item.FindControl("lblmsg");
                                lblmsg.Text = "Enter Issue Description";
                                isEmpty = true;
                                break;
                            }
                            else
                            {
                                Label lblmsg = (Label)item.FindControl("lblmsg");
                                lblmsg.Text = string.Empty;
                            }
                            HiddenField hdnID = (HiddenField)item.FindControl("hdnID");
                            HiddenField hdntype = (HiddenField)item.FindControl("hdntype");
                            hdnAgreementID = (HiddenField)item.FindControl("hdnAgreementID");
                            Label lblPlan = (Label)item.FindControl("lblPlan");
                            services.Append("<child><SID>" + hdnID.Value + "</SID><Plan>" + lblPlan.Text + "</Plan><cat>" + ddlService.SelectedItem.Value +
                                "</cat><type>" + hdntype.Value.Substring(0, 1) + "</type><Desc>" + txtDescription.Text.Replace("'", "") + "</Desc></child>");

                        }
                    }
                }
            }
            else
            {
                foreach (RepeaterItem item in RptService.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        RadioButton chk = (RadioButton)item.FindControl("chkID");
                        if (chk.Checked)
                        {
                            iSChecked = true;
                            TextBox txtDescription = (TextBox)item.FindControl("txtDesc");
                            if (txtDescription.Text == "")
                            {
                                Label lblmsg = (Label)item.FindControl("lblmsg");
                                lblmsg.Text = "Enter Issue Description";
                                isEmpty = true;
                                break;
                            }
                            else
                            {
                                Label lblmsg = (Label)item.FindControl("lblmsg");
                                lblmsg.Text = string.Empty;
                            }
                            HiddenField hdnID = (HiddenField)item.FindControl("hdnID");
                            hdnAgreementID = (HiddenField)item.FindControl("hdnAgreementID");
                            HiddenField hdntype = (HiddenField)item.FindControl("hdntype");
                            Label lblPlan = (Label)item.FindControl("lblPlan");
                            services.Append("<child><SID>" + hdnID.Value + "</SID><Plan>" + lblPlan.Text + "</Plan><cat>" + ddlService.SelectedItem.Value +
                                "</cat><type>" + hdntype.Value.Substring(0, 1) + "</type><Desc>" + txtDescription.Text.Replace("'", "") + "</Desc></child>");

                        }
                    }
                }
            }
            services.Append("</Root>");
            if (iSChecked)
            {
                if (isEmpty)
                {
                    //     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Enter Issue Description') ;", true);
                }
                else
                {
                   
                 
                    int result = objComplaint.AddComplaint(Convert.ToInt64(Session[Constant.Session.AdminSession]),
                        Convert.ToInt64(Session[Constant.Session.AdminSession]), Convert.ToInt64(hdnAgreementID.Value), services.ToString(), ddlticket.SelectedValue, txtdesc.Text);
                    if (result > 0)
                    {
                        Clear();
                        ddlService.Enabled = true;
                        ddlticket.Enabled = true;
                        //ddlticket.SelectedValue = "0";
                        ddlService.SelectedValue = "0";
                        dvDesc.Visible = false;
                        dvPlan.Visible = false;
                        txtdesc.Text = string.Empty;
                        dvRepair.Visible = false;
                        dvInspection.Visible = false;
                        SentMailSms(Convert.ToInt64(Session[Constant.Session.AdminSession]), result);
                        

                      int ticketStatus = objComplaint.ticketStatusByTicketId(Convert.ToInt64(result));
                        if (ticketStatus== 4)
                        {
                            BLCustomerCare care1 = new BLCustomerCare();
                            DataTable dtescaltion = care1.GetEsclationMnagerByZoneidticket(Convert.ToInt32(result));
                            string esclation = string.Empty;
                            if (dtescaltion.Rows.Count > 0 && dtescaltion != null)
                            {
                                for(int ie=0;ie<dtescaltion.Rows.Count;ie++)
                                {
                                esclation = Convert.ToString(dtescaltion.Rows[ie]["EFirstName"]);
                                string message1 = "";
                                


                                message1 = "Hi," + esclation + " ticket id :" + result + " .has been escalated for your necessary action.";


                                SendSms objSms1 = new SendSms();
                                try
                                {
                                    int i = objSms1.SendSmsOnMobile(message1, Convert.ToString(dtescaltion.Rows[ie]["MobileNumber"]));
                                    if (i != 1)
                                    {
                                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(Session[Constant.Session.AdminSession]), 0, "Ticket Status to Escation Manager", 1);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    BL.BLAdmin objAdmin = new BL.BLAdmin();
                                    objAdmin.AddNotSendSmsMail(Convert.ToInt64(Session[Constant.Session.AdminSession]), 0, "Error while Ticket Status to Client-Issue:-" + ex.Message, 1);
                                }

                                MEmail.SendGMail(Convert.ToString(dtescaltion.Rows[ie]["EmailID"]), "Service Ticket Make 'N' Make", message1, "");
                                }
                            }
                           // ddlticket.SelectedValue = "0";
                           
                        }

                        if (ddlticket.SelectedItem.Text== "Inspection/Quote")
                        {
                            Esclate(Convert.ToInt32(result));
                            ddlticket.SelectedValue = "0";
                            BLAdmin admindl = new BLAdmin();
                            DataTable dtadmin = admindl.GetAdmin();
                            if (dtadmin.Rows.Count > 0)
                            {
                                for (int ia = 0; ia < dtadmin.Rows.Count; ia++)
                                {
                                    string message1 = "";

                                    message1 = "Hi,Admin Inspection & Quote Id " + result + " has been raised for your necessary action.";


                                    SendSms objSms1 = new SendSms();
                                    try
                                    {
                                        int i = objSms1.SendSmsOnMobile(message1, Convert.ToString(dtadmin.Rows[ia]["MobileNumber"]));
                                        if (i != 1)
                                        {
                                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(Session[Constant.Session.AdminSession]), 0, "Ticket Status to Client", 1);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(Session[Constant.Session.AdminSession]), 0, "Error while Ticket Status to Client-Issue:-" + ex.Message, 1);
                                    }

                                    MEmail.SendGMail(Convert.ToString(dtadmin.Rows[ia]["EmailID"]), "Service Ticket Make 'N' Make", message1, "");
                                }
                            }
                        }



                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Our service engineer will contact you as soon as possible .Your Ticket Number is :" + result.ToString() + " .Please save it for future communication.Also Email and sms have been send') ;", true);
                    }
                    else if (result == -96)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot log this ticket because you have changed the plan and its different from your's current plan') ;", true);
                    }
                    else if (result == -92)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot log this ticket because you have already used your all tickets from current plan') ;", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select atleast one service') ;", true);
            }
        }
        private void Esclate (Int64 ticketID)
        {
            BLCustomerCare care1 = new BLCustomerCare();
            DataTable dtescaltion = care1.GetEsclationMnagerByZoneidticket(Convert.ToInt32(ticketID));
            if (dtescaltion.Rows.Count > 0 && dtescaltion != null)
            {
                for (int ie = 0; ie < dtescaltion.Rows.Count; ie++)
                {
                    string message1 = "";

                    message1 = "Hi," + dtescaltion.Rows[ie]["EFirstName"] + " Inspection and Quote Id :" + ticketID + " has been raised for your necessary action.";


                    SendSms objSms1 = new SendSms();
                    try
                    {
                        int i = objSms1.SendSmsOnMobile(message1, Convert.ToString(dtescaltion.Rows[ie]["MobileNumber"]));
                        if (i != 1)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(Session[Constant.Session.AdminSession]), 0, "Ticket Status to Escation Manager", 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(Session[Constant.Session.AdminSession]), 0, "Error while Ticket Status to Client-Issue:-" + ex.Message, 1);
                    }

                    MEmail.SendGMail(Convert.ToString(dtescaltion.Rows[ie]["EmailID"]), "Service Ticket Make 'N' Make", message1, "");
                }

            }
        }
        private void SentMailSms(Int64 UserID, Int64 ticketID)
        {
            Common objSend = new Common();
            DataTable dt = objSend.GetUserInfoByID(UserID);
            string firstname = Convert.ToString(dt.Rows[0]["firstname"]);
            string emailid = Convert.ToString(dt.Rows[0]["Emailid"]);
            string mobile = Convert.ToString(dt.Rows[0]["MNumber"]);
            string engineerName = string.Empty;
            BLCustomerCare care1 = new BLCustomerCare();
            BLAdmin admindl=new BLAdmin();
            
            DataTable dtcare1 = care1.GetEngineerByTicketID(ticketID);
           
            if (dtcare1 != null && dtcare1.Rows.Count > 0)
            {
                // engineerName = Convert.ToString(dtcare1.Rows[0]["name"]);
                engineerName = "Our engineer will serve you shortly";
                string message1 = "Hi," + firstname + "! Your Service ticket Id: " + ticketID + " has been submitted."+ engineerName +". Please log in with your account details on our website ( www.makenmake.in)/Mobile App to see the status of the ticket.Or call us at Helpline Nos: "+ReadConfig.helpLineNumber;
                SendSms objSms1 = new SendSms();
                try
                {
                    int i = objSms1.SendSmsOnMobile(message1, mobile);
                    if (i != 1)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(UserID), 0, "Ticket Status to Client", 1);
                    }
                }
                catch (Exception ex)
                {
                    BL.BLAdmin objAdmin = new BL.BLAdmin();
                    objAdmin.AddNotSendSmsMail(Convert.ToInt64(UserID), 0, "Error while Ticket Status to Client-Issue:-" + ex.Message, 1);
                }

                MEmail.SendGMail(emailid, "Service Ticket Make 'N' Make", message1, "");

            }
            else
            {
                engineerName = "Our engineer will serve you shortly";
                string message1 = "Hi," + firstname + "! Your Service ticket Id: " + ticketID + " has been submitted." + engineerName + " .Please log in with your account details on our website ( www.makenmake.in)/Mobile App to see the status of the ticket.Or call us at Helpline Nos: "+ReadConfig.helpLineNumber;
                SendSms objSms1 = new SendSms();
                try
                {
                    int i = objSms1.SendSmsOnMobile(message1, mobile);
                    if (i != 1)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(UserID), 0, "Ticket Status to Client", 1);
                    }
                }
                catch (Exception ex)
                {
                    BL.BLAdmin objAdmin = new BL.BLAdmin();
                    objAdmin.AddNotSendSmsMail(Convert.ToInt64(UserID), 0, "Error while Ticket Status to Client-Issue:-" + ex.Message, 1);
                }

                MEmail.SendGMail(emailid, "Service Ticket Make 'N' Make", message1, "");
            }

            

            if (dt != null && dt.Rows.Count > 0)
            {

                BLCustomerCare care = new BLCustomerCare();
                DataTable dtcare = care.GetEngineerByTicketID(ticketID);
                if (dtcare != null && dtcare.Rows.Count > 0)
                {
                    engineerName = Convert.ToString(dtcare.Rows[0]["name"]);

                    if (!string.IsNullOrEmpty(Convert.ToString(dtcare.Rows[0]["MobileNumber"])))
                    {
                        string message = "Hi," + engineerName + "! A new Service ticket Id: " + ticketID + " has been assigned to you. Please login to our website (www.makenmake.in)/Mobile App see the details.";
                        MEmail.SendGMail(Convert.ToString(dtcare.Rows[0]["EmailID"]), "New Service Ticket Make 'N' Make", message, "");

                        SendSms objSms = new SendSms();
                        try
                        {
                            int i = objSms.SendSmsOnMobile(message, Convert.ToString(dtcare.Rows[0]["MobileNumber"]));
                            if (i != 1)
                            {
                                BL.BLAdmin objAdmin = new BL.BLAdmin();
                                objAdmin.AddNotSendSmsMail(Convert.ToInt64(dtcare.Rows[0]["UserID"]), 0, "Ticket Status to Engineer", 1);
                            }
                            MEmail.SendGMail(Convert.ToString(dtcare.Rows[0]["EmailID"]), "New Service Ticket Make 'N' Make",
                             "Dear " + engineerName + ",<br><br> A new Service ticket Id: " + ticketID + " has been assigned to you. Please login to our website (www.makenmake.in)/Mobile App see the details.", "");

                        }
                        catch (Exception ex)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(dtcare.Rows[0]["UserID"]), 0, "Error while Ticket Status to Engineer-Issue:-" + ex.Message, 1);
                        }
                    }
                }
            }
        }
        private void Clear()
        {
            txtdesc.Text = string.Empty;
        }

        protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlticket.SelectedValue == "0")
            {
                ddlService.SelectedValue = "0";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select ticket type') ;", true);
            }
            else
            {
                Common objService = new Common();
                DataSet dt = objService.GetServiceAccordingToUser(Convert.ToInt64(Session[Constant.Session.AdminSession]), ddlService.SelectedValue, Convert.ToInt32(ddlticket.SelectedValue));

                if (dt != null && dt.Tables[0].Rows.Count > 0)
                {
                    int status = Convert.ToInt32(dt.Tables[0].Rows[0]["UStatus"]);

                    if (dt != null && status == 1)
                    {
                        if (dt.Tables[1].Rows.Count > 0)
                        {
                            ddlticket.Enabled = false;
                            ddlService.Enabled = false;
                            dvDesc.Visible = true;
                            dvRepair.Visible = true;

                            RptService.DataSource = dt.Tables[1];
                            RptService.DataBind();
                        }
                        else 
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert(' Please give us a chance to serve you by buying our services') ;", true);
                        }
                        //else if (ddlticket.SelectedValue =="1" && ddlService.SelectedValue =="C")
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert(' You have not bought our Commercial services ') ;", true);
                        //}
                    }
                    else if (status == 3)
                    {
                        ddlticket.Enabled = false;
                        ddlService.Enabled = false;
                        dvPlan.Visible = true;
                    }
                    else if (status == 2)
                    {
                        ddlticket.Enabled = false;
                        ddlService.Enabled = false;
                        dvPlan.Visible = true;
                    }
                    //else if (status == 2)
                    //{
                    //    ddlService.SelectedValue = "0";
                    //    ddlticket.SelectedValue = "0";
                    //    lblvalidationMsg.Visible = true;
                    //    lblvalidationMsg.Text = "You are premium Member,please select ticket type as repair.";
                    //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You are premium Member,please select ticket type as repair') ;", true);
                    //}
                    else if (status == 0)
                    {
                        ddlService.SelectedValue = "0";
                        ddlticket.SelectedValue = "0";
                        lblvalidationMsg.Visible = true;
                        lblvalidationMsg.Text = " Please give us a chance to serve you by buying our services .";
                       // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert(' Please give us a chance to serve you by buying our services ') ;", true);
                        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot booked your ticket as repair , you are not premium member.To take the repair , please buy our services first') ;", true);
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlService.SelectedValue = "0";
            ddlticket.SelectedValue = "0";
            ddlService.Enabled = true;
            ddlticket.Enabled = true;
            txtdesc.Text = string.Empty;
            dvDesc.Visible = false;
            dvInspection.Visible = false;
            dvPlan.Visible = false;
            dvRepair.Visible = false;
        }

        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPlan.SelectedValue != "0")
            {
                Common obj = new Common();
                DataTable dt = obj.GetInspectionServices(ddlPlan.SelectedValue, ddlService.SelectedValue);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dvDesc.Visible = true;
                    dvRepair.Visible = false;
                    dvInspection.Visible = true;
                    RptInspection.DataSource = dt;
                    RptInspection.DataBind();
                }
                else
                {
                    lblvadtionPlan.Visible = true;
                    lblvadtionPlan.Text = "No services regarding this plan.";
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No services regarding this plan') ;", true);
                }
            }
        }

    }
}
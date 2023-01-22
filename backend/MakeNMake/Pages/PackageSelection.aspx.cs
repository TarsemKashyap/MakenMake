using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MakeNMake.Utilities;
using System.Data;

namespace MakeNMake.CustomerCare
{
    public partial class PackageSelection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string clientID = Convert.ToString(Request.QueryString[Constant.QueryString.ClientID]);
                if (!IsPostBack)
                {
                }
            }
            catch (Exception ex)
            {
                Response.Redirect(Constant.Pages.Client);
            }
        }
        private void BindClientData(Int64 userID)
        {
            BL.BLCustomerCare objClient = new BL.BLCustomerCare();
            DataTable dt = objClient.GetConsumerByID(userID);
            lblname.Text = Convert.ToString(dt.Rows[0]["NAME"]);
            lblmob.Text = Convert.ToString(dt.Rows[0]["MobileNumber"]);
            lblemailid.Text = Convert.ToString(dt.Rows[0]["EmailID"]);
        }

        protected void btnCheckOtp_Click(object sender, EventArgs e)
        {

            divotp.Visible = false;
            divservice.Visible = true;

            string clientID = Convert.ToString(Request.QueryString[Constant.QueryString.ClientID]);
            if (!string.IsNullOrEmpty(clientID))
            {
                clientID = EncryptDecrypt.DecryptText(clientID);
                BL.BLCustomerCare objClient = new BL.BLCustomerCare();
                int result = objClient.CheckUserOtp(Convert.ToInt64(clientID), txtVOtp.Text.Trim());
                if (result == 1)
                {
                    int i = objClient.AddUpdateUserOTP(Convert.ToInt64(clientID), string.Empty, string.Empty, 3);
                    pnlAppointment.Visible = true;
                    txtVOtp.Text = string.Empty;
                    
                    txtVOtp.Enabled = false;
                    lblHeading.Text = "Select Services";
                    dvverify.Visible = false;
                    BindClientData(Convert.ToInt64( clientID));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Incorrect OTP') ;", true);
                }
            }
            else
            {
                Response.Redirect(Constant.Pages.Client);
            }
        }

        protected void btnResend_Click(object sender, EventArgs e)
        {
            //    string Otp = EncryptDecrypt.CreateRandomPassword(4);
            //    SendSms obj = new SendSms(); 
            //    int i = obj.SendSmsOnMobile("Hi User", lblMobile.Text, Otp);
            //if (i == 1)
            //{
            //}
            //    int i = objClient.AddUpdateUserOTP(0, string.Empty, string.Empty, 3);
        }

        protected void ddlappointment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlappointment.SelectedValue == "1")
            {
                avaiability.Visible = true;
                dvServices.Visible = false; btnUpdateInfo.Visible = false;
            }
            else if (ddlappointment.SelectedValue == "2")
            {
                dvServices.Visible = true;
                avaiability.Visible = false; btnUpdateInfo.Visible = false;
               // Response.Redirect(Constant.Pages.Services + Constant.Separators.QuestionMark + Constant.QueryString.ClientID + Constant.Separators.EqualTo + Convert.ToString(Request.QueryString[Constant.QueryString.ClientID]));
            }
            else
            {
                btnUpdateInfo.Visible = false;
                dvServices.Visible = false;
                avaiability.Visible = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BL.BLCustomerCare objClient = new BL.BLCustomerCare();
            string clientID = Convert.ToString(Request.QueryString[Constant.QueryString.ClientID]);
            clientID = EncryptDecrypt.DecryptText(clientID);
            try
            {
                Int64 result = objClient.AddAppoinment(Convert.ToInt64(clientID), Convert.ToInt64(Session[Constant.Session.AdminSession]),
                    txtdate.Text, txtaviltime.Text + " " + ddlTime.SelectedItem.Text, 0);
                if (result == -99)
                {
                    Response.Redirect(Constant.Pages.UpdateInfo + Constant.Separators.QuestionMark + Constant.QueryString.ClientID + Constant.Separators.EqualTo + Convert.ToString(Request.QueryString[Constant.QueryString.ClientID]));
                    //btnUpdateInfo.Visible = true;
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Client Address is not complete, please update it before appointment') ;", true);
                }
                else
                {
                    string engineerName = string.Empty;

                    BL.BLCustomerCare care = new BL.BLCustomerCare();
                    DataTable dtengineer = care.GetEngineerByAppointmentD(Convert.ToInt64(result));


                    string message = "Hi," + lblname.Text + "! Your Appointment ticket Id: " + result + " has been assigned to engineer:"+ engineerName + ". He will serve you shortly. Please log in with your account details on our website (www.makenmake.in)/Mobile App to see the status of the ticket.Or Call us Helpline Nos:"+ReadConfig.helpLineNumber;
                    MEmail.SendGMail(lblemailid.Text, "New Appointment Ticket Make 'N' Make", message, "");
                    SendSms objSms = new SendSms();
                    try
                    {
                        int i = objSms.SendSmsOnMobile(message, lblmob.Text);
                        if (i != 1)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(clientID), 0, "Appointment Status to Client", 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(clientID), 0, "Error while Appointment Status to Client-Issue:-" + ex.Message, 1);
                    }
                    
                    if (dtengineer != null && dtengineer.Rows.Count > 0)
                    {
                        engineerName = Convert.ToString(dtengineer.Rows[0]["name"]);
                        
                        if (!string.IsNullOrEmpty(Convert.ToString(dtengineer.Rows[0]["MobileNumber"])))
                        {
                           string messageToEngineer = "Hi," + engineerName + "! A new Appointment ticket Id: " + result + " has been assigned to you. Please login to see the details.";
                           MEmail.SendGMail(Convert.ToString(dtengineer.Rows[0]["EmailID"]), "New Appointment Ticket Make 'N' Make", messageToEngineer, "");
                        
                            objSms = new SendSms();
                            try
                            {
                                int i = objSms.SendSmsOnMobile(messageToEngineer, Convert.ToString(dtengineer.Rows[0]["MobileNumber"]));
                                if (i != 1)
                                {
                                    BL.BLAdmin objAdmin = new BL.BLAdmin();
                                    objAdmin.AddNotSendSmsMail(Convert.ToInt64(dtengineer.Rows[0]["UserID"]), 0, "Appointment Status to Engineer", 1);
                                }
                            }
                            catch (Exception ex)
                            {
                                BL.BLAdmin objAdmin = new BL.BLAdmin();
                                objAdmin.AddNotSendSmsMail(Convert.ToInt64(dtengineer.Rows[0]["UserID"]), 0, "Error while Appointment Status to Engineer-Issue:-" + ex.Message, 1);
                            }
                        }
                    }

                    mainPanel.Visible = false;
                    dvBack.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Appointment booked, our service engineer will contact you as soon as posible.The Appointment Id for future reference :" + result + "') ;", true);
                    // Response.Redirect("Clients.aspx");
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Problem Occurs ----> Reason :- 1.Entered incorrect date 2.Entered incorrect time') ;", true);
            }

        }

        protected void btnServices_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constant.Pages.Services + Constant.Separators.QuestionMark + Constant.QueryString.ClientID + Constant.Separators.EqualTo + Convert.ToString(Request.QueryString[Constant.QueryString.ClientID]));
        }

        protected void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constant.Pages.UpdateInfo + Constant.Separators.QuestionMark + Constant.QueryString.ClientID + Constant.Separators.EqualTo + Convert.ToString(Request.QueryString[Constant.QueryString.ClientID]));
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constant.Pages.DashBoard);
        }

        protected void btnAddOnServices_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constant.Pages.AddOnServices + Constant.Separators.QuestionMark + Constant.QueryString.ClientID + Constant.Separators.EqualTo + Convert.ToString(Request.QueryString[Constant.QueryString.ClientID]));
        }
        
          
    }
}
using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnForgot_Click(object sender, EventArgs e)
        {
            Common objForgotPassword = new Common();
            int result = objForgotPassword.CheckEmailID(txtloginid.Text.Trim());
            if (result == 1)
            {
                Guid g = Guid.NewGuid();
                int forgorPassword = objForgotPassword.ForgotPassword(txtloginid.Text.Trim(), g.ToString());
                if (forgorPassword == 1)
                {
                    //string content = MakeNMake.Utilities.EncryptDecrypt.Encript(txtloginid.Text.Trim() + ":" + g.ToString());
                    //string VerifyUrl = string.Format("{0}VerifyAccount.aspx?Content={1}", ReadConfig.SiteUrl, content);
                    //MEmail.SendGMail(txtloginid.Text.Trim(), "Please Reset your Password", "Dear User,<br><br> Please find the below Link and set new password .<br> Click on this link: <a target='_blank' style='color:blue;'  href='" + VerifyUrl + "'>" + VerifyUrl + "</a><br>.If your email system does not allow linking, please copy and paste the following into your browser. <br><br>Kind Regards:<br> Make 'N' Make Service Team", "");
                    //txtloginid.Text = string.Empty;
                   
                    String Otp = EncryptDecrypt.CreateRandomPassword(4);
                    SendSms obj = new SendSms();
                    BLCustomerCare objConsumer = new BLCustomerCare();

                    Common objSend = new Common();
                    BLAdmin dd = new BLAdmin();

                    DataTable dt = objSend.GetUserInfoByEmail(txtloginid.Text.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        hdnUserID.Value = Convert.ToString(dt.Rows[0]["UserID"]);
                        DataTable dtinfo = objSend.GetUserInfoByID(Convert.ToInt64(dt.Rows[0]["UserID"]));
                        if (dtinfo.Rows.Count > 0)
                        {
                            string firstname = Convert.ToString(dtinfo.Rows[0]["firstname"]);
                            string emailid = Convert.ToString(dtinfo.Rows[0]["Emailid"]);
                            string mobile = Convert.ToString(dtinfo.Rows[0]["MNumber"]);
                            string message = "Hi!" + firstname + ", Your OTP for Forgot Password  is " + Otp + " . Thanks MakeNake team";
                            MEmail.SendGMail(emailid, "Make n Make OTP", message, "");
                            int i = obj.SendSmsOnMobile(message, mobile);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please check sms for otp to set new password for your account') ;", true);
                            if (i == 1)
                            {
                                int resultotp = objConsumer.AddUpdateUserOTP(Convert.ToInt64(dt.Rows[0]["UserID"]), Otp, Convert.ToString(dtinfo.Rows[0]["MNumber"]), 1);
                                divforgotinfo.Visible = true;
                                dvVerifyOTP.Visible = true;
                            }
                            else
                            {
                                divforgotinfo.Visible = false;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Some Problem occurs due to insufficient balance while sending message') ;", true);
                            }
                        }
                    }
                
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('EmailD that you enters does not exist in our system ') ;", true);
            }
        }
        protected void btnVerify_Click(object sender, EventArgs e)
        {
            BL.BLCustomerCare objClient = new BL.BLCustomerCare();
            Int64 userID = Convert.ToInt64(hdnUserID.Value);
            int result = objClient.CheckUserOtp(userID, txtOTP.Text.Trim());
            if (result == 1)
            {
                int i = objClient.AddUpdateUserOTP(userID, string.Empty, string.Empty, 3);
                dvVerifyOTP.Visible = false;
                divforgotinfo.Visible = true;
                BLServiceEngineer obj = new BLServiceEngineer();
                Guid g = Guid.NewGuid();
                string content = MakeNMake.Utilities.EncryptDecrypt.Encript(txtloginid.Text.Trim() + ":" + g.ToString());
                string VerifyUrl = string.Format("{0}VerifyAccount.aspx?Content={1}", ReadConfig.SiteUrl, content);
                Response.Redirect(VerifyUrl);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Incorrect OTP') ;", true);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}
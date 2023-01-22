using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using System;
using NLog;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.UserControl
{
    public partial class ChangePassword : System.Web.UI.UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            try {
                Common ojPassword = new Common();
                string oldPassword = EncryptDecrypt.Encript(txtoldpass.Text);
                string newPassword = EncryptDecrypt.Encript(txtnewpass.Text);
                Int64 UserID = 0;
                UserID = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                int result = ojPassword.ChangePassword(UserID, oldPassword, newPassword);
                if (result == 1)
                {
                    Common objSend = new Common();
                    DataTable dt = objSend.GetUserInfoByID(UserID);
                    string firstname = Convert.ToString(dt.Rows[0]["firstname"]);
                    string emailid = Convert.ToString(dt.Rows[0]["Emailid"]);
                    string mobile = Convert.ToString(dt.Rows[0]["MNumber"]);
                    string message = "Hi , Dear User ! ,<br/>You have successfully changed password with MakenMake .<br/><br/> Login with your credentials :<br/>User ID:"
                   + emailid + "<br/>Password: " + newPassword + "<br/><br/> For any queries or complaints,you have our ears at Helpline Nos :" + ReadConfig.helpLineNumber + "or log in with your account details on our website (www.makenmake.in)/Mobile App <br/>Kind Regards:<br/>MakenMake Service Team";

                    string message1 = "Hi , Dear User ! ,You have successfully changed password with makenmake. For any queries or complaints,you have our ears at Helpline Nos :" + ReadConfig.helpLineNumber +
                   "or log in with your account details on our website (www.makenmake.in)/Mobile App Kind Regards : MakenMake Service Team";

                    
                    MEmail.SendGMail(emailid, "Make n Make Profile Updated", message, "");
                    SendSms objSms = new SendSms();
                    try
                    {
                        int i = objSms.SendSmsOnMobile(message1, mobile);
                        if (i != 1)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(UserID, 0, "Updating password", 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(UserID, 0, "Error while Updating password -Issue:-" + ex.Message, 1);
                    }

                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Password changed sucessfully') ;", true);
                }
                else if (result == -98)
                {
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('old password is not same') ;", true);
                }
            }
            catch(Exception ex)
            {
                logger.Error(logger.Name + ":" + ex.Message);
            }
        }
        public void Clear()
        {
            txtconfirmpass.Text = string.Empty;
            txtnewpass.Text = string.Empty;
            txtoldpass.Text = string.Empty;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard.aspx");
        }
    }
}
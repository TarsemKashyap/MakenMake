using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake
{
    public partial class EmailConfirmation : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string queryString = Convert.ToString(Request.QueryString["Content"]);
                if (!string.IsNullOrEmpty(queryString))
                {
                    string decrypted = EncryptDecrypt.DecryptText(queryString);
                    string[] userData = decrypted.Split(':');


                    BL.BLConsumer obj = new BL.BLConsumer();
                    int result = obj.ConfirmUser(Convert.ToString(userData[0]));
                    if (result > 0)
                    {
                        string VerifyUrl = string.Format("{0}Default.aspx", ReadConfig.SiteUrl);
                        MEmail.SendGMail(userData[0], "Account Confirmation Make 'N' Make", "Dear User,<br><br> You have successfully confirmed your account with Make N Make . Login with your credentials :<br><br> User ID: " + userData[0] + "<br>Password: " + userData[3] + " <br> Click on this link: <a target='_blank' style='color:blue;' href='" + VerifyUrl + "'>" + VerifyUrl + "</a><br>You can change the password after login to our site with these credentials.<br><br>Kind Regards:<br> Make 'N' Make Service Team", "");
                       
                        SendSms objSms = new SendSms();
                        string message="Account Confirmation Make 'N' Make,Dear User,You have successfully confirmed your account with Make N Make.Login with your credentials : User ID: " + userData[0] + "   ,Password: " + userData[3] ;
                       Common objSend = new Common();
                        DataTable dt = objSend.GetUserInfoByEmail(userData[0].Trim());
                        if (dt.Rows.Count > 0)
                        {
                            DataTable dtinfo = objSend.GetUserInfoByID(Convert.ToInt64(dt.Rows[0]["UserID"]));
                            if (dtinfo.Rows.Count > 0)
                            {

                                string mobile = Convert.ToString(dtinfo.Rows[0]["MNumber"]);
                                int i = objSms.SendSmsOnMobile(message, mobile);
                            }
                        }
                        
                        Response.Redirect("~/Default.aspx", false);
                        //FormAuth(result.ToString(), userData[1]);
                    }
                    else if (result == -99)
                    {
                        FormAuth(userData[2], userData[1]);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Some fatal error occurs') ;", true);
                    }
                }
                else
                {
                    Response.Redirect("~/" + Constant.Pages.Login);
                }
            }
            catch (Exception ex)
            {
                logger.Error(logger.Name + ":" + ex.Message);
                HttpContext.Current.Response.Redirect("~/" + Constant.Pages.Error, true);            
            }
        }
        private void FormAuth(string ID, string name)
        {
            Session[Constant.Session.AdminSession] = ID;
            FormsAuthenticationTicket tkt;
            string cookiestr;
            HttpCookie ck;
            tkt = new FormsAuthenticationTicket(1, name, DateTime.Now, DateTime.Now.AddMinutes(30), true, null);
            cookiestr = FormsAuthentication.Encrypt(tkt);
            ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
            //if (chkPersistCookie.Checked)
            ck.Expires = tkt.Expiration;
            ck.Path = FormsAuthentication.FormsCookiePath;
            Response.Cookies.Add(ck);
            string strRedirect;
            strRedirect = Request["ReturnUrl"];
            if (strRedirect == null)
                strRedirect = Constant.Pages.Login;
            Response.Redirect(Constant.Pages.Customer + Constant.Pages.CustomerDashBoard, false);
        }
    }
}
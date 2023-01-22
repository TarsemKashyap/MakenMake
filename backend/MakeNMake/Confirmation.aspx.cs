using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake
{
    public partial class Confirmation : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string queryString = Convert.ToString(Request.QueryString[Constant.QueryString.Confirmed]);
                if (!string.IsNullOrEmpty(queryString))
                {
                    string decrypted = EncryptDecrypt.DecryptText(queryString);
                    string[] userData = decrypted.Split(':');
                    Session[Constant.Session.AdminSession] = userData[0];
                    FormsAuthenticationTicket tkt;
                    string cookiestr;
                    HttpCookie ck;
                    tkt = new FormsAuthenticationTicket(1, userData[1], DateTime.Now, DateTime.Now.AddMinutes(30), true, null);
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
                    Response.Redirect("~/" + Constant.Pages.Customer + Constant.Pages.CustomerDashBoard, false);
                }
                else
                {
                    Response.Redirect(Constant.Pages.Login);
                }
            }
            catch (Exception ex)
            {
                logger.Error(logger.Name + ":" + ex.Message);
                HttpContext.Current.Response.Redirect("~/" + Constant.Pages.Error, true);
            }
        }
    }
}
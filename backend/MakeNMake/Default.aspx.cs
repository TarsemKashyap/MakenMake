using ASPSnippets.GoogleAPI;
using Facebook;
using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace MakeNMake
{
    public partial class Default : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session[Constant.Session.Role] = null;
                Session[Constant.Session.AdminSession] = null;
                if (Request.Cookies["EmailID"] != null && Request.Cookies["Password"] != null)
                {
                    txtloginid.Text = Request.Cookies["EmailID"].Value;
                    txtpass.Attributes["value"] = Request.Cookies["Password"].Value;
                    chkremember.Checked = true;
                 
                }
            }
            
             BLAdmin adduser = new BLAdmin();
             BL.BLConsumer objLogin = new BL.BLConsumer();
            if (Request.QueryString["code"] != null)
            {
                string accessCode = Request.QueryString["code"].ToString();
                var fb = new FacebookClient();
                dynamic result = fb.Post("oauth/access_token", new
                {

                    client_id = ReadConfig.FacebookID,

                    client_secret = ReadConfig.FacebookSecretKey,

                    redirect_uri = ReadConfig.FbUrl,

                    code = accessCode

                });
                var accessToken = result.access_token;
                var expires = result.expires;

                // Store the access token in the session
                Session["AccessToken"] = accessToken;

                // update the facebook client with the access token 
                fb.AccessToken = accessToken;
                if (Session["AccessToken"] != null)
                {

                    string session = Session["AccessToken"].ToString();

                    // Retrieve user information from database if stored or else create a new FacebookClient with this accesstoken and extract data again.



                    // throws OAuthException 

                    dynamic me = fb.Get("me?fields=friends,name,email");
                    string name = me.name;
                    string email = me.email;
                    //   Label1.Text = email;               
                    if (string.IsNullOrEmpty(email))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Your Facebook account Email is not accessable') ;", true);
                    }
                    else
                    {
                        string password = EncryptDecrypt.CreateRandomPassword(6);
                        int results = adduser.AddUsers(name, "", email, EncryptDecrypt.Encript(password), System.DateTime.Now, "", 0, 0, 0, 0, 4, 0, 0, 0, 1, "14,15,16", "33,34,35,36,37,20045,38,39,40", "", "", "", "", "", 0);
                        if (results > 0)
                        {
                            string VerifyUrl = string.Format("{0}Default.aspx", ReadConfig.SiteUrl);
                            string content = MakeNMake.Utilities.EncryptDecrypt.Encript(email + ":" + name + ":" + results + ":" + password);
                            MEmail.SendGMail(email, "Registration Make 'N' Make", "Dear User,<br><br> You have successfully registered with Make N Make . Login with your credentials :<br><br> User ID: " + email + "<br>Password: " + password + " <br><br> Click on this link: <a target='_blank' style='color:blue;'  href='" + VerifyUrl + "'>" + VerifyUrl + "</a><br><br>You can change the password after login to our site with these credentials.If your email system does not allow linking, please copy and paste the following into your browser. <br><br>Kind Regards:<br> Make 'N' Make Service Team", "");
                           // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Thanks for registering with us,please check your email to confirm your account') ;", true);

                            int roleID = 0;
                            // string password = Utilities.EncryptDecrypt.Encript(txtpass.Text.Trim());
                            Int64 resultlog = objLogin.ValidateUser(email, password, out roleID);
                            if (resultlog > 0)
                            {
                                CheckStatus(roleID, results);
                            }
                            else if (resultlog == -98)
                            {

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Incorrect EmailID and password') ;", true);
                            }
                            else if (resultlog == -99)
                            {

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('EmailId not exists in the system ,please register first') ;", true);
                            }
                            else
                            {

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Some fatal error occurs') ;", true);
                            }

                        }

                        else if (results == -99)
                        {
                            int roleID = 0;
                            string useridresult = adduser.GetUserID(email);
                            if (!string.IsNullOrEmpty(useridresult))
                            {
                                string[] values = useridresult.Split(':');
                                string userid = values[0].Trim();

                                CheckStatus(4, Convert.ToInt64(userid));
                            }
                            else
                            {
                                string useridtempresult = adduser.GetUserTempID(email);
                                if (!string.IsNullOrEmpty(useridtempresult))
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You have already registred with This email Please Activate your account from This " + email + "') ;", true);
                                }
                                else
                                {

                                }
                                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('user already exists with this emailid ,try with another') ;", true);
                            }
                            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('user already exists with this emailid ,try with another') ;", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Some fatal error occurs') ;", true);
                        }
                    }
                }
                // Check if redirected from facebook
                else if (Request.QueryString["code"] != null)
                {

                }
            }
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                BL.BLConsumer objLogin = new BL.BLConsumer();
                int roleID = 0;
                string password = Utilities.EncryptDecrypt.Encript(txtpass.Text.Trim());
                Int64 result = objLogin.ValidateUser(txtloginid.Text.Trim(), password, out roleID);
                if (result > 0)
                {
                    CheckStatus(roleID, result);
                }
                else if (result == -98)
                {
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Incorrect EmailID and password') ;", true);
                }
                else if (result == -99)
                {
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('EmailId not exists in the system ,please register first') ;", true);
                }
                else
                {
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Some fatal error occurs') ;", true);
                }
            }
            catch (Exception ex)
            {
                Clear();
                logger.Error(logger.Name + ":" + ex.Message);
            }
        }
        public void Clear()
        {
            txtpass.Text = string.Empty;
            txtloginid.Text = string.Empty;
            chkremember.Checked = false;
        }
        public void CheckStatus(int roleID, Int64 UserID)
        {
            BL.BLAdmin ojAdmin = new BL.BLAdmin();
            int status = ojAdmin.CheckUserStatus(UserID);
            if (status == 1)
            {
                FormAuthentication();
                RedirectUserToDashBoard(roleID, UserID);
            }
            else if (status == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You are blocked by Admin, pls contact to Administrator') ;", true);
            }
        }
        public void FormAuthentication()
        {


            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                  txtloginid.Text, DateTime.Now, DateTime.Now.AddDays(10),
                  true, "Hi");

            string ticketString = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketString);
            if (chkremember.Checked)
            {
                Response.Cookies["EmailID"].Value = txtloginid.Text;
                Response.Cookies["Password"].Value = txtpass.Text;
                cookie.Expires = DateTime.Now.AddDays(10);
                Response.Cookies.Add(cookie);
            }
            string strRedirect;
            strRedirect = Request["ReturnUrl"];
            if (strRedirect == null)
            {
                strRedirect = Constant.Pages.Login;
            }
        }
        public void RedirectUserToDashBoard(int roleID, Int64 UserID)
        {
            Session[Constant.Session.Role] = roleID;
            Session[Constant.Session.AdminSession] = UserID;
            Response.Redirect(Constant.Pages.Admin + Constant.Pages.DashBoard+"?FirstTime=1", false);
        }

        protected void lnkbtnSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constant.Pages.SignUp);
        }

        protected void lnkBtnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constant.Pages.ForgotPassword);
            //HttpApplication objApp = (HttpApplication)HttpContext.Current.ApplicationInstance;
            //HttpRequest Request = (HttpRequest)objApp.Context.Request;
            //NameValueCollection headers = Request.Headers;
            //headers.Add("MakeNMakeApiID", "b8##m.*2H");
            //Response.Redirect("MakenMakeCustomer.aspx?GetCustomerBy=emailid&CustomerData=Nisha@wiredsoft.org");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constant.Pages.SignUp);
        }

        
    }
}
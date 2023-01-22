
using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook;
using NLog;
using MakeNMake.BL;
using MakeNMake.Utilities;
using ASPSnippets.FaceBookAPI;
using System.Web.Script.Serialization;
using System.Security.Policy;
using System.Xml;
using ASPSnippets.TwitterAPI;
using ASPSnippets.GoogleAPI;
using System.Web.Security;
using ASPSnippets.LinkedInAPI;
using System.Globalization;


namespace MakeNMake
{
    public partial class SignUp : System.Web.UI.Page
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindCountry();
                if (!(String.IsNullOrEmpty(txtpass.Text.Trim())))
                {
                    txtpass.Attributes["value"] = txtpass.Text;
                }
                if (!(String.IsNullOrEmpty(txtcfpwd.Text.Trim())))
                {
                    txtcfpwd.Attributes["value"] = txtcfpwd.Text;
                }
            }

            txtday.Attributes.Add("onkeyup", "myKeyUpFunction();");
            txtDob.Attributes.Add("onkeyup", "myKeyUpFunction();");

            txtday.Attributes.Add("onkeydown", "myKeyDownFunction();");
            txtDob.Attributes.Add("onkeydown", "myKeyDownFunction();");


            twiiterlogin();
            googlelogin();
           
                linkdinlogin();
            
           
             
            
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnlinkdinlogin);
        }


        protected override void OnPreRender(EventArgs e)
        {
            txtpass.Attributes.Add("value", txtpass.Text);
            txtcfpwd.Attributes.Add("value", txtcfpwd.Text);
            base.OnPreRender(e);
        }
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path ="SignUp/FacebookCallback";
                return uriBuilder.Uri;
            }
        }
        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkTerms.Checked)
                {
                    BL.BLConsumer createUser = new BL.BLConsumer();
                    string password = MakeNMake.Utilities.EncryptDecrypt.Encript(txtpass.Text);
                    // string dob =Convert.ToInt16(Ddlm.SelectedValue) + "+" + txtday.Text + "+" + txtDob.Text;
                     string dob="";
                     string dob1="";
                   
                    dob = txtday.Text + "/" +  Ddlm.SelectedValue + "/" + txtDob.Text;
                    if (dob == "/0/")
                    {
                        dob1 = System.DateTime.Now.ToString();
                    }
                    else
                    {
                        dob1 = DateTime.ParseExact(dob, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                           .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    }
                    // DateTime dt = DateTime.ParseExact(dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //   DateTime dt = DateTime.ParseExact(dob, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    //string dt1=dt.ToString("yyyy-MM-dd HH:mm:tt"); 




                    //DateTime.ParseExact(txtDob.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    string address = txtaddresslocality.Text + "+" + txtstreet.Text;
                    int result = createUser.RegisterSignUpConsumer(txtfirstName.Text, txtlastName.Text, txtsignupid.Text, password, txtmobile.Text, ddlGender.SelectedValue,dob1, address, Convert.ToInt64(1), Convert.ToInt64(ddlState.SelectedValue), Convert.ToInt64(ddlDistrict.SelectedValue), Convert.ToInt64(ddlCity.SelectedValue), -99);

                    if (result > 0)
                    {
                        string content = MakeNMake.Utilities.EncryptDecrypt.Encript(txtsignupid.Text + ":" + txtfirstName.Text + " " + txtlastName.Text + ":" + result + ":" + txtpass.Text);
                        hdverifycontent.Value = content;
                        //string content = MakeNMake.Utilities.EncryptDecrypt.Encript(txtsignupid.Text + ":" + txtfirstName.Text + " " + txtlastName.Text + ":" + result + ":" + txtpass.Text);
                        //string VerifyUrl = string.Format("{0}EmailConfirmation.aspx?Content={1}", ReadConfig.SiteUrl, content);
                        //MEmail.SendGMail(txtsignupid.Text.Trim(), "Please activate your Make 'N' Make",
                        //    "Hi , " + txtfirstName.Text + " !,<br><br>  Thanks for Registering with us. A warm welcome to the MakenMake family. Give us a chance to serve you.  " +
                        //    " Please find the below Activation Link :<br><br> User ID: "
                        //    + txtsignupid.Text
                        //    + " <br> Click on this link: <a target='_blank' style='color:blue;'  href='" + VerifyUrl + "'>" +
                        //    VerifyUrl + "</a><br>You can change the password after login to our site with these credentials. If your email system does not allow linking, please copy and paste the following into your " +
                        //    "browser. For any queries or complaints, you have our ears at Helpline No : " + ReadConfig.helpLineNumber + ". <br><br>Kind Regards:<br> Make 'N' Make Service Team", "");
                        BLCustomerCare objConsumer = new BLCustomerCare();
                        String Otp = EncryptDecrypt.CreateRandomPassword(4);
                        hdnUserID.Value = result.ToString();
                        string message = "Hi , " + txtfirstName.Text + " ! Thanks for Registering with us. A warm welcome to the MakenMake family. Give us a chance to serve you.To Verify your account.Your Otp is" + Otp + " .For any queries or complaints, you have our ears at Helpline No : " + ReadConfig.helpLineNumber;
                        SendSms objSms = new SendSms();
                        try
                        {
                            
                            int resultotp = objConsumer.AddUpdateUserOTP(Convert.ToInt64(result), Otp, Convert.ToString(txtmobile.Text), 1);
                            int i = objSms.SendSmsOnMobile(message, txtmobile.Text);
                            dvVerifyOTP.Visible = true;
                            divAdduser.Visible = false;
                            if (i != 1)
                            {
                                BL.BLAdmin objAdmin = new BL.BLAdmin();
                                objAdmin.AddNotSendSmsMail(result, 0, "Sent to Client when SignUp", 1);
                            }

                        }
                        catch (Exception ex)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(result, 0, "Error while Sent to Client when when SignUp-Issue:-" + ex.Message, 1);
                        }
                        if (ddlCity.SelectedItem.Text.ToLower() == "gurgaon")
                        {
                            string mes = "Hi ," + txtfirstName.Text + "! Thanks for Registering with us. We regret that we are not currently serving in your city. We will be coming soon to cater your needs . For any queries , you have our ears at Helpline Nos : " + ReadConfig.helpLineNumber + " or email us at info@makenmake.co.in";
                            MEmail.SendGMail(txtsignupid.Text.Trim(), " Make 'N' Make", mes, "");
                            int i = objSms.SendSmsOnMobile(mes, txtmobile.Text);
                        }
                        Clear();

                        ddlState.Items.Clear();
                        ddlState.AppendDataBoundItems = true;
                        ddlState.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlState.SelectedIndex = 0;

                        ddlDistrict.Items.Clear();
                        ddlDistrict.AppendDataBoundItems = true;
                        ddlDistrict.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlDistrict.SelectedIndex = 0;

                        ddlCity.Items.Clear();
                        ddlCity.AppendDataBoundItems = true;
                        ddlCity.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlCity.SelectedIndex = 0;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Thanks for registering with us,Please Enter OTP recieved on your  to confirm your account') ;", true);
                    }
                    else if (result == -99)
                    {
                        Clear();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('user already exists with this emailid ,try with another') ;", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Some fatal error occurs') ;", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please check terms and conditions') ;", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error(logger.Name + ":" + ex.Message);
                Response.Redirect(Constant.Pages.Error);
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
                //divforgotinfo.Visible = true;
                BLServiceEngineer obj = new BLServiceEngineer();
                Guid g = Guid.NewGuid();

                string VerifyUrl = string.Format("{0}EmailConfirmation.aspx?Content={1}", ReadConfig.SiteUrl, hdverifycontent.Value);
               
                Response.Redirect(VerifyUrl);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Incorrect OTP') ;", true);
            }
        }
        private void Clear()
        {
            txtfirstName.Text = string.Empty;
            txtlastName.Text = string.Empty;
            txtsignupid.Text = string.Empty;
            Ddlm.SelectedValue = "0";
            txtpass.Text = string.Empty;
            txtcfpwd.Text = string.Empty;
            txtday.Text = string.Empty;

            txtmobile.Text = string.Empty;
            ddlGender.SelectedValue = "0";
            txtDob.Text = string.Empty;
            txtstreet .Text = string.Empty;
            txtaddresslocality.Text = string.Empty;
            //ddlCountry.SelectedValue = "0";
            ddlState.SelectedValue = "0";
            ddlDistrict.SelectedValue = "0";
            ddlCity.SelectedValue = "0";

            ddlPlans.SelectedValue = "0";
            chkTerms.Checked = false;

            rdbServices.ClearSelection();
        }

        protected void btnMicrosoft_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://login.live.com/oauth20_authorize.srf?client_id=" + ReadConfig.MicroSoftID + "&scope=wl.signin,wl.emails,wl.birthday&response_type=token");
        }
        protected void btnFacebook_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Under Construction') ;", true);
        }
        protected void btnGmail_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Under Construction') ;", true);

        }
        protected void btnTwitter_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Under Construction') ;", true);
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
        //change on 23-05-2015
        private void BindCountry()
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetCountry(ddlCountry);
            ddlCountry.SelectedValue = "2002";
            BindState(Convert.ToInt32(ddlCountry.SelectedValue));
        }
        private void BindState(int countryID)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetStatesByCountryID(ddlState, countryID);
            BindDistrict(Convert.ToInt32(ddlState.SelectedValue));
        }
        private void BindDistrict(Int64 StateID)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetDistrictsByID(ddlDistrict, StateID);
            BindCity(Convert.ToInt64(ddlDistrict.SelectedValue));

        }
        private void BindCity(Int64 districtID)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetCityByDistrictID(ddlCity, districtID);
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue != "0")
            {
                ddlState.Items.Clear();
                BindState(Convert.ToInt32(ddlCountry.SelectedValue));
            }
            else
            {
                ddlState.Items.Clear();
                ddlCity.Items.Clear();
            }
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue != "0")
            {
                ddlDistrict.Items.Clear();
                ddlCity.Items.Clear();
                BindDistrict(Convert.ToInt64(ddlState.SelectedValue));
            }
            else
            {
                ddlDistrict.Items.Clear();
                ddlCity.Items.Clear();
            }
        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDistrict.SelectedValue != "0")
            {
                ddlCity.Items.Clear();
                BindCity(Convert.ToInt64(ddlDistrict.SelectedValue));
            }
            else
            {
                ddlCity.Items.Clear();
            }
        }

        protected void btnSignupPay_Click(object sender, EventArgs e)
        {
            if (chkTerms.Checked)
            {
                BL.BLConsumer createUser = new BL.BLConsumer();
                string password = MakeNMake.Utilities.EncryptDecrypt.Encript(txtpass.Text);
                string dob = "";
                string dob1 = "";

                dob = txtday.Text + "/" + Ddlm.SelectedValue + "/" + txtDob.Text;
                if (dob == "/0/")
                {
                    dob1 = System.DateTime.Now.ToString();
                }
                else
                {
                    dob1 = DateTime.ParseExact(dob, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                       .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
                string address = txtaddresslocality.Text + "+" + txtstreet.Text;
                int result = createUser.RegisterSignUpConsumer(txtfirstName.Text, txtlastName.Text, txtsignupid.Text, password, txtmobile.Text, ddlGender.SelectedValue, dob1, address, Convert.ToInt64(1), Convert.ToInt64(ddlState.SelectedValue), Convert.ToInt64(ddlDistrict.SelectedValue), Convert.ToInt64(ddlCity.SelectedValue), -99);
                if (result > 0)
                {
                    string VerifyUrl = string.Format("{0}Default.aspx", ReadConfig.SiteUrl);
                    string message = "Hi , " + txtfirstName.Text + " ! Thanks for Registering with us. A warm welcome to the MakenMake family. Give us a chance to serve you. Your credentials have been sent to registered E-mail Id or directly be in touch with us through our Helpline No : " + ReadConfig.helpLineNumber;
                    SendSms objSms = new SendSms();
                    try
                    {
                        int i = objSms.SendSmsOnMobile(message, txtmobile.Text);
                        if (i != 1)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(result, 0, "Sent to AdminUser when SignUp", 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(result, 0, "Error while Sent to AdminUser when SignUp-Issue:-" + ex.Message, 1);
                    }
                    BL.BLConsumer obj = new BL.BLConsumer();
                    int result1 = obj.ConfirmUser(Convert.ToString(txtsignupid.Text));
                    if (result1 > 0)
                    {
                        string sVerifyUrl = string.Format("{0}Default.aspx", ReadConfig.SiteUrl);
                        MEmail.SendGMail(txtsignupid.Text, "Registration Make 'N' Make", "Dear " +
                            txtfirstName.Text + ",<br><br> You have successfully registered with Make N Make . Login with your credentials :<br><br> User ID: " + txtsignupid.Text
                            + "<br>Password: " + txtpass.Text + " <br> Click on this link: <a target='_blank' style='color:blue;' href='" + sVerifyUrl 
                            + "'>" + sVerifyUrl  + "</a><br>You can change the password after login to our site with these credentials.<br><br>Kind Regards:<br> Make 'N' Make Service Team", "");

                        Session[Constant.Session.Role] = 4;
                        Session[Constant.Session.AdminSession] = result1;
                        Response.Redirect("~/Pages/SUserServices.aspx");

                    }
                }
                else if (result == -99)
                {
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('user already exists with this emailid ,try with another') ;", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Some fatal error occurs') ;", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please check terms and conditions') ;", true);
            }
        }

        protected void btn_fblogin_Click(object sender, ImageClickEventArgs e)
        {
            //FaceBookConnect.Authorize("user_photos,email", Request.Url.AbsoluteUri.Split('?')[0]);
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {

                client_id = ReadConfig.FacebookID,

                redirect_uri = ReadConfig.FbUrl,

                response_type = "code",

                scope = "email" // Add other permissions as needed

            });
            Response.Redirect(loginUrl.AbsoluteUri);
            
        }
        
     
        protected void btn_twitterlogin_Click(object sender, ImageClickEventArgs e)
        {
            if (!TwitterConnect.IsAuthorized)
            {
                TwitterConnect twitter = new TwitterConnect();
                twitter.Authorize(Request.Url.AbsoluteUri.Split('?')[0]);
            }
        }
        public void twiiterlogin()
        {
            TwitterConnect.API_Key = "lGkS4AbLFLKgeIvWPWwHWDFib";
            TwitterConnect.API_Secret = "qWCfMuzBcklJZ5JvUKBLB9YiAXIFECkxiU2VvcPtRIt3CQ1Pih";
            if (!IsPostBack) if (!IsPostBack)
                {
                    if (TwitterConnect.IsAuthorized)
                    {
                        TwitterConnect twitter = new TwitterConnect();

                        //LoggedIn User Twitter Profile Details
                        DataTable dt = twitter.FetchProfile();
                       
                        string name = dt.Rows[0]["name"].ToString();
                       string id = dt.Rows[0]["Id"].ToString();
                        string screen = dt.Rows[0]["screen_name"].ToString();
                       

                        
                    }
                    if (TwitterConnect.IsDenied)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "key", "alert('User has denied access.')", true);
                    }
                }
        }

        protected void btnglogin_Click(object sender, ImageClickEventArgs e)
        {
            GoogleConnect.Authorize("profile", "email");
        }

        public void googlelogin()
        {
            GoogleConnect.ClientId = ReadConfig.GmailID;
            GoogleConnect.ClientSecret = ReadConfig.GmailSecretKey;
            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];
            BLAdmin adduser = new BLAdmin();
            BL.BLConsumer objLogin = new BL.BLConsumer();
            if (!string.IsNullOrEmpty(Request.QueryString["code"]) && !string.IsNullOrEmpty(Request.QueryString["state"]))
            {
                if (Request.QueryString["state"].ToString() == "/profile")
                {
                    string code = Request.QueryString["code"];

                    string json = GoogleConnect.Fetch("me", code);
                    GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);

                    string name = profile.DisplayName;
                    string emails = profile.Emails.Find(email => email.Type == "account").Value;
                    string gender = profile.Gender;
                    if (string.IsNullOrEmpty(emails))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Your Google account Email is not accessable or authorized') ;", true);
                    }
                    else
                    {

                       // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('"+ emails+"') ;", true);
                        string password = EncryptDecrypt.CreateRandomPassword(6);
                        int results = adduser.AddUsers(name, "", emails, EncryptDecrypt.Encript(password), System.DateTime.Now, "", 0, 0, 0, 0, 4, 0, 0, 0, 1, "14,15,16", "33,34,35,36,37,20045,38,39,40", "", "", "", "", "", 0);
                        if (results > 0)
                        {
                            string VerifyUrl = string.Format("{0}Default.aspx", ReadConfig.SiteUrl);
                            string content = MakeNMake.Utilities.EncryptDecrypt.Encript(emails + ":" + name + ":" + results + ":" + password);
                            MEmail.SendGMail(emails, "Registration Make 'N' Make", "Dear User,<br><br> You have successfully registered with Make N Make . Login with your credentials :<br><br> User ID: " + emails + "<br>Password: " + password + " <br><br> Click on this link: <a target='_blank' style='color:blue;'  href='" + VerifyUrl + "'>" + VerifyUrl + "</a><br><br>You can change the password after login to our site with these credentials.If your email system does not allow linking, please copy and paste the following into your browser. <br><br>Kind Regards:<br> Make 'N' Make Service Team", "");
                            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Thanks for registering with us,please check your email to confirm your account') ;", true);

                            int roleID = 0;
                            // string password = Utilities.EncryptDecrypt.Encript(txtpass.Text.Trim());
                            Int64 resultlog = objLogin.ValidateUser(emails, password, out roleID);
                            if (resultlog > 0)
                            {
                                CheckStatus(roleID, results, emails);
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
                            string useridresult = adduser.GetUserID(emails);
                            if (!string.IsNullOrEmpty(useridresult))
                            {
                                string[] values = useridresult.Split(':');
                                string userid = values[0].Trim();

                                CheckStatus(4, Convert.ToInt64(userid), emails);
                            }
                            else
                            {
                                string useridtempresult = adduser.GetUserTempID(emails);
                                if (!string.IsNullOrEmpty(useridtempresult))
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You have already registred with This email Please Activate your account from This " + emails + "') ;", true);
                                }
                                else
                                {

                                }
                                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('user already exists with this emailid ,try with another') ;", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Some fatal error occurs') ;", true);
                        }
                    }
                }
                if (Request.QueryString["error"] == "access_denied")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Access denied.')", true);
                }
            }
        }


        public void CheckStatus(int roleID, Int64 UserID,string email)
        {
            BL.BLAdmin ojAdmin = new BL.BLAdmin();
            int status = ojAdmin.CheckUserStatus(UserID);
            if (status == 1)
            {
                FormAuthentication(email);
                RedirectUserToDashBoard(roleID, UserID);
            }
            else if (status == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You are blocked by Admin, pls contact to Administrator') ;", true);
            }
        }
        public void FormAuthentication(string email)
        {


            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                  email, DateTime.Now, DateTime.Now.AddDays(10),
                  true, "Hi");

            string ticketString = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketString);
           
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
            Response.Redirect(Constant.Pages.Admin + Constant.Pages.DashBoard + "?FirstTime=1", false);
        }
        public class GoogleProfile
        {
            public string Id { get; set; }
            public string DisplayName { get; set; }
            public Image Image { get; set; }
            public List<Email> Emails { get; set; }
            public string Gender { get; set; }
            public string ObjectType { get; set; }
        }

        public class Email
        {
            public string Value { get; set; }
            public string Type { get; set; }
        }

        protected void btnlinkdinlogin_Click(object sender, ImageClickEventArgs e)
        {
            LinkedInConnect.Authorize();
           
        }
        public void linkdinlogin()
        {
            
            BLAdmin adduser = new BLAdmin();
            BL.BLConsumer objLogin = new BL.BLConsumer();
            LinkedInConnect.APIKey = ReadConfig.LinkedInID;
            LinkedInConnect.APISecret = ReadConfig.LinkedInSecretKey;
            LinkedInConnect.RedirectUrl = Request.Url.AbsoluteUri.Split('?')[0];
           
                    if (LinkedInConnect.IsAuthorized)
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["state"]))
                        {
                            if (Request.QueryString["state"].ToString() != "/profile")
                            {
                                DataSet ds = LinkedInConnect.Fetch();

                                string fname = ds.Tables["person"].Rows[0]["first-name"].ToString();
                                string lname = " " + ds.Tables["person"].Rows[0]["last-name"].ToString();
                                string emailid = ds.Tables["person"].Rows[0]["email-address"].ToString();
                                string password = EncryptDecrypt.CreateRandomPassword(6);
                                int results = adduser.AddUsers(fname, lname, emailid, EncryptDecrypt.Encript(password), System.DateTime.Now, "", 0, 0, 0, 0, 4, 0, 0, 0, 1, "14,15,16", "33,34,35,36,37,20045,38,39,40", "", "", "", "", "", 0);
                                if (results > 0)
                                {
                                    string VerifyUrl = string.Format("{0}Default.aspx", ReadConfig.SiteUrl);
                                    string content = MakeNMake.Utilities.EncryptDecrypt.Encript(emailid + ":" + fname + ":" + results + ":" + password);
                                    MEmail.SendGMail(emailid, "Registration Make 'N' Make", "Dear User,<br><br> You have successfully registered with Make N Make . Login with your credentials :<br><br> User ID: " + emailid + "<br>Password: " + password + " <br><br> Click on this link: <a target='_blank' style='color:blue;'  href='" + VerifyUrl + "'>" + VerifyUrl + "</a><br><br>You can change the password after login to our site with these credentials.If your email system does not allow linking, please copy and paste the following into your browser. <br><br>Kind Regards:<br> Make 'N' Make Service Team", "");
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Thanks for registering with us,please check your email to confirm your account') ;", true);

                                    int roleID = 0;
                                    // string password = Utilities.EncryptDecrypt.Encript(txtpass.Text.Trim());
                                    Int64 resultlog = objLogin.ValidateUser(emailid, password, out roleID);
                                    if (resultlog > 0)
                                    {
                                        CheckStatus(roleID, results, emailid);
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
                                    string useridresult = adduser.GetUserID(emailid);
                            if (!string.IsNullOrEmpty(useridresult))
                            {
                                string[] values = useridresult.Split(':');
                                string userid = values[0].Trim();

                                CheckStatus(4, Convert.ToInt64(userid), emailid);
                            }
                            else
                            {
                                string useridtempresult = adduser.GetUserTempID(emailid);
                                if (!string.IsNullOrEmpty(useridtempresult))
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You have already registred with This Email Id Please Activate your account from This " + emailid + "') ;", true);
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
            }
        }

        protected void Ddlm_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "isValidDOB();", true);
        }
    }
}
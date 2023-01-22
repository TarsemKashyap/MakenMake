using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.Live;
using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using System.Web.Script.Serialization;
using NLog;
using MakeNMake.Utilities;

namespace MakeNMake.Handler
{
    /// <summary>
    /// Summary description for MicroSoft
    /// </summary>
    public class MicroSoft : IHttpHandler
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (HttpContext.Current.Request.QueryString["access_token"] != null)
                {
                    WebRequest request = WebRequest.Create("https://apis.live.net/v5.0/me?access_token=" + HttpContext.Current.Request.QueryString["access_token"]);
                    WebResponse response = request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string userInfo = reader.ReadToEnd();
                    JavaScriptSerializer serialize = new JavaScriptSerializer();
                    var data = serialize.Deserialize<MUsers>(userInfo);
                    BLAdmin obj = new BLAdmin();
                    string emailid = string.Empty;
                    if (!string.IsNullOrEmpty(data.emails.account))
                    {
                        emailid = data.emails.account;
                    }
                    else
                    {
                        emailid = data.emails.preferred;
                    }
                    string password = EncryptDecrypt.Encript(EncryptDecrypt.CreateRandomPassword(6));
                    int result = 0;// obj.AddUsers(data.first_name, data.last_name, emailid, password, 4, 0,0,-99,1);
                    if (result > 0)
                    {
                        string VerifyUrl = string.Format("{0}/Default.aspx", ReadConfig.SiteUrl);
                        MEmail.SendGMail(emailid.Trim(), "Registration Make 'N' Make", "Dear User,<br><br> You have successfully registered with Make N Make . Login with your credentials :<br><br> User ID: " + emailid + "<br>Password: " + password + " <br> Click on this link: <a href='" + VerifyUrl + "'>" + VerifyUrl + "</a><br>You can change the password after login to our site with these credentials.If your email system does not allow linking, please copy and paste the following into your browser. <br><br>Kind Regards:<br> Make 'N' Make Service Team", "");        
                        context.Response.Redirect("~/" + Constant.Pages.Confirmation + Constant.Separators.QuestionMark + Constant.QueryString.Confirmed + Constant.Separators.EqualTo + EncryptDecrypt.Encript(Convert.ToString(result + ":" + (data.first_name + " " + data.last_name))),false);
                    }
                    else if (result == -99)
                    {
                        string userID=Convert.ToString(obj.GetUserID(emailid.Trim()));
                        context.Response.Redirect("~/" + Constant.Pages.Confirmation + Constant.Separators.QuestionMark + Constant.QueryString.Confirmed + Constant.Separators.EqualTo + EncryptDecrypt.Encript(Convert.ToString(userID)), false);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(logger.Name + ":" + ex.Message);
                HttpContext.Current.Response.Redirect("~/" + Constant.Pages.Error,true);
            }
        }
        public class MUsers
        {
            public string id { get; set; }
            public string name { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string birth_day { get; set; }
            public string birth_month { get; set; }
            public string birth_year { get; set; }
            public string gender { get; set; }
            public string locale { get; set; }
            public emails emails { get; set; }
        }

        public class emails
        {
            public string preferred { get; set; }
            public string account { get; set; }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
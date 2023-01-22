using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace MakeNMake.Handler
{
    /// <summary>
    /// Summary description for Gmail1
    /// </summary>
    public class Gmail1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String URI = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + System.Web.HttpContext.Current.Request.QueryString["access_token"].ToString();

            WebClient webClient = new WebClient();
            Stream stream = webClient.OpenRead(URI);
            string b;

            /*I have not used any JSON parser because I do not want to use any extra dll/3rd party dll*/
            using (StreamReader br = new StreamReader(stream))
            {
                b = br.ReadToEnd();
            }

            b = b.Replace("id", "").Replace("email", "");
            b = b.Replace("given_name", "");
            b = b.Replace("family_name", "").Replace("link", "").Replace("picture", "");
            b = b.Replace("gender", "").Replace("locale", "").Replace(":", "");
            b = b.Replace("\"", "").Replace("name", "").Replace("{", "").Replace("}", "");

            /*
                 
            "id": "109124950535374******"
              "email": "usernamil@gmail.com"
              "verified_email": true
              "name": "firstname lastname"
              "given_name": "firstname"
              "family_name": "lastname"
              "link": "https://plus.google.com/10912495053537********"
              "picture": "https://lh3.googleusercontent.com/......./photo.jpg"
              "gender": "male"
              "locale": "en" } 
           */

            Array ar = b.Split(",".ToCharArray());
            for (int p = 0; p < ar.Length; p++)
            {
                ar.SetValue(ar.GetValue(p).ToString().Trim(), p);

            }
            /// Email_address = ar.GetValue(1).ToString();
            // Google_ID = ar.GetValue(0).ToString();
            // firstName = ar.GetValue(4).ToString();
            // LastName = ar.GetValue(5).ToString();


            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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
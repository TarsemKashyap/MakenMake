using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MakeNMake.CommomFunctions
{
    public static class ReadConfig
    {
        #region Properties
        public static string SiteUrl
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["SiteUrl"]); }
        }
        public static string CssPath
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["SiteUrl"]+ConfigurationManager.AppSettings["Css"]); }
        }
        public static string StaticImages
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["SiteUrl"]+ConfigurationManager.AppSettings["StaticImages"]); }
        }
        public static string JsPath
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["SiteUrl"]+ConfigurationManager.AppSettings["Js"]); }
        }
        public static string Bootstrap
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["SiteUrl"] + ConfigurationManager.AppSettings["Bootstrap"]); }
        }
        public static string helpLineNumber
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["helplineNumber"]); }
        }
        public static string QuotationTemplate
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["QuotationTemplate"]); }
        }
        public static string CitrusPostUrl
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["CitrusPostUrl"]); }
        }
        public static string CitrusVanityUrl
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["VanityUrl"]); }
        }
        public static string CitrusSecretKey
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["CitrusSecretKey"]); }
        }
        public static string PDfDownloadFiles
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["PDfDownloadFiles"]); }
        }
        #endregion

        #region Authentication

        public static string MicroSoftID
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["MicroSoftID"]); }
        }
        public static string MicroSoftSecretKey
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["MicroSoftSecretKey"]); }
        }
       
        public static string FacebookID
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["facebookID"]); }
        }
        public static string FbUrl
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["FbUrl"]); }
        }
        public static string FacebookSecretKey
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["facebookSecretKey"]); }
        }

        public static string twitterID
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["TwiterID"]); }
        }
        public static string twitterSecretKey
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["TwiterSecretKey"]); }
        }

        public static string LinkedInID
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["LinkedInID"]); }
        }
        public static string LinkedInSecretKey
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["LinkedInSecretKey"]); }
        }

        public static string GmailID
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["GmailID"]); }
        }
        public static string GmailSecretKey
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["GmailIDKey"]); }
        }

        #endregion

    }
}
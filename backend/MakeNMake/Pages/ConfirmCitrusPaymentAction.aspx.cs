using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Pages
{
    public partial class ConfirmCitrusPaymentAction : System.Web.UI.Page
    {
        public string formPostUrl { get; set; }
        public string merchantTxnId { get; set; }
        public string orderAmount { get; set; }
        public string currency { get; set; }
        public string returnUrl { get; set; }
        public string notifyUrl { get; set; }
        public string securitySignature { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    form1.Visible = false;
                    form2.Action = ReadConfig.CitrusPostUrl;
                    string ActionParameterFromMakenMake = EncryptDecrypt.DecryptText(Convert.ToString(Request.QueryString.Get("ActionParameter")));

                    string[] parameters = ActionParameterFromMakenMake.Split(':');
                    //parameter in squence as follow
                    //changeplan 0,IsMakenMakeClient 1,CustomerID 2,remainingAmount 3,CreatedBy 4,invoiceNumber 5,amount 6,plan 7,servicetype 8,category 9,payment method 10, wallet money 11, status 12 ,plan 13,type 14

                    lblAmount.Text = parameters[6];
                    formPostUrl = ReadConfig.CitrusPostUrl;
                    string secret_key = ReadConfig.CitrusSecretKey;
                    string vanityUrl = ReadConfig.CitrusVanityUrl;
                    //1 code by rajdeep yadav for mobile app

                    if (Session[Constant.Session.Role] == null)
                    {
                        Session[Constant.Session.Role] = 4;
                    }
                    if (Session[Constant.Session.AdminSession] == null)
                    {
                        Session[Constant.Session.AdminSession] = parameters[2];
                    }

                    // 1 end here

                    merchantTxnId = System.DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    lblMerchantID.Text = merchantTxnId;
                    orderAmount = parameters[6];
                    currency = "INR";
                    string data1 = vanityUrl + orderAmount + merchantTxnId + currency;

                    returnUrl = ReadConfig.SiteUrl + "Pages/PaymentResponsePage.aspx?ResponseParameter=" + Convert.ToString(Request.QueryString.Get("ActionParameter"));
                    notifyUrl = ReadConfig.SiteUrl + "Pages/PaymentNotification.aspx?ResponseParameter=" + Convert.ToString(Request.QueryString.Get("ActionParameter"));
                    if (parameters.Length > 14)
                    {
                        if (parameters[15] == "app")
                        {
                            returnUrl = ReadConfig.SiteUrl + "Pages/PaymentResponsePageApp.aspx?ResponseParameter=" + Convert.ToString(Request.QueryString.Get("ActionParameter"));
                            notifyUrl = ReadConfig.SiteUrl + "Pages/PaymentNotification.aspx?ResponseParameter=" + Convert.ToString(Request.QueryString.Get("ActionParameter"));
                        }
                    }

                    System.Security.Cryptography.HMACSHA1 myhmacsha1 = new System.Security.Cryptography.HMACSHA1(Encoding.ASCII.GetBytes(secret_key));
                    System.IO.MemoryStream stream = new System.IO.MemoryStream(Encoding.ASCII.GetBytes(data1));
                    securitySignature = BitConverter.ToString(myhmacsha1.ComputeHash(stream)).Replace("-", "").ToLower();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx");
                }
            }
        }
    }
}
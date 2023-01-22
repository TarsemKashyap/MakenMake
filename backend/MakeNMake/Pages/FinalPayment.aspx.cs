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

namespace MakeNMake.Customer
{

    public partial class FinalPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServicesFinalPayment.Getinfo += new EventHandler(UserControlID_buttonClick);

            bool IsClient = false;
            int roleID = Convert.ToInt32(Session[Constant.Session.Role]);
            if (roleID == 4)
            {
                IsClient = true;
            }
            if (!IsPostBack)
            {
                int status = 0;
                string plan = string.Empty;
                string type = string.Empty;
                try
                {
                    string[] data = Convert.ToString(EncryptDecrypt.DecryptText(Request.QueryString.Get("PaymentAction"))).Split(':');
                    status = Convert.ToInt32(data[0]);
                    plan = Convert.ToString(data[1]);
                    type = Convert.ToString(data[2]);
                }
                catch
                {
                    if (IsClient)
                    {
                        Response.Redirect("ProceedToPayment.aspx");
                    }
                    else
                    {
                        string customerID = Convert.ToString(Request.QueryString["ClientID"]);
                        Response.Redirect("ProceedToPayment.aspx?ClientID=" + EncryptDecrypt.DecryptText(customerID));
                    }
                }
                if (IsClient)
                {
                    ServicesFinalPayment.BindPayment(Convert.ToInt64(Session[Constant.Session.AdminSession]), status, plan, type);
                }
                else
                {
                    string customerID = Convert.ToString(Request.QueryString["ClientID"]);
                    ServicesFinalPayment.BindPayment(Convert.ToInt64(EncryptDecrypt.DecryptText(customerID)), status, plan, type);
                }
            }
        }
        protected void UserControlID_buttonClick(object sender, EventArgs e)
        {
            try
            {
                bool isClient = false;
                string[] data = Convert.ToString(EncryptDecrypt.DecryptText(Request.QueryString.Get("PaymentAction"))).Split(':');

                ServicesFinalPayment.status = Convert.ToInt32(data[0]);
                ServicesFinalPayment.servicePlan = data[1];
                ServicesFinalPayment.serviceType = data[2];

                int roleID = Convert.ToInt32(Session[Constant.Session.Role]);
                
                if (roleID == 4)
                {
                    isClient = true;
                }
                if (!isClient)
                {
                    ServicesFinalPayment.IsClient = false;
                    ServicesFinalPayment.CreatedBy = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    string customerID = Convert.ToString(Request.QueryString["ClientID"]);
                    if (string.IsNullOrEmpty(customerID))
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                    else
                    {
                        ServicesFinalPayment.CustomerID = Convert.ToInt64(EncryptDecrypt.DecryptText(customerID));
                        ServicesFinalPayment.EncryptdClientID = customerID;
                    }
                }
                else
                {
                    ServicesFinalPayment.IsClient = true;
                    ServicesFinalPayment.CustomerID = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    ServicesFinalPayment.CreatedBy = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    ServicesFinalPayment.EncryptdClientID = string.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }
      
    }

}
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
    public partial class ProceedToPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServicesProceedToPayment.Getinfo += new EventHandler(UserControlID_buttonClick);
            bool IsClient = false;

            int roleID = Convert.ToInt32(Session[Constant.Session.Role]);
            if (roleID == 4)
            {
                IsClient = true;
            }

            if (!IsPostBack)
            {
                if (IsClient)
                {
                    ServicesProceedToPayment.BindData(Convert.ToInt64(Session[Constant.Session.AdminSession]));
                }
                else
                {
                    string customerID = Convert.ToString(Request.QueryString["ClientID"]);
                    ServicesProceedToPayment.BindData(Convert.ToInt64(EncryptDecrypt.DecryptText(customerID)));
                }
            }
        }
        protected void UserControlID_buttonClick(object sender, EventArgs e)
        {
            try
            {
                bool isClient = false;
                int roleID = Convert.ToInt32(Session[Constant.Session.Role]);
                if (roleID == 4)
                {
                    isClient = true;
                }
                if (!isClient)
                {
                    ServicesProceedToPayment.IsClient = false;
                    ServicesProceedToPayment.CreatedBy = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    string customerID = Convert.ToString(Request.QueryString["ClientID"]);
                    if (string.IsNullOrEmpty(customerID))
                    {
                        Response.Redirect("Clients.aspx");
                    }
                    else
                    {
                        ServicesProceedToPayment.CustomerID = Convert.ToInt64(EncryptDecrypt.DecryptText(customerID));
                        ServicesProceedToPayment.EncryptdClientID = customerID;
                    }
                }
                else
                {
                    ServicesProceedToPayment.IsClient = true;
                    ServicesProceedToPayment.CustomerID = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    ServicesProceedToPayment.CreatedBy = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    ServicesProceedToPayment.EncryptdClientID = string.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
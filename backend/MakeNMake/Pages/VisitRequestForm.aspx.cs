using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Customer
{
    public partial class VisitRequestForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            RequestForm.buttonClick += new EventHandler(UserControlID_buttonClick);
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
                    RequestForm.IsClient = false;
                    RequestForm.CreatedBy = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    string customerID = Convert.ToString(Request.QueryString["ClientID"]);
                    if (string.IsNullOrEmpty(customerID))
                    {
                        Response.Redirect("Clients.aspx");
                    }
                    else
                    {
                        RequestForm.CustomerID = Convert.ToInt64(EncryptDecrypt.DecryptText(customerID));
                        RequestForm.EncryptdClientID = customerID;
                    }
                }
                else
                {
                    RequestForm.IsClient = true;
                    RequestForm.CustomerID = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    RequestForm.CreatedBy = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    RequestForm.EncryptdClientID = string.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }
   
    }
}
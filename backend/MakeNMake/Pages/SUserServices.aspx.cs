using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Pages
{
    public partial class SUserServices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserServices.buttonClick += new EventHandler(UserControlID_buttonClick);
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
                    UserServices.IsClient = false;
                    UserServices.CreatedBy = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    string customerID = Convert.ToString(Request.QueryString["ClientID"]);
                    if (string.IsNullOrEmpty(customerID))
                    {
                        Response.Redirect("Clients.aspx");
                    }
                    else
                    {
                        UserServices.CustomerID = Convert.ToInt64(EncryptDecrypt.DecryptText(customerID));
                        UserServices.EncryptdClientID = customerID;
                    }
                }
                else
                {
                    UserServices.IsClient = true;
                    UserServices.CustomerID = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    UserServices.CreatedBy = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    UserServices.EncryptdClientID = string.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
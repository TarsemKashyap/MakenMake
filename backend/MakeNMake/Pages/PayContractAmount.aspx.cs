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

namespace MakeNMake.Pages
{
    public partial class PayContractAmount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PayServiceContract.Getinfo += new EventHandler(UserControlID_buttonClick);
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
                    PayServiceContract.IsClient = false;
                    PayServiceContract.CreatedBy = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    string customerID = Convert.ToString(Request.QueryString["ClientID"]);
                    if (string.IsNullOrEmpty(customerID))
                    {
                        Response.Redirect("Clients.aspx");
                    }
                    else
                    {
                        PayServiceContract.CustomerID = Convert.ToInt64(EncryptDecrypt.DecryptText(customerID));
                        PayServiceContract.EncryptdClientID = customerID;
                    }
                }
                else
                {
                    PayServiceContract.IsClient = true;
                    PayServiceContract.CustomerID = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    PayServiceContract.CreatedBy = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    PayServiceContract.EncryptdClientID = string.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
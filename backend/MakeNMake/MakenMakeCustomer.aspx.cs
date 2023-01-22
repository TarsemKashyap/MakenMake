using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake
{
    public partial class MakenMakeCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.Headers != null)
                    {
                        if (Convert.ToString(Request.Headers["makenmakeapiid"]) == "b8##m.*2H")
                        {
                            string getCustomerBy = Convert.ToString(Request.QueryString["GetCustomerBy"]);
                            string customerdata = Convert.ToString(Request.QueryString["CustomerData"]);
                            if (string.IsNullOrEmpty(getCustomerBy) && string.IsNullOrEmpty(customerdata))
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please send proper parameters ') ;", true);
                            }
                            else
                            {
                                BL.BLConsumer objLogin = new BL.BLConsumer();
                                Int64 result = 0;
                                if (getCustomerBy.ToLower() == "emailid")
                                {
                                    result = objLogin.AmeyoUser("emailid", customerdata);
                                }
                                else if (getCustomerBy.ToLower() == "mobilenumber")
                                {
                                    result = objLogin.AmeyoUser("mobilenumber", customerdata);
                                }
                                else if (getCustomerBy.ToLower() == "userid")
                                {
                                    result = objLogin.AmeyoUser("userid", customerdata);
                                }
                                if (result > 0)
                                {
                                    CheckStatus(result);
                                }
                                else if (result == -99)
                                {
                                    Response.Redirect("Default.aspx", false);
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You have not access to this page , please contact administrator for further queries') ;", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You have not access to this page , please contact administrator for further queries') ;", true);
                    }
                }
                catch (Exception ex)
                {
                    //Response.Redirect("SignUp.aspx");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please send proper parameters') ;", true);
                }
            }
        }
        public void RedirectUserToDashBoard( Int64 UserID)
        {
            Session[Constant.Session.Role] = 4;
            Session[Constant.Session.AdminSession] = UserID;
            Server.Transfer(Constant.Pages.Admin + Constant.Pages.DashBoard + "?Confirm=yes", true);
        }
   
        public void CheckStatus(Int64 UserID)
        {
            BL.BLAdmin ojAdmin = new BL.BLAdmin();
            int status = ojAdmin.CheckUserStatus(UserID);
            if (status == 1)
            {
                RedirectUserToDashBoard( UserID);
            }
            else if (status == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Customer is blocked , pls contact to Administrator') ;", true);
            }
        }
    }
}
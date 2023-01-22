using MakeNMake.BL;
using MakeNMake.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake
{
    public partial class VerifyAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    

                }
                catch(Exception ex)
                {
                    Response.Redirect("Default.aspx");
                }
            }
            
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            Common objSetPAssword = new Common();
            string content = Convert.ToString(Request.QueryString["Content"]);
             string[] ContentData=EncryptDecrypt.DecryptText(content).Split(':');
             int result = objSetPAssword.SetNewPassword(ContentData[0], EncryptDecrypt.Encript(txtnewpass.Text));
             if (result == 1)
             {
                 Response.Redirect("Default.aspx");
                 //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Your New password updated successfully , please login with new credentials') ;", true);
             }
             else if(result==-99)
             {
                 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You have already set the Password, please again go to login page and click forgot password ') ;", true);
             }
        }
    }
}
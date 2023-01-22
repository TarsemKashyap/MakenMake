using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Pages
{
    public partial class CommercialInspection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblhreqserviceID.Text = Convert.ToString(Utilities.EncryptDecrypt.DecryptText(Request.QueryString["RequestData"]));
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BLServiceEngineer obj = new BLServiceEngineer();
                string RequestData = Convert.ToString(Utilities.EncryptDecrypt.DecryptText(Request.QueryString["RequestData"]));

                Int64 result = obj.UpdateRequestData(Convert.ToInt64(RequestData), Convert.ToInt64(Session[Constant.Session.AdminSession]), Convert.ToInt32(ddlStatus.SelectedValue), txtReason.Text);
                if (result > 0)
                {
                    Response.Redirect("RequestData.aspx",false);
                }
                else if (result == -99)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You already have the same status') ;", true);
                }
                else if (result == -98)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot change the status as you had already submit the assessment form') ;", true);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx");
            }
         }
      
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestData.aspx");
        }

    }
}
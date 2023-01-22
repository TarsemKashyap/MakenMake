using MakeNMake.Datatable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Pages
{
    public partial class EditServiceContract : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        public void BindData()
        {
            ClientHIstory obj = new ClientHIstory();
            BL.BLAdmin objAdmin = new BL.BLAdmin();
            DataTable dt = objAdmin.GetAllClient();
            if (dt != null && dt.Rows.Count > 0)
            {
                RptClient.Visible = true;
                RptClient.DataSource = dt;
                RptClient.DataBind();
            }
            else
            {
                RptClient.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No one has purchased any service') ;", true);
            }
        }

        protected void RptClient_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "editcontract")
            {
                Response.Redirect("EditServiceContractByAgreement.aspx?ServiceContractData="+Utilities.EncryptDecrypt.Encript(e.CommandArgument.ToString()));
            }
        }
    }
}
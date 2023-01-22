using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Customer
{
    public partial class ServicePurchasedByClient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        private void BindData()
        {
            BL.BLConsumer objAdmin = new BL.BLConsumer();
            DataTable dt = objAdmin.GetPurchasedClient(Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if (dt != null && dt.Rows.Count > 0)
            {
                RptService.DataSource = dt;
                RptService.DataBind();
            }
            else
            {
                lblMsg.Text = "No Service Purchased By You";
                lblMsg.CssClass = "label-success";
            }
        }

        protected void RptService_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Customer")
            {
                Int64 userID = Convert.ToInt64(e.CommandArgument);
                UserInfo.BindData(userID);
                hdnCustomer.Value = "1";
            }
            else if (e.CommandName == "Agreement")
            {
                Response.Redirect("AgreementDetail.aspx?AgreementID="+Utilities.EncryptDecrypt.Encript(Convert.ToString(e.CommandArgument)));
            }
        }

        protected void RptService_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton LinkButton1 = (LinkButton)e.Item.FindControl("LinkButton1");
                LinkButton1.OnClientClick = "return OpenInvoice(" + Session[Constant.Session.AdminSession] + ");";
            }
        }
    }
}
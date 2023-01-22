using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MakeNMake.Customer
{
    public partial class AgreementDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindData(Convert.ToInt64(Utilities.EncryptDecrypt.DecryptText(Convert.ToString(Request.QueryString["AgreementID"]))));
                }
                catch (Exception ex)
                {
                    Response.Redirect("ServicePurchasedByClient.aspx");
                }
            }
        }
        private void BindData(Int64 agreementID)
        {

            BL.BLConsumer objAdmin = new BL.BLConsumer();
            DataTable dt = objAdmin.GetClientService(agreementID);
            if (dt != null && dt.Rows.Count > 0)
            {
                Rptagreement.DataSource = dt;
                Rptagreement.DataBind();
            }
            else
            {
                lblMsg.Text = "No Service Purchased By Client";
            }
        }

        protected void Rptagreement_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Customer")
            {
                Int64 agreementId = Convert.ToInt64(e.CommandArgument);
                UserInfo.BindData(agreementId);
                hdnCustomer.Value = "1";
            }

        }

        public string AgreementNumber { get; set; }

        protected void Rptagreement_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblplan = (Label)e.Item.FindControl("lblplan");
                string plan = lblplan.Text.Substring(0, 1).ToLower();
                if (plan == "u")
                {
                    Control HeaderTemplate = Rptagreement.Controls[0].Controls[0];
                    HtmlTableCell thunlimitedarea = HeaderTemplate.FindControl("unlimitedarea") as HtmlTableCell;
                    HtmlTableCell thunlimitedcategory = HeaderTemplate.FindControl("unlimitedcategory") as HtmlTableCell;
                    HtmlTableCell tdArea = (HtmlTableCell)e.Item.FindControl("tdArea");
                    HtmlTableCell tdCategory = (HtmlTableCell)e.Item.FindControl("tdCategory");
                    thunlimitedarea.Visible = true;
                    thunlimitedcategory.Visible = true;
                    tdArea.Visible = true;
                    tdCategory.Visible = true;
                }
                else
                {
                    Control HeaderTemplate = Rptagreement.Controls[0].Controls[0];
                    HtmlTableCell thunlimitedarea = HeaderTemplate.FindControl("unlimitedarea") as HtmlTableCell;
                    HtmlTableCell thunlimitedcategory = HeaderTemplate.FindControl("unlimitedcategory") as HtmlTableCell;
                    HtmlTableCell tdArea = (HtmlTableCell)e.Item.FindControl("tdArea");
                    HtmlTableCell tdCategory = (HtmlTableCell)e.Item.FindControl("tdCategory");
                    thunlimitedarea.Visible = false;
                    thunlimitedcategory.Visible = false;
                    tdArea.Visible = false;
                    tdCategory.Visible = false;
                }
            }
        }
    }
}
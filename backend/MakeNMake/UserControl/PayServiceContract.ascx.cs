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

namespace MakeNMake.UserControl
{
    public partial class PayServiceContract : System.Web.UI.UserControl
    {
        public decimal totalCount = 0;
        public decimal totalSaving = 0;
        public Int64 CustomerID { get; set; }
        public Int64 CreatedBy { get; set; }
        public string EncryptdClientID { get; set; }
        public bool IsClient { get; set; }
        public event EventHandler Getinfo;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BindData(string plan, string type)
        {
            Common obj = new Common();
            DataTable dt = obj.GetContractForClient(plan, Convert.ToInt64(Session[Constant.Session.AdminSession]), type);
            if (dt != null && dt.Rows.Count > 0)
            {
                RptService.Visible = true;
                RptService.DataSource = dt;
                RptService.DataBind();
            }
            else
            {
                RptService.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No contract regarding this plan') ;", true);
            }
        }

        protected void RptService_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                Int64 orderID = Convert.ToInt64(e.CommandArgument);
                Common obj = new Common();
                obj.DeleteBasketItem(orderID);
                if (RptService.Items.Count == 1)
                {
                    Response.Redirect("PayContractAmount.aspx");
                }
                else
                {
                    BindData(ddlplan.SelectedValue, ddltype.SelectedValue);
                }
            }
        }

        protected void RptService_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnOriginal = (HiddenField)e.Item.FindControl("hdnOriginal");
                Label lblTotalSaving = (Label)e.Item.FindControl("lblTotalSaving");
                totalCount += Convert.ToDecimal(hdnOriginal.Value);
                totalSaving += Convert.ToDecimal(lblTotalSaving.Text);
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblTotal = (Label)e.Item.FindControl("lblTotal");
                lblTotal.Text = "Proceed to Payment : Rs." + totalCount.ToString();
                Session["TotalSaving"] = Convert.ToString(totalSaving);
            }
        }

        protected void btnFinalPayment_Click(object sender, EventArgs e)
        {
            if (RptService.Items.Count > 0)
            {
                Getinfo(null, null);
                if (IsClient)
                {
                    Response.Redirect("FinalPayment.aspx?PaymentAction=" + EncryptDecrypt.Encript("8:" + ddlplan.SelectedValue + ":" + ddltype.SelectedValue));
                }
                else
                {
                    Response.Redirect("FinalPayment.aspx?ClientID=" + EncryptdClientID + "&PaymentAction=" + EncryptDecrypt.Encript("8:" + ddlplan.SelectedValue + ":" + ddltype.SelectedValue));
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No item in the cart') ;", true);
            }
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }

        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlplan.SelectedValue == "0")
            {
                ddltype.SelectedValue = "0";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select plan') ;", true);
            }
            else
            {
                if (ddltype.SelectedValue == "0")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select type') ;", true);
                }
                else
                {
                    BindData(ddlplan.SelectedValue, ddltype.SelectedValue);
                }
            }
        }
    }
}
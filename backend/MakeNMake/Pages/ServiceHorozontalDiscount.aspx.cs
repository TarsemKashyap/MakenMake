using MakeNMake.BL;
using System;
using System.Collections.Generic;
using MakeNMake.CommomFunctions;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Admin
{
    public partial class ServiceHorozontalDiscount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlStatus.SelectedValue = "true";
                GetDiscount();
            }
        }
        private void GetDiscount()
        {
            BLAdmin admin = new BLAdmin();
            DataTable dt = admin.GetHorizontalDiscount();
            if (dt != null && dt.Rows.Count > 0)
            {
                RptService.Visible = true;
                RptService.DataSource = dt;
                RptService.DataBind();
            }
            else
            {
                RptService.Visible = false;
            }
        }
        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlQuantity.SelectedValue == "0")
            {
                dvFixed.Visible = false;
                dvVariableFrom.Visible = false;
                dvVariableTo.Visible = false;
            }
            else if (ddlQuantity.SelectedValue == "1")
            {
                dvFixed.Visible = false;
                dvVariableFrom.Visible = true;
                dvVariableTo.Visible = true;
            }
            else
            {
                dvFixed.Visible = true;
                dvVariableFrom.Visible = false;
                dvVariableTo.Visible = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            BLAdmin objDiscount = new BLAdmin(); int QuantFrom = 0;
            int QuantTo = 0;
            if (ddlQuantity.SelectedValue == "1")
            {
                QuantFrom = Convert.ToInt32(txtQuanFrom.Text);
                QuantTo = Convert.ToInt32(txtQuantityTo.Text);
            }
            else
            {
                QuantFrom = Convert.ToInt32(txtquantityfrom.Text);
                QuantTo = Convert.ToInt32(txtquantityfrom.Text);
            }
            if (btnAdd.Text.ToLower() == "add")
            {
                int result = objDiscount.AddServiceHorizontalDiscount(QuantFrom, QuantTo, Convert.ToInt32(ddlStatus.SelectedValue == "true" ? "1" : "0"), Convert.ToDecimal(txtdiscount.Text), Convert.ToInt64(Session[Constant.Session.AdminSession]));
                if (result == 1)
                {
                    txtdiscount.Text = string.Empty;
                    txtQuanFrom.Text = string.Empty;
                    txtquantityfrom.Text = string.Empty;
                    txtQuantityTo.Text = string.Empty;
                    ddlStatus.SelectedValue = "0";
                    ddlQuantity.SelectedValue = "0";
                    GetDiscount();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Discount added sucessfully') ;", true);
                }
            }
            else
            {
                int result = objDiscount.UpdateServiceHorizontalDiscount(Convert.ToInt32(hdnServiceID.Value), QuantFrom, QuantTo, Convert.ToInt32(ddlStatus.SelectedValue == "true" ? "1" : "0"), Convert.ToDecimal(txtdiscount.Text), Convert.ToInt64(Session[Constant.Session.AdminSession]));
                if (result == 1)
                {
                    ddlQuantity.Enabled = true;
                    txtdiscount.Text = string.Empty;
                    txtQuanFrom.Text = string.Empty;
                    txtquantityfrom.Text = string.Empty;
                    txtQuantityTo.Text = string.Empty;
                    ddlStatus.SelectedValue = "0";
                    ddlQuantity.SelectedValue = "0";
                    GetDiscount();
                    btnAdd.Text = "Add";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Discount Updated sucessfully') ;", true);
                }
            }
        }
        protected void RptService_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                BLAdmin objDelete = new BLAdmin();
                int DisID = Convert.ToInt32(e.CommandArgument);
                int result = objDelete.DeleteHoriZontalDIscount(DisID);
                if (result == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Deleted') ;", true);
                    GetDiscount();
                }
            }
            else if (e.CommandName == "edit")
            {
                hdnServiceID.Value = Convert.ToString(e.CommandArgument);
                Label lblQuant = (Label)e.Item.FindControl("lblquantFrom");
                Label lblDiscount = (Label)e.Item.FindControl("lblDiscount");
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                btnAdd.Text = "Edit";
                HiddenField serviceID = (HiddenField)e.Item.FindControl("hdnID");
                string [] Quant=lblQuant.Text.Split('-');
                if (Quant.Count() > 1)
                {
                    ddlQuantity.SelectedValue = "1";
                    dvFixed.Visible = false;
                    dvVariableFrom.Visible = true;
                    dvVariableTo.Visible = true;
                    txtQuanFrom.Text = Quant[0];
                    txtQuantityTo.Text = Quant[1];
                }
                else
                {
                    ddlQuantity.SelectedValue = "2";
                    dvFixed.Visible = true;
                    dvVariableFrom.Visible = false;
                    dvVariableTo.Visible = false;
                    txtquantityfrom.Text = lblQuant.Text;
                }
                txtdiscount.Text = lblDiscount.Text;
                ddlStatus.SelectedValue = lblStatus.Text == "Active" ? "true" : "false";
                ddlQuantity.Enabled = false;
            }
           
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }
    }
}
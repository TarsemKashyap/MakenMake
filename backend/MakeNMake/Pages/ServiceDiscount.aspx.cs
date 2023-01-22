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
    public partial class ServiceDiscount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlStatus.SelectedValue = "true";
                BindServiceDiscount();
            }
        }
        //private void BindPlan()
        //{
        //    BLAdmin objGetServices = new BLAdmin();
        //    DataTable dt = objGetServices.GetServiceList();
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        ddlService.DataSource = dt;
        //        ddlService.DataTextField = "fullPlan";
        //        ddlService.DataValueField = "PlanID";
        //        ddlService.DataBind();
        //        ddlService.Items.Insert(0, new ListItem("--Select Service Plan--", "0"));

        //    }
        //}
        private void BindServiceDiscount()
        {
            BLAdmin objGetServices = new BLAdmin();
            DataTable dt = objGetServices.GetServiceListDiscount();
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
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string plan = ddlService.SelectedValue;
             if (plan == "U")
             {
                 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Discounts for unlimited plan are defined on service plan page') ;", true);
             }
             else
             {
                 BL.BLAdmin objPlan = new BL.BLAdmin();

                 if (btnAdd.Text.ToLower() == "add")
                 {

                     int result = objPlan.AddServiceDiscount(ddlService.SelectedValue,
                        Convert.ToInt32(ddlPlanMode.SelectedValue)
                        ,Convert.ToInt32(txtFromCall.Text),
                        Convert.ToInt32(ddlPlanMode.SelectedValue) == 1 ? 0 : Convert.ToInt32(txtToCall.Text), Convert.ToInt32(ddlStatus.SelectedValue == "true" ? "1" : "0")
                        , Convert.ToDecimal(txtdiscount.Text), Convert.ToInt64(Session[Constant.Session.AdminSession]));
                     if (result > 0)
                     {
                         Clear(); BindServiceDiscount();
                         ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Service Discount added sucessfully') ;", true);
                     }
                     else if (result == -99)
                     {
                         ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Service Discount already exists with this plan') ;", true);
                     }
                 }
                 else
                 {

                     int result = objPlan.UpdateServiceDiscount( Convert.ToInt32(hdnDiscountID.Value),
                         Convert.ToInt32(ddlPlanMode.SelectedValue), Convert.ToInt32(txtFromCall.Text),
                        Convert.ToInt32(ddlPlanMode.SelectedValue) == 1 ? 0 : Convert.ToInt32(txtToCall.Text),
                         Convert.ToInt32(ddlStatus.SelectedValue == "true" ? "1" : "0"), Convert.ToDecimal(txtdiscount.Text), Convert.ToInt64(Session[Constant.Session.AdminSession]));
                     if (result == 1)
                     {
                         hdnDiscountID.Value = string.Empty;
                         ddlService.Enabled = true;
                         btnAdd.Text = "Add";
                         BindServiceDiscount();
                         Clear();
                         ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Service Discount updated sucessfully') ;", true);
                     }
                 }
             }
        }
        private void Clear()
        {
            ddlService.SelectedValue = "0";
            txtdiscount.Text = string.Empty;
            ddlPlanMode.SelectedValue = "0";
            ddlStatus.SelectedValue = "0";
            txtFromCall.Text = string.Empty;
            txtToCall.Text = string.Empty; 
            dvFromCall.Visible = false;
            dvToCall.Visible = false;
        }
        public bool ISDefined(int discount)
        {
            if (discount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlService.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select service plan') ;", true);
            }
            else
            {
                if (ddlPlanMode.SelectedValue == "0")
                {
                    dvFromCall.Visible = false;
                    dvToCall.Visible = false;
                }
                else if (ddlPlanMode.SelectedValue == "1")
                {
                    dvFromCall.Visible = true;
                    dvToCall.Visible = false;
                }
                else if (ddlPlanMode.SelectedValue == "2")
                {
                    dvFromCall.Visible = true;
                    dvToCall.Visible = true;
                }
            }
        }
        protected void RptService_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                HiddenField serviceplanID = (HiddenField)e.Item.FindControl("hdnID");
                BLAdmin objDelete = new BLAdmin();
                int serviceDiscountID = Convert.ToInt32(e.CommandArgument);
                int result = objDelete.DeleteDiscount(Convert.ToInt32(serviceplanID.Value), Convert.ToInt32(serviceDiscountID));
                if (result == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Deleted') ;", true);
                    BindServiceDiscount();
                }
            }
            else if (e.CommandName == "edit")
            {
                
                Label lblplan = (Label)e.Item.FindControl("lblName");
                Label lblDiscount = (Label)e.Item.FindControl("lblDiscount");
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                Label lblCalls = (Label)e.Item.FindControl("lblCallDiscount");
                btnAdd.Text = "Edit";
                HiddenField serviceID = (HiddenField)e.Item.FindControl("hdnID");                
                hdnDiscountID.Value = serviceID.Value;
                HiddenField hdnIsFixed = (HiddenField)e.Item.FindControl("hdnIsFixed");
                string plan = lblplan.Text.Substring(0, 1).ToUpper();
                ddlService.SelectedValue = plan;
                txtdiscount.Text = lblDiscount.Text;
                ddlStatus.SelectedValue = lblStatus.Text == "Active" ? "true" : "false";
                if (hdnIsFixed.Value == "1")
                {
                   
                    dvFromCall.Visible = true;
                    dvToCall.Visible = false;
                    txtFromCall.Text = lblCalls.Text;
                }
                else
                {
                    dvFromCall.Visible = true;
                    dvToCall.Visible = true;
                    txtFromCall.Text = lblCalls.Text.Substring(0, lblCalls.Text.IndexOf("-"));
                    txtToCall.Text = lblCalls.Text.Substring(lblCalls.Text.IndexOf("-") + 1);
                }
                ddlService.Enabled = false;
            }
        }

     

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }
    }
}
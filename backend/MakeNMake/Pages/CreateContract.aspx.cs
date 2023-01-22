using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MakeNMake.Pages
{
    public partial class CreateContract : System.Web.UI.Page
    {
        BLAdmin createcontract = new BLAdmin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindREquestServices(Convert.ToInt64(Utilities.EncryptDecrypt.DecryptText(Convert.ToString(Request.QueryString["RequestID"]))));
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx");
                }
            }
        }
        private void BindREquestServices(Int64 RequestID)
        {
            DataSet ds = createcontract.GetRequests(RequestID);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                hdnUserID.Value = Convert.ToString(ds.Tables[0].Rows[0]["UserID"]);
                txtfirstname.Text = Convert.ToString(ds.Tables[0].Rows[0]["firstname"]);
                txtlastname.Text = Convert.ToString(ds.Tables[0].Rows[0]["lastname"]);
                txtEmailID.Text = Convert.ToString(ds.Tables[0].Rows[0]["Emailid"]);
                txtphono.Text = Convert.ToString(ds.Tables[0].Rows[0]["MNumber"]);
                txtAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["UserAddress"]);
                lbTotalPayment.Text = Convert.ToString(ds.Tables[0].Rows[0]["totalPrice"]);
                Quotetitle.InnerText = "Create Contract from Quotation " + Convert.ToString("( " + ds.Tables[0].Rows[0]["QuotationTitle"] + " )");
            }
            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                rp_quotatioContract.DataSource = ds.Tables[1];
                rp_quotatioContract.DataBind();
            }
        }
        public string PreviousItem = "";


        protected void lb_proceedTopay_Click(object sender, EventArgs e)
        {
            bool isChecked = false;
            for (int i = 0; i < rp_quotatioContract.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)rp_quotatioContract.Items[i].FindControl("cb_service");
                if (chk.Checked)
                {
                    isChecked = true;
                    break;
                }
            }
            if (isChecked)
            {
                string servicesData = string.Empty;
                
                foreach (RepeaterItem item in rp_quotatioContract.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("cb_service");
                    if (chk.Checked)
                    {
                        decimal totalAmount = 0;
                        HiddenField hdnServceID = (HiddenField)item.FindControl("hdnServceID");
                        HiddenField hdnPlanID = (HiddenField)item.FindControl("hdnPlanID");
                        Label lblduration = (Label)item.FindControl("lblduration");
                        TextBox txtQuantity = (TextBox)item.FindControl("txtQuantity");
                        TextBox txtAmount = (TextBox)item.FindControl("txtAmount");
                        Label lblPlan = (Label)item.FindControl("lblPlan");
                        Label lbServiceType = (Label)item.FindControl("lbServiceType");
                        string plan = lblPlan.Text.Substring(0, 1).ToUpper();
                        string stype = lbServiceType.Text.Substring(0, 1).ToUpper();
                        TextBox txtdiscount = (TextBox)item.FindControl("txtdiscount");
                        decimal discount = txtdiscount.Text == "" ? 0 : Convert.ToDecimal(txtdiscount.Text);
                        int quantity = Convert.ToInt32(txtQuantity.Text == "" ? "1" : txtQuantity.Text);
                        decimal amount = Convert.ToDecimal(txtAmount.Text);
                        totalAmount += ((quantity * amount) - (((quantity * amount) * discount) / 100));
                        if (stype == "B" && plan == "U")
                        {
                            servicesData += "<data><ID>" + hdnServceID.Value + "</ID><Splan>" + plan + "</Splan><quantity>1</quantity><PlanID>" + hdnPlanID.Value +
                                "</PlanID><Amount>" + amount + "</Amount><Discount>" + discount + "</Discount><Duration>" + lblduration.Text + "</Duration><totalAmount>" + totalAmount + "</totalAmount></data>";
                        }
                        else
                        {
                            servicesData += "<data><ID>" + hdnServceID.Value + "</ID><Splan>" + plan + "</Splan><quantity>" + quantity + "</quantity><PlanID>" + hdnPlanID.Value +
                                   "</PlanID><Amount>" + amount + "</Amount><Discount>" + discount + "</Discount><Duration>" + lblduration.Text + "</Duration><totalAmount>" + totalAmount + "</totalAmount></data>";
                        }
                    }
                }
                Common objBasket = new Common();

                int result = objBasket.AddContractToBasket(Convert.ToInt64(hdnUserID.Value), Convert.ToInt64(Session[Constant.Session.AdminSession]), servicesData);
                if (result == 1)
                {
                    Response.Redirect("QutationHistroy.aspx");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Problem occurs while filling basket') ;", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select services') ;", true);
            }
        }

        protected void rp_quotatioContract_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblplans = (Label)e.Item.FindControl("lblPlan");
                Label lbServiceType = (Label)e.Item.FindControl("lbServiceType");
                string serviceType = lbServiceType.Text.Substring(0, 1).ToLower();
                
                string plan = lblplans.Text.Substring(0, 1).ToLower();
                if (plan == "u" && serviceType=="b")
                {
                    Control HeaderTemplate = rp_quotatioContract.Controls[0].Controls[0];
                    HtmlTableCell thunlimitedarea = HeaderTemplate.FindControl("thCalls") as HtmlTableCell;
                    HtmlTableCell thunlimitedcategory = HeaderTemplate.FindControl("thamount") as HtmlTableCell;
                    HtmlTableCell thduration = HeaderTemplate.FindControl("thDuration") as HtmlTableCell;

                    HtmlTableCell tdArea = (HtmlTableCell)e.Item.FindControl("tdCalls");
                    HtmlTableCell tdCategory = (HtmlTableCell)e.Item.FindControl("tdamount");
                    HtmlTableCell tdDuration = (HtmlTableCell)e.Item.FindControl("tdDuration");
                    

                    thunlimitedarea.Visible = false;
                    thunlimitedcategory.Visible = true;
                    thduration.Visible = false;

                    tdArea.Visible = false;
                    tdCategory.Visible = true;
                    tdDuration.Visible = false;
                }
                else
                {
                    Control HeaderTemplate = rp_quotatioContract.Controls[0].Controls[0];
                    HtmlTableCell thunlimitedarea = HeaderTemplate.FindControl("thCalls") as HtmlTableCell;
                    HtmlTableCell thunlimitedcategory = HeaderTemplate.FindControl("thamount") as HtmlTableCell;
                    HtmlTableCell thduration = HeaderTemplate.FindControl("thDuration") as HtmlTableCell;

                    HtmlTableCell tdArea = (HtmlTableCell)e.Item.FindControl("tdCalls");
                    HtmlTableCell tdCategory = (HtmlTableCell)e.Item.FindControl("tdamount");
                    HtmlTableCell tdDuration = (HtmlTableCell)e.Item.FindControl("tdDuration");


                    thunlimitedarea.Visible = true;
                    thunlimitedcategory.Visible = true;
                    thduration.Visible = true;

                    tdArea.Visible = true;
                    tdCategory.Visible = true;
                    tdDuration.Visible = true;

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("QutationHistroy.aspx");
        }
    }
}


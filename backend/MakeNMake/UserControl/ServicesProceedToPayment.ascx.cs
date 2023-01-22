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
    public partial class ServicesProceedToPayment : System.Web.UI.UserControl
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
            if (!IsPostBack)
            {
                try
                {
                    hdnPage.Value = Request.UrlReferrer.Segments[2].ToString();
                }
                catch
                {
                    Getinfo(null, null);

                    if (IsClient)
                    {
                        if (hdnPage.Value == "AddOnServices.aspx")
                        {
                            Response.Redirect("AddOnServices.aspx");
                        }
                        else
                        {
                            Response.Redirect("SUserServices.aspx");
                        }
                    }
                    else
                    {
                        if (hdnPage.Value == "AddOnServices.aspx")
                        {
                            Response.Redirect("AddOnServices.aspx?ClientID=" + EncryptdClientID);
                        }
                        else
                        {
                            Response.Redirect("SUserServices.aspx?ClientID=" + EncryptdClientID);
                        }
                    }
                }
            }
        }
        public void BindData(Int64 userid)
        {
            Common obj = new Common();
            DataTable dt = obj.GetServicesAfterDiscount(userid);
            if (dt != null && dt.Rows.Count > 0)
            {
                RptService.DataSource = dt;
                RptService.DataBind();
            }
            else
            {
                //   Response.Redirect("Clients.aspx");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('problem occurs') ;", true);
            }
        }
        protected void RptService_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                Int64 orderID = Convert.ToInt64(e.CommandArgument);
                Common obj = new Common();
                obj.DeleteBasketItem(orderID);
                Getinfo(null, null);
                if (RptService.Items.Count == 1)
                {
                    if (IsClient)
                    {
                        if (hdnPage.Value == "AddOnServices.aspx")
                        {
                            Response.Redirect("AddOnServices.aspx");
                        }
                        else
                        {
                            Response.Redirect("SUserServices.aspx");
                        }
                    }
                    else
                    {
                        if (hdnPage.Value == "AddOnServices.aspx")
                        {
                            Response.Redirect("AddOnServices.aspx?ClientID=" + EncryptdClientID);
                        }
                        else
                        {
                            Response.Redirect("SUserServices.aspx?ClientID=" + EncryptdClientID);
                        }
                    }
                }
                BindData(CustomerID);
            }
        }

        protected void RptService_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblTotalSaving = (Label)e.Item.FindControl("lblTotalSaving");
                totalSaving += Convert.ToDecimal(lblTotalSaving.Text);

                //Label lblAfteramunt = (Label)e.Item.FindControl("lblAfteramunt");
                HiddenField hdnOriginal = (HiddenField)e.Item.FindControl("hdnOriginal");
                totalCount += Convert.ToDecimal(hdnOriginal.Value);
                //Label lblSaving = (Label)e.Item.FindControl("lblSaving");
                //Label lblSavingHorizontal = (Label)e.Item.FindControl("lblSavingHorizontal");
                //totalSaving += Convert.ToDecimal(lblSavingHorizontal.Text) + Convert.ToDecimal(lblSaving.Text); ;
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblTotal = (Label)e.Item.FindControl("lblTotal");
                lblTotal.Text = "Proceed to Payment : Rs." + totalCount.ToString();
                Session["TotalSaving"]=Convert.ToString(totalSaving);
            }
        }

        protected void btnFinalPayment_Click(object sender, EventArgs e)
        {
            if (RptService.Items.Count > 0)
            {
                Getinfo(null, null);
                if (IsClient)
                {
                    Response.Redirect("FinalPayment.aspx?PaymentAction=" + EncryptDecrypt.Encript("0::"));
                }
                else
                {
                    Response.Redirect("FinalPayment.aspx?ClientID=" + EncryptdClientID + "&PaymentAction=" + EncryptDecrypt.Encript("0::"));
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No item in the cart') ;", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Getinfo(null, null);

            if (IsClient)
            {
                if (hdnPage.Value == "AddOnServices.aspx")
                {
                    Response.Redirect("AddOnServices.aspx");
                }
                else
                {
                    Response.Redirect("SUserServices.aspx");
                }
            }
            else
            {
                if (hdnPage.Value == "AddOnServices.aspx")
                {
                    Response.Redirect("AddOnServices.aspx?ClientID=" + EncryptdClientID);
                }
                else
                {
                    Response.Redirect("SUserServices.aspx?ClientID=" + EncryptdClientID);
                }
            }
        }

    }
}
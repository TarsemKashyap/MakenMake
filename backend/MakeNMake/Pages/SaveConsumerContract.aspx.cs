using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Pages
{
    public partial class SaveConsumerContract : System.Web.UI.Page
    {
        public decimal totalCount = 0;
        public decimal totalSaving = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData(Convert.ToInt64(Utilities.EncryptDecrypt.DecryptText(Request.QueryString.Get("ConsumerData").ToString())));
            }
        }
        public void BindData(Int64 userid)
        {
            Common obj = new Common();
            DataTable dt = obj.GetEditServicesAfterDiscount(userid);
            if (dt != null && dt.Rows.Count > 0)
            {
                RptService.DataSource = dt;
                RptService.DataBind();
            }
            else
            {
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
            }
        }

        protected void RptService_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblTotalSaving = (Label)e.Item.FindControl("lblTotalSaving");
                totalSaving += Convert.ToDecimal(lblTotalSaving.Text);
                HiddenField hdnOriginal = (HiddenField)e.Item.FindControl("hdnOriginal");
                totalCount += Convert.ToDecimal(hdnOriginal.Value);
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblTotal = (Label)e.Item.FindControl("lblTotal");
                lblTotal.Text = "Proceed to Payment : Rs." + totalCount.ToString();
                Session["TotalServiceAmount"] = Convert.ToString(totalCount);
            }
        }
        protected void btnFinalPayment_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(Session["TotalServiceAmount"]) > 0)
            {
                BLAdmin objAdmin = new BLAdmin();
                Int64 userID = Convert.ToInt64(Utilities.EncryptDecrypt.DecryptText(Request.QueryString.Get("ConsumerData").ToString()));
                int result = objAdmin.PaymentByAdmin(userID, Convert.ToInt64(Session[Constant.Session.AdminSession]), txtpaymentInfo.Text, Convert.ToDecimal(Session["TotalServiceAmount"]));
                if (result == 1)
                {
                    Common obj = new Common();
                    DataTable dt = obj.GetUserInfoByID(userID);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string gender = Convert.ToString(dt.Rows[0]["Gender"]);
                        string salutation = gender == "M" ? "Mr." : gender == "F" ? "Ms." : "";
                        string message = "Hi," + salutation + Convert.ToString(dt.Rows[0]["firstname"] + " " + dt.Rows[0]["lastname"])
                            + "!  Thanks for giving us a chance to serve you by buying our services. "
                    + "We hope for a long-term relation with us. For any queries or complaints, you have our ears at Helpline No:" +
                        ReadConfig.helpLineNumber + " or log in with your account details on our website (www.makenmake.in)/Mobile App .Please note that you can avail our services after one bussiness day of purchase.";
                        MEmail.SendGMail(Convert.ToString(dt.Rows[0]["EmailID"]), "Make n Make", message, "");
                        SendSms objSms = new SendSms();
                        try
                        {
                            int i = objSms.SendSmsOnMobile(message, Convert.ToString(dt.Rows[0]["MNumber"]));
                            if (i != 1)
                            {
                                objAdmin.AddNotSendSmsMail(Convert.ToInt64(dt.Rows[0]["UserID"]), 0, "User bought Services", 1);
                            }
                        }
                        catch (Exception ex)
                        {
                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(dt.Rows[0]["UserID"]), 0, "Error while User bought Services-Issue:-" + ex.Message, 1);
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "GoToPage();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Problem occurs') ;", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No item in the cart') ;", true);
                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditServiceContract.aspx");
        }
    }
}
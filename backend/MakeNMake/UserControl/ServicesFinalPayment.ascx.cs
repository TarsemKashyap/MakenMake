using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.UserControl
{
    public partial class ServicesFinalPayment : System.Web.UI.UserControl
    {
        public Int64 CustomerID { get; set; }
        public Int64 CreatedBy { get; set; }
        public string EncryptdClientID { get; set; }
        public bool IsClient { get; set; }
        public int status { get; set; }
        public event EventHandler Getinfo;
        public string servicePlan { get; set; }
        public string serviceType { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TotalSaving"] == null)
            {
                Getinfo(null, null);
                if (IsClient)
                {
                    Response.Redirect("ProceedToPayment.aspx");
                }
                else
                {
                    Response.Redirect("ProceedToPayment.aspx?ClientID=" + EncryptdClientID);
                }
            }
        }

        public void BindPayment(Int64 userID,int status,string splan ,string type)
        {
            Common pay = new Common();
            DataSet dt = pay.GetPayment(userID, status, splan, type);
            if (dt != null && dt.Tables[0].Rows.Count > 0)
            {
                if (dt.Tables[0].Rows.Count < 1)
                {
                    Getinfo(null, null);
                    if (IsClient)
                    {
                        Response.Redirect("ProceedToPayment.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("ProceedToPayment.aspx?ClientID=" + EncryptdClientID, false);
                    }
                }
                lblcategory.Text = Convert.ToString(dt.Tables[0].Rows[0]["category"]);
                if (lblcategory.Text == "")
                {
                    Getinfo(null, null);
                    if (IsClient)
                    {
                        Response.Redirect("ProceedToPayment.aspx");
                    }
                    else
                    {
                        Response.Redirect("ProceedToPayment.aspx?ClientID=" + EncryptdClientID);
                    }
                }

                string serviceType = Convert.ToString(dt.Tables[0].Rows[0]["Stype"]);
                serviceType = serviceType.Substring(0, serviceType.Length - 1);
                lblServiceType.Text = serviceType;
                string services = Convert.ToString(dt.Tables[0].Rows[0]["services"]);
                services = services.Substring(0, services.Length - 1);
                lblServices.Text = services;
                string plan = Convert.ToString(dt.Tables[0].Rows[0]["plans"]);
                hdnWalletMoney.Value = Convert.ToString(dt.Tables[0].Rows[0]["WalletMoney"]);
                lblwalletMoney.Text = "Rs. " + hdnWalletMoney.Value;
                lblBothWalletmoney.Text = "Rs. " + hdnWalletMoney.Value;
                hdnCurrent.Value = plan;
                if (plan.ToLower() == "u")
                {
                    lblSevicePlan.Text = "Unlimited";
                }
                else if (plan.ToLower() == "m")
                {
                    lblSevicePlan.Text = "Make your Plan";
                }
                else
                {
                    lblSevicePlan.Text = "Flexi";
                }
                lblActualtotalPayment.Text = Convert.ToDecimal(dt.Tables[0].Rows[0]["total"]).ToString("0.00");
                lblSavngs.Text = Convert.ToDecimal(Session["TotalSaving"]).ToString("0.00");
                lblTotalWithoutTax.Text = Convert.ToDecimal(dt.Tables[0].Rows[0]["totalwithOverhead"]).ToString("0.00");
                btnPay.Text = "Pay Now";// "Proceed To Payment (Rs." + lblTotalWithoutTax.Text + " )";
                Session["Payamount"] = lblTotalWithoutTax.Text;
                hdnPreviousPlan.Value = Convert.ToString(dt.Tables[0].Rows[0]["AlreadyActivePlan"]);
                if (hdnPreviousPlan.Value != "No")
                {
                    AlreadyPairServices.Visible = true;
                    lnkBtnInvoice.OnClientClick = "OpenInvoice(" + userID + ")";

                    hdnPrevious.Value = Convert.ToString(dt.Tables[1].Rows[0]["previousPlan"]);
                    lblInfo.Text = "";
                    if (Convert.ToString(dt.Tables[0].Rows[0]["AlreadyActivePlan"]) == lblSevicePlan.Text)
                    {
                        dvRemaingAmount.Visible = false;
                        hdnRemainingAmount.Value = "0.0";
                    }
                    else
                    {
                        dvRemaingAmount.Visible = true;
                        hdnRemainingAmount.Value = Convert.ToString(Convert.ToDecimal(dt.Tables[1].Rows[0]["Amount"]) - Convert.ToDecimal(dt.Tables[1].Rows[0]["TotalUsedAmount"]));
                    }
                    if (Convert.ToDecimal(hdnWalletMoney.Value) > Convert.ToDecimal(lblTotalWithoutTax.Text))
                    {
                        dvCitrus.Visible = true;
                        dvWallet.Visible = true;
                        dvWalletAndCitrus.Visible = false;
                    }
                    else if (Convert.ToDecimal(hdnWalletMoney.Value) == Convert.ToDecimal(lblTotalWithoutTax.Text))
                    {
                        dvCitrus.Visible = true;
                        dvWalletAndCitrus.Visible = true;
                    }
                    else
                    {
                        if ((Convert.ToDecimal(hdnWalletMoney.Value) < Convert.ToDecimal(lblTotalWithoutTax.Text)) && Convert.ToDecimal(hdnWalletMoney.Value) > 0)
                        {
                            dvCitrus.Visible = true;
                            dvWalletAndCitrus.Visible = true;
                            dvWallet.Visible = false;
                        }
                        else
                        {
                            dvCitrus.Visible = true;
                            dvWallet.Visible = false;
                            dvWalletAndCitrus.Visible = false;
                        }
                    }
                }
                else
                {
                    hdnRemainingAmount.Value = "0.0";
                    if (Convert.ToDecimal(hdnWalletMoney.Value) > 0)
                    {
                        if (Convert.ToDecimal(hdnWalletMoney.Value) > Convert.ToDecimal(lblTotalWithoutTax.Text))
                        {
                            dvCitrus.Visible = true;
                            dvWallet.Visible = true;
                            dvWalletAndCitrus.Visible = false;
                        }
                        else
                        {
                            dvCitrus.Visible = true;
                            dvWallet.Visible = false;
                            dvWalletAndCitrus.Visible = false;
                        }
                    }
                    else
                    {
                        dvCitrus.Visible = true;
                        dvWallet.Visible = false;
                        dvWalletAndCitrus.Visible = false;
                    }
                }
            }
            else
            {
                Getinfo(null, null);
                if (IsClient)
                {
                    Response.Redirect("ProceedToPayment.aspx");
                }
                else
                {
                    Response.Redirect("ProceedToPayment.aspx?ClientID=" + EncryptdClientID);
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Getinfo(null, null);
            if (IsClient)
            {
                Response.Redirect("ProceedToPayment.aspx");
            }
            else
            {
                Response.Redirect("ProceedToPayment.aspx?ClientID=" + EncryptdClientID);
            }
        }

        public void SubmitWalletData()
        {
            Common objPayment = new Common();
            Getinfo(null, null);
            string invoiceNumber = Convert.ToString(CustomerID + DateTime.Now.ToString("MMddyyyy"));
            bool changeplan = false;
            if (hdnPreviousPlan.Value == lblSevicePlan.Text)
            {
                changeplan = false;
            }
            else
            {
                changeplan = true;
            }
            decimal remainingAmount = Convert.ToDecimal(hdnRemainingAmount.Value);

            decimal walletMoney = Convert.ToDecimal(hdnWalletMoney.Value);

            if (Convert.ToDecimal(hdnWalletMoney.Value) > 0 &&
                (Convert.ToDecimal(hdnWalletMoney.Value) > Convert.ToDecimal(lblTotalWithoutTax.Text)))
            {
                walletMoney = Convert.ToDecimal(lblTotalWithoutTax.Text);
            }
            int result = objPayment.Payment(invoiceNumber, CustomerID, CreatedBy, 1, 1, Convert.ToDecimal(Session["Payamount"]),
                changeplan == true ? 1 : 0, remainingAmount, walletMoney, 1, 0, string.Empty, string.Empty, string.Empty, string.Empty, Convert.ToInt32(status), servicePlan, serviceType);

            if (result == 1)
            {
                Session["Payamount"] = null;

                Common obj = new Common();
                DataTable dt = obj.GetUserInfoByID(CustomerID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string gender = Convert.ToString(dt.Rows[0]["Gender"]);
                    string salutation = gender == "M" ? "Mr." : gender == "F" ? "Ms." : "";
                    string message = "Hi," + salutation + Convert.ToString(dt.Rows[0]["firstname"] + " " + dt.Rows[0]["lastname"])
                        + "!  Thanks for giving us a chance to serve you by buying our services. "
                + "We hope for a long-term relation with us. For any queries or complaints, you have our ears at Helpline No:" +
                ReadConfig.helpLineNumber + " or log in with your account details on our website (www.makenmake.in)/Mobile App .Please note that you can avail our services after one bussiness day of purchase. ";
                    MEmail.SendGMail(Convert.ToString(dt.Rows[0]["EmailID"]), "Make n Make", message, "");
                    SendSms objSms = new SendSms();
                    try
                    {
                        int i = objSms.SendSmsOnMobile(message, Convert.ToString(dt.Rows[0]["MNumber"]));
                        if (i != 1)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(dt.Rows[0]["UserID"]), 0, "User bought Services", 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(dt.Rows[0]["UserID"]), 0, "Error while User bought Services-Issue:-" + ex.Message, 1);
                    }
                    if (hdnPrevious.Value != "")
                    {
                        if (hdnCurrent.Value != hdnPrevious.Value)
                        {
                            string previous = hdnPrevious.Value.ToLower() == "m" ? "Make your Plan" : hdnPrevious.Value.ToLower() == "f" ? "Flexi" : "Unlimited";
                            string message1 = "Hi," + salutation + Convert.ToString(dt.Rows[0]["firstname"] + " " + dt.Rows[0]["lastname"])
                          + "!  You have just changed your service plan from " + previous + " to " + lblSevicePlan.Text + "  . Now you can enjoy the benefits of the new opted Plan . For any queries or complaints, you have our ears at Helpline Nos : " + ReadConfig.helpLineNumber + "  or log in with your account details on our website (www.makenmake.in)";
                            MEmail.SendGMail(Convert.ToString(dt.Rows[0]["EmailID"]), "Make n Make", message1, "");
                            SendSms objSms1 = new SendSms();
                            try
                            {
                                int i = objSms1.SendSmsOnMobile(message1, Convert.ToString(dt.Rows[0]["MNumber"]));
                                if (i != 1)
                                {
                                    BL.BLAdmin objAdmin = new BL.BLAdmin();
                                    objAdmin.AddNotSendSmsMail(Convert.ToInt64(dt.Rows[0]["UserID"]), 0, "User bought Services", 1);
                                }
                            }
                            catch (Exception ex)
                            {
                                BL.BLAdmin objAdmin = new BL.BLAdmin();
                                objAdmin.AddNotSendSmsMail(Convert.ToInt64(dt.Rows[0]["UserID"]), 0, "Error while User bought Services-Issue:-" + ex.Message, 1);
                            }
                        }
                    }
                }
                btnPay.Visible = false;
                btnBack.Visible = false;
                payment.Visible = false;


                string data = EncryptDecrypt.Encript(CustomerID + ":" + CreatedBy + ":" + EncryptdClientID + ":" + lblSevicePlan.Text + ":" + lblServiceType.Text + ":" + lblcategory.Text.Substring(0, 1) + ":" + IsClient);

                Response.Redirect("SubscriptionDetail.aspx?UserDetail=" + data);

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Payment Successfull') ;", true);
            }
            else if (result == -97)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Error Occurs, pls try again') ;", true);
            }
        }

        public void SubmitCitrusData()
        {
            int changeplan;
            if (hdnPrevious.Value == hdnCurrent.Value)
            {
                changeplan = 0;
            }
            else
            {
                changeplan = 1;
            }
            int IsMakenMakeClient = 0;
            string ID = string.Empty;
            Getinfo(null, null);
            if (IsClient)
            {
                IsMakenMakeClient = 1;
                ID = EncryptdClientID;
            }
            else
            {
                IsMakenMakeClient = 0;
            }
            string remainingAmount = string.Empty;
            if ((Convert.ToDecimal(hdnRemainingAmount.Value) > 0))
            {
                remainingAmount = hdnRemainingAmount.Value;
            }
            else
            {
                remainingAmount = "0";
            }

            string invoiceNumber = Convert.ToString(CustomerID + DateTime.Now.ToString("MMddyyyy"));
            string amount = Convert.ToString(Session["Payamount"]);


            string actionparameters = EncryptDecrypt.Encript(changeplan + ":" + IsMakenMakeClient + ":" + CustomerID + ":" + remainingAmount + ":" + CreatedBy + ":" + invoiceNumber + ":" + amount + ":" + lblSevicePlan.Text + ":" + lblServiceType.Text + ":" + lblcategory.Text.Substring(0, 1) + ":" + 2 + ":" + 0 + ":" + status + ":" + servicePlan + ":" + serviceType);
            Response.Redirect("ConfirmCitrusPaymentAction.aspx?ActionParameter=" + actionparameters);

        }

        public void SubmitBothData()
        {
            decimal walletMoney = Convert.ToDecimal(hdnWalletMoney.Value);
            decimal citusPay = Convert.ToDecimal(lblTotalWithoutTax.Text) - walletMoney;

            int changeplan;
            if (hdnPrevious.Value == hdnCurrent.Value)
            {
                changeplan = 0;
            }
            else
            {
                changeplan = 1;
            }
            int IsMakenMakeClient = 0;
            string ID = string.Empty;
            Getinfo(null, null);
            if (IsClient)
            {
                IsMakenMakeClient = 1;
                ID = EncryptdClientID;
            }
            else
            {
                IsMakenMakeClient = 0;
            }
            string remainingAmount = string.Empty;
            if ((Convert.ToDecimal(hdnRemainingAmount.Value) > 0))
            {
                remainingAmount = hdnRemainingAmount.Value;
            }
            else
            {
                remainingAmount = "0";
            }

            string invoiceNumber = Convert.ToString(CustomerID + DateTime.Now.ToString("MMddyyyy"));
            string amount = Convert.ToString(Session["Payamount"]);


            string actionparameters = EncryptDecrypt.Encript(changeplan + ":" + IsMakenMakeClient + ":" + CustomerID + ":" + remainingAmount + ":" + CreatedBy + ":" + invoiceNumber + ":" + citusPay + ":" + lblSevicePlan.Text + ":" + lblServiceType.Text + ":" + lblcategory.Text.Substring(0, 1) + ":" + 3 + ":" + walletMoney+":" + status + ":" + servicePlan + ":" + serviceType);
            Response.Redirect("ConfirmCitrusPaymentAction.aspx?ActionParameter=" + actionparameters);

        }

        protected void btnPay_Click1(object sender, EventArgs e)
        {
            if (chkTerms.Checked)
            {
                if (dvCitrus.Visible && dvWalletAndCitrus.Visible)
                {
                    if (rdbCitrus.Checked)
                    {
                        SubmitCitrusData();
                    }
                    else if (rdbWalletandCitrus.Checked)
                    {
                        SubmitBothData();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select method for payment') ;", true);
                    }
                }
                else if (dvCitrus.Visible && dvWallet.Visible)
                {
                    if (rdbCitrus.Checked)
                    {
                        SubmitCitrusData();
                    }
                    else if (rdbMakenMakeWallet.Checked)
                    {
                        SubmitWalletData();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select method for payment') ;", true);
                    }
                }
                else if (dvCitrus.Visible)
                {
                    if (rdbCitrus.Checked)
                    {
                        SubmitCitrusData();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select method for payment') ;", true);
                    }
                }
                else if (dvWallet.Visible)
                {
                    if (rdbMakenMakeWallet.Checked)
                    {
                        SubmitWalletData();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select method for payment') ;", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please read the terms and conditions mentioned by MakenMake before payment') ;", true);
            }
        }
    }
}
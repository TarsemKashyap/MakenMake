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


namespace MakeNMake.Pages
{
    public partial class PaymentResponsePageApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session[Constant.Session.AdminSession] != null)
                {
                    try
                    {
                        //string referrer = Convert.ToString(Request.UrlReferrer);
                        //if (referrer.Contains(ReadConfig.SiteUrl + Request.UrlReferrer.LocalPath.Substring(1)))
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please do not refresh the page') ;", true);
                        //}
                        //else
                        //{
                        string txnStatus = Request["TxStatus"];
                        if (txnStatus == "SUCCESS")
                        {
                            string pgTxnId = Request["pgTxnNo"];
                            string issuerRefNo = Request["issuerRefNo"];
                            string authIdCode = Request["authIdCode"];
                            string firstName = Request["firstName"];
                            string lastName = Request["lastName"];
                            string pgRespCode = Request["pgRespCode"];
                            lblName.Text = firstName + " " + lastName;
                            string zipCode = Request["addressZip"];
                            string reqSignature = Request["signature"];
                            lblamount.Text = Request["amount"];
                            lbltransactionID.Text = Request["TxId"];
                            lblPaymentStatus.Text = "Successfull";

                            string ResponseParameterFromCitrus = EncryptDecrypt.DecryptText(Convert.ToString(Request.QueryString.Get("ResponseParameter")));
                            string[] parameters = ResponseParameterFromCitrus.Split(':');
                            //parameter in squence as follow
                            //changeplan 0,IsMakenMakeClient 1,CustomerID 2,remainingAmount 3,CreatedBy 4,invoiceNumber 5,amount 6,plan 7,servicetype 8,category 9,payment method 10, wallet money 11,status 12, plan 13, type 13
                            decimal totalAmountSpent;
                            int method = Convert.ToInt32(parameters[10]);
                            if (method == 2)
                            {
                                totalAmountSpent = Convert.ToDecimal(parameters[6]);
                            }
                            else
                            {
                                totalAmountSpent = Convert.ToDecimal(parameters[6]) + Convert.ToDecimal(parameters[11]);
                            }
                            Common payment = new Common();
                            int result = payment.Payment(parameters[5], Convert.ToInt64(parameters[2]), Convert.ToInt64(parameters[4]), 3, 1,
                                totalAmountSpent, Convert.ToInt32(parameters[0]), Convert.ToDecimal(parameters[3]), Convert.ToDecimal(parameters[11]), method, 0,
                                pgTxnId, Request["TxId"], "SUCCESS", issuerRefNo, Convert.ToInt32(parameters[12]), parameters[13], parameters[14]);

                            if (result == 1)
                            {

                                Common obj = new Common();
                                DataTable dt = obj.GetUserInfoByID(Convert.ToInt64(parameters[2]));
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    string gender = Convert.ToString(dt.Rows[0]["Gender"]);
                                    string salutation = gender == "M" ? "Mr." : gender == "F" ? "Ms." : "";
                                    string message = "Hi," + salutation + Convert.ToString(dt.Rows[0]["firstname"] + " " + dt.Rows[0]["lastname"])
                                        + "!  Thanks for giving us a chance to serve you by buying our services. "
                                + "We hope for a long-term relation with us. For any queries or complaints, you have our ears at Helpline No:" +
                                ReadConfig.helpLineNumber +
                                " or log in with your account details on our website (www.makenmake.in) .Please note that you can avail our services after one bussiness day of purchase.";
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
                                }

                              //  subscriberForm.Visible = true;
                                Session["Payamount"] = null;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Payment successfully done , please fill subscription form') ;", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Error Occurs while returning to MakenMake') ;", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Error Occurs while transaction " + txnStatus + "') ;", true);
                        }
                        // }
                    }
                    catch (Exception ex)
                    {
                        //Response.Redirect("~/Error.aspx");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Error Occurs while transaction " + Request["TxStatus"] + "') ;", true);
                    }


                }
               
            }

        }

        
    }
}
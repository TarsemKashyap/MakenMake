using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.UserControl
{
    public partial class RequestForm : System.Web.UI.UserControl
    {
        public Int64 CustomerID { get; set; }
        public Int64 CreatedBy { get; set; }
        public string EncryptdClientID { get; set; }
        public bool IsClient { get; set; }
        public event EventHandler buttonClick;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string[] servicedata = Convert.ToString(Utilities.EncryptDecrypt.DecryptText(Convert.ToString(Request.QueryString["service"]))).Split(':');
                    buttonClick(sender, e);
                    BindUserData(Convert.ToInt64(CustomerID));
                    Binddata(servicedata[4].Substring(0, 1), servicedata[3].Substring(0, 1), servicedata[6].Substring(0, 1));
                }
            }
            catch
            {
                Response.Redirect("~/Error.aspx");
            }
        }
        private void BindUserData(Int64 userid)
        {
            Common obj = new Common();
            DataTable dt = obj.GetUserInfoByID(userid);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtfirstName.Text = Convert.ToString(dt.Rows[0]["firstname"]);
                txtfirstName.Enabled = false;
                txtlastName.Text = Convert.ToString(dt.Rows[0]["lastname"]);
                if (txtlastName.Text == "")
                {
                    txtlastName.Enabled = true;
                }
                else
                {
                    txtlastName.Enabled = false;
                }
                txtmobile.Text = Convert.ToString(dt.Rows[0]["MNumber"]);
                txtmobile.Enabled = false;
                txtaddress.Text = Convert.ToString(dt.Rows[0]["UserAddress"]);
                if (txtaddress.Text == "")
                {
                    txtaddress.Enabled = true;
                }
                else
                {
                    txtaddress.Enabled = false;
                }
                txtEmailID.Text = Convert.ToString(dt.Rows[0]["Emailid"]);
                txtEmailID.Enabled = false;
            }
        }
        private void Binddata(string category,string plan,string type)
        {
            Common obj = new Common();
            DataTable dt = obj.GetServicesForRequest(category,plan,type);
            if (dt != null)
            {
                RptServices.Visible = true;
                RptServices.DataSource = dt;
                RptServices.DataBind();
            }
            else
            {
                RptServices.Visible = false;
            }

        }
        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            bool checkValidate = true;
            if (txtfname1.Text == string.Empty)
            {
                //RequiredFieldValidator1.Enabled = true;
                checkValidate = false;
            }
            if (txtlname1.Text == string.Empty)
            {
                //RequiredFieldValidator10.Enabled = true;
                checkValidate = false;
            }
            if (txtmobilenumber.Text == string.Empty)
            {
                //RequiredFieldValidator12.Enabled = true;
                checkValidate = false;
            }
            if (txtaddress.Text == string.Empty)
            {
                //RequiredFieldValidator2.Enabled = true;
                checkValidate = false;
            }            
            if (txtemailid1.Text == string.Empty)
            {
               // RequiredFieldValidator13.Enabled = true;
                checkValidate = false;
            }
            if (checkValidate)
            {
                buttonClick(sender, e);
                Common objBakset = new Common();
                bool IsChecked = false;
                string planid = string.Empty;
                string serviceid = string.Empty;
                string unlimitedID = string.Empty;
                foreach (DataListItem item in RptServices.Items)
              
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        CheckBox chk = (CheckBox)item.FindControl("chkService");
                        if (chk.Checked)
                        {
                            IsChecked = true;
                            HiddenField ServiceID = (HiddenField)item.FindControl("hdnID");
                            HiddenField ServicePlanID = (HiddenField)item.FindControl("hdnPlanID");
                         //   HiddenField ServicePlanUnlimitedID = (HiddenField)item.FindControl("hdnUnlimitedID");
                            serviceid += ServiceID.Value + ",";
                            planid += ServicePlanID.Value + ",";
                         //   unlimitedID += ServicePlanUnlimitedID.Value + ",";                               
                        }
                    }
                }
                if (IsChecked)
                {
                    int result = objBakset.CommercialRequest(serviceid.Substring(0, serviceid.Length - 1), planid.Substring(0, planid.Length - 1), "", txtfirstName.Text, txtlastName.Text, CustomerID, txtmobile.Text, txtlandline.Text, txtEmailID.Text, txtaddress.Text, txttotalarea.Text, txtbuildArea.Text, Convert.ToDateTime(txtPrefferedate.Text + " " + txtPrefferedtime.Text), Convert.ToInt32(chkContactable.Checked), txtfname1.Text, txtlname1.Text, txtemailid1.Text, txtmobilenumber.Text, txtcont_address.Text);
                    if (result > 0)
                    {
                        string message = string.Empty;
                        message = "Hi , " + txtfirstName.Text + "! Thanks for Giving us a chance to serve you. Your request has been submitted . Your Request Id is " + result + ". Our representative will get back to you shortly . Please log in with your account details on our website (www.makenmake.in)/Mobile App to see the status of the Visiting Request.Or call us at Helpline Nos:"+ReadConfig.helpLineNumber;

                        SendSms objSms = new SendSms();

                        try
                        {
                            int i = objSms.SendSmsOnMobile(message, txtmobile.Text);
                            if (i != 1)
                            {
                                BL.BLAdmin objAdmin = new BL.BLAdmin();
                                objAdmin.AddNotSendSmsMail(CustomerID, 0, "visit form", 1);
                            }
                        }
                        catch (Exception ex)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(CustomerID, 0, "Error while visit form-Issue:-" + ex.Message, 1);
                        }
                        MEmail.SendGMail(txtEmailID.Text.Trim(), "Visit Request Form", message, "");
                        txtfirstName.Text = string.Empty;
                        txtlastName.Text = string.Empty;
                        txtaddress.Text = string.Empty;
                        txttotalarea.Text = string.Empty;
                        txtbuildArea.Text = string.Empty;
                        txtPrefferedate.Text = string.Empty;
                        txtPrefferedtime.Text = string.Empty;
                        btnSignUp.Visible = false;
                        txtmobile.Text = string.Empty;
                        txtfname1.Text = string.Empty;
                        txtlname1.Text = string.Empty;
                        txtmobilenumber.Text = string.Empty;
                        txtmobile.Text = string.Empty;
                        txtEmailID.Text = string.Empty;
                        txtlandline.Text = string.Empty;
                        txtemailid1.Text = string.Empty;
                        txtcont_address.Text = string.Empty;
                        chkContactable.Checked = false;
                        if (IsClient)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "PageReload(1," + result + ")", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "PageReload(0," + result + ")", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select services') ;", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please enter necessary fields') ;", true);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            buttonClick(sender, e);
            if (IsClient)
            {
                Response.Redirect("DashBoard.aspx");
            }
            else
            {
                Response.Redirect("Clients.aspx");
            }
        }

        protected void chkContactable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkContactable.Checked)
            {
                if (txtfirstName.Text == string.Empty)
                {
                    RequiredFieldValidator3.Enabled = true;
                }
                else
                {
                    txtfname1.Text = txtfirstName.Text;
                }
                if (txtlastName.Text == string.Empty)
                {
                    RequiredFieldValidator4.Enabled = true;
                }
                else
                {
                    txtlname1.Text = txtlastName.Text;
                }
                if (txtmobile.Text == string.Empty)
                {
                    RequiredFieldValidator7.Enabled = true;
                }
                else
                {
                    txtmobilenumber.Text = txtmobile.Text;
                }



                if (txtaddress.Text == string.Empty)
                {
                   // RequiredFieldValidator2.Enabled = true;
                }
                else
                {
                    txtcont_address.Text = txtaddress.Text;
                }



                if (txtEmailID.Text == string.Empty)
                {
                    RequiredFieldValidator6.Enabled = true;
                }
                else
                {
                    txtemailid1.Text = txtEmailID.Text;
                }
                txtfname1.Enabled = false;
                txtlname1.Enabled = false;
                txtmobilenumber.Enabled = false;
                txtcont_address.Enabled = false;
                txtemailid1.Enabled = false;
                txtfname1.CssClass = "form-control normalinput";
                txtlname1.CssClass = "form-control normalinput";
                txtmobilenumber.CssClass = "form-control normalinput";
                txtcont_address.CssClass = "form-control normalinput";
                txtemailid1.CssClass = "form-control normalinput";
            }
            else
            {
                txtfname1.Text = string.Empty;
                txtlname1.Text = string.Empty;
                txtmobilenumber.Text = string.Empty;
                txtcont_address.Text = string.Empty;
                txtemailid1.Text = string.Empty;
                txtfname1.Enabled = true;
                txtlname1.Enabled = true;
                txtmobilenumber.Enabled = true;
                txtemailid1.Enabled = true;
                txtcont_address.Enabled = true;

              
            }
        }

    }
}
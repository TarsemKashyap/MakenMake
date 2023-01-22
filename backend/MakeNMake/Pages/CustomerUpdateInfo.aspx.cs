using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MakeNMake.BL;

namespace MakeNMake.Pages
{
    public partial class CustomerUpdateInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[Constant.Session.AdminSession] == null)
                {
                    Response.Redirect("~/SignUp.aspx");
                }
                BindCountry();
                string clientID = Convert.ToString(Request.QueryString[Constant.QueryString.ClientID]);
                clientID = EncryptDecrypt.DecryptText(clientID);
                BindName(Convert.ToInt64(clientID));
                bindalternateno(Convert.ToInt64(clientID));

            }
        }
        public void bindalternateno(Int64 userid)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            DataTable dt = obj.GetUseralternateno(userid);
            if (dt.Rows.Count > 0)
            {
                int rowcount = dt.Rows.Count;
                if (rowcount > 0)
                {
                    txtAltMob1.Text = Convert.ToString(dt.Rows[0]["ContactNumber"]);
                }
                if (rowcount > 1)
                {
                    txtAltMob2.Text = Convert.ToString(dt.Rows[1]["ContactNumber"]);
                }
                if (rowcount > 2)
                {
                    txtAltMob3.Text = Convert.ToString(dt.Rows[2]["ContactNumber"]);
                }
                if (rowcount > 3)
                {
                    txtAltMob4.Text = Convert.ToString(dt.Rows[3]["ContactNumber"]);
                }
            }

        }
        private void BindName(Int64 userID)
        {
            Common objCommon = new Common();
            DataTable dt = objCommon.GetCustomerByID(userID);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblName.Text = Convert.ToString(dt.Rows[0]["name"]);
                lblemailid.Text = Convert.ToString(dt.Rows[0]["Emailid"]);
                txtMobileNumber.Text = Convert.ToString(dt.Rows[0]["MNumber"]);
                //    ddlGender.SelectedValue = Convert.ToString(dt.Rows[0]["Gender"]);
                txtDob.Text = Convert.ToString(dt.Rows[0]["DOB"]);
                // txtaddress.Text = Convert.ToString(dt.Rows[0]["UserAddress"]);
                string[] values = Convert.ToString(dt.Rows[0]["UserAddress"]).Split('+');
                if (values.Length > 0)
                {
                    txtaddresslocality.Text = values[0].Trim();
                    if (values.Length > 1)
                        {
                        txtstreet.Text = values[1].Trim();
                    }
                }
                else
                {
                    txtaddresslocality.Text = Convert.ToString(dt.Rows[0]["UserAddress"]);
                }
                ddlCountry.SelectedValue = Convert.ToString(dt.Rows[0]["UserCountry"]);
                BindState(Convert.ToInt32(ddlCountry.SelectedValue));
                ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["UserState"]);
                BindDistrict(Convert.ToInt64(ddlState.SelectedValue));
                ddlDistrict.SelectedValue = Convert.ToString(dt.Rows[0]["UserDistrict"]);
                BindCity(Convert.ToInt64(ddlDistrict.SelectedValue));
                ddlCity.SelectedValue = Convert.ToString(dt.Rows[0]["UserCity"]);
            }
        }
        protected void btnUPdateInfo_Click(object sender, EventArgs e)
        {
            string clientID = Convert.ToString(Request.QueryString[Constant.QueryString.ClientID]);
            clientID = EncryptDecrypt.DecryptText(clientID);
            BL.BLCustomerCare objClient = new BL.BLCustomerCare();
            MakeNMake.BL.Common obj = new BL.Common();
            string address = txtaddresslocality.Text + "+" + txtstreet.Text;
            int result = obj.UpdateUserInfo(txtMobileNumber.Text, address, Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToDateTime(txtDob.Text), Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt64(ddlDistrict.SelectedValue), Convert.ToInt64(ddlCity.SelectedValue), "", Convert.ToInt64(clientID), Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if (result > 0)
            {
                try
                {
                    BLAdmin addUser = new BLAdmin();
                    var resultphonenoexists = addUser.checkphonenoexistsinMobileno(txtAltMob1.Text + "," + txtAltMob2.Text + "," + txtAltMob3.Text + "," + txtAltMob4.Text);
                    if (resultphonenoexists.Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Any Alternate Mobile no  is already exists in users  primary no') ;", true);
                    }
                    else
                    {
                        string alternatemobileno1 = "";
                        string alternatemobileno2 = "";
                        string alternatemobileno3 = "";
                        string alternatemobileno4 = "";
                      
                            alternatemobileno1 = txtAltMob1.Text;
                            alternatemobileno2 = txtAltMob2.Text;
                            alternatemobileno3 = txtAltMob3.Text;
                            alternatemobileno4 = txtAltMob4.Text;
                            Common objc = new Common();
                            int resultaltno = objc.UpdateUserAlternateno(alternatemobileno1, alternatemobileno2, alternatemobileno3, alternatemobileno4, Convert.ToInt64(clientID));
                        
                    }
                    
                    string message = "Hi ," + lblName.Text + "! Your details have been updated as per your request. For any queries or complaints, you have" +
                    "our ears at Helpline No :" + ReadConfig.helpLineNumber + "or log in with your account details on our website (www.makenmake.in)/Mobile App";
                    MEmail.SendGMail(lblemailid.Text,"Make n Make Profile Updated", message, "");
                    SendSms objSms = new SendSms();
                    try
                    {
                    int i = objSms.SendSmsOnMobile(message, txtMobileNumber.Text);
                        if (i != 1)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(clientID), 0, "Updating Client Profile By Care", 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(clientID), 0, "Error while Updating Client Profile By Care-Issue:-" + ex.Message, 1);
                    }
                    Response.Redirect("Clients.aspx");
                }
                catch
                {
                    info.Visible = false;
                    bookAppoinment.Visible = true;
                }
            }
        }
        private void BindCountry()
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetCountry(ddlCountry);
        }
        private void BindState(int countryID)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetStatesByCountryID(ddlState, countryID);
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue != "0")
            {
                ddlState.Items.Clear();
                BindState(Convert.ToInt32(ddlCountry.SelectedValue));
            }
            else
            {
                ddlState.Items.Clear();
                ddlCity.Items.Clear();
            }
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue != "0")
            {
                ddlDistrict.Items.Clear();
                ddlCity.Items.Clear();
                BindDistrict(Convert.ToInt64(ddlState.SelectedValue));
            }
            else
            {
                ddlDistrict.Items.Clear();
                ddlCity.Items.Clear();
            }
        }
        private void BindDistrict(Int64 StateID)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetDistrictsByID(ddlDistrict, StateID);
        }
        private void BindCity(Int64 districtID)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetCityByDistrictID(ddlCity, districtID);
        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDistrict.SelectedValue != "0")
            {
                ddlCity.Items.Clear();
                BindCity(Convert.ToInt64(ddlDistrict.SelectedValue));
            }
            else
            {
                ddlCity.Items.Clear();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BL.BLCustomerCare objClient = new BL.BLCustomerCare();
            string clientID = Convert.ToString(Request.QueryString[Constant.QueryString.ClientID]);
            clientID = EncryptDecrypt.DecryptText(clientID);
            int result = objClient.AddAppoinment(Convert.ToInt64(clientID), Convert.ToInt64(Session[Constant.Session.AdminSession]), txtdate.Text, txttime.Text, 0);
            if (result == -99)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Client Address is not complete, please update it before appointment') ;", true);
            }
            else
            {
                string engineerName = string.Empty;

                BL.BLCustomerCare care = new BL.BLCustomerCare();
                DataTable dtengineer = care.GetEngineerByAppointmentD(Convert.ToInt64(result));


                string message = "Hi," + lblName.Text + "! Your Appointment ticket Id: " + result + " has been submitted. We will serve you shortly. Please log in with your account details on our website (www.makenmake.in)/Mobile App to see the status of the ticket.Or call us at Helpline Nos:"+ReadConfig.helpLineNumber;
                MEmail.SendGMail(lblemailid.Text, "New Appointment Ticket Make 'N' Make", message, "");
                SendSms objSms = new SendSms();
                try
                {
                    int i = objSms.SendSmsOnMobile(message, txtMobileNumber.Text);
                    if (i != 1)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(clientID), 0, "Appointment Status to Client", 1);
                    }
                }
                catch (Exception ex)
                {
                    BL.BLAdmin objAdmin = new BL.BLAdmin();
                    objAdmin.AddNotSendSmsMail(Convert.ToInt64(clientID), 0, "Error while Appointment Status to Client-Issue:-" + ex.Message, 1);
                }

                if (dtengineer != null && dtengineer.Rows.Count > 0)
                {
                    engineerName = Convert.ToString(dtengineer.Rows[0]["name"]);

                    if (!string.IsNullOrEmpty(Convert.ToString(dtengineer.Rows[0]["MobileNumber"])))
                    {
                        message = "Hi," + engineerName + "! A new Appointment ticket Id: " + result + " has been assigned to you. Please login to see the details.";
                        MEmail.SendGMail(Convert.ToString(dtengineer.Rows[0]["EmailID"]), "New Appointment Ticket Make 'N' Make", message, "");

                        objSms = new SendSms();
                        try
                        {
                            int i = objSms.SendSmsOnMobile(message, Convert.ToString(dtengineer.Rows[0]["MobileNumber"]));
                            if (i != 1)
                            {
                                BL.BLAdmin objAdmin = new BL.BLAdmin();
                                objAdmin.AddNotSendSmsMail(Convert.ToInt64(dtengineer.Rows[0]["UserID"]), 0, "Appointment Status to Engineer", 1);
                            }
                        }
                        catch (Exception ex)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(dtengineer.Rows[0]["UserID"]), 0, "Error while Appointment Status to Engineer-Issue:-" + ex.Message, 1);
                        }
                    }

                    Response.Redirect("Clients.aspx");
                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Appointment booked, our service engineer will contact you as soon as posible') ;", true);
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Clients.aspx");
        }
    }
}
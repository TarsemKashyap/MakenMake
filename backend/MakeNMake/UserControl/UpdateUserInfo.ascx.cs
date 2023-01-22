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
    public partial class UpdateUserInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCountry();
            }
        }

        protected void btnUPdateInfo_Click(object sender, EventArgs e)
        {
            Int64 userID = 0;
            userID = Convert.ToInt64(Session[Constant.Session.AdminSession]);
            MakeNMake.BL.Common obj = new BL.Common();
            int result = obj.UpdateUserInfo(txtMobileNumber.Text, txtaddress.Text, Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToDateTime(txtDob.Text), Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt64(ddlCity.SelectedValue), ddlGender.SelectedValue, userID, Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if (result > 0)
            {
                string salutation = ddlGender.SelectedValue == "M" ? "Mr." : "Ms.";
                string message = "Hi , User ! Your  details have been updated for very first time. For any queries or complaints, you have our ears at Helpline No :" +
                       ReadConfig.helpLineNumber + "or log in with your account details on our website (www.makenmake.in)";
                Common objSend = new Common();
                DataTable dt = objSend.GetUserInfoByID(userID);
                string firstname = Convert.ToString(dt.Rows[0]["firstname"]);
                string emailid = Convert.ToString(dt.Rows[0]["Emailid"]);
                string mobile = Convert.ToString(dt.Rows[0]["MNumber"]);
                MEmail.SendGMail(emailid, "Make n Make Profile Updated", message, "");
                SendSms objSms = new SendSms();
                try
                {
                    int i = objSms.SendSmsOnMobile(message, txtMobileNumber.Text);
                    if (i != 1)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(userID, 0, "Updating firsttime Profile ", 1);
                    }
                }
                catch (Exception ex)
                {
                    BL.BLAdmin objAdmin = new BL.BLAdmin();
                    objAdmin.AddNotSendSmsMail(userID, 0, "Error while Updating firsttime Profile -Issue:-" + ex.Message, 1);
                }
                Response.Redirect("DashBoard.aspx");
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
                ddlDistrict.Items.Clear();
                ddlCity.Items.Clear();
                BindState(Convert.ToInt32(ddlCountry.SelectedValue));
            }
            else
            {
                ddlState.Items.Clear();
                ddlDistrict.Items.Clear();
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
    }
}
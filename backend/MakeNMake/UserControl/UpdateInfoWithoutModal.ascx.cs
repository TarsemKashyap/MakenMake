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
    public partial class UpdateInfoWithoutModal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Int64 userID = 0;
                userID = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                BindCountry(); bindData(userID);
            }
        }

        protected void btnUPdateInfo_Click(object sender, EventArgs e)
        {
            MakeNMake.BL.Common obj = new BL.Common();
            Int64 userID = 0;
            userID = Convert.ToInt64(Session[Constant.Session.AdminSession]);
            int result = obj.UpdateUserInfo(txtMobileNumber.Text, txtaddress.Text, Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToDateTime(txtDob.Text), Convert.ToInt32(ddlState.SelectedValue),Convert.ToInt64(ddlDistrict.SelectedValue) ,Convert.ToInt64(ddlCity.SelectedValue), ddlGender.SelectedValue, userID,Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if (result > 0)
            {
                string message = string.Empty;
                    message = "Hi , User ! Your  details have been updated as per your request. For any queries or complaints, you have our ears at Helpline No :" +
                        ReadConfig.helpLineNumber + "or log in with your account details on our website (www.makenmake.in)";
               
                SendSms objSms = new SendSms();
                try
                {
                    int i = objSms.SendSmsOnMobile(message, txtMobileNumber.Text);
                    if (i != 1)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(userID, 0, "Updating Client Profile By Client", 1);
                    }
                }
                catch(Exception ex)
                {
                    BL.BLAdmin objAdmin = new BL.BLAdmin();
                    objAdmin.AddNotSendSmsMail(userID, 0, "Error while Updating Client Profile By Client -Issue:-"+ex.Message, 1);
                }
              ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Updated successfully');", true);
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
        private void BindState()
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetStates(ddlState);
        }
        private void BindDistrict()
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetDistricts(ddlDistrict);
        }
        private void BindCities()
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetCites(ddlCity);
        }
        private void bindData(Int64 UserID)
        {
            Common obj = new Common();
            DataTable dt = obj.GetUserInfoByID(UserID);
            if (dt != null && dt.Rows.Count > 0)
            {
                try
                {
                    txtMobileNumber.Text = Convert.ToString(dt.Rows[0]["MNumber"]);
                    txtDob.Text = Convert.ToString(Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("MM/dd/yyyy"));
                    txtaddress.Text = Convert.ToString(dt.Rows[0]["UserAddress"]);
                    ddlGender.SelectedValue = Convert.ToString(dt.Rows[0]["Gender"]);
                    ddlCountry.SelectedValue = Convert.ToString(dt.Rows[0]["UserCountry"]);
                    BindState(Convert.ToInt32(ddlCountry.SelectedValue));
                    ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["UserState"]);
                    BindDistrict(Convert.ToInt64(ddlState.SelectedValue));
                    ddlDistrict.SelectedValue = Convert.ToString(dt.Rows[0]["UserDistrict"]);
                    BindCity(Convert.ToInt64(ddlDistrict.SelectedValue));
                    ddlCity.SelectedValue = Convert.ToString(dt.Rows[0]["UserCity"]);
                }
                catch(Exception ex)
                {
                }
            }
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
                ddlDistrict.Items.Clear();
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Request.RawUrl.ToLower() == "/customer/updateinfo.aspx")
            {
                Response.Redirect("CustomerDashBoard.aspx");
            }
            else
            {
                Response.Redirect("DashBoard.aspx");
            }
        }
    }
}
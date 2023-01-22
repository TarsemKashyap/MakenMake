using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Globalization;
using System.Web.UI.WebControls;

namespace MakeNMake.Admin
{
    public partial class UpdateInfo : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Int64 userID = 0;
                string user = Convert.ToString(Request.QueryString["EngineerID"]);
                if (string.IsNullOrEmpty(user))
                {
                    userID = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                }
                else
                {
                    userID = Convert.ToInt64(Utilities.EncryptDecrypt.DecryptText(user));
                }
                BindCountry(); bindData(userID);
                bindalternateno(userID);
            }
        }

        protected void btnUPdateInfo_Click(object sender, EventArgs e)
        {
            MakeNMake.BL.Common obj = new BL.Common();
            Int64 userID = 0;
            string user = Convert.ToString(Request.QueryString["EngineerID"]);
            if (string.IsNullOrEmpty(user))
            {
                userID = Convert.ToInt64(Session[Constant.Session.AdminSession]);
            }
            else
            {
                userID = Convert.ToInt64(Utilities.EncryptDecrypt.DecryptText(user));
            }
            string alternatemobileno1 = "";
            string alternatemobileno2 = "";
            string alternatemobileno3 = "";
            string alternatemobileno4 = "";
            BLAdmin addUser = new BLAdmin();
            // string dob = Convert.ToDateTime(txtDob.Text).ToString("MM/dd/yyyy");
            string dob= DateTime.ParseExact(txtDob.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                         .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string address = txtaddresslocality.Text + "+" + txtstreet.Text;
            int result = obj.UpdateUserInfo(txtMobileNumber.Text, address, Convert.ToInt32(ddlCountry.SelectedValue),
                                                             Convert.ToDateTime(dob), Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt64(ddlDistrict.SelectedValue),
                                                             Convert.ToInt64(ddlCity.SelectedValue), ddlGender.SelectedValue, userID, Convert.ToInt64(Session[Constant.Session.AdminSession]));


                           if (result > 0)
                           {
                              
                                   alternatemobileno1 = txtAltMob1.Text;
                                       alternatemobileno2 = txtAltMob2.Text;
                                           alternatemobileno3 = txtAltMob3.Text;
                                               alternatemobileno4 = txtAltMob4.Text;
                                               int resultaltno = obj.UpdateUserAlternateno(alternatemobileno1, alternatemobileno2, alternatemobileno3, alternatemobileno4, userID);


                                           
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
                    txtname.Text = Convert.ToString(dt.Rows[0]["firstname"]) + " " + Convert.ToString(dt.Rows[0]["lastname"]);
                    txtEmailid.Text = Convert.ToString(dt.Rows[0]["Emailid"]);
                    txtEmailid.Enabled = false;
                    txtname.Enabled = false;
                    txtMobileNumber.Text = Convert.ToString(dt.Rows[0]["MNumber"]);
                    txtDob.Text = Convert.ToString(Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd/MM/yyyy"));
                    string[] values =Convert.ToString(dt.Rows[0]["UserAddress"]).Split('+');
                    if (values.Length > 0)
                    {
                        txtaddresslocality.Text = values[0].Trim();
                        if (values.Length == 1)
                        {

                            txtstreet.Text = values[1].Trim();
                        }
                    }
                    else
                    {
                        txtaddresslocality.Text = Convert.ToString(dt.Rows[0]["UserAddress"]);
                    }
                   // txtaddress.Text = Convert.ToString(dt.Rows[0]["UserAddress"]);
                    ddlGender.SelectedValue = Convert.ToString(dt.Rows[0]["Gender"]);
                    ddlCountry.SelectedValue = Convert.ToString(dt.Rows[0]["UserCountry"]);
                    BindState(Convert.ToInt32(ddlCountry.SelectedValue));
                    ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["UserState"]);
                    BindDistrict(Convert.ToInt64(ddlState.SelectedValue));
                    ddlDistrict.SelectedValue = Convert.ToString(dt.Rows[0]["UserDistrict"]);
                    BindCity(Convert.ToInt64(ddlDistrict.SelectedValue));
                    ddlCity.SelectedValue = Convert.ToString(dt.Rows[0]["UserCity"]);
                }
                catch (Exception ex)
                {
                }
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
            int roleID = Convert.ToInt32(Session[Constant.Session.Role]);
            if (roleID == 1)
            {
                Response.Redirect("EngineerSkills.aspx");
            }
            else 
            {
                Response.Redirect("DashBoard.aspx");
            }
        }
    }
}
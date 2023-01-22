using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using MakeNMake.Utilities;

namespace MakeNMake.UserControl
{
    public partial class SubscriptionForm : System.Web.UI.UserControl
    {

        public Int64 CustomerID { get; set; }
        public Int64 CreatedByID { get; set; }
        public string EncryptdClientID { get; set; }
        public string Plan { get; set; }
        public string SType { get; set; }
        public string Category { get; set; }
        public bool isClient { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string[] userdetails = EncryptDecrypt.DecryptText(Convert.ToString(Request.QueryString.Get("UserDetail"))).Split(':');
                    CustomerID = Convert.ToInt64(userdetails[0]);
                    CreatedByID = Convert.ToInt64(userdetails[1]);
                    EncryptdClientID = userdetails[2];
                    Plan = userdetails[3];
                    SType = userdetails[4];
                    Category = userdetails[5];
                    if (Convert.ToInt32(Session[Constant.Session.Role]) == 4)
                    {
                        isClient = true;
                    }
                    else
                    {
                        isClient = false;
                    }
                    BindCountry();
                    getinfo(CustomerID);
                    lnkBtnContract.OnClientClick = "OpenInvoice(" + CustomerID + ")";
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx");
                }
            }
        }

        protected void getinfo(Int64 userID)
        {
            MakeNMake.BL.Common obj = new BL.Common();
            DataTable dt = obj.GetSubscriberInfo(userID);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblServicePlan.Text = Plan;
                ddlcategory.SelectedValue = Category.ToUpper();
                txtServiceType.Text = SType;
                txtServiceType.Enabled = false;
                lblclientid.Text = dt.Rows[0]["UserID"].ToString();
                ddlcategory.Enabled = false;
                lblServicePlan.Enabled = false;
                lblfname.Text = dt.Rows[0]["FirstName"].ToString();
                lbllname.Text = dt.Rows[0]["LastName"].ToString();
                lblemailid.Text = dt.Rows[0]["EmailID"].ToString();
                lblclientid.Text = dt.Rows[0]["UserID"].ToString();
                lblfname.Text = dt.Rows[0]["FirstName"].ToString();
                lbllname.Text = dt.Rows[0]["LastName"].ToString();
                lblemailid.Text = dt.Rows[0]["EmailID"].ToString();
               // string dob = Convert.ToDateTime(txtDob.Text).ToString("MM/dd/yyyy");
                txtDob.Text = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("MM/dd/yyyy");
                //if (dt.Rows[0]["DOB"].ToString() != "")
                //{
                //    string dob = (dt.Rows[0]["DOB"].ToString()).Substring(0, 10);
                //    txtDob.Text = Convert.ToDateTime(dob).ToString("MM/dd/yyyy");
                //}
                //else
                //{
                //    txtDob.Text = string.Empty;
                //}
                lblmobile.Text = dt.Rows[0]["MobileNumber"].ToString();
                string country = dt.Rows[0]["CountryName"].ToString();
                if (!String.IsNullOrEmpty(country))
                {
                    if (country != "")
                    {
                        ddlCountry0.SelectedValue = ddlCountry0.Items.FindByText(country).Value;
                        ddlCountry0_SelectedIndexChanged(this, EventArgs.Empty);
                    }
                    string state = dt.Rows[0]["StateName"].ToString();
                    if (state != "" && country != "")
                    {
                        ddlState0.SelectedValue = ddlState0.Items.FindByText(state).Value;
                        ddlState0_SelectedIndexChanged(this, EventArgs.Empty);
                    }
                    string district = dt.Rows[0]["DistrictName"].ToString();
                    if (district != "" && state != "")
                    {
                        ddlDistrict0.SelectedValue = ddlDistrict0.Items.FindByText(district).Value;
                        ddlDistrict0_SelectedIndexChanged(this, EventArgs.Empty);
                    }                    
                    string city = dt.Rows[0]["CityName"].ToString();
                    if (city != "" && district != "")
                    {
                        try
                        {
                            ddlCity0.SelectedValue = ddlCity0.Items.FindByText(city).Value;
                        }
                        catch
                        {
                            ddlCity0.SelectedValue = "0";
                        }
                    }
                }
                txtaddress0.Text = dt.Rows[0]["CurrentAddress"].ToString();
                rdsms.SelectedValue = "1"; 
            }
        }

        protected void btnUPdateInfo_Click(object sender, EventArgs e)
        {
            MakeNMake.BL.Common obj = new BL.Common();
            string dob = Convert.ToDateTime(txtDob.Text).ToString("MM/dd/yyyy");
            int result = obj.Updatesubscriberinfo(Convert.ToDateTime(dob), txtmob2.Text, txtemail2.Text, txtabout.Text, txtnear.Text, Convert.ToInt32(rdsms.SelectedValue), txtaddress.Text, Convert.ToString(ddlcategory.SelectedValue), lblServicePlan.Text, txtaddress0.Text, Convert.ToInt32(ddlCountry0.SelectedValue), Convert.ToInt32(ddlState0.SelectedValue), Convert.ToInt32(ddlDistrict0.SelectedValue), Convert.ToInt32(ddlCity0.SelectedValue), Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlCity.SelectedValue), Convert.ToInt64(CustomerID), CreatedByID);
            if (result > 0)
            {
                //   Session["TotalSaving"] = null;
                btnUPdateInfo.Visible = false;
                if (Convert.ToInt32(Session[Constant.Session.Role]) == 4)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "PageReload(1)", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "PageReload(0)", true);
                }
            }
        }
        private void BindCountry()
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetCountry(ddlCountry);
            obj.GetCountry(ddlCountry0);
        }
        private void BindState(int countryID)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetStatesByCountryID(ddlState, countryID);
        }

        private void BindState0(int countryID)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetStatesByCountryID(ddlState0, countryID);
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
        private void BindCity0(Int64 districtID)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetCityByDistrictID(ddlCity0, districtID);
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

        protected void chkContactable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkContactable.Checked)
            {
                if (txtaddress0.Text == string.Empty)
                {
                    RequiredFieldValidator4.Enabled = true;
                    return;
                }
                else
                {
                    txtaddress.Text = txtaddress0.Text;
                }
                if (ddlCountry0.SelectedValue == "0")
                {
                    RequiredFieldValidator12.Enabled = true;
                    return;
                }
                else
                {
                    //txtlname1.Text = txtlastName.Text;
                    ddlCountry.SelectedValue = ddlCountry0.SelectedValue;
                    ddlCountry_SelectedIndexChanged(this, EventArgs.Empty);
                }
                if (ddlState0.SelectedValue == "0")
                {
                    RequiredFieldValidator13.Enabled = true;
                    return;
                }
                else
                {
                    ddlState.SelectedValue = ddlState0.SelectedValue;
                    ddlState_SelectedIndexChanged(this, EventArgs.Empty);
                }
                if (ddlDistrict0.SelectedValue == "0")
                {
                    RequiredFieldValidator14.Enabled = true;
                    return;
                }
                else
                {
                    ddlDistrict.SelectedValue = ddlDistrict0.SelectedValue;
                    ddlDistrict_SelectedIndexChanged(this, EventArgs.Empty);
                }
                if (ddlCity0.SelectedValue == "0")
                {
                    RequiredFieldValidator15.Enabled = true;
                    return;
                }
                else
                {
                    ddlCity.SelectedValue = ddlCity0.SelectedValue;
                }
            }
            else
            {
                txtaddress.Text = string.Empty;
                ddlCountry.SelectedValue = "0";
                ddlState.SelectedValue = "0";
                ddlDistrict.SelectedValue = "0";
                ddlCity.SelectedValue = "0";
            }
        }
       
        protected void ddlCountry0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry0.SelectedValue != "0")
            {
                ddlState0.Items.Clear();
                ddlDistrict0.Items.Clear();
                ddlCity0.Items.Clear();
                BindState0(Convert.ToInt32(ddlCountry0.SelectedValue));
            }
            else
            {
                ddlState0.Items.Clear();
                ddlDistrict0.Items.Clear();
                ddlCity0.Items.Clear();
            }
        }
        private void BindDistrict0(Int64 StateID)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetDistrictsByID(ddlDistrict0, StateID);
        }
        protected void ddlState0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState0.SelectedValue != "0")
            {
                ddlDistrict0.Items.Clear();
                ddlCity0.Items.Clear();
                BindDistrict0(Convert.ToInt64(ddlState0.SelectedValue));
            }
            else
            {
                ddlDistrict0.Items.Clear();
                ddlCity0.Items.Clear();
            }
        }

        protected void ddlDistrict0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDistrict0.SelectedValue != "0")
            {
                ddlCity0.Items.Clear();
                BindCity0(Convert.ToInt64(ddlDistrict0.SelectedValue));
            }
            else
            {
                ddlCity0.Items.Clear();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (isClient)
            {
                Response.Redirect("ProceedToPayment.aspx");
            }
            else
            {
                Response.Redirect("ProceedToPayment.aspx?ClientID=" + EncryptdClientID);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using NLog;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MakeNMake.BL;
using MakeNMake.CommomFunctions;

namespace MakeNMake.Admin
{
    public partial class CityArea : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCountry();
            }
        }
        private void BindCountry()
        {
            BLAdmin bl = new BLAdmin();
            bl.GetCountry(ddlCountry);
        }
        protected void btncity_Click(object sender, EventArgs e)
        {
            try
            {
                BLAdmin addcity = new BLAdmin();
                int result = addcity.Addcity(Convert.ToInt64(ddlDistrict.SelectedItem.Value), Txtcity.Text,txtcode.Text,Convert.ToInt64(Session[Constant.Session.AdminSession]),Convert.ToInt64(Session[Constant.Session.AdminSession]));
                if (result == -99)
                {
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('This City already exists') ;", true);
                }
                else if (result > 0)
                {
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Added') ;", true);
                }
                else
                {
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Some fatal error occurs') ;", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error(logger.Name + ":" + ex.Message);
            }
        }

        private void Clear()
        {
            ddlCountry.SelectedValue = "0";
            ddlDistrict.Items.Clear();
            ddlstate.Items.Clear();
            Txtcity.Text = string.Empty;
            txtcode.Text = string.Empty;
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("CityList.aspx");
        }

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlstate.SelectedValue == "0")
            {
                ddlDistrict.Items.Clear();
            }
            else
            {
                ddlDistrict.Items.Clear();
                BindDistrict(Convert.ToInt64(ddlstate.SelectedValue));
            }
        }
        private void BindDistrict(Int64 stateID)
        {
          BLAdmin admin=new BLAdmin();
          admin.GetDistrictsByID(ddlDistrict, stateID);
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue != "0")
            {
                ddlstate.Items.Clear();
                BLAdmin bl = new BLAdmin();
                bl.GetStatesByCountryID(ddlstate, Convert.ToInt32(ddlCountry.SelectedValue));
            }
            else
            {
                ddlstate.Items.Clear();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }
    }
}
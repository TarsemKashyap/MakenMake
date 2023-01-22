using MakeNMake.BL;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MakeNMake.CommomFunctions;

namespace MakeNMake.Admin
{
    public partial class District : System.Web.UI.Page
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
                int result = addcity.AddDistrict(Convert.ToInt64(ddlstate.SelectedItem.Value), txtDistrict.Text,Convert.ToInt64(Session[Constant.Session.AdminSession]),Convert.ToInt64(Session[Constant.Session.AdminSession]));
                if (result == -99)
                {
                    ddlCountry.SelectedValue = "0";
                    ddlstate.Items.Clear();
                    txtDistrict.Text = string.Empty;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('This District already exists for this state') ;", true);
                }
                else if (result > 0)
                {
                    ddlCountry.SelectedValue = "0";
                    txtDistrict.Text = string.Empty;
                    ddlstate.Items.Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Added') ;", true);
                }
                else
                {
                    ddlCountry.SelectedValue = "0";
                    ddlstate.Items.Clear();
                    txtDistrict.Text = string.Empty;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Some fatal error occurs') ;", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error(logger.Name + ":" + ex.Message);
            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("DistrictList.aspx");
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
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MakeNMake.BL;
using MakeNMake.CommomFunctions;

namespace MakeNMake.Admin
{
    public partial class CountryState : System.Web.UI.Page
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
            bl.GetCountry(ddlcountry);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BLAdmin addcntry = new BLAdmin();
                int result = addcntry.AddCountry(txtcountry.Text, Convert.ToInt64(Session[Constant.Session.AdminSession]), Convert.ToInt64(Session[Constant.Session.AdminSession]));
                if (result == -99)
                {
                    txtcountry.Text = string.Empty;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Country with name " + txtcountry.Text + " already exists') ;", true);
                }
                else if (result >0)
                {
                    txtcountry.Text = string.Empty;
                    BindCountry();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Added') ;", true);
                }
                else
                {
                    txtcountry.Text = string.Empty;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Some fatal error occurs') ;", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error(logger.Name + ":" + ex.Message);
            }

        }
        protected void btnstate_Click(object sender, EventArgs e)
        {
            try
            {
                BLAdmin addstate = new BLAdmin();
                int result = addstate.AddState(Convert.ToInt32(ddlcountry.SelectedItem.Value), txtstate.Text,Convert.ToInt64(Session[Constant.Session.AdminSession]),Convert.ToInt64(Session[Constant.Session.AdminSession]));
                if (result == -99)
                {
                    ddlcountry.SelectedValue = "0";
                    txtstate.Text = string.Empty;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('State with name " + txtstate.Text + " already exists') ;", true);
                }
                else if (result > 0)
                {
                    ddlcountry.SelectedValue = "0";
                    txtstate.Text = string.Empty;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Added') ;", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error(logger.Name + ":" + ex.Message);
            }
        }

        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtstate.Text = string.Empty;
        }
        protected void btnAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("StateList.aspx");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }
        protected void btnCancels_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }
    }
}
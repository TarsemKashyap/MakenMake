using MakeNMake.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Pages
{
    public partial class FullClientHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        public void BindData()
        {
            BLAdmin getUsers = new BLAdmin();
            DataTable dt = getUsers.GetUsersByRoleID(4);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlClient.DataSource = dt;
                ddlClient.DataTextField = "Name";
                ddlClient.DataValueField = "UserID";
                ddlClient.DataBind();
                ddlClient.Items.Insert(0, new ListItem("--Select Client--", "0"));
            }
            else
            {
                ddlClient.Items.Insert(0, new ListItem("--No Client--", "0"));
            }
        }
        protected void RptServices_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "CheckHistory")
            {
            }
        }

        protected void btnRole_Click(object sender, EventArgs e)
        {
            RptServices.DataSource = null;
            RptServices.DataBind();
            BLAdmin getUsers = new BLAdmin();
            DataTable dt = getUsers.GetUserHistoryByID(Convert.ToInt64(ddlClient.SelectedValue));
            if (dt != null && dt.Rows.Count > 0)
            {
                RptServices.DataSource = dt;
                RptServices.DataBind();
            }
            else
            {
                ddlClient.SelectedValue = "0";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No Service bought by user') ;", true);
            }
        }

        protected void btnCancels_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }

        protected void RptServices_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
            //    HiddenField hdnAgreementID = (HiddenField)e.Item.FindControl("hdnAgreementID");
            //    LinkButton lblHistory = (LinkButton)e.Item.FindControl("lblHistory");
            //    lblHistory.OnClientClick = "CheckHistory(" + hdnAgreementID.Value + ")";
            }
        }
    }
}
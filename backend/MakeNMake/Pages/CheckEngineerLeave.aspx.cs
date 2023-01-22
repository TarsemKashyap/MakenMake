using MakeNMake.BL;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Pages
{
    public partial class CheckEngineerLeave : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                GetAppliedleave();
            }
        }
        private void GetAppliedleave()
        {
            BLAdmin serviceengineer = new BLAdmin();
            DataTable dt = serviceengineer.GetServiceEngineerLeave();
            if (dt.Rows.Count > 0)
            {
                rptleave.DataSource = dt;
                rptleave.DataBind();
            }
        }
        protected void rptleave_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName=="UpdateStatus")
            {
                divUpdateStatus.Visible = true;
                hdleaveIDmain.Value = Convert.ToString(e.CommandArgument);
                Label lbEngineer = (Label)e.Item.FindControl("lbLeaveappliedby");
                Label lblLeavedate = (Label)e.Item.FindControl("lblLeaveOn");
                Label lbreason = (Label)e.Item.FindControl("lblLeaveReason");
                HiddenField hdstaus = (HiddenField)e.Item.FindControl("hdstatus");
                HiddenField hdEngineerID = (HiddenField)e.Item.FindControl("hdEngineerID");
                txtengineername.Text = lbEngineer.Text;
                txtleaveon.Text = lblLeavedate.Text;
                txtReason.Text = lbreason.Text;
                ddlStaus.SelectedValue = hdstaus.Value;

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BLAdmin service = new BLAdmin();
                int result = service.UpdateLeaveStatus(Convert.ToInt32(hdleaveIDmain.Value), Convert.ToInt32(ddlStaus.SelectedValue));
                if (result == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Leave Status Successfully Updated.') ;", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error(logger.Name + ":" + ex.Message);
            }
            GetAppliedleave();
            divUpdateStatus.Visible = false;
            
        }

        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divUpdateStatus.Visible = false;
            GetAppliedleave();
        }

        protected void rptleave_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
              RepeaterItem item = e.Item;
              if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
              {
                  Label lbEngineer = (Label)e.Item.FindControl("lbLeaveappliedby");
                  Label lblstatus = (Label)e.Item.FindControl("lblstatus");
                  HiddenField hdEngineerID = (HiddenField)e.Item.FindControl("hdEngineerID");
                  HiddenField hdstaus = (HiddenField)e.Item.FindControl("hdstatus");
                  BLAdmin serviceengineer = new BLAdmin();
                  string name=serviceengineer.GetLoginuserName(Convert.ToInt64(hdEngineerID.Value));
                  lbEngineer.Text = name;
                  if(hdstaus.Value=="1")
                  {
                      lblstatus.Text = "Approved";
                  }
                  else if(hdstaus.Value=="0")
                  {
                      lblstatus.Text = "UnApproved";
                  }
                  else if(hdstaus.Value=="2")
                  {
                      lblstatus.Text = "Deined";
                  }
              }
        }
    }
}
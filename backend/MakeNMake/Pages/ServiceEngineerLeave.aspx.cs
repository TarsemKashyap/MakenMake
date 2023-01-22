using MakeNMake.BL;
using MakeNMake.CommomFunctions;
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
    public partial class ServiceEngineerLeave : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetAppliedleave();
            }
        }
        private void GetAppliedleave()
        {
            BLServiceEngineer serviceengineer = new BLServiceEngineer();
            DataTable dt= serviceengineer.GetAppliedleave(Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if(dt.Rows.Count>0)
            {
                rptleave.DataSource=dt;
                rptleave.DataBind();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BLServiceEngineer serviceengineer = new BLServiceEngineer();
                int result = serviceengineer.ApplyLeave(Convert.ToInt64(Session[Constant.Session.AdminSession]), txtReason.Text, Convert.ToDateTime(txtleaveon.Text), System.DateTime.Now, 0);
           if (result == -99)
                {
               ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You have already applied leave for this date') ;", true);
           }
           else
           {
               ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Leave successfully applied.') ;", true);
           }
            }
            catch (Exception ex)
            {
                logger.Error(logger.Name + ":" + ex.Message);
            }
            GetAppliedleave();
        }

        protected void rptleave_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
               
                Label lblstatus = (Label)e.Item.FindControl("lblstatus");
             
                HiddenField hdstaus = (HiddenField)e.Item.FindControl("hdstatus");

               
                if (hdstaus.Value == "1")
                {
                    lblstatus.Text = "Approved";
                }
                else if (hdstaus.Value == "0")
                {
                    lblstatus.Text = "UnApproved";
                }
                else if (hdstaus.Value == "2")
                {
                    lblstatus.Text = "Deined";
                }
            }
        }
    }
}
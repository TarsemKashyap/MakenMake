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
    public partial class ServiceTaxInfo : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        BLAdmin addUser = new BLAdmin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                bindooverheadifo();
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try{
            if (btnSubmit.Text.ToLower() == "add")
            {
                Int64 Userid = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                int result = addUser.AddOverheadInfo(txtServiceName.Text, txtDescription.Text, Convert.ToDecimal(txtValue.Text), Userid, System.DateTime.Now, Userid, System.DateTime.Now);
                if (result > 0)
                {
                    bindooverheadifo();
                    clear();
                }
            }
            else if (btnSubmit.Text.ToLower() == "edit")
            {
                Int64 Userid = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                int result = addUser.UpdateOverheadInfo(Convert.ToInt32(hd_overheadId.Value),txtServiceName.Text, txtDescription.Text, Convert.ToDecimal(txtValue.Text), Userid, System.DateTime.Now);
                if (result > 0)
                {
                    bindooverheadifo();
                    clear();
                    btnSubmit.Text = "Add";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('updated sucessfully') ;", true);
                }
            }
                        }
            catch (Exception ex)
            {
                clear();
                logger.Error(logger.Name + ":" + ex.Message);
            }
        }

        public void   bindooverheadifo()
        {
            DataTable dtable = addUser.GetOverheadvalue();
            if(dtable.Rows.Count>0)
            {
                rp_SericeTaxs.DataSource = dtable;
                rp_SericeTaxs.DataBind();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }

        protected void rp_SericeTaxs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                HiddenField overheadid = (HiddenField)e.Item.FindControl("hd_propertyId");

               
                BLAdmin objDelete = new BLAdmin();
                int result = objDelete.DeleteOverhead(Convert.ToInt64(overheadid.Value));
                if (result == 1)
                {
                    bindooverheadifo();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Deleted') ;", true);
                }
                else 
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot delete this Property) ;", true);
                }
            }
            else if (e.CommandName == "edit")
            {


                HiddenField overheadid = (HiddenField)e.Item.FindControl("hd_propertyId");

                hd_overheadId.Value = overheadid.Value;

                Label lblproperty = (Label)e.Item.FindControl("lb_propertyName");
                Label lbldescription = (Label)e.Item.FindControl("lb_description");
                Label lblvalue = (Label)e.Item.FindControl("lbvalue");
                txtServiceName.Text = lblproperty.Text;
                txtDescription.Text = lbldescription.Text;
                txtValue.Text = lblvalue.Text.Replace("%","");

               
                btnSubmit.Text = "Edit";
            }
        }
        private void clear()
        {
            txtValue.Text = "";
            txtServiceName.Text = "";
            txtDescription.Text = "";
        }
    }
}
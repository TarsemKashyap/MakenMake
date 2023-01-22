using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Pages
{
    public partial class GraceMinutes : System.Web.UI.Page
    {
        BLAdmin admin = new BLAdmin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        public void BindData()
        {
            DataTable dt = admin.GetGraceTime();
            if (dt != null && dt.Rows.Count > 0)
            {
                btnReset.Visible = true;
                txtminutes.Text = Convert.ToString(dt.Rows[0]["GraceTime"]);
                txtminutes.Enabled = false;
                btnadd.Visible = false;
            }
            else
            {
                btnReset.Visible = false;
                txtminutes.Enabled = true;
                btnadd.Text = "Add";
                btnadd.Visible = true;
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (btnadd.Text == "Add")
            {
                int result = admin.AddGraceTime(Convert.ToInt64(Session[Constant.Session.AdminSession]), Convert.ToInt32(txtminutes.Text));
                if (result > 0)
                {
                    BindData();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Added') ;", true);
                }
            }
            else
            {
                int result = admin.UpdateGraceTime(Convert.ToInt64(Session[Constant.Session.AdminSession]), Convert.ToInt32(txtminutes.Text));
                if (result > 0)
                {
                    BindData();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Modified') ;", true);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            btnReset.Visible = false;
            btnadd.Visible = true;
            txtminutes.Enabled = true;
            btnadd.Text = "Edit";
        }
    }
}
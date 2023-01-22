using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Customer
{
    public partial class Balance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Binddata();
            }
        }
        public void Binddata()
        {
            BL.BLConsumer objAdmin = new BL.BLConsumer();
            DataTable dt = objAdmin.GetBalance(Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if (dt != null && dt.Rows.Count > 0)
            {
                RptService.DataSource = dt;
                RptService.DataBind();
            }
            else
            {
                lblMsg.Text = "No Service have been used By you after purchase";
                lblMsg.CssClass = "label-success";
            }
        }

        protected void RptService_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "history")
            {
            }
        }
    }
}
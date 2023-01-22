using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Pages
{
    public partial class WorkHistory : System.Web.UI.Page
    {
        BL.BLAdmin objAdmin = new BL.BLAdmin();
        Int64 TicketID;
        protected void Page_Load(object sender, EventArgs e)
        {
            TicketID = Convert.ToInt64(Request.QueryString["TicketID"]);
            if (!IsPostBack)
            {
                BindData();
            }
        }
        private void BindData()
        {
            DataTable dt = objAdmin.GetWorkHistory(TicketID);
           
                if (dt != null && dt.Rows.Count > 0)
                {
                    RptTickets.DataSource = dt;
                    RptTickets.DataBind();
                }
                else
                {
                    lblMsg.Text = "No Work History";
                }

        }

       
    }
}
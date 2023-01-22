using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.UserControl
{
    public partial class ViewRequests : System.Web.UI.UserControl
    {
        public Int64 RequestID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Binddata();
            }
        }
        public void Binddata()
        {
            if (RequestID > 0)
            {
                BL.BLAdmin objAdmin = new BL.BLAdmin();
                DataTable dtable = objAdmin.GetCommercialRequestByID(RequestID);
                if (dtable != null)
                {
                    RptService.DataSource = dtable;
                    RptService.DataBind();
                }
            }
        }
    }
}
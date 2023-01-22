using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Pages
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                TabName.Value = Request.Form[TabName.UniqueID];
            }
        }
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    ToolkitScriptManager masterlbl = (ToolkitScriptManager)Master.FindControl("ToolkitScriptManager1");
        //    masterlbl.EnablePartialRendering = false;
        //}
    }
}
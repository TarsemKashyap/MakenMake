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
    public partial class QutationHistroy : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        BLAdmin addUser = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindDataList();

            }
        }
        private int BindDataList()
        {

            DataTable dt = addUser.GetQuotationAdmin();
            if (dt != null && dt.Rows.Count > 0)
            {
                //ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / pagesize));
            }
            pgsource.DataSource = dt.DefaultView;

            rpQuotation.DataSource = dt;
            rpQuotation.DataBind();

            return (Convert.ToInt32(dt.Rows.Count));
        }
        public string PreviousItem = "";
        protected void rpQuotation_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
            RepeaterItem item = e.Item;
            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                HiddenField lblTest = (HiddenField)e.Item.FindControl("hdRequestId");
                Label lbprices = (Label)e.Item.FindControl("lbPrice");
                if (lblTest.Value != "")
                {
                    if (lblTest.Value == PreviousItem)
                    {
                        lbprices.Text = "";
                        //Do something
                    }
                }
                PreviousItem = lblTest.Value;
            }


        }

        protected void rpQuotation_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Create Contract")
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Under Construction') ;", true);
                Response.Redirect("CreateContract.aspx?RequestID=" + Utilities.EncryptDecrypt.Encript(Convert.ToString(e.CommandArgument)));
            }
        }
    }

}
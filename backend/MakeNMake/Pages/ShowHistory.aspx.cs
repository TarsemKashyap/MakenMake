using MakeNMake.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Admin
{
    public partial class ShowHistory : System.Web.UI.Page
    {
        BLAdmin objAdmin = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        int pagesize, curntpage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
            }
          
        }

        DataTable GetBindEngineers()
        {
            DataTable dtable = objAdmin.GetAllEngineers();
            return dtable;
        }
        private int binddata()
        {
            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetBindEngineers();
            ddlEngineer.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlEngineer.DataTextField = "Name";
                ddlEngineer.DataValueField = "Userid";
                ddlEngineer.DataBind();
                ddlEngineer.Items.Insert(0, new ListItem("--Select Engineer--", "0"));
            }
            else
            {
                ddlEngineer.Items.Insert(0, new ListItem("--No Engineer mentioned by Admin--", "0"));
            }
            return (Convert.ToInt32(dt.Rows.Count));
        }


        public void clear()
        {
            divrptdistrict.Visible = false;
        }
        
        DataTable GetEngineerHistory(int curntpage, DateTime date, Int64 EngineerID)
        {
            DataTable dtable = objAdmin.GetEngineerHistoryByDate(curntpage, date, EngineerID);
             return dtable;
        }

        private int GetDataBindHistory()
        {
            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetEngineerHistory(curntpage, Convert.ToDateTime(txtdate.Text), Convert.ToInt64(ddlEngineer.SelectedValue));
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];


            divrptdistrict.Visible = false;
            if (dt != null && dt.Rows.Count > 0)
            {
                RptShow_History.DataSource = dt;
                RptShow_History.DataBind();
                divrptdistrict.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No History available') ;", true);
            }

            doPaging();
            RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            return (Convert.ToInt32(dt.Rows.Count));
        }

        private void doPaging()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageText");
            findex = CurrentPage - 5;
            if (CurrentPage > 5)
            {
                lindex = CurrentPage + 5;
            }
            else
            {
                lindex = 10;
            }

            if (lindex > Convert.ToInt32(ViewState["totpage"]))
            {
                lindex = Convert.ToInt32(ViewState["totpage"]);
                findex = lindex - 10;
            }

            if (findex < 0)
            {
                findex = 0;
            }

            for (int i = findex; i < lindex; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            RepeaterPaging.DataSource = dt;
            RepeaterPaging.DataBind();

        }
        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                {
                    return 0;
                }
                else
                {
                    return ((int)ViewState["CurrentPage"]);
                }
            }
            set
            {

                ViewState["CurrentPage"] = value;
            }
        }


        protected void RepeaterPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("newpage"))
            {
                CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
                GetDataBindHistory();
            }
        }

        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            GetDataBindHistory();
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            GetDataBindHistory();
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetDataBindHistory();
            }
            else
            {
                CurrentPage = 0;
                GetDataBindHistory();
            }
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetDataBindHistory();
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                GetDataBindHistory();
            }
        }


        protected void RepeaterPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            LinkButton lnkPage = (LinkButton)e.Item.FindControl("Pagingbtn");
            if (lnkPage.CommandArgument.ToString() == CurrentPage.ToString())
            {
                lnkPage.Enabled = false;
                lnkPage.BackColor = System.Drawing.Color.FromName("#970915");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            GetDataBindHistory();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }
    }
}
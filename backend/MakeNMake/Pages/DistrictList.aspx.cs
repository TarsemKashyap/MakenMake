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
    public partial class DistrictList : System.Web.UI.Page
    {
        BLAdmin objAdmin = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        Int64 StateID;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                GetCountryStatList();
            }
            StateID=Convert.ToInt64(ViewState["StateID"]);
        }
        private void GetCountryStatList()
        {
            objAdmin.GetCountry(ddlCountry);
        }

        public void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            divrptdistrict.Visible = false;
            ddlState.Items.Clear();
           
            int countryID = Convert.ToInt32(ddlCountry.SelectedValue);
            objAdmin.GetStatesByCountryID(ddlState, countryID);
        }
        public void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            divrptdistrict.Visible = false;
            ViewState["StateID"] = ddlState.SelectedValue;
            StateID = Convert.ToInt64(ViewState["StateID"]);
            GetDistrictBindFun(StateID);
           
        }

        //change by sohan singh on 15/05/2015


        DataTable Getcnrtystat(int currentpage, Int64 StateID)
        {
            DataTable dtable = objAdmin.GetDistrictViaStateID(currentpage, StateID);
            return dtable;
        }


        private int GetDistrictBindFun(Int64 StateID)
        {


            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = Getcnrtystat(CurrentPage, StateID);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];

            if (dt != null && dt.Rows.Count > 0)
            {
                RptDistrict.DataSource = dt;
                RptDistrict.DataBind();
                divrptdistrict.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No District is available , please first district city') ;", true);
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
                GetDistrictBindFun(StateID);
            }
        }

        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            GetDistrictBindFun(StateID);
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            GetDistrictBindFun(StateID);
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetDistrictBindFun(StateID);
            }
            else
            {
                CurrentPage = 0;
                GetDistrictBindFun(StateID);

            }

        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetDistrictBindFun(StateID);
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                GetDistrictBindFun(StateID);
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


    }
}
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
    public partial class CityList : System.Web.UI.Page
    {
        BLAdmin objAdmin = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        int pagesize, curntpage;
        Int64 DistrictID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCountryStatList();
            }
            DistrictID = Convert.ToInt64(ViewState["DistrictID"]);
        }
        private void GetCountryStatList()
        {
            objAdmin.GetCountry(ddlCountry);
        }

        public void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear();
            ddlDistrict.Items.Clear();
            ddlState.Items.Clear();
            divrptdistrict.Visible = false;
            int countryID = Convert.ToInt32(ddlCountry.SelectedValue);
            objAdmin.GetStatesByCountryID(ddlState, countryID);
        }
        public void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlDistrict.Items.Clear();
            divrptdistrict.Visible = false;
            Int64 StateID = Convert.ToInt64(ddlState.SelectedValue);
            objAdmin.GetDistrictByStateID(ddlDistrict, StateID);
        }
        public void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Int64 DistrictID = Convert.ToInt64(ddlDistrict.SelectedValue);
            ViewState["DistrictID"] = ddlDistrict.SelectedValue;
            DistrictID = Convert.ToInt64(ViewState["DistrictID"]);
            GetDataBindCity(DistrictID);


        }

        public void clear()
        {
            ddlDistrict.Items.Clear();
            ddlState.Items.Clear();
            divrptdistrict.Visible = false;

        }


        DataTable Getcnrtystatcity(int currentpage, Int64 DistrictID)
        {
            DataTable dtable = objAdmin.GetCityByDistrictID(currentpage, DistrictID);
            return dtable;
        }


        private int GetDataBindCity(Int64 DistrictID)
        {

            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = Getcnrtystatcity(CurrentPage, DistrictID);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];


            divrptdistrict.Visible = false;
            if (dt != null && dt.Rows.Count > 0)
            {
                RptCity.DataSource = dt;
                RptCity.DataBind();
                divrptdistrict.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No City is available , please first create city') ;", true);
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
                GetDataBindCity(DistrictID);
            }
        }

        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            GetDataBindCity(DistrictID);
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            GetDataBindCity(DistrictID);
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetDataBindCity(DistrictID);
            }
            else
            {
                CurrentPage = 0;
                GetDataBindCity(DistrictID);

            }

        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetDataBindCity(DistrictID);
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                GetDataBindCity(DistrictID);
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
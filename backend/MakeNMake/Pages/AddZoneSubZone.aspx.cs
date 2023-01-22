using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Admin
{
    public partial class AddZoneSubZone : System.Web.UI.Page
    {
        BLAdmin objAdmin = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindState();
                rdbStatus.SelectedIndex = 0;
                GetSubZoneList();
            }
        }
        private void BindState()
        {
           // MakeNMake.BL.BLAdmin objAdmin = new BL.BLAdmin();
            objAdmin.GetStates(ddlZone);
        }
        //private void GetSubZoneList()
        //{
        //    BLAdmin objAdmin = new BLAdmin();
        //    DataTable dtZone = objAdmin.GetZoneSubZoneList();
        //    //if (dtZone != null && dtZone.Rows.Count > 0)
        //    //{
        //    //    RptZone.DataSource = dtZone;
        //    //    RptZone.DataBind();
        //    //}
        //    //else
        //    //{
        //    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No Zone is available , please first create zone') ;", true);
        //    //}
        //}
        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlZone.SelectedValue == "0")
            {
                ddlSubZone.Items.Clear();
            }
            else
            {
                ddlSubZone.Items.Clear();
                BindDistrict(Convert.ToInt64(ddlZone.SelectedValue));
            }
        }
        private void BindDistrict(Int64 ID)
        {
            MakeNMake.BL.BLAdmin objAdmin = new BL.BLAdmin();
            objAdmin.GetDistrictByStateID(ddlSubZone,Convert.ToInt32(ID));
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtSearchclient.Text != "")
            {
                CurrentPage = 0;
                int x = GetSubZoneList();
                if (x != 0)
                {
                   // tblPaging.Visible = true;
                    //divClientList.Visible = true;
                }
                else
                {
                   // tblPaging.Visible = false;
                  //  divClientList.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No Record Found!') ;", true);
                    txtSearchclient.Text = null;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please Enter the Zone Or Subzone!') ;", true);
            }
        }


        protected void btnADD_Click(object sender, EventArgs e)
        {
            BLAdmin objZone = new BLAdmin();
            if (btnADD.Text.ToLower() == "add")
            {
                int result = objZone.InsertZoneSubZone(Convert.ToInt64(ddlZone.SelectedValue), 
                    Convert.ToInt64(ddlSubZone.SelectedValue), Convert.ToInt64(Session[Constant.Session.AdminSession]),
                    Convert.ToInt32(rdbStatus.SelectedItem.Value), Convert.ToInt32(rdbStatus.SelectedItem.Value),Convert.ToInt64(Session[Constant.Session.AdminSession]));
                if (result == 1)
                {
                    Clear(); GetSubZoneList();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Added') ;", true);
                }
                else if (result == -99)
                {
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Zone with this subzone already exists') ;", true);
                }
            }
            else if (btnADD.Text.ToLower() == "edit")
            {
                int result = objZone.UpdateZoneSubZone(Convert.ToInt64(ddlZone.SelectedValue), Convert.ToInt64(ddlSubZone.SelectedValue), Convert.ToInt32(rdbStatus.SelectedItem.Value), Convert.ToInt32(rdbStatus.SelectedItem.Value));
                if (result == 1)
                {
                    Clear(); GetSubZoneList();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Updated') ;", true);
                    ddlZone.Enabled = true;
                    ddlSubZone.Enabled = true;
                    rdbStatus.SelectedValue = 1.ToString();
                    btnADD.Text = "Add";
                }
                else if (result == -99)
                {
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Zone not exists. Firstly Please Add the Zone') ;", true);
                    ddlZone.Enabled = true;
                    ddlSubZone.Enabled = true;
                    rdbStatus.SelectedValue = 1.ToString();
                    btnADD.Text = "Add";
                }

            }
        }
        public void Clear()
        {
            ddlZone.SelectedValue = "0";
            ddlSubZone.SelectedValue = "0";
            rdbStatus.SelectedValue = "0";
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }
        DataTable GetClientData(int currentpage,string Zonename)
        {
            DataTable dtable = objAdmin.GetZoneSubZoneList(currentpage, Zonename);

            return dtable;
        }
        private int GetSubZoneList()
        {


            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetClientData(CurrentPage, txtSearchclient.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];

            RptZone.DataSource = dt;
            RptZone.DataBind();

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
                GetSubZoneList();
            }
        }
        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            GetSubZoneList();
        }
        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            GetSubZoneList();
        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetSubZoneList();
            }
            else
            {
                CurrentPage = 0;
                GetSubZoneList();

            }

        }
        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetSubZoneList();
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                GetSubZoneList();
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
        protected void RptZone_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                BLAdmin obj = new BLAdmin();
                HiddenField hdnZone = (HiddenField)e.Item.FindControl("hdnZoneID");
                HiddenField hdnSubZone = (HiddenField)e.Item.FindControl("hdnSubZone");

                int result = obj.DeleteZone(Convert.ToInt64(hdnZone.Value), Convert.ToInt64(hdnSubZone.Value));
                if (result == 1)
                {
                    GetSubZoneList();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Deleted') ;", true);
                }
                else if(result==-99)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot delete this Zone as it is assigned to someone') ;", true);
                }
                else if (result == -98)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot delete inactive zone') ;", true);
                }
            }
            else if (e.CommandName == "edit")
            {
                //divzone.Visible = false;
                //diveditZone.Visible = true;

                HiddenField zone = (HiddenField)e.Item.FindControl("hdnZone");
                HiddenField subZone = (HiddenField)e.Item.FindControl("hdnSubZone");

                // ddlZone.SelectedValue = zone.Value;
                // ddlSubZone.SelectedValue = subZone.Value;

                Label lblZone = (Label)e.Item.FindControl("lblzoneID");
                Label lblSubZone = (Label)e.Item.FindControl("lblSubzoneID");
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");


                btnADD.Text = "Edit";
                ddlZone.SelectedValue = lblZone.Text;
                BindDistrict(Convert.ToInt64(ddlZone.SelectedValue));
                //ViewState["PreSubZone"] = lblSubZone.Text;
                ddlSubZone.SelectedValue = lblSubZone.Text;
                ddlZone.Enabled = false;
                ddlSubZone.Enabled = false;
                rdbStatus.SelectedValue = (lblStatus.Text == "Active" ? 1 : 0).ToString();




            }
        }

    }
}
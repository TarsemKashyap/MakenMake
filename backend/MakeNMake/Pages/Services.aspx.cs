using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Admin
{
    public partial class Services : System.Web.UI.Page
    {
        BLAdmin objGetServices = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                BindServicePlan();
            }
        }

        private int BindServicePlan()
        {


            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = objGetServices.GetServiceList(CurrentPage,txtSearchclient.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                tblpaging.Visible = true;
                
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
            }
            else
            {
                    tblpaging.Visible = false;
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];

            if (dt != null && dt.Rows.Count > 0)
            {
                RptService.DataSource = dt;
                RptService.DataBind();
                doPaging();
                RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            }
            else
            {
            }

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
                BindServicePlan();
            }
        }

        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            BindServicePlan();
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            BindServicePlan();
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindServicePlan();
            }
            else
            {
                CurrentPage = 0;
                BindServicePlan();

            }

        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindServicePlan();
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                BindServicePlan();
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


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAdd.Text.ToLower() == "add")
                {
                    //if (txtname.Text.ToLower().Contains("unlimited") && ddlCategory.SelectedValue == "C")
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Service with unlimted name and  with commercial category cannot be added') ;", true);
                    //}
                    //else
                    //{
                        int result = 0;
                        BL.BLAdmin objService = new BL.BLAdmin();
                        result = objService.AddServices(txtname.Text, txtdesc.Text, txtservicestart.Text + " " + ddlFrmTime.SelectedItem.Text, txtserviceend.Text + " " + ddlToTime.SelectedItem.Text,
                            ddlCategory.SelectedValue, ddlServiceType.SelectedValue, Convert.ToInt64(Session[Constant.Session.AdminSession]), rdbstatus.SelectedIndex == 0 ? 1 : 0);
                        if (result > 0)
                        {
                            Clear();
                            BindServicePlan();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Service data added sucessfully') ;", true);
                        }
                        else if (result == -98)
                        {
                            Clear();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please Change Service Type') ;", true);
                        }
                        else if (result == -99)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Service with this category already exists') ;", true);
                        }
                   // }
                }
                else
                {
                    BL.BLAdmin objService = new BL.BLAdmin();
                    int result = objService.UpdateServices(Convert.ToInt32(hdnServiceID.Value), Convert.ToInt64(Session[Constant.Session.AdminSession]), txtdesc.Text, txtservicestart.Text, txtserviceend.Text, ddlCategory.SelectedValue, ddlServiceType.SelectedValue, rdbstatus.SelectedIndex == 0 ? 1 : 0);
                    if (result == 1)
                    {
                        btnAdd.Text = "Add";
                        txtname.Enabled = true;
                        hdnServiceID.Value = string.Empty; Clear();
                        ddlCategory.Enabled = true;
                        ddlServiceType.Enabled = true;
                        BindServicePlan();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Updated') ;", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Service with this category and type already exists') ;", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Clear();
                logger.Error(logger.Name + ":" + ex.Message);
            }
        }
        public void Clear()
        {
            txtname.Text = string.Empty;
            txtdesc.Text = string.Empty;
            txtserviceend.Text = string.Empty;
            txtservicestart.Text = string.Empty;
            ddlCategory.SelectedValue = "0";
            ddlServiceType.SelectedValue = "0";
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("ServiceMasterList.aspx");
        }

        protected void RptService_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                HiddenField serviceID = (HiddenField)e.Item.FindControl("hdnID");
                BLAdmin objDelete = new BLAdmin();
                int servicePlanID = Convert.ToInt32(e.CommandArgument);
                int result = objDelete.DeleteService(Convert.ToInt32(serviceID.Value), servicePlanID);
                if (result == 1)
                {
                    BindServicePlan();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Deleted') ;", true);
                }
            }
            else if (e.CommandName == "edit")
            {
                hdnServiceID.Value = Convert.ToString(e.CommandArgument);
                Label lblName = (Label)e.Item.FindControl("lblName");
                Label lblDesc = (Label)e.Item.FindControl("lbldesc");
                Label lblFromTime = (Label)e.Item.FindControl("lblFrom");
                Label lblToTime = (Label)e.Item.FindControl("lblTo");
                Label lblCat = (Label)e.Item.FindControl("lblCategory");
                Label lblType = (Label)e.Item.FindControl("lblType");
                Label lblstatus = (Label)e.Item.FindControl("lblstatus");
                rdbstatus.SelectedIndex = (lblstatus.Text == "Active") ? 0 : 1;
                btnAdd.Text = "Edit";
                txtname.Text = lblName.Text;
                txtname.Enabled = false;
                txtdesc.Text = lblDesc == null ? string.Empty : lblDesc.Text;
                txtservicestart.Text = lblFromTime.Text;
                txtserviceend.Text = lblToTime.Text;
                string cateory = lblCat.Text;
                ddlCategory.SelectedValue = cateory.Substring(0, 1).ToUpper(); 
                ddlServiceType.SelectedValue = lblType.Text.Substring(0,1).ToUpper();
                ddlCategory.Enabled = false;
                ddlServiceType.Enabled = false;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text.ToLower() == "edit")
            {
                Response.Redirect("Services.aspx");
            }
            else
            {
                Response.Redirect("DashBoard.aspx");
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtSearchclient.Text != "")
            {
                CurrentPage = 0;
                int x = BindServicePlan();
                if (x != 0)
                {
                    //  tblPaging.Visible = true;
                    //  divClientList.Visible = true;
                }
                else
                {
                    //tblPaging.Visible = false;
                    // divClientList.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No Record Found!') ;", true);
                    txtSearchclient.Text = null;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please Enter the Name Or Mobile Number!') ;", true);
            }
        }

        protected void RptService_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblreasn = (Label)e.Item.FindControl("lbldesc");
                if (lblreasn.Text.Length > 25)
                {
                    lblreasn.Text = lblreasn.Text.Substring(0, 25);
                    LinkButton lnkBtnMore = (LinkButton)e.Item.FindControl("lnkBtnMore");
                    lnkBtnMore.Visible = true;
                    lnkBtnMore.OnClientClick = "ShowMsg(this)";
                }
                else
                {
                    LinkButton lnkBtnMore = (LinkButton)e.Item.FindControl("lnkBtnMore");
                    lnkBtnMore.Visible = false;
                }
            }
        }

    }
}
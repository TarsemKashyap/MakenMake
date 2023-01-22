using MakeNMake.BL;
using System;
using MakeNMake.CommomFunctions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NLog;

namespace MakeNMake
{
    public partial class ServiceAssesment : System.Web.UI.Page
    {
        
        BLAdmin objGetServices = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindServices();
                BindServiceAssesment();
            }

        }
        private void BindServices()
        {
            BL.BLAdmin objService = new BL.BLAdmin();
            objService.GetBasicServiced(ddlcategory);
        }
        DataTable GetBindAssesment(int currentpage)
        {
            DataTable dtable = objGetServices.GetBasicCommercialoServiceList(CurrentPage);
            return dtable;
        }
        private int BindServiceAssesment()
        {
            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetBindAssesment(CurrentPage);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalcount"]) / 10));
            }
           
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];

            if (dt != null && dt.Rows.Count > 0)
            {
                divService.Visible = true;
                RptService.DataSource = dt;
                RptService.DataBind();
                doPaging();
                RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            }
            else
            {
                divService.Visible = false;
            }

            return (Convert.ToInt32(dt.Rows.Count));
        }
        private void Clear()
        {
            txtValidation.Text = string.Empty;
            ddlcategory.SelectedValue = "0";
            txtservice.Text = string.Empty;
           
        }
        protected void RptService_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                HiddenField serviceID = (HiddenField)e.Item.FindControl("hdnID");
                BLAdmin objDelete = new BLAdmin();
                //int servicePlanID = Convert.ToInt32(e.CommandArgument);
                int result = objDelete.DeleteServiceAssesment(Convert.ToInt32(serviceID.Value));
                if (result == 1)
                {
                    BindServiceAssesment();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Deleted') ;", true);
                }
            }
            else if (e.CommandName == "edit")
            {
                hdnServiceID.Value = Convert.ToString(e.CommandArgument);
                HiddenField hdserviceid = (HiddenField)e.Item.FindControl("hdserviceid");
                Label lblName = (Label)e.Item.FindControl("lblName");
                Label lblvalidation = (Label)e.Item.FindControl("lblvalidation");
              
                btnadd.Text = "Edit";
                ddlcategory.SelectedValue = hdserviceid.Value;
                //txtname.Enabled = false;
                ddlcategory.Enabled = false;
                txtservice.Text = lblName.Text;
                txtValidation.Text = lblvalidation.Text;
            }
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
                BindServiceAssesment();
            }
        }
        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            BindServiceAssesment();
        }
        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            BindServiceAssesment();
        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindServiceAssesment();
            }
            else
            {
                CurrentPage = 0;
                BindServiceAssesment();

            }

        }
        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindServiceAssesment();
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                BindServiceAssesment();
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

       

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnadd.Text.ToLower() == "add")
                {
                    DateTime Created = DateTime.Now;
                    int result = 0;
                    BLAdmin objService = new BLAdmin();
                    string services = ddlcategory.SelectedValue;
                    int serviceid = Convert.ToInt16(services.Substring(0,services.IndexOf("$")));
                    int planid = Convert.ToInt16(services.Substring(services.IndexOf("$") + 1));
                    result = objService.AddServicesAssesment(serviceid,planid,txtservice.Text,txtValidation.Text,Created, Convert.ToInt64(Session[Constant.Session.AdminSession]));
                    if (result > 0)
                    {

                        BindServiceAssesment();
                        Clear();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Service data added sucessfully') ;", true);
                    }
                    else if (result == -98)
                    {
                        
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
                    string services = ddlcategory.SelectedValue;
                    int serviceid = Convert.ToInt16(services.Substring(0, services.IndexOf("$")));
                    int result = objService.UpdateServicesAssesment( Convert.ToInt16(hdnServiceID.Value),serviceid, txtservice.Text,txtValidation.Text);
                    if (result == 1)
                    {
                        btnadd.Text = "Add";
                        //txtservice.Enabled = true;
                        hdnServiceID.Value = string.Empty;
                        ddlcategory.Enabled = true;
                        BindServiceAssesment();
                        Clear();
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
               // Clear();
                logger.Error(logger.Name + ":" + ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }
    }
}
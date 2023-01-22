using MakeNMake.BL;
using System;
using MakeNMake.CommomFunctions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Admin
{
    public partial class ServicePlan : System.Web.UI.Page
    {
        BLAdmin objGetServices = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindServices();
                BindServicePlan();
                rdbMakeVisit.SelectedIndex = 1;
                rdbUnimitedVisit.SelectedIndex = 1;
            }
        }
        private void BindServices()
        {
            BL.BLAdmin objService = new BL.BLAdmin();
            objService.GetServices(ddlService);
        }
        private int BindServicePlan()
        {
            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = objGetServices.GetServiceList(CurrentPage, txtSearchclient.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
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
            if (btnAdd.Text.ToLower() == "add")
            {
                BL.BLAdmin objPlan = new BL.BLAdmin(); int result = 0;
                if (dvunlimited.Visible)
                {
                    string area = ddlArea.SelectedItem.Value == "01" ? "0" : ddlArea.SelectedItem.Value;
                    string planUnlimited = "<data><child><Area>" + area + "</Area><AreaTo>" + ddlAreaToSqft.SelectedItem.Value + "</AreaTo><Category>" + ddlCategory.SelectedItem.Value + "</Category><Amount>" + txtuAmount.Text + "</Amount><Discount>" + txtUDiscount.Text + "</Discount></child></data>";
                    result = objPlan.AddServicePlan(Convert.ToInt32(ddlService.SelectedItem.Value), ddlPlan.SelectedItem.Value, planUnlimited, 0, 0, 0, Convert.ToInt32(rdbUnimitedVisit.SelectedItem.Value), Convert.ToInt64(Session[Constant.Session.AdminSession]), Convert.ToDecimal("0.0"));
                }
                else
                {
                    if (ddlPlan.SelectedItem.Value == "U")
                    {
                        result = objPlan.AddServicePlan(Convert.ToInt32(ddlService.SelectedItem.Value), ddlPlan.SelectedItem.Value, "", 1, Convert.ToInt32(txtDuration.Text), Convert.ToDecimal(txtamount.Text), Convert.ToInt32(rdbMakeVisit.SelectedItem.Value), Convert.ToInt64(Session[Constant.Session.AdminSession]), txtFixedDiscount.Text == "" ? Convert.ToDecimal("0.0") : Convert.ToDecimal(txtFixedDiscount.Text));
                    }
                    else
                    {
                        result = objPlan.AddServicePlan(Convert.ToInt32(ddlService.SelectedItem.Value), ddlPlan.SelectedItem.Value, "", 1, Convert.ToInt32(txtDuration.Text), Convert.ToDecimal(txtamount.Text), Convert.ToInt32(rdbMakeVisit.SelectedItem.Value), Convert.ToInt64(Session[Constant.Session.AdminSession]), txtFixedDiscount.Text == "" ? Convert.ToDecimal("0.0") : Convert.ToDecimal(txtFixedDiscount.Text));
                    }
                }

                if (result > 0)
                {
                    Clear();
                    dvFixedmake.Visible = false;
                    dvunlimited.Visible = false;
                    BindServicePlan();
                    ddlPlan.SelectedValue = "0";
                    ddlService.SelectedValue = "0";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Service plan added sucessfully') ;", true);
                }
                else if (result == -99)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Service Plan already exists') ;", true);
                }
                else if (result == -97)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('This Category already exists in unlimited plan') ;", true);
                }
            }
            else if (btnAdd.Text.ToLower() == "edit")
            {
                BL.BLAdmin objPlan = new BL.BLAdmin();
                string serviceType = ddlService.SelectedItem.Text.Substring(ddlService.SelectedItem.Text.IndexOf("-") + 1).ToLower();
                decimal discount = (ddlPlan.SelectedValue == "F" && serviceType == "addon)") || (ddlPlan.SelectedValue == "M" && serviceType == "addon)")
                    || (ddlPlan.SelectedValue == "U" && serviceType == "addon)") ?
                    Convert.ToDecimal(txtFixedDiscount.Text) : Convert.ToDecimal("0.0");
                int result = objPlan.UpdateServicePlan(Convert.ToInt32(ddlService.SelectedItem.Value), Convert.ToInt32(hdnServiceID.Value), 1, Convert.ToInt32(txtDuration.Text), Convert.ToDecimal(txtamount.Text), Convert.ToInt32(rdbMakeVisit.SelectedItem.Value), Convert.ToInt64(Session[Constant.Session.AdminSession]),discount);
                if (result > 0)
                {
                    dvDiscount.Visible = false;
                    ddlPlan.Enabled = true;
                    btnAdd.Text = "Add";
                    ddlService.Enabled = true;
                    ddlPlan.SelectedValue = "0";
                    ddlService.SelectedValue = "0";
                    Clear(); BindServicePlan();
                    dvunlimited.Visible = false;
                    dvFixedmake.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Service plan updated sucessfully') ;", true);
                }
            }
        }
        private void Clear()
        {
            ddlArea.SelectedValue = "0";
            txtamount.Text = string.Empty;
            txtDuration.Text = string.Empty;
            txtamount.Text = string.Empty;
            ddlCategory.SelectedValue = "0";
            txtuAmount.Text = string.Empty;
            txtUDiscount.Text = string.Empty;
            ddlAreaToSqft.SelectedValue = "0";
            txtFixedDiscount.Text = string.Empty;
        }
        public bool ISDefined(int planType)
        {
            if (planType == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void RptService_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                Label lblType = (Label)e.Item.FindControl("lblPlan");
                if (lblType.Text.Substring(0, 1).ToUpper() == "U")
                {
                    hdnServiceID.Value = Convert.ToString(e.CommandArgument);
                    Response.Redirect("ServiceUnlimitedPlan.aspx?ServiceID=" + hdnServiceID.Value);
                }
                else
                {
                    HiddenField serviceID = (HiddenField)e.Item.FindControl("hdnID");
                    BLAdmin objDelete = new BLAdmin();
                    int servicePlanID = Convert.ToInt32(e.CommandArgument);
                    int result = objDelete.DeleteServicePlan(Convert.ToInt32(serviceID.Value), servicePlanID);
                    if (result == 1)
                    {
                        BindServicePlan();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Deleted') ;", true);
                    }
                }
            }
            else if (e.CommandName == "edit")
            {
                txtFixedDiscount.Text = string.Empty; 
                Label lblType = (Label)e.Item.FindControl("lblPlan");
                Label lblSType = (Label)e.Item.FindControl("lblType");
                if (lblType.Text.Substring(0, 1).ToUpper() == "U" && lblSType.Text.ToLower()=="basic")
                {
                    hdnServiceID.Value = Convert.ToString(e.CommandArgument);
                    Response.Redirect("ServiceUnlimitedPlan.aspx?ServiceID=" + hdnServiceID.Value);
                }
                else
                {
                    hdnServiceID.Value = Convert.ToString(e.CommandArgument);
                    Label lblName = (Label)e.Item.FindControl("lblName");
                    Label lblNoCalls = (Label)e.Item.FindControl("lblCalls");
                    Label lblDuration = (Label)e.Item.FindControl("lblDuration");
                    Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                    Label lblCat = (Label)e.Item.FindControl("lblCategory");
                    btnAdd.Text = "Edit";
                    HiddenField serviceID = (HiddenField)e.Item.FindControl("hdnID");
                    ddlService.SelectedValue = serviceID.Value;
                    txtamount.Text = lblAmount.Text;
                    txtDuration.Text = lblDuration.Text;
                    ddlPlan.SelectedValue = lblType.Text.Substring(0, 1).ToUpper();
                    Label lblvisit = (Label)e.Item.FindControl("lblvisit");
                    if (lblvisit.Text.ToLower() == "yes")
                    {
                        rdbMakeVisit.SelectedIndex = 0;
                    }
                    else if (lblvisit.Text.ToLower() == "no")
                    {
                        rdbMakeVisit.SelectedIndex = 1;
                    }
                    if (lblType.Text.Substring(0, 1).ToUpper() == "U")
                    {
                        if (lblSType.Text.ToLower() == "addon")
                        {
                            dvunlimited.Visible = false;
                            dvFixedmake.Visible = true;
                            dvDiscount.Visible = true; 
                            HiddenField hdnFDiscount = (HiddenField)e.Item.FindControl("hdnFDiscount");
                            txtFixedDiscount.Text = hdnFDiscount.Value;
                            dvmakeVisitRequired.Visible = true;
                        }
                        else
                        {
                            dvVisitRequired.Visible = true;
                            dvDiscount.Visible = false;
                            dvunlimited.Visible = true;
                            dvFixedmake.Visible = false;
                        }
                    }
                    else if (lblType.Text.Substring(0, 1).ToUpper() == "M")
                    {
                        dvmakeVisitRequired.Visible = true;
                        dvunlimited.Visible = false;
                        dvFixedmake.Visible = true;
                        if (lblSType.Text.ToLower() == "addon")
                        {
                            dvDiscount.Visible = true;
                            HiddenField hdnFDiscount = (HiddenField)e.Item.FindControl("hdnFDiscount");
                            txtFixedDiscount.Text = hdnFDiscount.Value;                           
                        }
                        else
                        {
                            dvDiscount.Visible = false;
                        }
                    }
                    else
                    {
                        dvmakeVisitRequired.Visible = true;
                        dvunlimited.Visible = false;
                        dvFixedmake.Visible = true;
                       // dvmakeVisitRequired.Visible = true;
                        if (lblType.Text.Substring(0, 1).ToUpper() == "F")
                        {
                            //if (lblSType.Text.ToLower() == "addon")
                            //{
                                dvDiscount.Visible = true;
                                HiddenField hdnFDiscount = (HiddenField)e.Item.FindControl("hdnFDiscount");
                                txtFixedDiscount.Text = hdnFDiscount.Value;
                            //}
                            //else
                            //{
                            //    dvDiscount.Visible = false;
                            //}
                        }
                    }
                    ddlPlan.Enabled = false;
                    ddlService.Enabled = false;
                }
            }
        }

        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPlan.SelectedValue == "0")
            {
                Clear();
                dvFixedmake.Visible = false;
                dvunlimited.Visible = false;
            }
            else if (ddlPlan.SelectedValue == "U")
            {
                dvDiscount.Visible = false;
                Clear();
                if (ddlService.SelectedValue != "0")
                {
                    string serviceType = ddlService.SelectedItem.Text.Substring(ddlService.SelectedItem.Text.IndexOf("-") + 1).ToLower();
                    if (serviceType == "addon)")
                    {
                        //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot add unlimited plan for the services which are AddOn, you can add flexi and make your plan for that') ;", true);
                        dvFixedmake.Visible = true;
                        Clear(); dvDiscount.Visible = true;
                        dvunlimited.Visible = false;
                        dvVisitRequired.Visible = true;//visit request
                    }
                    else
                    {
                        Clear();
                        dvDiscount.Visible = false;
                        dvFixedmake.Visible = false;
                        dvVisitRequired.Visible = true;
                        dvunlimited.Visible = true;
                    }
                }
                else
                {
                    ddlPlan.SelectedValue = "0";
                    ddlService.SelectedValue = "0";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select services first') ;", true);
                }
            }
            else
            {
                if (ddlService.SelectedItem.Value == "0")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select Service first') ;", true);
                }
                else
                {
                   dvVisitRequired.Visible = false;
                    if (ddlPlan.SelectedValue == "U")
                    {
                        Clear(); dvDiscount.Visible = false;
                        dvunlimited.Visible = true;
                        dvFixedmake.Visible = false;
                    }
                    else if (ddlPlan.SelectedValue == "F")
                    {
                        Clear();
                        dvDiscount.Visible = true;
                        dvunlimited.Visible = false;
                        dvFixedmake.Visible = true;
                        dvmakeVisitRequired.Visible = true;
                    }
                    else if (ddlPlan.SelectedValue == "M")
                    {
                        string serviceType = ddlService.SelectedItem.Text.Substring(ddlService.SelectedItem.Text.IndexOf("-") + 1).ToLower();
                        if (serviceType == "addon)")
                        {
                            Clear();
                            dvDiscount.Visible = true;
                            dvunlimited.Visible = false;
                            dvFixedmake.Visible = true;
                            dvmakeVisitRequired.Visible = true;
                        }
                        else
                        {
                            Clear();
                            dvDiscount.Visible = false;
                            dvunlimited.Visible = false;
                            dvFixedmake.Visible = true;
                            dvmakeVisitRequired.Visible = true;
                        }
                    }
                }
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "PageReload()", true);
                    txtSearchclient.Text = null;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please Enter the Name Or Mobile Number!') ;", true);
            }
        }

        protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            ddlPlan.SelectedValue = "0";
            dvunlimited.Visible = false;
            dvFixedmake.Visible = false;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlAreaToSqft.Visible = true;
            dvto.Visible = true;
            if (ddlCategory.SelectedValue != "0")
            {
                if (ddlCategory.SelectedValue == "A")
                {
                    ddlArea.SelectedValue = "01";
                    ddlAreaToSqft.SelectedValue = "1500";
                }
                else if (ddlCategory.SelectedValue == "B")
                {
                    ddlArea.SelectedValue = "1501";
                    ddlAreaToSqft.SelectedValue = "3000";
                }
                else if (ddlCategory.SelectedValue == "C")
                {
                    ddlArea.SelectedValue = "3001";
                    ddlAreaToSqft.SelectedValue = "4500";
                }
                else if (ddlCategory.SelectedValue == "D")
                {
                    ddlArea.SelectedValue = "4501";
                    ddlAreaToSqft.SelectedValue = "7500";
                }
                else if (ddlCategory.SelectedValue == "E")
                {
                    ddlArea.SelectedValue = "7500";
                    ddlAreaToSqft.Visible = false;
                    rdbUnimitedVisit.SelectedValue = "1";
                    dvto.Visible = false;
                }
            }
            else
            {
                ddlArea.SelectedValue = "0";
                ddlAreaToSqft.SelectedValue = "0";
            }
        }
    }
}
using MakeNMake.BL;
using System;
using System.Collections.Generic;
using MakeNMake.CommomFunctions;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Admin
{
    public partial class ServiceUnlimitedPlan : System.Web.UI.Page
    {
        BLAdmin objGetServices = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        int ServiceplanID; 
        protected void Page_Load(object sender, EventArgs e)
        {
            ServiceplanID = Convert.ToInt32(Request.QueryString["ServiceID"]);
            BindServices();
            if (!IsPostBack)
            {
                BindUnlimitedServicePlan();
            }
        }
        private void BindServices()
        {
            BL.BLAdmin objService = new BL.BLAdmin();
            objService.GetServices(ddlService);
        }
      

       //change on 1-06-2015 paging
        private int BindUnlimitedServicePlan()
        {


            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = objGetServices.GetUnlimitedServiceList(CurrentPage, ServiceplanID);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalcount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];

            if (dt != null && dt.Rows.Count > 0)
            {
                divService.Visible = true;
                RptUnlimitedService.DataSource = dt;
                RptUnlimitedService.DataBind();
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
                BindUnlimitedServicePlan();
            }
        }

        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            BindUnlimitedServicePlan();
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            BindUnlimitedServicePlan();
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindUnlimitedServicePlan();
            }
            else
            {
                CurrentPage = 0;
                BindUnlimitedServicePlan();

            }

        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindUnlimitedServicePlan();
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                BindUnlimitedServicePlan();
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



        //----------------


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            
            if (btnAdd.Text.ToLower() == "edit")
            {
                BL.BLAdmin objPlan = new BL.BLAdmin();
                string area = ddlArea.SelectedItem.Value == "01" ? "0" : ddlArea.SelectedItem.Value;
                string planUnlimited = "<data><child><Area>" + area + "</Area><AreaTo>" + ddlAreaToSqft.SelectedValue + "</AreaTo><Category>" + ddlCategory.SelectedValue + "</Category><Amount>" + txtuAmount.Text + "</Amount><Discount>" + txtUDiscount.Text + "</Discount><visitrequired>" + Convert.ToInt32(rbtLstVisitReq.SelectedValue) + "</visitrequired></child></data>";
                int result = objPlan.UpdateUnlimitedServicePlan(Convert.ToInt32(hdnServiceID.Value), Convert.ToInt32(hdnPlanID.Value), planUnlimited, Convert.ToInt64(Session[Constant.Session.AdminSession]));
                if (result > 0)
                {
                    ddlArea.Enabled = true;
                    ddlAreaToSqft.Enabled = true;
                    dvunlimited.Visible = false;
                    ddlPlan.Enabled = true;
                    btnAdd.Text = "Add";
                    ddlCategory.Enabled = true;
                    ddlService.Enabled = true;
                    ddlPlan.SelectedValue = "0";
                    ddlService.SelectedValue = "0";
                    Clear(); BindUnlimitedServicePlan();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Unlimited Service plan updated sucessfully') ;", true);
                }
            }
            
        }
        private void Clear()
        {
            ddlArea.SelectedValue = "0";
            txtuAmount.Text = string.Empty;
            txtUDiscount.Text = string.Empty;
            rbtLstVisitReq.SelectedValue = "1";
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
        protected void RptUnlimitedService_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
             if (e.CommandName == "delete")
             {
                 Label lblPlanId = (Label)e.Item.FindControl("lblPlanId");
                 Label lblServiceplanID  = (Label)e.Item.FindControl("lblServiceplanID");
                  
                    BLAdmin objDelete = new BLAdmin();
                    int PlanId = Convert.ToInt32(lblPlanId.Text);
                    int servicePlanID = Convert.ToInt32(lblServiceplanID.Text);
                    int result = objDelete.DeleteServicePlan(PlanId, servicePlanID);
                    if (result == 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Deleted') ;", true);
                        BindUnlimitedServicePlan();
                        Clear();
                        dvunlimited.Visible = false;

                    }
               
            }
            else if (e.CommandName == "edit")
            {

                    hdnPlanID.Value = Convert.ToString(e.CommandArgument);
                    Label PlanId = (Label)e.Item.FindControl("lblPlanId");
                    Label ServiceplanID = (Label)e.Item.FindControl("lblServiceplanID");
                    hdnServiceID.Value = ServiceplanID.Text;
                    Label ServiceID = (Label)e.Item.FindControl("lblServiceID");
                    Label PlanType = (Label)e.Item.FindControl("lblPlanType");
                
                    Label Area = (Label)e.Item.FindControl("lblArea");
                    Label AreaTo = (Label)e.Item.FindControl("lblAreaTo");
                    Label  Category = (Label)e.Item.FindControl("lblCategory");
                    Label  Amount  = (Label)e.Item.FindControl("lblAmount");
                    Label Discount = (Label)e.Item.FindControl("lblDiscount");
                    Label visitrequired = (Label)e.Item.FindControl("lblvisitrequired");

                    btnAdd.Text = "Edit";
                    HiddenField serviceID = (HiddenField)e.Item.FindControl("hdnID");
                    ddlService.SelectedValue = ServiceID.Text;

                    ddlPlan.SelectedValue=PlanType.Text;
                    ddlArea.SelectedValue = Area.Text == "0" ? "01" : Area.Text;
                    ddlAreaToSqft.Visible = true;
                    if (AreaTo.Text == "0")
                    {
                        ddlAreaToSqft.Visible = false;
                    }
                    else
                    {
                        ddlAreaToSqft.Visible = true;
                        ddlAreaToSqft.SelectedValue = AreaTo.Text;
                    }
                    ddlArea.Enabled = false;
                    ddlAreaToSqft.Enabled = false;
                    ddlCategory.SelectedValue=Category.Text.ToUpper();
                    ddlCategory.Enabled = false;
                    txtuAmount.Text = Amount.Text;
                    txtUDiscount.Text = Discount.Text;
                    rbtLstVisitReq.SelectedIndex=Convert.ToInt32( visitrequired.Text=="Yes" ?0:1);
                    //txtarea.Text = Area.Text;
                  
                    ddlPlan.Enabled = false;
                    ddlService.Enabled = false;
                    dvunlimited.Visible = true;
                }
            }
        

       
        protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            ddlPlan.SelectedValue = "0";
            dvunlimited.Visible = false;
            //dvFixedmake.Visible = false;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ServicePlan.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ServicePlan.aspx");
        }

    }
}
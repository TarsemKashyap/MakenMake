using MakeNMake.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MakeNMake.UserControl
{
    public partial class AddOnServicesUserControl : System.Web.UI.UserControl
    {
       public Int64 CustomerID { get; set; }
        public Int64 CreatedBy { get; set; }
        public string EncryptdClientID { get; set; }
        public bool IsClient { get; set; }
        public event EventHandler buttonClick;

        protected void btnProceedToPayment_Click(object sender, EventArgs e)
        {
            buttonClick(sender, e);
            SaveUserServicedata();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        private void SaveUserServicedata()
        {
            int makeYourPlanQuantity = 0;

            if (RptAddonServices.Items.Count > 0)
            {
                int totalServices = 0;
                bool iSchecked = false;
                foreach (RepeaterItem item in RptAddonServices.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkService");
                    if (chk.Checked)
                    {
                        iSchecked = true; totalServices++;
                        // break;
                    }
                }
                if (iSchecked)
                {
                    if (totalServices < 13)
                    {
                        string services = string.Empty;
                        string plan = string.Empty;
                        string visitInfo = string.Empty;
                        bool iSAddOn = false;
                        foreach (RepeaterItem item in RptAddonServices.Items)
                        {
                            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                            {
                                CheckBox chk = (CheckBox)item.FindControl("chkService");
                                if (chk.Checked)
                                {
                                    HiddenField hdnID = (HiddenField)item.FindControl("hdnID");
                                    HiddenField hdnPlanID = (HiddenField)item.FindControl("hdnPlanID");
                                    HiddenField hdnplan = (HiddenField)item.FindControl("hdnplans");
                                    TextBox ddlQuant = (TextBox)item.FindControl("txtQuantity");
                                    HiddenField hdnUnlimitedPlanID = (HiddenField)item.FindControl("hdnUnlimitedPlanID");
                                    HiddenField hdnServiceType = (HiddenField)item.FindControl("hdnServiceType");
                                    //if (hdnplan.Value.ToLower() == "m")
                                    //{
                                    //    makeYourPlanQuantity += Convert.ToInt16(ddlQuant.SelectedValue);
                                    //}
                                    if (hdnplan.Value.ToLower() == "u" && hdnServiceType.Value.ToLower() == "a")
                                    {
                                        services += "<data><ID>" + hdnID.Value + "</ID><Type>A</Type><PlanID>" + hdnPlanID.Value + "</PlanID><Splan>" + hdnplan.Value + "</Splan><quantity>" + ddlQuant.Text + "</quantity></data>";
                                    }
                                    else if (hdnplan.Value.ToLower() == "u")
                                    {
                                        services += "<data><ID>" + hdnID.Value + "</ID><Type>A</Type><PlanID>" + hdnUnlimitedPlanID.Value + "</PlanID><Splan>" + hdnplan.Value + "</Splan><quantity>1</quantity></data>";
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(ddlQuant.Text) > 0)
                                        {
                                            services += "<data><ID>" + hdnID.Value + "</ID><Type>A</Type><PlanID>" + hdnPlanID.Value + "</PlanID><Splan>" + hdnplan.Value + "</Splan><quantity>" + ddlQuant.Text + "</quantity></data>";
                                        }
                                    }
                                    plan = hdnplan.Value;
                                }
                            }
                        }
                        Common objBakset = new Common();

                        if (plan.ToLower() == "m" && makeYourPlanQuantity < 6 && iSAddOn == true)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select minimun 6 calls, 2 from each service  to avail the Make your Own plan') ;", true);
                        }
                        else
                        {
                            int result = objBakset.AddServiceToBasket(CustomerID, CreatedBy, services, plan, 0, 0, 0);
                            if (result == 1)
                            {
                                if (IsClient)
                                {
                                    Response.Redirect("ProceedToPayment.aspx");
                                }
                                else
                                {
                                    Response.Redirect("ProceedToPayment.aspx?ClientID=" + EncryptdClientID);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Problem occurs while filling basket') ;", true);
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You can select maximum 12 services from Basic and AddOn') ;", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select atleast one  service') ;", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select atleast one  service') ;", true);
            }
        }
  
        protected void RptAddonServices_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnplans = (HiddenField)e.Item.FindControl("hdnplans");
                HiddenField hdnServiceType = (HiddenField)e.Item.FindControl("hdnServiceType");
                string plan = hdnplans.Value.Substring(0, 1).ToLower();
                if (plan == "u" && hdnServiceType.Value.ToLower() == "b")
                {
                    Control HeaderTemplate = RptAddonServices.Controls[0].Controls[0];
                    HtmlTableCell thunlimitedarea = HeaderTemplate.FindControl("unlimitedarea") as HtmlTableCell;
                    HtmlTableCell thunlimitedcategory = HeaderTemplate.FindControl("unlimitedcategory") as HtmlTableCell;
                    HtmlTableCell calls = HeaderTemplate.FindControl("calls") as HtmlTableCell;

                    HtmlTableCell tdArea = (HtmlTableCell)e.Item.FindControl("tdArea");
                    HtmlTableCell tdCategory = (HtmlTableCell)e.Item.FindControl("tdCategory");
                    HtmlTableCell tdcalls = (HtmlTableCell)e.Item.FindControl("tdcalls");
                    thunlimitedarea.Visible = true;
                    thunlimitedcategory.Visible = true;
                    calls.Visible = false;
                    tdcalls.Visible = false;
                    tdArea.Visible = true; CheckBox chk = (CheckBox)e.Item.FindControl("chkService");
                    tdCategory.Visible = true;
                    Label lblVisit = (Label)e.Item.FindControl("lblVisit");
                    if (lblVisit.Text.ToLower() == "yes")
                    {
                        chk.Enabled = false;
                        Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                        lblAmount.Visible = false;
                        LinkButton lnkBtnMore = (LinkButton)e.Item.FindControl("lnkBtnMore");
                        lnkBtnMore.Visible = true;
                    }

                    TextBox ddlQuantity = (TextBox)e.Item.FindControl("txtQuantity");
                    ddlQuantity.Text = "1";
                }
                else if (plan == "u" && hdnServiceType.Value.ToLower() == "a")
                {
                    Control HeaderTemplate = RptAddonServices.Controls[0].Controls[0];
                    HtmlTableCell thunlimitedarea = HeaderTemplate.FindControl("unlimitedarea") as HtmlTableCell;
                    HtmlTableCell thunlimitedcategory = HeaderTemplate.FindControl("unlimitedcategory") as HtmlTableCell;
                    HtmlTableCell calls = HeaderTemplate.FindControl("calls") as HtmlTableCell;
                    HtmlTableCell tdArea = (HtmlTableCell)e.Item.FindControl("tdArea");
                    HtmlTableCell tdCategory = (HtmlTableCell)e.Item.FindControl("tdCategory");
                    HtmlTableCell tdcalls = (HtmlTableCell)e.Item.FindControl("tdcalls");
                    thunlimitedarea.Visible = false;
                    thunlimitedcategory.Visible = false;
                    tdArea.Visible = false;
                    tdCategory.Visible = false;
                    calls.Visible = true;
                    tdcalls.Visible = true;
                    TextBox ddlQuantity = (TextBox)e.Item.FindControl("txtQuantity");
                    ddlQuantity.Text = "1";
                    Label lblVisit = (Label)e.Item.FindControl("lblVisit");
                    if (lblVisit.Text.ToLower() == "yes")
                    {
                        CheckBox chk = (CheckBox)e.Item.FindControl("chkService");
                        chk.Enabled = false;
                        Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                        lblAmount.Visible = false;
                        LinkButton lnkBtnMore = (LinkButton)e.Item.FindControl("lnkBtnMore");
                        lnkBtnMore.Visible = true;
                    }
                }
                else if (plan == "m")
                {
                    Control HeaderTemplate = RptAddonServices.Controls[0].Controls[0];
                    HtmlTableCell thunlimitedarea = HeaderTemplate.FindControl("unlimitedarea") as HtmlTableCell;
                    HtmlTableCell thunlimitedcategory = HeaderTemplate.FindControl("unlimitedcategory") as HtmlTableCell;
                    HtmlTableCell calls = HeaderTemplate.FindControl("calls") as HtmlTableCell;
                    HtmlTableCell tdArea = (HtmlTableCell)e.Item.FindControl("tdArea");
                    HtmlTableCell tdCategory = (HtmlTableCell)e.Item.FindControl("tdCategory");
                    HtmlTableCell tdcalls = (HtmlTableCell)e.Item.FindControl("tdcalls");
                    thunlimitedarea.Visible = false;
                    thunlimitedcategory.Visible = false;
                    tdArea.Visible = false;
                    tdCategory.Visible = false;
                    calls.Visible = true;
                    tdcalls.Visible = true;
                    TextBox ddlQuantity = (TextBox)e.Item.FindControl("txtQuantity");
                    //  ddlQuantity.Attributes.Add("onchange", "CheckAmount(this);");
                    ddlQuantity.Text = "1";
                    Label lblVisit = (Label)e.Item.FindControl("lblVisit");
                    if (lblVisit.Text.ToLower() == "yes")
                    {
                        CheckBox chk = (CheckBox)e.Item.FindControl("chkService");
                        chk.Enabled = false;
                        // chk.Attributes.Add("onclick", "AllowVisit()");

                        Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                        lblAmount.Visible = false;
                        LinkButton lnkBtnMore = (LinkButton)e.Item.FindControl("lnkBtnMore");
                        lnkBtnMore.Visible = true;
                    }
                }
                else
                {
                    Control HeaderTemplate = RptAddonServices.Controls[0].Controls[0];
                    HtmlTableCell thunlimitedarea = HeaderTemplate.FindControl("unlimitedarea") as HtmlTableCell;
                    HtmlTableCell thunlimitedcategory = HeaderTemplate.FindControl("unlimitedcategory") as HtmlTableCell;
                    HtmlTableCell calls = HeaderTemplate.FindControl("calls") as HtmlTableCell;
                    HtmlTableCell tdArea = (HtmlTableCell)e.Item.FindControl("tdArea");
                    HtmlTableCell tdCategory = (HtmlTableCell)e.Item.FindControl("tdCategory");
                    HtmlTableCell tdcalls = (HtmlTableCell)e.Item.FindControl("tdcalls");
                    calls.Visible = true;
                    tdcalls.Visible = true;
                    thunlimitedarea.Visible = false;
                    thunlimitedcategory.Visible = false;
                    tdArea.Visible = false;
                    tdCategory.Visible = false;
                    TextBox ddlQuantity = (TextBox)e.Item.FindControl("txtQuantity");
                    ddlQuantity.Text = "1";
                    Label lblVisit = (Label)e.Item.FindControl("lblVisit");
                    if (lblVisit.Text.ToLower() == "yes")
                    {
                        CheckBox chk = (CheckBox)e.Item.FindControl("chkService");
                        chk.Enabled = false;
                        //  chk.Attributes.Add("onclick", "AllowVisit()");

                        Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                        lblAmount.Visible = false;
                        LinkButton lnkBtnMore = (LinkButton)e.Item.FindControl("lnkBtnMore");
                        lnkBtnMore.Visible = true;
                    }
                }
            }
        }
        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPlan.SelectedValue != "0")
            {
                if (ddlcategory.SelectedValue == "0")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select service category') ;", true);
                }
                else
                {
                    dvaddon.Visible = true;
                    //dvaddonHeading.Visible = true;
                    Common obj = new Common();
                    lblAddOnmmsg.Text = string.Empty;
                    RptAddonServices.DataSource = null;
                    RptAddonServices.DataBind();
                    DataSet dt = obj.GetServicesForUser(ddlcategory.SelectedValue, ddlPlan.SelectedValue);

                    if (dt != null && dt.Tables[1].Rows.Count > 0)
                    {
                        dvSubmit.Visible = true;
                        RptAddonServices.DataSource = dt.Tables[1];
                        RptAddonServices.DataBind();
                    }
                    else
                    {
                        dvSubmit.Visible = false;
                        lblAddOnmmsg.Text = "No Add On Services are found regarding to that plan";
                    }
                }
            }
            else
            {
                dvaddon.Visible = false;
                dvSubmit.Visible = false;
                //dvaddonHeading.Visible = false;
                Common obj = new Common();
                lblAddOnmmsg.Text = string.Empty;
                RptAddonServices.DataSource = null;
                RptAddonServices.DataBind();
            }
        }
        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPlan.SelectedValue = "0";
        }

        protected void RptAddonServices_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "fillform")
            {
                string service =Utilities.EncryptDecrypt.Encript( Convert.ToString(e.CommandArgument));
                buttonClick(source, e);
                if (IsClient)
                {
                    Response.Redirect("VisitRequestForm.aspx?service="+service);
                }
                else
                {
                    Response.Redirect("VisitRequestForm.aspx?ClientID=" + EncryptdClientID + "&service=" + service); 
                }
            }
        }

        protected void RptService_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "fillform")
            {
                string service = Utilities.EncryptDecrypt.Encript(Convert.ToString(e.CommandArgument));
                buttonClick(source, e);
                if (IsClient)
                {
                    Response.Redirect("VisitRequestForm.aspx?service=" + service);
                }
                else
                {
                    Response.Redirect("VisitRequestForm.aspx?ClientID=" + EncryptdClientID + "&service=" + service);
                }
            }
        }

    }
}
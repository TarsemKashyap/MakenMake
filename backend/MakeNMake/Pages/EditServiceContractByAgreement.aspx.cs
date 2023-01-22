using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MakeNMake.Pages
{
    public partial class EditServiceContractByAgreement : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string[] ServiceContractData = Convert.ToString(Utilities.EncryptDecrypt.DecryptText(Request.QueryString.Get("ServiceContractData").ToString())).Split(':');
                    lblnumber.Text = ServiceContractData[2];
                    BindData(Convert.ToInt64(ServiceContractData[1]));
                    BindServices(ServiceContractData[3], ServiceContractData[4]);
                }
                catch
                {
                    Response.Redirect("EditServiceContract.aspx");
                }
            }
        }
        private void BindData(Int64 agreementID)
        {

            BL.BLConsumer objAdmin = new BL.BLConsumer();
            DataTable dt = objAdmin.GetClientService(agreementID);
            if (dt != null && dt.Rows.Count > 0)
            {
                Rptagreement.DataSource = dt;
                Rptagreement.DataBind();
            }
        }
        public void BindServices(string category,string plan )
        {
            dvbasic.Visible = true;
            dvaddon.Visible = true;
            dvaddonHeading.Visible = true;
            Common obj = new Common();
            lblBasicMsg.Text = string.Empty;
            lblAddOnmmsg.Text = string.Empty;
            RptService.DataSource = null;
            RptService.DataBind();
            RptAddonServices.DataSource = null;
            RptAddonServices.DataBind();
            DataSet dt = obj.GetServicesForUser(category, plan);

            if (plan.ToUpper() == "A")
            {
                if (dt != null && dt.Tables[0].Rows.Count > 0)
                {
                    dvSubmit.Visible = true;
                }
                else
                {
                    dvSubmit.Visible = false;
                }
                lblBasicMsg.Text = "No Basic Services are found regarding to that plan";
                if (dt != null && dt.Tables[0].Rows.Count > 0)
                {
                    RptAddonServices.DataSource = dt.Tables[0];
                    RptAddonServices.DataBind();
                }
                else
                {
                    lblAddOnmmsg.Text = "No Add On Services are found regarding to that plan";
                }
            }
            else
            {
                if (dt != null && (dt.Tables[0].Rows.Count > 0 || dt.Tables[1].Rows.Count > 0))
                {
                    dvSubmit.Visible = true;
                }
                else
                {
                    dvSubmit.Visible = false;
                }
                if (dt != null && dt.Tables[0].Rows.Count > 0)
                {
                    RptService.DataSource = dt.Tables[0];
                    RptService.DataBind();
                }
                else
                {
                    lblBasicMsg.Text = "No Basic Services are found regarding to that plan";
                }
                if (dt != null && dt.Tables[1].Rows.Count > 0)
                {
                    RptAddonServices.DataSource = dt.Tables[1];
                    RptAddonServices.DataBind();
                }
                else
                {
                    lblAddOnmmsg.Text = "No Add On Services are found regarding to that plan";
                }
            }

        }
        protected void Rptagreement_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblplan = (Label)e.Item.FindControl("lblplan");
                string plan = lblplan.Text.Substring(0, 1).ToLower();
                if (plan == "u")
                {
                    Control HeaderTemplate = Rptagreement.Controls[0].Controls[0];
                    HtmlTableCell thunlimitedarea = HeaderTemplate.FindControl("unlimitedarea") as HtmlTableCell;
                    HtmlTableCell thunlimitedcategory = HeaderTemplate.FindControl("unlimitedcategory") as HtmlTableCell;
                    HtmlTableCell tdArea = (HtmlTableCell)e.Item.FindControl("tdArea");
                    HtmlTableCell tdCategory = (HtmlTableCell)e.Item.FindControl("tdCategory");
                    thunlimitedarea.Visible = true;
                    thunlimitedcategory.Visible = true;
                    tdArea.Visible = true;
                    tdCategory.Visible = true;
                }
                else
                {
                    Control HeaderTemplate = Rptagreement.Controls[0].Controls[0];
                    HtmlTableCell thunlimitedarea = HeaderTemplate.FindControl("unlimitedarea") as HtmlTableCell;
                    HtmlTableCell thunlimitedcategory = HeaderTemplate.FindControl("unlimitedcategory") as HtmlTableCell;
                    HtmlTableCell tdArea = (HtmlTableCell)e.Item.FindControl("tdArea");
                    HtmlTableCell tdCategory = (HtmlTableCell)e.Item.FindControl("tdCategory");
                    thunlimitedarea.Visible = false;
                    thunlimitedcategory.Visible = false;
                    tdArea.Visible = false;
                    tdCategory.Visible = false;
                }
            }
        }

        protected void btnProceedToPayment_Click(object sender, EventArgs e)
        {
            int makeYourPlanQuantity = 0;

            if (RptService.Items.Count > 0 || RptAddonServices.Items.Count > 0)
            {
                int totalServices = 0;
                bool iSchecked = false;
                foreach (RepeaterItem item in RptService.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkService");
                    if (chk.Checked)
                    {
                        iSchecked = true; totalServices++;
                        //break;
                    }
                }
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
                        foreach (RepeaterItem item in RptService.Items)
                        {
                            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                            {
                                CheckBox chk = (CheckBox)item.FindControl("chkService");
                                if (chk.Checked)
                                {
                                    iSAddOn = true;
                                    HiddenField hdnID = (HiddenField)item.FindControl("hdnID");
                                    HiddenField hdnPlanID = (HiddenField)item.FindControl("hdnPlanID");
                                    HiddenField hdnplan = (HiddenField)item.FindControl("hdnplans");
                                    DropDownList ddlQuant = (DropDownList)item.FindControl("ddlQuantity");

                                    HiddenField hdnUnlimitedPlanID = (HiddenField)item.FindControl("hdnUnlimitedPlanID");
                                    if (hdnplan.Value.ToLower() == "m")
                                    {
                                        makeYourPlanQuantity += Convert.ToInt16(ddlQuant.SelectedValue);
                                    }

                                    if (hdnplan.Value.ToLower() == "u")
                                    {
                                        services += "<data><ID>" + hdnID.Value + "</ID><Type>B</Type><PlanID>" + hdnUnlimitedPlanID.Value + "</PlanID><Splan>" + hdnplan.Value + "</Splan><quantity>1</quantity></data>";
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(ddlQuant.SelectedValue) > 0)
                                        {
                                            services += "<data><ID>" + hdnID.Value + "</ID><Type>B</Type><PlanID>" + hdnPlanID.Value + "</PlanID><Splan>" + hdnplan.Value + "</Splan><quantity>" + ddlQuant.SelectedValue + "</quantity></data>";
                                        }
                                    }
                                    plan = hdnplan.Value;
                                }
                            }
                        }
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
                            string[] ServiceContractData = Convert.ToString(Utilities.EncryptDecrypt.DecryptText(Request.QueryString.Get("ServiceContractData").ToString())).Split(':');
                            int result = objBakset.EditServiceContractToBasket(Convert.ToInt64(ServiceContractData[0]), Convert.ToInt64(Session[Constant.Session.AdminSession]), services, plan, 0, 0, 0);
                            if (result == 1)
                            {
                                Response.Redirect("SaveConsumerContract.aspx?ConsumerData=" + Utilities.EncryptDecrypt.Encript(ServiceContractData[0]));
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

        protected void RptService_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnplans = (HiddenField)e.Item.FindControl("hdnplans");
                string plan = hdnplans.Value.Substring(0, 1).ToLower();
                if (plan == "u")
                {

                    Control HeaderTemplate = RptService.Controls[0].Controls[0];
                    HtmlTableCell thunlimitedarea = HeaderTemplate.FindControl("unlimitedarea") as HtmlTableCell;
                    HtmlTableCell thunlimitedcategory = HeaderTemplate.FindControl("unlimitedcategory") as HtmlTableCell;
                    HtmlTableCell calls = HeaderTemplate.FindControl("calls") as HtmlTableCell;
                    HtmlTableCell tdcalls = (HtmlTableCell)e.Item.FindControl("tdcalls");
                    HtmlTableCell tdArea = (HtmlTableCell)e.Item.FindControl("tdArea");
                    HtmlTableCell tdCategory = (HtmlTableCell)e.Item.FindControl("tdCategory");
                    thunlimitedarea.Visible = true;
                    thunlimitedcategory.Visible = true;
                    tdArea.Visible = true;
                    tdCategory.Visible = true;
                    calls.Visible = false;
                    tdcalls.Visible = false;
                    CheckBox chk = (CheckBox)e.Item.FindControl("chkService");
                    //chk.Attributes.Add("onclick", "CheckSameIds(this)");
                    HiddenField hdnID = (HiddenField)e.Item.FindControl("hdnID");
                    Label lblcategory = (Label)e.Item.FindControl("lblUCategory");
                    chk.Attributes.Add("serviceid", lblcategory.Text.ToLower());
                    Label lblVisit = (Label)e.Item.FindControl("lblVisit");
                    chk.AutoPostBack = true;
                    if (lblVisit.Text.ToLower() == "yes")
                    {
                        chk.Enabled = false;
                        //chk.Attributes.Add("onclick", "AllowVisit()");
                        Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                        lblAmount.Visible = false;
                        LinkButton lnkBtnMore = (LinkButton)e.Item.FindControl("lnkBtnMore");
                        lnkBtnMore.Visible = true;
                    }
                    //lblAmount
                    //lnkBtnMore
                }
                else if (plan == "m")
                {
                    Control HeaderTemplate = RptService.Controls[0].Controls[0];
                    HtmlTableCell thunlimitedarea = HeaderTemplate.FindControl("unlimitedarea") as HtmlTableCell;
                    HtmlTableCell thunlimitedcategory = HeaderTemplate.FindControl("unlimitedcategory") as HtmlTableCell;
                    HtmlTableCell tdArea = (HtmlTableCell)e.Item.FindControl("tdArea");
                    HtmlTableCell tdCategory = (HtmlTableCell)e.Item.FindControl("tdCategory");
                    HtmlTableCell calls = HeaderTemplate.FindControl("calls") as HtmlTableCell;
                    HtmlTableCell tdcalls = (HtmlTableCell)e.Item.FindControl("tdcalls");
                    thunlimitedarea.Visible = false;
                    thunlimitedcategory.Visible = false;
                    tdArea.Visible = false;
                    tdCategory.Visible = false;
                    calls.Visible = true;
                    tdcalls.Visible = true;

                    DropDownList ddlQuantity = (DropDownList)e.Item.FindControl("ddlQuantity");
                    ddlQuantity.Attributes.Add("onchange", "CheckAmount(this);");
                    ddlQuantity.SelectedIndex = 1;
                    Label lblVisit = (Label)e.Item.FindControl("lblVisit");
                    if (lblVisit.Text.ToLower() == "yes")
                    {
                        CheckBox chk = (CheckBox)e.Item.FindControl("chkService");
                        chk.Enabled = false;
                        //chk.Attributes.Add("onclick", "AllowVisit()");

                        Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                        lblAmount.Visible = false;
                        LinkButton lnkBtnMore = (LinkButton)e.Item.FindControl("lnkBtnMore");
                        lnkBtnMore.Visible = true;
                    }
                }
                else
                {
                    Control HeaderTemplate = RptService.Controls[0].Controls[0];
                    HtmlTableCell thunlimitedarea = HeaderTemplate.FindControl("unlimitedarea") as HtmlTableCell;
                    HtmlTableCell thunlimitedcategory = HeaderTemplate.FindControl("unlimitedcategory") as HtmlTableCell;
                    HtmlTableCell tdArea = (HtmlTableCell)e.Item.FindControl("tdArea");
                    HtmlTableCell tdCategory = (HtmlTableCell)e.Item.FindControl("tdCategory");
                    HtmlTableCell calls = HeaderTemplate.FindControl("calls") as HtmlTableCell;
                    HtmlTableCell tdcalls = (HtmlTableCell)e.Item.FindControl("tdcalls");
                    thunlimitedarea.Visible = false;
                    thunlimitedcategory.Visible = false;
                    tdArea.Visible = false;
                    tdCategory.Visible = false;
                    calls.Visible = true;
                    tdcalls.Visible = true;

                    DropDownList ddlQuantity = (DropDownList)e.Item.FindControl("ddlQuantity");
                    ddlQuantity.SelectedValue = "1";
                    Label lblVisit = (Label)e.Item.FindControl("lblVisit");
                    if (lblVisit.Text.ToLower() == "yes")
                    {
                        CheckBox chk = (CheckBox)e.Item.FindControl("chkService");
                        chk.Enabled = false;
                        //chk.Attributes.Add("onclick", "AllowVisit()");
                        Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                        lblAmount.Visible = false;
                        LinkButton lnkBtnMore = (LinkButton)e.Item.FindControl("lnkBtnMore");
                        lnkBtnMore.Visible = true;
                    }
                }
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
                    //DropDownList ddlQuantity = (DropDownList)e.Item.FindControl("ddlQuantity");
                    //ddlQuantity.SelectedIndex = 1;
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
                    //DropDownList ddlQuantity = (DropDownList)e.Item.FindControl("ddlQuantity");
                    //ddlQuantity.SelectedValue = "1";

                    TextBox ddlQuantity = (TextBox)e.Item.FindControl("txtQuantity");
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
                    //DropDownList ddlQuantity = (DropDownList)e.Item.FindControl("ddlQuantity");
                    //ddlQuantity.SelectedValue = "1";

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

        protected void RptAddonServices_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "fillform")
            {
                string[] ServiceContractData = Convert.ToString(Utilities.EncryptDecrypt.DecryptText(Request.QueryString.Get("ServiceContractData").ToString())).Split(':');
                string service = Utilities.EncryptDecrypt.Encript(Convert.ToString(e.CommandArgument));
                Response.Redirect("VisitRequestForm.aspx?ClientID=" + Utilities.EncryptDecrypt.Encript(ServiceContractData[0]) + "&service=" + service);
            }
        }

        protected void RptService_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "fillform")
            {
                string[] ServiceContractData = Convert.ToString(Utilities.EncryptDecrypt.DecryptText(Request.QueryString.Get("ServiceContractData").ToString())).Split(':');
                string service = Utilities.EncryptDecrypt.Encript(Convert.ToString(e.CommandArgument));
                Response.Redirect("VisitRequestForm.aspx?ClientID=" + Utilities.EncryptDecrypt.Encript(ServiceContractData[0]) + "&service=" + service);
            }
        }

        protected void chkService_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            string category = chk.Attributes["dataService"].ToLower();
            HiddenField hdnplans = (HiddenField)chk.FindControl("hdnplans");
            if (hdnplans.Value.ToLower() == "u")
            {
                foreach (RepeaterItem item in RptService.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        CheckBox otherChk = (CheckBox)item.FindControl("chkService");
                        string chkcategory = otherChk.Attributes["dataService"].ToLower();
                        Label lblVisit = (Label)item.FindControl("lblVisit");
                        if (lblVisit.Text.ToLower() == "yes")
                        {
                            otherChk.Checked = false;
                            otherChk.Enabled = false;
                        }
                        else
                        {
                            if (category == chkcategory)
                            {
                                otherChk.Checked = true;
                                otherChk.Enabled = false;
                            }
                            else
                            {
                                otherChk.Checked = false;
                                otherChk.Enabled = true;
                            }
                        }
                    }
                }
            }

        }
    }
}
using MakeNMake.CommomFunctions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace MakeNMake.Pages
{
    public partial class AssessmentForm : System.Web.UI.Page
    {
        Int64 requestID;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    requestID = Convert.ToInt64(Utilities.EncryptDecrypt.DecryptText(Request.QueryString["RequestData"]));                    
                    lblhreqserviceID.Text = requestID.ToString();
                    BindAssessmentFields(requestID);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx");
            }
        }
        private void BindAssessmentFields(Int64 requestID)
        {
            BL.BLServiceEngineer obj = new BL.BLServiceEngineer();
            DataTable dt = obj.GetAssesmentForm(requestID);
            if (dt != null && dt.Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                dvPanel.Visible = true;
                dvPanel.Controls.Clear();
                int count = 55;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(Convert.ToString(dt.Rows[i]["AssessProperties"]));
                        count += 230;
                        RootObject results;
                        var rows = xmlDoc.SelectNodes("//a");
                        if (rows.Count == 1)
                        {
                            XmlNode RootNode = xmlDoc.SelectSingleNode("//MyRoot");
                            XmlNode newElem = xmlDoc.CreateNode("element", "a", "");
                            newElem.InnerText = "";
                            XmlNode newElemparent = xmlDoc.CreateNode("element", "PropertyID", "");
                            newElem.InnerText = "";
                            XmlNode newElemchild = xmlDoc.CreateNode("element", "PropertyName", "");
                            newElem.InnerText = "";
                            newElem.AppendChild(newElemparent);
                            newElem.AppendChild(newElemchild);
                            RootNode.AppendChild(newElem);
                            xmlDoc.InnerXml = xmlDoc.InnerXml.Replace(",null]", "]");
                            string jsonText = JsonConvert.SerializeXmlNode(xmlDoc);
                            results = JsonConvert.DeserializeObject<RootObject>(jsonText);
                        }
                        else
                        {
                            string jsonText = JsonConvert.SerializeXmlNode(xmlDoc);
                            results = JsonConvert.DeserializeObject<RootObject>(jsonText);
                        }
                        int account = 40;
                        string ServiceName = Convert.ToString(dt.Rows[i]["ServiceName"]);
                        HtmlGenericControl dvServiceouter = new HtmlGenericControl("div");
                        dvServiceouter.Attributes.Add("class", "panel panel-success linkBottom");
                      
                        HtmlGenericControl dvHeading = new HtmlGenericControl("div");
                        dvHeading.Attributes.Add("class", "panel-heading");

                        HtmlGenericControl h3 = new HtmlGenericControl("h3");
                        h3.Attributes.Add("class", "panel-title paneltitle");
                        h3.InnerText = ServiceName;

                        dvHeading.Controls.Add(h3);

                        HtmlGenericControl dvBody = new HtmlGenericControl("div");
                        dvBody.Attributes.Add("class", "panel-body");
                        int j = 0;
                        foreach (var items in results.MyRoot.a)
                        {
                            if (items.PropertyID != null)
                            {
                                j += 1;
                                count += 10;
                                HtmlGenericControl dvouter = new HtmlGenericControl("div");
                                dvouter.Attributes.Add("class", "col-sm-6 text-left linkBottom");
                                HtmlGenericControl dvinner = new HtmlGenericControl("div");
                                dvinner.Attributes.Add("class", "input-group input-group-sm");
                                HtmlGenericControl span = new HtmlGenericControl("span");
                                span.Attributes.Add("class", "input-group-addon");
                                span.InnerText = items.PropertyName;// Convert.ToString(dt.Rows[i]["PropertyName"]);
                                TextBox txt = new TextBox();
                                string id = "txt" + items.PropertyID;//Convert.ToString(dt.Rows[i]["PropertyID"]);
                                txt.ID = id;
                                txt.CssClass = "form-control";
                                txt.Attributes.Add("placeholder", items.Validation);
                                txt.Attributes.Add("propertyid", items.PropertyID);
                                dvinner.Controls.Add(span);
                                dvinner.Controls.Add(txt);
                                dvouter.Controls.Add(dvinner);

                                if ((string.IsNullOrEmpty(items.Validation)) || items.Validation == "")
                                {
                                }
                                else
                                {
                                    RequiredFieldValidator requiredField = new RequiredFieldValidator();
                                    requiredField.ControlToValidate = id;
                                    requiredField.ValidationGroup = "fillform";
                                    requiredField.ForeColor = System.Drawing.Color.Red;
                                    requiredField.ErrorMessage = "Enter " + items.PropertyName;
                                    dvouter.Controls.Add(requiredField);
                                }
                                dvBody.Controls.Add(dvouter);
                            }
                        }
                        if (j == 1)
                        {
                            account =(account)+ j * 68;
                        }
                        else
                        {
                            account += (account) + j * 32;
                        }
                        dvServiceouter.Attributes.Add("style", "clear:both;height:" + account + "px");
                        dvServiceouter.Controls.Add(dvHeading);
                        dvServiceouter.Controls.Add(dvBody);
                        dvPanel.Controls.Add(dvServiceouter);
                        account = 0;
                }
                outerdv.Attributes.Add("style", "height:" + count + "px");
                allbuttons.Attributes.Add("style", "margin-top:" + (count-150) + "px");
            }   
            else
            {
                btnSubmit.Visible = false;
                dvPanel.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No Assessment fields, pls contact administrator') ;", true);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestData.aspx");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string formData = Server.HtmlDecode(hdndata.Value);
                lblhreqserviceID.Text = requestID.ToString();
                BL.BLServiceEngineer obj = new BL.BLServiceEngineer();
                requestID = Convert.ToInt64(Utilities.EncryptDecrypt.DecryptText(Request.QueryString["RequestData"]));                    
                Int64 result = obj.AddAssesmentForm(requestID, 0, Convert.ToInt64(Session[Constant.Session.AdminSession]), formData, txtRemarks.Text);
                if (result >0)
                {
                    hdndata.Value = string.Empty;
                    Response.Redirect("RequestData.aspx", false);
                }
                else if (result == -99)
                {
                    BindAssessmentFields(requestID);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Some error occurs ,pls try  again') ;", true);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx", false);
            }
        }
    }

    public class A
    {
        public string PropertyID { get; set; }
        public string PropertyName { get; set; }
        public string Validation { get; set; }
    }

    public class MyRoot
    {
        public List<A> a { get; set; }
    }

    public class RootObject
    {
        public MyRoot MyRoot { get; set; }
    }
}
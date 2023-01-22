using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NLog;

namespace MakeNMake.Pages
{
    public partial class Quotation : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Int64 RequestID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RequestID = Convert.ToInt64(Request.QueryString["RequestID"]);
                BindQuoationData(RequestID);
            }
        }
        private void BindQuoationData(Int64 RequestID)
        {
            BL.BLServiceEngineer obj=new BL.BLServiceEngineer();
            DataSet ds = obj.GetAssesmentData(RequestID);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                txtEngineerRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["EngineerRemarks"]);
                txtService.Text = Convert.ToString(ds.Tables[0].Rows[0]["ServiceName"]);
                RptAssesmentForm.DataSource = ds.Tables[1];
                RptAssesmentForm.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BL.BLAdmin obj = new BL.BLAdmin();
            int result = obj.AddQuotation(Convert.ToInt64(Request.QueryString["RequestID"]), Convert.ToInt64(Session[Constant.Session.AdminSession]), Convert.ToDecimal(txtPrice.Text), txtQuote.Text, txtConsumable.Text,"", txtactivation.Text);
            if (result > 0)
            {
                Int64 userID = Convert.ToInt64(Request.QueryString["ConsumerID"]);

                Common objCommon = new Common();
                DataTable dt = objCommon.GetUserInfoByID(userID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    try
                    {
                        string body = PopulateBody(Convert.ToString(dt.Rows[0]["FirstName"]), result.ToString(), txtQuote.Text, txtService.Text, txtPrice.Text, txtConsumable.Text, "", txtactivation.Text, DateTime.Now.ToString());
                        MEmail.SendGMail(Convert.ToString(dt.Rows[0]["Emailid"]), "Quotation Make 'N' Make", body, "");
                        Response.Redirect("CommercialRequest.aspx",false);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(logger.Name + ":" + ex.Message);
                        Response.Redirect("~/Error.aspx");
                       
                    }
                }
            }
            else if (result == -99)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You already send quote to this client for this request id') ;", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CommercialRequest.aspx",false);
        }
        private string PopulateBody(string userName, string number, string Titile, string Plan, string price,string consumble,string mode,string activationtime,string date)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(ReadConfig.QuotationTemplate))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{Number}", number);
            body = body.Replace("{Titile}", Titile);
            body = body.Replace("{PlanService}", Plan);
            body = body.Replace("{Consumables}", consumble);
            body = body.Replace("{mode}", mode);
            body = body.Replace("{time}", activationtime);
            body = body.Replace("{Price}", price);
            body = body.Replace("{Date}", date);
            return body;
        }
    }
}
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using MakeNMake.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.tool.xml;
using MakeNMake.CommomFunctions;
using NLog;

namespace MakeNMake.Pages
{
    public partial class ClientInvoice : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
            }
        }

        private void binddata()
        {
            BLConsumer obj = new BLConsumer();
            DataTable dt = obj.GetInvoiceByUserID(Convert.ToInt64(Request.QueryString["UserID"]));
            if (dt != null && dt.Rows.Count > 0)
            {
                lblName.InnerText = Convert.ToString(dt.Rows[0]["name"]);
                lblUserID.InnerText = Convert.ToString(dt.Rows[0]["userid"]);
                lblEmailID.InnerText = Convert.ToString(dt.Rows[0]["emailid"]);

                txtEmailID.Text = Convert.ToString(dt.Rows[0]["emailid"]);
                txtEmailID.Enabled = false;

                lblAgreementNumber.InnerText = Convert.ToString(dt.Rows[0]["AgreementNumber"]);
                lblInvoice.InnerText = Convert.ToString(dt.Rows[0]["invoicenumber"]);
                lblMobileNo.InnerText = Convert.ToString(dt.Rows[0]["mobilenumber"]);
                lblAddress.InnerText = Convert.ToString(dt.Rows[0]["currentaddress"]);
                if (lblAddress.InnerText == "")
                {
                    lblAddress.InnerText = "Not Available";
                }
                lblStartDate.InnerText = Convert.ToString(dt.Rows[0]["Invoicedate"]);
                lblExpirydate.InnerText = Convert.ToString(dt.Rows[0]["Expirationdate"]);
                lblAmount.InnerText = "Rs. " + Convert.ToString(dt.Rows[0]["Amount"]);
                lblAmountInclusive.InnerText = "Rs. " + Convert.ToString(dt.Rows[0]["Amount"]);
                lblServiceCall.InnerText = Convert.ToString(dt.Rows[0]["Services"]);
                lblServicePlan.InnerText = Convert.ToString(dt.Rows[0]["previousPlan"]);
                //lblAddon.InnerText = Convert.ToString(dt.Rows[0][""]);
            }
            else
            {
                Response.Redirect("~/Error.aspx");
            }
        }
        protected void btnSavePdf_Click(object sender, EventArgs e)
        {
            panelemail.Visible = false;

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {

                    string fileName = System.DateTime.Now.ToString("yyyyMMddHHmmssff");
                    pnlInvoiceData.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());

                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";

                    Response.AddHeader("content-disposition", "attachment; filename= " + fileName + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }

        public void downloadPdf()
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=TestPage.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            this.Page.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.End();

            //using (StringWriter sw = new StringWriter())
            //{
            //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            //    {


            //        string fileName = System.DateTime.Now.ToString("yyyyMMddHHmmssff");
            //        pnlInvoiceData.RenderControl(hw);
            //        StringReader sr = new StringReader(sw.ToString());

            //        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //        pdfDoc.Open();
            //        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //       // HTMLWorker.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            //       // pdfDoc.Close();
            //        Response.ContentType = "application/pdf";

            //        Response.AddHeader("content-disposition", "attachment; filename= " + fileName + ".pdf");
            //        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //        //Response.Write(pdfDoc);
            //        //Response.End();

            //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //        pdfDoc.Open();
            //        htmlparser.Parse(sr);
            //        pdfDoc.Close();
            //        Response.Write(pdfDoc);
            //        Response.End();
            //    }
            //}
        }


        protected void btnEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtEmailID.Text) || txtEmailID.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Problem occurs') ;", true);
                }
                else
                {
                    string fileName = System.DateTime.Now.ToString("yyyyMMddHHmmssff");

                    string physicalPath = ReadConfig.PDfDownloadFiles + "/Files/" + fileName + ".pdf";

                    using (StringWriter sw = new StringWriter())
                    {
                        using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                        {
                            pnlInvoiceData.RenderControl(hw);
                            StringReader sr = new StringReader(sw.ToString());
                            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(physicalPath, FileMode.Create));
                            pdfDoc.Open();
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                            pdfDoc.Close();
                            MEmail.SendGMail(txtEmailID.Text, "Please Find the Attached File", "", physicalPath);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully send .Please check your mail.') ;", true);
                           // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "closedialog();", true);
                        }
                    }
                   

                }
            }
            catch (Exception ex)
            {
                logger.Error(logger.Name + ":" + ex.Message);
            }
        }

        protected void btnEmailSent_Click(object sender, EventArgs e)
        {
            panelemail.Visible = true;
            txtEmailID.Focus();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "PrintPanel();", true);
        }

        protected void chkTerms_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTerms.Checked)
            {
                sendingdv.Visible = true;
            }
            else
            {
                sendingdv.Visible = false;
            }
        }

    }
}

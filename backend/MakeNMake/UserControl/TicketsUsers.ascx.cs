using MakeNMake.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Globalization;

namespace MakeNMake.UserControl
{
    public partial class TicketsUsers : System.Web.UI.UserControl
    {
        BL.BLAdmin objAdmin = new BL.BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                BindData();
                BindZones();
                    
            }
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.btnexcelreport);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.btnPdfreport);
        }
        public void BindZones()
        {
            BLAdmin addUser = new BLAdmin();
            addUser.GetZones(ddlZone);
           // ddlSubZone.Items.Insert(0, "0");
            ddlSubZone.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Subzone--", "0"));
        }
        DataTable GetBindData(int currentpage, string UserName)
        {
            DataTable dtable = objAdmin.GetAllTicketsReport(currentpage, UserName);
            return dtable;
        }
        DataTable GetBindDataFilterbyDate(int currentpage, string UserName,DateTime fromdate,DateTime todate)
        {
            DataTable dtable = objAdmin.GetAllTicketsReportFIlterbydate(currentpage, UserName,fromdate,todate);
            return dtable;
        }
        protected void RptTickets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            BLAdmin addUser = new BLAdmin();
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                Label lbcategoryid = (Label)e.Item.FindControl("lbCategoryid");
                Label lbplansid = (Label)e.Item.FindControl("lbplanid");

                Label lbcategory = (Label)e.Item.FindControl("lbcatgoryname");
                Label lbplans = (Label)e.Item.FindControl("lbplanname");

                Label hdzoneid = (Label)e.Item.FindControl("hdnZone");
                Label hdsubzneid = (Label)e.Item.FindControl("hdnSubZone");
                Label lbzone = (Label)e.Item.FindControl("lblZonename");
                Label lbsubzone = (Label)e.Item.FindControl("lblSubzone");

                if (lbcategoryid.Text == "C")
                {
                    lbcategory.Text = "Commercial";
                }
                else
                {
                    lbcategory.Text = "Domestic";
                }
                if (lbplansid.Text == "U")
                {
                    lbplans.Text = "Unlimited";
                }
                else if (lbplansid.Text == "M")
                {
                    lbplans.Text = "Make your Plan";

                }
                else
                {
                    lbplans.Text = "Flexi";
                }
                if (hdzoneid.Text != null)
                {
                    DataTable dt = addUser.getZonenameByid(Convert.ToInt32(hdzoneid.Text));
                    if (dt.Rows.Count > 0)
                    {
                        lbzone.Text = Convert.ToString(dt.Rows[0]["ZoneName"]);
                    }
                }
                if (hdsubzneid.Text != null)
                {
                    DataTable dtsubzone = addUser.getSubZonenameByid(Convert.ToInt32(hdsubzneid.Text));
                    if (dtsubzone.Rows.Count > 0)
                    {
                        lbsubzone.Text = Convert.ToString(dtsubzone.Rows[0]["DistrictName"]);
                    }
                }
            }
        }
        
        protected void btnexcelreport_Click1(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=RepeaterExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            RptTickets.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        private int serachbindfilterbydate(string text)
        {
            pgsource.AllowPaging = true;
            CurrentPage = 0;
            pgsource.CurrentPageIndex = CurrentPage;
            string dobfrom = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string dobto = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            DataTable dt = GetBindDataFilterbyDate(CurrentPage, text, Convert.ToDateTime(dobfrom),Convert.ToDateTime(dobto));
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;
            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];
            RptTickets.DataSource = dt;
            RptTickets.DataBind();
            ViewState["DataTable"] = dt;
            doPaging();
            RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            return (Convert.ToInt32(dt.Rows.Count));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtfromdate.Text != "")
            {
                CurrentPage = 0;
                int x = 0;
                if (ddlZone.SelectedValue != "0")
                {
                    x = serachbindfilterbydate(ddlZone.SelectedItem.Text);
                }
                else if (ddlSubZone.SelectedValue != "0")
                {
                    x = serachbindfilterbydate(ddlSubZone.SelectedItem.Text);
                }
                else
                {
                    x = serachbindfilterbydate("");
                }
                if (x != 0)
                {
                    tblpaging.Visible = true;
                    divTicketstatus.Visible = true;
                }
                else
                {
                    tblpaging.Visible = false;
                    divTicketstatus.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "PageReload()", true);
                    txtfromdate.Text = null;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Client Name,Engineer Name,Ticket Id or Ticket Type!') ;", true);
            }
        }
        private int BindData()
        {

            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetBindData(CurrentPage, txtfromdate.Text);
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
                RptTickets.DataSource = dt;
                RptTickets.DataBind();

                lblMsg.Text = string.Empty;
            }
            else
            {
                lblMsg.Text = "No Tickets";
            }

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
                BindData();
            }
        }
        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            BindData();
        }
        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            BindData();
        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindData();
            }
            else
            {
                CurrentPage = 0;
                BindData();

            }

        }
        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindData();
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                BindData();
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
        protected void btnPdfreport_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            RptTickets.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlZone.SelectedValue != "0")
            {
                BindSubZones(Convert.ToInt32(ddlZone.SelectedValue));
                int x = serachbind(ddlZone.SelectedItem.Text);
                if (x != 0)
                {
                    pgsource.CurrentPageIndex = CurrentPage;
                }
                else
                {
                  
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "PageReload()", true);
                    txtfromdate.Text = null;
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "callingMYFunction", "rdFunction() ;", true);

            }
            else
            {
                ddlSubZone.Items.Clear();
            }
        }
        public void BindSubZones(int ZoneID)
        {
            BLAdmin addUser = new BLAdmin();
            ddlSubZone.Items.Clear();
            addUser.GetSubZoneDistrict(ddlSubZone, ZoneID);
        }
        private int serachbind(string text)
        {
            pgsource.AllowPaging = true;
           
            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetBindData(CurrentPage, text);
            if (dt != null && dt.Rows.Count > 0)
            {
                tblpaging.Visible = true;
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;
            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];
            RptTickets.DataSource = dt;
            RptTickets.DataBind();
            ViewState["DataTable"] = dt;
            doPaging();
            RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            return (Convert.ToInt32(dt.Rows.Count));
        }
        protected void ddlSubZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubZone.SelectedValue != "0")
            {
                // BindSubZones(Convert.ToInt32(ddlZone.SelectedValue));
                int x = serachbind(ddlSubZone.SelectedItem.Text);
                if (x != 0)
                {
                    tblpaging.Visible = true;
                    divTicketstatus.Visible = true;
                }
                else
                {
                    tblpaging.Visible = false;
                    divTicketstatus.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "PageReload()", true);
                    txtfromdate.Text = null;
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "callingMYFunction", "rdFunction() ;", true);

            }

        }

    }
}
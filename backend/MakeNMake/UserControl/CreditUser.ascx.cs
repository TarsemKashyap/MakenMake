using AjaxControlToolkit;
using MakeNMake.BL;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace MakeNMake.UserControl
{
    public partial class AddCredit : System.Web.UI.UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        BLAdmin addUser = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        int pagesize;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDataList();
                BindZones();
            }
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.btnexcelreport);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.btnPdfreport);
        }
        public void BindZones()
        {
            addUser.GetZones(ddlZone);
            ddlSubZone.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Subzone--", "0"));
        }
        protected void ddlIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindDataList();
        }
        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            BindDataList();
        }
        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            BindDataList();
        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindDataList();
            }
            else
            {
                CurrentPage = 0;
                BindDataList();

            }

        }
        protected void RepeaterPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("newpage"))
            {
                CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
                BindDataList();
            }
        }
        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                BindDataList();
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                BindDataList();
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
        private int BindDataList()
        {
            pgsource.AllowPaging = true;
            if (ddlIndex.SelectedIndex == -1 || ddlIndex.SelectedIndex == 0)
            {
                pgsource.PageSize = 10;
                pagesize = pgsource.PageSize;
            }
            else
            {
                pgsource.PageSize = Convert.ToInt32(ddlIndex.SelectedItem.Value);
                pagesize = pgsource.PageSize;
            }
            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetData(pagesize, CurrentPage, txtfromdate.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / pagesize));
            }
            pgsource.DataSource = dt.DefaultView;
            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];
            RptAllUser.DataSource = dt;
            RptAllUser.DataBind();
            ViewState["DataTable"] = dt;
            doPaging();
            RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            return (Convert.ToInt32(dt.Rows.Count));
        }
        private int serachbind(string text)
        {
            pgsource.AllowPaging = true;
            if (ddlIndex.SelectedIndex == -1 || ddlIndex.SelectedIndex == 0)
            {
                pgsource.PageSize = 10;
                pagesize = pgsource.PageSize;
            }
            else
            {
                pgsource.PageSize = Convert.ToInt32(ddlIndex.SelectedItem.Value);
                pagesize = pgsource.PageSize;
            }
            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetData(pagesize, CurrentPage, text);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / pagesize));
            }
            pgsource.DataSource = dt.DefaultView;
            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];
            RptAllUser.DataSource = dt;
            RptAllUser.DataBind();
            ViewState["DataTable"] = dt;
            doPaging();
            RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            return (Convert.ToInt32(dt.Rows.Count));
        }
        private int serachbindfilterbydate(string text)
        {
            pgsource.AllowPaging = true;
            if (ddlIndex.SelectedIndex == -1 || ddlIndex.SelectedIndex == 0)
            {
                pgsource.PageSize = 10;
                pagesize = pgsource.PageSize;
            }
            else
            {
                pgsource.PageSize = Convert.ToInt32(ddlIndex.SelectedItem.Value);
                pagesize = pgsource.PageSize;
            }
            pgsource.CurrentPageIndex = CurrentPage;
            string dobfrom = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string dobto = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            DataTable dt = GetDataFilterDate(pagesize, CurrentPage, text, Convert.ToDateTime(dobto), Convert.ToDateTime(dobfrom));
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / pagesize));
            }
            pgsource.DataSource = dt.DefaultView;
            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];
            RptAllUser.DataSource = dt;
            RptAllUser.DataBind();
            ViewState["DataTable"] = dt;
            doPaging();
            RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            return (Convert.ToInt32(dt.Rows.Count));
        }

        DataTable GetData(int pagesize, int currentpage, string UserName)
        {
            DataTable dtable = addUser.Getuserreport(pagesize, currentpage, UserName);
            return dtable;
        }
        DataTable GetDataFilterDate(int pagesize, int currentpage, string UserName, DateTime todate, DateTime fromdate)
        {
            DataTable dtable = addUser.GetuserreportFilterbydate(pagesize, currentpage, UserName, fromdate, todate);
            return dtable;
        }
        private void doPaging()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageText");

            //Assign First Index starts from which number in paging data list
            findex = CurrentPage - 5;

            //Set Last index value if current page less than 5 then last index added "5" values to the Current page else it set "10" for last page number
            if (CurrentPage > 5)
            {
                lindex = CurrentPage + 5;
            }
            else
            {
                lindex = 10;
            }

            //Check last page is greater than total page then reduced it to total no. of page is last index
            if (lindex > Convert.ToInt32(ViewState["totpage"]))
            {
                lindex = Convert.ToInt32(ViewState["totpage"]);
                findex = lindex - 10;
            }

            if (findex < 0)
            {
                findex = 0;
            }

            //Now creating page number based on above first and last page index
            for (int i = findex; i < lindex; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            //Finally bind it page numbers in to the Paging DataList "RepeaterPaging"
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

        protected void RptAllUser_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label hdzoneid = (Label)e.Item.FindControl("hdnZone");
                Label hdsubzneid = (Label)e.Item.FindControl("hdnSubZone");
                Label lbzone = (Label)e.Item.FindControl("lblZonename");
                Label lbsubzone = (Label)e.Item.FindControl("lblSubzone");
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

        protected void btnexcelreport_Click(object sender, ImageClickEventArgs e)
        {



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
                    tblPaging.Visible = true;
                    divClientList.Visible = true;
                }
                else
                {
                    tblPaging.Visible = false;
                    divClientList.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "PageReload()", true);
                    txtfromdate.Text = null;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please Enter the Name Or Mobile Number!') ;", true);
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
            RptAllUser.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            txtfromdate.Text = string.Empty;
            CurrentPage = 0;
            BindZones();
            BindDataList();
        }
        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlZone.SelectedValue != "0")
            {
                BindSubZones(Convert.ToInt32(ddlZone.SelectedValue));
                int x = serachbind(ddlZone.SelectedItem.Text);
                if (x != 0)
                {
                    tblPaging.Visible = true;
                    divClientList.Visible = true;
                }
                else
                {
                    tblPaging.Visible = false;
                    divClientList.Visible = false;
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
            ddlSubZone.Items.Clear();
            addUser.GetSubZoneDistrict(ddlSubZone, ZoneID);
        }

        protected void ddlSubZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubZone.SelectedValue != "0")
            {
                // BindSubZones(Convert.ToInt32(ddlZone.SelectedValue));
                int x = serachbind(ddlSubZone.SelectedItem.Text);
                if (x != 0)
                {
                    tblPaging.Visible = true;
                    divClientList.Visible = true;
                }
                else
                {
                    tblPaging.Visible = false;
                    divClientList.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "PageReload()", true);
                    txtfromdate.Text = null;
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "callingMYFunction", "rdFunction() ;", true);

            }

        }

        protected void btnPdfreport_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            RptAllUser.RenderControl(hw);
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
    }
}
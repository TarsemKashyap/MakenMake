using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Pages
{
    public partial class RequestData : System.Web.UI.Page
    {
        BLAdmin objAdmin = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetRequestdata();
            }
        }
        protected void RptRequest_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "RequestID")
            {
                try
                {
                 //   HiddenField hdnserviceid = (HiddenField)e.Item.FindControl("hdnserviceid");                    
                    Int64 RequestID = Convert.ToInt64(e.CommandArgument);
                    string RequestData = Utilities.EncryptDecrypt.Encript(RequestID.ToString());
                    Response.Redirect("CommercialInspection.aspx?RequestData=" + RequestData);
                }
                catch (Exception te)
                {
                    //do nothing or handle as required
                }
            }
            else if (e.CommandName == "fillform")
            {
                LinkButton btn = (LinkButton)e.CommandSource;
                if (btn.Text.ToLower() == "filled")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Form had already filled') ;", true);
                }
                else
                {
                    Int64 RequestID = Convert.ToInt64(e.CommandArgument);
                    string RequestData = Utilities.EncryptDecrypt.Encript(RequestID.ToString());
                    Response.Redirect("AssessmentForm.aspx?RequestData=" + RequestData);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }

        DataTable GetClientData(int currentpage)
        {
            DataTable dtable = objAdmin.GetRequestdata(currentpage,Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if (dtable != null && dtable.Rows.Count > 0)
            {
                RptRequest.Visible = true; paging.Visible = true;
            }
            else
            {
                RptRequest.Visible = false; paging.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No Commercial request has been assigned to You') ;", true);
            }
            return dtable;
        }
        private int GetRequestdata()
        {
            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetClientData(CurrentPage);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalcount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];

            RptRequest.DataSource = dt;
            RptRequest.DataBind();

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
                GetRequestdata();
            }
        }
        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            GetRequestdata();
        }
        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            GetRequestdata();
        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetRequestdata();
            }
            else
            {
                CurrentPage = 0;
                GetRequestdata();

            }

        }
        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetRequestdata();
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                GetRequestdata();
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
       
    }
}
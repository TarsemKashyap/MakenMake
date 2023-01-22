using MakeNMake.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Admin
{
    public partial class CommercialRequest : System.Web.UI.Page
    {
        BL.BLAdmin objAdmin = new BL.BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        DataTable GetBindData(int currentpage)
        {
            DataTable dtable = objAdmin.GetCommercialRequest(currentpage);
            return dtable;
        }
        private int BindData()
        {
            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetBindData(CurrentPage);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];


            if (dt != null && dt.Rows.Count > 0)
            {
                RptService.Visible = true;
                RptService.DataSource = dt;
                RptService.DataBind();
            }
            else
            {
                RptService.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No Commercial Request') ;", true);
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

        protected void RptService_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "RequestID")
            {
                try
                {
                    HiddenField hdnEngineerID = (HiddenField)e.Item.FindControl("hdnEngineerID");
                    Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                    string id = Convert.ToString(e.CommandArgument);
                    int status = 0;
                    if (lblStatus.Text.ToLower() == "accepted")
                    {
                        status = 5;
                    }
                    else if (lblStatus.Text.ToLower() == "assigned")
                    {
                        status = 1;
                    }

                    else if (lblStatus.Text.ToLower() == "escalated")
                    {
                        status = 4;


                        hdnEngineerID.Value = "0";
                    }
                    else if (lblStatus.Text.ToLower() == "completed")
                    {
                        status = 3;
                    }
                    else
                    {
                        status = 0;
                        hdnEngineerID.Value = "0";
                    }
                    Response.Redirect("AllocateEngineerByRequestID.aspx?RequestID=" + id + "&Status=" + status + "&EngineerID=" + Convert.ToString(hdnEngineerID.Value), true);
                }
                catch (Exception te)
                {
                    //do nothing or handle as required
                }
            }
            else if (e.CommandName == "quotation")
            {
                HiddenField hdnUserid = (HiddenField)e.Item.FindControl("hdnUserid");

                int result = objAdmin.IsRequestDOne(Convert.ToInt64(e.CommandArgument));
                if (result == 3)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You have already sent the quotation') ;", true);
                }
                else 
                {
                    Response.Redirect("Quotation.aspx?RequestID=" + e.CommandArgument + "&ConsumerID=" + hdnUserid.Value);
                }
            }
            else if (e.CommandName == "viewRequest")
            {
                hdnCustomer.Value = "1";
                BL.BLAdmin objAdmin = new BL.BLAdmin();
                DataTable dtable = objAdmin.GetCommercialRequestByID(Convert.ToInt64(e.CommandArgument));
                if (dtable != null)
                {
                    Repeater1.DataSource = dtable;
                    Repeater1.DataBind();
                }
            }
        }
    }
}
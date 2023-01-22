using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MakeNMake.BL;
using System.Data;
using MakeNMake.CommomFunctions;
using NLog;
using MakeNMake.Utilities;

namespace MakeNMake.Admin
{
    public partial class PageInfo : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        int pagesize, curntpage;
        BLAdmin objAdmin = new BLAdmin();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
               
                BindRole();
                GetParentNode();
            }
        }
        DataTable GetBindParentAllPages(int currentpage)
        {
            DataTable dtable = objAdmin.GetParentNode(currentpage);
            return dtable;
        }
        private int GetParentNode()
        {
             pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetBindParentAllPages(CurrentPage);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];



            if (dt != null && dt.Rows.Count > 0)
            {

                RptParent.Visible = true;
                RptParent.DataSource = dt;
                RptParent.DataBind();
            }
            else
            {
                RptParent.Visible = false;
            } doPaging();
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
        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            GetParentNode();
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            GetParentNode();
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetParentNode();
            }
            else
            {
                CurrentPage = 0;
                GetParentNode();

            }

        }
        protected void RepeaterPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("newpage"))
            {

                CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
                GetParentNode();
            }
        }


        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetParentNode();
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                GetParentNode();
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



        private void BindRole()
        {
            BL.BLAdmin bl = new BL.BLAdmin();
            bl.GetRoles(ddlRole);
        }

        protected void btnSubmit_Click(object sender, System.EventArgs e)
        {

            try
            {
                
                if (btnSubmit.Text == "Add")
                {
                    BLAdmin addpage = new BLAdmin();
                    int result = addpage.AddParentPage(txtparentname.Text, Convert.ToInt32(ddlRole.SelectedItem.Value));
                     if (result <0)
                    {
                        ddlRole.SelectedValue = "0";
                        txtparentname.Text = string.Empty;

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Parent Page with name " + txtparentname.Text + " already exists') ;", true);
                    }
                     else if (result>0)
                     {
                        ddlRole.SelectedValue = "0";
                        txtparentname.Text = string.Empty;

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Added') ;", true);
                        GetParentNode();
                    }
                }



                else
                {
                    BLAdmin objEdit = new BLAdmin();
                    int result = objEdit.UpdateParentPage(Convert.ToInt32(hdnParentID.Value), txtparentname.Text, Convert.ToInt32(ddlRole.SelectedValue));
                    if (result == 1)
                    {
                       
                        txtparentname.Text = string.Empty;
                       
                        ddlRole.SelectedValue = "0";

                        GetParentNode();
                        btnSubmit.Text = "Add";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Parent Page Updated sucessfully') ;", true);
                    }
                }




            }
            catch (Exception ex)
            {
                logger.Error(logger.Name + ":" + ex.Message);
            }
        }
        protected void RptParent_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                BLAdmin objDelete = new BLAdmin();
                int ParentNodeID = Convert.ToInt32(e.CommandArgument);
                int result = objDelete.DeleteParentPage(ParentNodeID);
                if (result == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Deleted') ;", true);
                    GetParentNode();
                }
            }

            else if (e.CommandName == "edit")
            {
                try
                {
                    hdnParentID.Value = Convert.ToString(e.CommandArgument);
                    Label lblrolename = (Label)e.Item.FindControl("lblrolename");
                    Label lblparentName = (Label)e.Item.FindControl("lblparentName");

                    btnSubmit.Text = "Edit";
                    HiddenField hdnparentId = (HiddenField)e.Item.FindControl("hdnparentId");


                    // ddlRole.SelectedValue = Convert.ToString(lblrolename.Text);
                  
                    ddlRole.SelectedValue = ddlRole.Items.FindByText(Convert.ToString(lblrolename.Text)).Value;
                        // ddlRole.Items.FindByValue(Convert.ToString(lblrolename.Text)).Selected=true;
                    txtparentname.Text = lblparentName.Text;
                }
                catch (Exception ee)
                {
                }
            }

        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            ddlRole.SelectedValue = "0";
            txtparentname.Text = string.Empty;
        }



       
    }
}
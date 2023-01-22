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
    public partial class Page : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        BLAdmin objAdmin = new BLAdmin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetChildNode();
                BindAdminRole();
                BLAdmin bl = new BLAdmin();
                bl.GetPageByRoleID(ddlParent, 0);
            }
        }
        DataTable GetBindChildAllPages(int currentpage)
        {
            DataTable dtable = objAdmin.GetChildNode(currentpage);
            return dtable;
        }
        private void BindRole()
        {
           BL. BLAdmin bl = new BL.BLAdmin();
           bl.GetPageRole(ddlParent);
        }
        private int GetChildNode()
        {
            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetBindChildAllPages(CurrentPage);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;

            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];

            if (dt != null && dt.Rows.Count > 0)
            {
                RptAllpage.Visible = true;
                RptAllpage.DataSource = dt;
                RptAllpage.DataBind();
            }
            else
            {
                RptAllpage.Visible = false;
            }
            RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            return (Convert.ToInt32(dt.Rows.Count));
        }
        protected void btnSubmit_Click(object sender, System.EventArgs e)
        {

            try
            {
                if (btnSubmit.Text == "Add")
                {
                    BLAdmin addpage = new BLAdmin();
                    int result = addpage.AddPage(Convert.ToInt32(ddlParent.SelectedItem.Value), txtpagename.Text, txtdescription.Text, txttitle.Text,txtLinkeddescription.Text);
                    if (result == -99)
                    {
                        ddlRole.SelectedValue = "0";
                        ddlParent.SelectedValue = "0";
                        txtpagename.Text = string.Empty;
                        txtdescription.Text = string.Empty;
                        txtLinkeddescription.Text = string.Empty;
                        txttitle.Text = string.Empty;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Child Page with name " + txtpagename.Text + " already exists') ;", true);
                    }
                    else if (result > 0)
                    {
                        ddlRole.SelectedValue = "0";
                        ddlParent.SelectedValue = "0";
                        txtpagename.Text = string.Empty;
                        txtdescription.Text = string.Empty;
                        txtLinkeddescription.Text = string.Empty;
                        txttitle.Text = string.Empty;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Added') ;", true);
                        GetChildNode();
                    }
                }

                else
                {
                    BLAdmin objEdit = new BLAdmin();
                    int result = objEdit.UpdateChildPage(Convert.ToInt32(hdnuserID.Value), Convert.ToInt32(ddlParent.SelectedValue), txtpagename.Text, txtdescription.Text, txttitle.Text,txtLinkeddescription.Text);
                    if (result == 1)
                    {
                        ddlRole.SelectedValue = "0";
                        ddlParent.SelectedValue = "0";
                        txtpagename.Text = string.Empty;
                        txtdescription.Text = string.Empty;
                        txttitle.Text = string.Empty;
                        GetChildNode();
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
            GetChildNode();
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            GetChildNode();
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetChildNode();
            }
            else
            {
                CurrentPage = 0;
                GetChildNode();
            }
        }
        protected void RepeaterPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("newpage"))
            {
                CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
                GetChildNode();
            }
        }


        protected void lnkNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetChildNode();
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                GetChildNode();
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

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            ddlParent.SelectedValue = "0";
            txtpagename.Text = string.Empty;
            txtdescription.Text = string.Empty;
            txttitle.Text = string.Empty;

        }

        protected void RptAllpage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                BLAdmin objDelete = new BLAdmin();
                int tblChildID = Convert.ToInt32(e.CommandArgument);
                int result = objDelete.DeleteChildPage(tblChildID);
                if (result == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Deleted') ;", true);
                    GetChildNode();
                }
            }
            else if (e.CommandName == "edit")
            {
                try
                {
                    //hdnroleid.Value=Convert.ToString(e.CommandArgument);
                    hdnuserID.Value = Convert.ToString(e.CommandArgument);
                    Label lblParentname = (Label)e.Item.FindControl("lblParentname");
                    Label lblpagename = (Label)e.Item.FindControl("lblpagename");
                    Label lbldescription = (Label)e.Item.FindControl("lbldescription");
                    Label lbltitle = (Label)e.Item.FindControl("lbltitle");
                    Label lblrolename = (Label)e.Item.FindControl("lblrolename");
                    Label lblLinkedPages = (Label)e.Item.FindControl("lblLinkedPages");

                    btnSubmit.Text = "Edit";
                    HiddenField hdnChildID = (HiddenField)e.Item.FindControl("hdnChildID");

                    ddlRole.SelectedValue = ddlRole.Items.FindByText(Convert.ToString(lblrolename.Text)).Value;
                    //  ddlRole_SelectedIndexChanged(this, EventArgs.Empty);
                    try
                    {
                        ddlParent.SelectedValue = hdnChildID.Value;// ddlParent.Items.FindByText(Convert.ToString(lblParentname.Text)).Value;
                    }
                    catch
                    {
                        ddlParent.SelectedValue = "0";
                    }
                    //   BLAdmin bl = new BLAdmin();
                    //   bl.GetPageByRoleID(ddlParent, Convert.ToInt32(ddlRole.SelectedValue));
                    txtpagename.Text = lblpagename.Text;
                    txtdescription.Text = lbldescription.Text;
                    txttitle.Text = lbltitle.Text;
                    txtLinkeddescription.Text = lblLinkedPages.Text;
                }
                catch (Exception ee)
                {
                }
            }
        }

        private void BindAdminRole()
        {
            BL.BLAdmin bl = new BL.BLAdmin();
            bl.GetRoles(ddlRole);
        }


        protected void ddlRole_SelectedIndexChanged(object sender, System.EventArgs e)
        {
             if (ddlRole.SelectedValue != "0")
            {
                ddlParent.Items.Clear();
                BLAdmin bl = new BLAdmin();
                bl.GetPageByRoleID(ddlParent, Convert.ToInt32(ddlRole.SelectedValue));
            }
            else
            {
                ddlParent.Items.Clear();
            }
        }
        

        }

       
    
}
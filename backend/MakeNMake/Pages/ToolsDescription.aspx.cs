using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake
{
    public partial class ToolsDescription : System.Web.UI.Page
    {
        BLAdmin objAdmin = new BLAdmin();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        int pagesize;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rdbStatus.SelectedValue = "1";
                BindDataList();
            }
        }

        DataTable GetBindTool(int pagesize, int currentpage)
        {
            DataTable dtable = objAdmin.GetTool(pagesize, currentpage);
            return dtable;
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
            DataTable dt = GetBindTool(pagesize, CurrentPage);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / pagesize));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];

            if (dt != null && dt.Rows.Count > 0)
            {
                divClientList.Visible = true;
                RptService.DataSource = dt;
                RptService.DataBind();
            }
            else
            {
                divClientList.Visible = false;
                RptService.DataSource = null;
                RptService.DataBind();
            }

            doPaging();
            RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

            return (Convert.ToInt32(dt.Rows.Count));

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 CreatedBy = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                DateTime Created = DateTime.Now;
                int tooltype = Convert.ToInt32(ddltooltype.SelectedItem.Value);
                int status = Convert.ToInt32(rdbStatus.SelectedValue);
                if (btnSubmit.Text == "Add")
                {
                    
                    int result = objAdmin.AddTool(txttoolName.Text, tooltype, Convert.ToInt32(txtquantity.Text), status, txtdescription.Text, Created, CreatedBy);
                    if (result > 0)
                    {
                        clear();
                        BindDataList();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Tool added sucessfully') ;", true);
                    }
                    else
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please Change Service Type') ;", true);
                    }
                }
                else if (btnSubmit.Text == "Edit")
                {
                    int result = objAdmin.UpdateTool(Convert.ToInt32(hdnServiceID.Value),txttoolName.Text, tooltype,Convert.ToInt32(txtquantity.Text), status, txtdescription.Text, Created, CreatedBy);
                    if (result == 1)
                    {
                        btnSubmit.Text = "Add";
                        hdnServiceID.Value = string.Empty;
                        clear();
                        BindDataList();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Updated') ;", true);
                        
                    }
                }
            }

            catch (Exception ex)
            {
                //clear();
                logger.Error(logger.Name + ":" + ex.Message);
            }


        }
        private void clear()
        {
            txttoolName.Text = string.Empty;
            txtdescription.Text = string.Empty;
            txtquantity.Text = string.Empty;
            ddltooltype.SelectedValue = "0";
           
        }
        protected void RepeaterPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("newpage"))
            {
                CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
                BindDataList();
            }
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
        protected void ddlIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindDataList();
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

        void LoadDDL()
        {

            for (int i = 1; i <= 10; i++)
            {
                ddlIndex.Items.Add(i.ToString());
            }
            ddlIndex.Items.Insert(0, new ListItem("--Select--", "--Select--"));
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
           if (e.CommandName == "edit")
            {
                hdnServiceID.Value = Convert.ToString(e.CommandArgument);
                Label Llblname = (Label)e.Item.FindControl("Llblname");
                Label lblType = (Label)e.Item.FindControl("lblType");
                Label lblQuantity = (Label)e.Item.FindControl("lblQuantity");
                Label lbldescription = (Label)e.Item.FindControl("lbldescription");
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                btnSubmit.Text = "Edit";
                txttoolName.Text = Llblname.Text;
                if (lblType.Text == "For Repair")
                {
                    ddltooltype.SelectedValue = "1";
                }
                else
                {
                  //  ddltooltype.SelectedValue = "2";
                }
                
                txtquantity.Text = lblQuantity.Text;
                txtdescription.Text = lbldescription.Text;
                rdbStatus.SelectedValue = lblStatus.Text == "Active" ? "1" : "0";
               
            }
           else if (e.CommandName == "delete")
           {
               HiddenField toolid = (HiddenField)e.Item.FindControl("hdnID");
               BLAdmin objDelete = new BLAdmin();
               //int servicePlanID = Convert.ToInt32(e.CommandArgument);
               int result = objAdmin.DeleteTool(Convert.ToInt32(toolid.Value));
               if (result == 1)
               {
                   BindDataList();
                   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Deleted') ;", true);
               }
               else if (result == -99)
               {
                   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot delete the Tool as it was issued by someone') ;", true);
               }
           }
         
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
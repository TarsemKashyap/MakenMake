using MakeNMake.BL;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MakeNMake.CommomFunctions;

namespace MakeNMake
{
    public partial class ToolAssignment : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        BLAdmin obj = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        int pagesize;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // BindToolType();
                BindEngineer();
                BindDataList();
                ddlStatus.Items.Add(new ListItem("--Select Status--", "0"));
                ddlStatus.Items.Add(new ListItem("Issued", "1"));
            }
        }
        DataTable GetData(int pagesize, int currentpage)
        {
            DataTable dtable = obj.GetToolAssignment(pagesize, currentpage);
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
            DataTable dt = GetData(pagesize, CurrentPage);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / pagesize));
            }
            pgsource.DataSource = dt.DefaultView;
            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];

            if (dt != null && dt.Rows.Count > 0)
            {
                RptAllUser.Visible = true;
                RptAllUser.DataSource = dt;
                RptAllUser.DataBind();
                tblPaging.Visible = true;
                lblMsg.Text = string.Empty;
            }
            else
            {
                RptAllUser.Visible = false;
                tblPaging.Visible = false;
                lblMsg.Text = "No Data found";
            }
           
            doPaging();
            RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            return (Convert.ToInt32(dt.Rows.Count));
        }
        protected void BindToolType()
        {
            //DataTable dt = obj.GetAllToolType();
            //ddlToolType.DataSource = dt;
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    ddlToolType.DataTextField = "ToolType";
            //    ddlToolType.DataValueField = "ToolId";                
            //    ddlToolType.DataBind();
            //    ddlToolType.Items.Insert(0, new ListItem("--Select ToolType --", "0"));
                
            //}
            //else
            //{
            //    ddlToolType.Items.Insert(0, new ListItem("--No Other Tool Type Found --", "0"));
            //}
        }
        protected void BindEngineer()
        {
            DataTable dt = obj.GetAllEngineers();
            ddlEngineer.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlEngineer.DataTextField = "Name";
                ddlEngineer.DataValueField = "Userid";                
                ddlEngineer.DataBind();
                ddlEngineer.Items.Insert(0, new ListItem("--Select Engineer--", "0"));               
            }
            else
            {
                ddlEngineer.Items.Insert(0, new ListItem("--No Other Engineer Found in this Zone--", "0"));
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
        protected void ddlIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindDataList();
        }
        private void BindTool(int ToolType)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetToolByToolType(ddlTool, ToolType);
        }

        private void BindTicketByEngineerID(Int64 Engid)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetTicketByEng(ddlTickets, Engid);
        }
        protected void ddlToolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlToolType.SelectedValue != "0")
            {
                ddlTool.Items.Clear();

                BindTool(Convert.ToInt32(ddlToolType.SelectedValue));
            }
            else
            {
                ddlTool.Items.Clear();
                
            }           
        }

        protected void ddlEngineer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEngineer.SelectedValue != "0")
            {
                
                ddlTickets.Items.Clear();

                BindTicketByEngineerID(Convert.ToInt32(ddlEngineer.SelectedValue));
            }
            else
            {
                ddlTickets.Items.Clear();

            }           
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSubmit.Text == "Add")
                {
                    BLAdmin objEdit = new BLAdmin();

                    int result = objEdit.AddToolAssignment( Convert.ToInt32(ddlTool.SelectedItem.Value),
                        Convert.ToInt64(ddlEngineer.SelectedItem.Value), Convert.ToInt64(ddlTickets.SelectedItem.Value), 
                        Convert.ToInt32(ddlStatus.SelectedItem.Value), Convert.ToInt64(Session[Constant.Session.AdminSession]), 
                        txtRemark.Text);
                    if (result > 0)
                    {
                        BindDataList();
                        clear();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert(Added sucessfully.') ;", true);
                    }
                }
                else
                {
                    int result = obj.UpdateToolAssignment(Convert.ToInt32(hdnuserID.Value), Convert.ToInt32(ddlStatus.SelectedItem.Value));
                    if (result == 1)
                    {
                        txtRemark.Enabled = true;
                        ddlTool.Enabled = true;
                        ddlToolType.Enabled = true;
                        ddlTickets.Enabled = true;
                        ddlEngineer.Enabled = true;

                        hdnuserID.Value = string.Empty;
                        ddlToolType.SelectedValue = "0";
                        ddlTool.SelectedValue = "0";
                        ddlEngineer.SelectedValue = "0";
                        ddlTickets.SelectedValue = "0";
                        ddlStatus.SelectedValue = "0";
                        txtRemark.Text = string.Empty;
                        ddlStatus.Items.Clear();
                        ddlStatus.Items.Add(new ListItem("--Select Status--", "0"));
                        ddlStatus.Items.Add(new ListItem("Issued", "1"));
                        BindDataList();
                        btnSubmit.Text = "Add";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Assignment Tool Updated sucessfully') ;", true);
                    }
                }
            }
            catch (Exception ex)
            {
                //  clear();
                logger.Error(logger.Name + ":" + ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }
        protected void clear()
        {
            hdnuserID.Value = string.Empty;
            ddlToolType.SelectedValue = "0";
            ddlTool.SelectedValue = "0";
            ddlEngineer.SelectedValue = "0";
            ddlTickets.SelectedValue = "0";
            ddlStatus.SelectedValue = "0";
            txtRemark.Text = string.Empty;
        }
        protected void RptAllUser_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string id = Convert.ToString(e.CommandArgument);
            //Label lblStatus = (Label)e.Item.FindControl("lblStatus");
           
            if (e.CommandName == "AssignmentID")
            {
                Response.Redirect("ChangeStatusOfAssignmentTool.aspx?AssignmentID=" + id , true);
            }
            else if (e.CommandName == "edit")
            {
                hdnuserID.Value = Convert.ToString(e.CommandArgument);
                HiddenField hdnToolId = (HiddenField)e.Item.FindControl("hdnToolId");
                HiddenField hdnengid = (HiddenField)e.Item.FindControl("hdnengid");
                 Label LblticketID = (Label)e.Item.FindControl("LblticketID");
                Label Status = (Label)e.Item.FindControl("lblStatus");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                btnSubmit.Text = "Edit";
                ddlToolType.SelectedValue = "1";
                BindTool(Convert.ToInt32(1));
                ddlTool.SelectedValue = hdnToolId.Value;
                ddlEngineer.SelectedValue = hdnengid.Value;
                BindTicketByEngineerID(Convert.ToInt64(hdnengid.Value));
                ddlTickets.SelectedValue = ddlTickets.Items.FindByText(Convert.ToString(LblticketID.Text)).Value;
               // ddlStatus.SelectedValue = Status.Text == "Assign" ? "1" : "2";
                txtRemark.Text = lblremark.Text;
                ddlStatus.Items.Clear();
                ddlStatus.Items.Add(new ListItem("--Select Status--", "0"));
                ddlStatus.Items.Add(new ListItem("Returned", "2"));
                txtRemark.Enabled = false;
                ddlTool.Enabled = false;
                ddlToolType.Enabled = false;
                ddlTickets.Enabled = false;
                ddlEngineer.Enabled = false;

            }
            else if (e.CommandName == "delete")
            {
                HiddenField hdnAssignID = (HiddenField)e.Item.FindControl("hdnAssignID");
                BLAdmin objDelete = new BLAdmin();
                int result = objDelete.DeleteToolAssignment(Convert.ToInt32(hdnAssignID.Value));
                if (result == 1)
                {
                    BindDataList();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Deleted') ;", true);
                }
                else if (result == -98)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot delete this tool Assignment') ;", true);
                }
            }
        }
    }
}
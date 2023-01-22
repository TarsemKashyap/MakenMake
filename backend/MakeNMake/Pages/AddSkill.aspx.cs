using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MakeNMake.Admin
{
    public partial class AddSkill : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        BLServiceEngineer getskill = new BLServiceEngineer();
        BLAdmin objAdmin = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPrimaryskill();
                try
                {
                    BindEngineerSkills(Convert.ToInt64(Utilities.EncryptDecrypt.DecryptText(Convert.ToString(Request.QueryString["EngineerID"]))));
                    EngineerInfo();
                }
                catch
                {
                    Response.Redirect("EngineerSkills.aspx");
                }
                Clear();
            }
        }
        private void EngineerInfo()
        {
            DataTable ds = getskill.GetTotalSkills(Convert.ToInt64(Utilities.EncryptDecrypt.DecryptText(Convert.ToString(Request.QueryString["EngineerID"]))));
            if (ds != null && ds.Rows.Count > 0)
            {
                lblname.Text = Convert.ToString(ds.Rows[0]["name"]);
                lbltotalskills.Text = Convert.ToString(ds.Rows[0]["TotalSkills"]);
            }
        }
        private void BindPrimaryskill()
        {
            getskill.GetPrimaryskill(ddlpskill);
        }

        public void Clear()
        {
            ddlpskill.SelectedValue= "0"; 
            txtskillrate.Text = string.Empty;
            ddlskilltype.SelectedValue= "0";
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSubmit.Text.ToLower() == "add")
                {
                    string engineerID = Utilities.EncryptDecrypt.DecryptText(Convert.ToString(Request.QueryString["EngineerID"]));
                    Int64 UserID = Convert.ToInt64(engineerID);
                    int skill = Convert.ToInt32(ddlpskill.SelectedItem.Value);
                    int skilrate = Convert.ToInt32(txtskillrate.Text);
                    char skilltype = Convert.ToChar(ddlskilltype.SelectedItem.Value);

                    Int64 CreatedBy=Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    Int64 ModifiedBy = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    int result = getskill.Addskill(UserID, skill, skilrate, skilltype, CreatedBy, ModifiedBy);

                    if (result > 0)
                    {
                      //  Response.Redirect("Engineerskills.aspx");
                        BindEngineerSkills(Convert.ToInt64(engineerID));
                        Clear();
                        EngineerInfo();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Added') ;", true);
                    }
                    else if (result == -98)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('you cannot repeat skills') ;", true);
                    }
                    else if (result == -99)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('you cannot add more than one primary skill') ;", true);
                    }
                }

                else if (btnSubmit.Text.ToLower() == "edit")
                {
                    string engineerID = Utilities.EncryptDecrypt.DecryptText(Convert.ToString(Request.QueryString["EngineerID"]));
                    divaddskill.Visible = true;
                    diveditskill.Visible = false;

                    int result = objAdmin.UpdateSkill(Convert.ToInt64(hdnSkillid.Value),Convert.ToInt32(ddlpskill.SelectedValue), Convert.ToInt64(engineerID), Convert.ToString(ddlskilltype.SelectedValue), Convert.ToInt32(txtskillrate.Text), Convert.ToInt64(Session[Constant.Session.AdminSession]));
                    if (result == 1)
                    {
                        Clear();
                        hdnSkillid.Value = string.Empty;
                        BindEngineerSkills(Convert.ToInt64(engineerID));
                        
                        ddlpskill.Enabled = true;
                        btnSubmit.Text = "Add";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Updated') ;", true);
                    }
                    else if (result == -99)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You have already one primary skill') ;", true);
                    }
                }
            }
            catch (Exception ex)
            {

                logger.Error(logger.Name + ":" + ex.Message);
            }

        }

        

        protected void RptClient_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                Int64 UserID =Convert.ToInt64(  Utilities.EncryptDecrypt.DecryptText(Convert.ToString(Request.QueryString["EngineerID"])));
                int i = getskill.deleteskill(UserID, Convert.ToInt32(e.CommandArgument));
                if (i == 1)
                {
                    BindEngineerSkills(UserID);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert(' Successfully Deleted') ;", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please Try Again ?') ;", true);
                }
            }
            else if (e.CommandName == "edit")
            {
                divaddskill.Visible = false;
                diveditskill.Visible = true;
                hdnSkillid.Value = e.CommandArgument.ToString();
                Label lblName = (Label)e.Item.FindControl("lblservicename");
                Label lblrate = (Label)e.Item.FindControl("lblEmail");
                Label lblskilltype = (Label)e.Item.FindControl("lblservicetype");
                btnSubmit.Text = "Edit";
                BindPrimaryskill();
                HiddenField serviceid = (HiddenField)e.Item.FindControl("serviceid");
                ddlpskill.SelectedValue = Convert.ToString(serviceid.Value);
                ddlpskill.Enabled = false;                                  //change
                ddlskilltype.SelectedValue = lblskilltype.Text;
                txtskillrate.Text = lblrate.Text;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Engineerskills.aspx");
        }




        //=========================================
        DataTable GetBindEngineerskills(int currentpage, Int64 EngineerID)
        {
            DataTable dtable = getskill.GetConsumer(currentpage, EngineerID);
           // DataTable dtable = objAdmin.GetAllEngineerskills(currentpage);
            return dtable;
        }
        private int BindEngineerSkills(Int64 EngineerID)
        {

            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetBindEngineerskills(CurrentPage, EngineerID);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalcount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];



            if (dt != null && dt.Rows.Count > 0)
            {
                Rptskill.Visible = true;
                tblPaging.Visible = true;
                lblpage.Visible = true;
                Rptskill.DataSource = dt;
                Rptskill.DataBind();
                doPaging();
                RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            }
            else
            {
                Rptskill.Visible = false;
                tblPaging.Visible = false;
                lblpage.Visible = false;
            }

           
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
                BindEngineerSkills(Convert.ToInt64(ViewState["engineerID"]));
            }
        }
        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            BindEngineerSkills(Convert.ToInt64(ViewState["engineerID"]));
        }
        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            BindEngineerSkills(Convert.ToInt64(ViewState["engineerID"]));
        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                 BindEngineerSkills(Convert.ToInt64(ViewState["engineerID"]));
            }
            else
            {
                CurrentPage = 0;
                BindEngineerSkills(Convert.ToInt64(ViewState["engineerID"]));

            }

        }
        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                  BindEngineerSkills(Convert.ToInt64(ViewState["engineerID"]));
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                BindEngineerSkills(Convert.ToInt64(ViewState["engineerID"]));
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

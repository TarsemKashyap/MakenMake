using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MakeNMake.BL;
using MakeNMake.Utilities;
using MakeNMake.CommomFunctions;

namespace MakeNMake.Admin
{
    public partial class EngineerSkills : System.Web.UI.Page
    {
        BLAdmin objAdmin = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["Session_UserID"] = Convert.ToString(Session[Constant.Session.AdminSession]);
                binddata();
            }
        }
        protected void RptTickets_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName=="AddSkill")
            {
                Response.Redirect("Addskill.aspx?EngineerID=" + Utilities.EncryptDecrypt.Encript(Convert.ToString(e.CommandArgument)));
            }
            else if (e.CommandName == "updateInfo")
            {
                Response.Redirect("UpdateInfo.aspx?EngineerID=" + Utilities.EncryptDecrypt.Encript(Convert.ToString(e.CommandArgument)));
            }
        }
        DataTable GetBindEngineerskills(int currentpage,  string clientName)
        {
            DataTable dtable = objAdmin.GetAllEngineerskill(currentpage,  clientName);
            return dtable;
        }
        private int binddata()
        {
            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetBindEngineerskills(CurrentPage,  txtSearchclient.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalcount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];



            if (dt != null && dt.Rows.Count > 0)
               {
                  RptTickets.Visible = true;
                  RptTickets.DataSource = dt;
                  RptTickets.DataBind();
                  doPaging();
                  RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
               }
            else
               {
                  RptTickets.Visible = false;
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
                binddata();
            }
        }
        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            binddata();
        }
        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            binddata();
        }
       protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                binddata();
            }
            else
            {
                CurrentPage = 0;
                binddata();

            }

        }
        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                binddata();
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                binddata();
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

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            if (txtSearchclient.Text != "")
            {
                CurrentPage = 0;
                int x = binddata();
                if (x != 0)
                {
                    tblPaging.Visible = true;
                    //divClientList.Visible = true;
                }
                else
                {
                    tblPaging.Visible = false;
                   // divClientList.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "PageReload()", true);
                    txtSearchclient.Text = null;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please Enter the Name Or Mobile Number!') ;", true);
            }
        }

    }
}
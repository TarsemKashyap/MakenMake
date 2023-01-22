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

namespace MakeNMake.Pages
{
    public partial class Announcements : System.Web.UI.Page
    {
        BLAdmin Savemsg = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetMessage();
                BindRoles();
            }
        }

        private void BindRoles()
        {
            BLAdmin getRoles = new BLAdmin();
            getRoles.GetRoles(ddlRole);
        }
        DataTable GetMessageData(int currentpage)
        {
            DataTable dtable = Savemsg.GetMessage(currentpage);
            if (dtable != null && dtable.Rows.Count > 0)
            {
                RptMsg.Visible = true;
                tblpaging.Visible = true;
            }
            else
            {
                RptMsg.Visible = false;
                tblpaging.Visible = false;
            }
            return dtable;
        }
        private int GetMessage()
        {
            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetMessageData(CurrentPage);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];

            RptMsg.DataSource = dt;
            RptMsg.DataBind();

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
                GetMessage();
            }
        }
        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            GetMessage();
        }
        protected void lnkLast_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            GetMessage();
        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            CurrentPage -= 1;
            if (CurrentPage >= 0 && CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetMessage();
            }
            else
            {
                CurrentPage = 0;
                GetMessage();

            }

        }
        protected void lnkNext_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;

            if (CurrentPage < Convert.ToInt16(ViewState["totpage"]))
            {
                GetMessage();
            }
            else
            {
                CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
                GetMessage();
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


        protected void RptMsg_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                BLAdmin obj = new BLAdmin();
                HiddenField hdnroleid = (HiddenField)e.Item.FindControl("hdnroleid");
                HiddenField hdnmessageid = (HiddenField)e.Item.FindControl("hdnmessageid");

                int result = obj.DeleteMessage(Convert.ToInt32(hdnroleid.Value), Convert.ToInt64(hdnmessageid.Value));
                if (result == 1)
                {
                    GetMessage();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Deleted') ;", true);
                }

            }
            else if (e.CommandName == "edit")
            {

                HiddenField hdnroleid = (HiddenField)e.Item.FindControl("hdnroleid");
                HiddenField hdnmessageid = (HiddenField)e.Item.FindControl("hdnmessageid");
                Label lblRoleId = (Label)e.Item.FindControl("lblRoleId");
                Label lblMessage = (Label)e.Item.FindControl("lblMessage");
                Label lblMessageId = (Label)e.Item.FindControl("lblMessageId");
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                Label lblsms = (Label)e.Item.FindControl("lblsms");
                btnRole.Text = "Edit";
                ddlRole.SelectedValue = lblRoleId.Text;
                ddlRole.Enabled = false;
                // GetRoles(Convert.ToInt64(ddlRole.SelectedValue));
               // rdbStatus.SelectedValue = lblStatus.Text == "Sent" ? "1" : "0";
              
                chksendsms.Checked =Convert.ToBoolean( lblsms.Text == "Allowed" ? 1 : 0);

                txtmsg.Text = lblMessage.Text;


            }
        }

        protected void btnRole_Click(object sender, EventArgs e)
        {
            int relocate = chksendsms.Checked ? 1 : 0;
            if (btnRole.Text.ToLower() == "save")
            {
                BLAdmin Savemsg = new BLAdmin();

                if (chksendsms.Checked == true)
                {
                    SendSms();
                }            
                int result = Savemsg.SaveMessage(Convert.ToInt64(Session[Constant.Session.AdminSession]), Convert.ToInt64(Session[Constant.Session.AdminSession]), Convert.ToInt32(ddlRole.SelectedValue), txtmsg.Text,1,relocate);
                

                if (result > 0)
                {
                    Clear(); GetMessage();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Added') ;", true);
                    Clear();
                }
            }
            else if (btnRole.Text.ToLower() == "edit")
            {
                if (chksendsms.Checked == true)
                {
                    SendSms();
                }            
                int result = Savemsg.UpdateMessage(Convert.ToInt16(ddlRole.SelectedValue), txtmsg.Text,1,relocate);
                
                    Clear(); GetMessage();
                    ddlRole.Enabled = true;
                    btnRole.Text = "Save";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Updated') ;", true);
                
            }

        }

        public void SendSms()
        {
            BLAdmin getUsers = new BLAdmin();
            DataTable dt = getUsers.GetUsersByRoleID(Convert.ToInt16(ddlRole.SelectedValue));
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string message = txtmsg.Text;

                    MEmail.SendGMail(Convert.ToString(dt.Rows[i]["EmailID"]), "Make n Make Annoucements", message, "");

                    SendSms objSms = new SendSms();
                    try
                    {
                        int length = Convert.ToString(dt.Rows[i]["MobileNumber"]).Length;
                        if (length > 0)
                        {
                            int j = objSms.SendSmsOnMobile(message, Convert.ToString(dt.Rows[i]["MobileNumber"]));
                            if (j != 1)
                            {
                                BL.BLAdmin objAdmin = new BL.BLAdmin();
                                objAdmin.AddNotSendSmsMail(Convert.ToInt64(dt.Rows[i]["UserID"]), 0, "Sending Announcements", 1);
                            }
                        }
                        else
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(dt.Rows[i]["UserID"]), 0, "Number not Avaiable:-", 1);

                        }
                    }
                    catch (Exception ex)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(dt.Rows[i]["UserID"]), 0, "Error while Sending Announcements to Client-Issue:-" + ex.Message, 1);
                    }
                }
            }
       }

        public void Clear()
        {
            ddlRole.SelectedValue = "0";
            txtmsg.Text = string.Empty;
            chksendsms.Checked = false;
           // rdbStatus.SelectedValue = "0";

        }
        protected void btnCancels_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }

       
    }
}
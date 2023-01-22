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


namespace MakeNMake.ServiceEngineer
{
    public partial class ServiceEngineerDiary : System.Web.UI.Page
    {
     
        BLServiceEngineer adskill = new BLServiceEngineer();
        protected void Page_Load()
        {
            if (!IsPostBack)
            {
                Bindticketid(Convert.ToInt64(Session[Constant.Session.AdminSession]));
                //GetDailyDiary(Convert.ToInt64(Session[Constant.Session.AdminSession]));
                Clear();
            }
        }
        //private void GetDailyDiary(Int64 EngineerID)
        //{
        //    adskill.GetDailyDiary(RptDiary, EngineerID);
        //}


        private void Bindticketid(Int64 EngineerID)
        {
            adskill.GetUserId(ddlticketid, EngineerID);
            ddlticketid.Items.Insert(0, new ListItem("Select a Ticket Id", "0"));
        }

        protected void ddlticketid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlticketid.SelectedValue != "0")
            {
                DataTable dt = adskill.GetDiaryByTicketID(Convert.ToInt64(ddlticketid.SelectedValue));
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblmsg.Visible = false;
                    RptDiary.Visible = true;
                    RptDiary.DataSource = dt;
                    RptDiary.DataBind();
                }
                else
                {
                    lblmsg.Visible = true;
                    RptDiary.Visible = false;
                    lblmsg.Text = "No Ticket history related to this ticketID";
                }
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "No Ticket history related to this ticketID";
            }
        }
        public void Clear()
        {

            TextTitle.Text = string.Empty;
            TextDesc.Text = string.Empty;

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int UserID = Convert.ToInt32(Session[Constant.Session.AdminSession]);
                string Title = TextTitle.Text;
                string Description = TextDesc.Text;
                DateTime moddate = DateTime.Now;
                int ReportTo = 110;
                DateTime Created = DateTime.Now;
                int result = adskill.AddDailyDiary(UserID, Title, Description, moddate, ReportTo, Created, Convert.ToInt64(ddlticketid.SelectedValue));

                if (result > 0)
                {
                    Clear();
                   // GetDailyDiary(Convert.ToInt64(Session[Constant.Session.AdminSession]));
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Added') ;", true);
                }

                else if (result == -99)
                {
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Already Added for today') ;", true);
                }
                else if (result == -98)
                {
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot fill the diary as the ticket is closed') ;", true);
                }
                else if (result == -97)
                {
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot fill the diary as you have rejected the ticket') ;", true);
                }
                else if (result == -96)
                {
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot fill the diary until you have not accepted the ticket') ;", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            if (Request.RawUrl.ToLower() == "/customer/ServiceEngineerDiary.aspx")
            {
                Response.Redirect("Clients.aspx");
            }
            else
            {
                Response.Redirect("DashBoard.aspx");
            }
        }
    }
}

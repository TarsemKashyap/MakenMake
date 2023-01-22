using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Customer
{
    public partial class CustomerFeedback : System.Web.UI.Page
    {

        BLConsumer objAdmin = new BLConsumer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTicketdata();
                BindData();
            }
        }
        public void BindData()
        {
            DataTable dt = objAdmin.GetCustomerFeedbackData(Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if (dt != null && dt.Rows.Count > 0)
            {
                RptTickets.Visible = true;
                RptTickets.DataSource = dt;
                RptTickets.DataBind();
            }
            else
            {
                RptTickets.Visible = false;
            }
        }
        private void BindTicketdata()
        {
            DataTable dt = objAdmin.GetEngineerTicketsData(Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlticket.DataSource = dt;
                ddlticket.DataTextField = "TicketID";
                ddlticket.DataValueField = "TicketID";
                ddlticket.DataBind();
                ddlticket.Items.Insert(0, new ListItem("--Select Ticket ID--", "0"));
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Ticket Feedback will be available only after any of your tickets is completed.') ;", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }
      
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlticket.SelectedValue != "0")
            {
                int result = objAdmin.CustomerFeedback(Convert.ToInt64(Session[Constant.Session.AdminSession]), 
                    Convert.ToInt64(ddlticket.SelectedValue), Convert.ToInt32(ddlSatisfied.SelectedValue),
                    Convert.ToInt32(txtRating.Text), 0, txtdscrpt.Text);
                if (result == 1)
                {
                    ddlticket.SelectedValue = "0";
                    ddlSatisfied.SelectedValue = "0";
                    txtdscrpt.Text = string.Empty;
                    txtRating.Text= string.Empty;
                    BindData();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Submitted successfully') ;", true);
                }
                else if (result == -96)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Ticket Feedback will be available only after any of your tickets is completed.') ;", true);
                }
                else if (result == -95)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You Cannot Add More Than 1 Feedback for One Ticket') ;", true);
                }
            }
            else
            {
                clear();
                dvSatisfied.Visible = false;
                dvrating.Visible = false;
                Dvdescription.Visible = false;
            }
        }

        protected void ddlticket_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlticket.SelectedValue != "0")
            {
                DataTable dt = objAdmin.GetCustomerFeedbackData(Convert.ToInt64(Session[Constant.Session.AdminSession]));
                if (dt != null && dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You Cannot Add More Than 1 Feedback for One Ticket') ;", true);
                }
                else
                {
                    dvSatisfied.Visible = true;
                    dvrating.Visible = true;
                    Dvdescription.Visible = true;
                }
                
            }
        }

        protected void clear()
        {

            
            ddlticket.SelectedValue = "0";
            ddlSatisfied.SelectedValue = "0";
            txtRating.Text = "";
           
            txtdscrpt.Text = "";
        }

     

    }
}
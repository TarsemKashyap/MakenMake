using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace MakeNMake.ServiceEngineer
{
    public partial class AppointMentDetails : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    lblTicketID.Text = "Ticket ID : " + Utilities.EncryptDecrypt.DecryptText(Convert.ToString(Request.QueryString["AppointMentID"]));
                    string status = Request.QueryString["Status"];
                    if (status.ToLower() == "accepted")
                    {
                        ddlStatus.SelectedValue = "5";
                    }
                    else if (status.ToLower() == "completed")
                    {
                        ddlStatus.SelectedValue = "3";
                    }
                    else if (status.ToLower() == "rejected")
                    {
                        ddlStatus.SelectedValue = "2";
                    }
                    else if (status.ToLower() == "assigned")
                    {
                        ddlStatus.Items.Add(new ListItem("Assigned", "1"));
                        ddlStatus.SelectedValue = "1";
                    }
                   
                }
                catch (Exception ex)
                {
                    Response.Redirect("AppoinmentTickets.aspx");
                }
            }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedValue == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot change the status to Assigned') ;", true);
            }
            else
            {
                Int64 appointmentID = Convert.ToInt64(Utilities.EncryptDecrypt.DecryptText(Convert.ToString(Request.QueryString["AppointMentID"])));
                BLServiceEngineer obj = new BLServiceEngineer();
                int result = obj.EngineerAppointment(appointmentID, Convert.ToInt64(Session[Constant.Session.AdminSession]),
                    Convert.ToInt64(Session[Constant.Session.AdminSession]), Convert.ToInt32(ddlStatus.SelectedValue), txtReason.Text);
                if (result > 0)
                {
                    //ddlStatus.SelectedValue = "0";
                    //txtReason.Text = string.Empty;

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You have changed the appointment status') ;", true);
                    Response.Redirect("AppoinmentTickets.aspx");
                }
                else if (result == -99)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Your status is already " + ddlStatus.SelectedItem.Text + "') ;", true);
                }
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AppoinmentTickets.aspx");
        }
    }
}
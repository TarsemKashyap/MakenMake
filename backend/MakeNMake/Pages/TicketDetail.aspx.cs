using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.ServiceEngineer
{
    public partial class TicketDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    binddata();
                    int status = Convert.ToInt16(Request.QueryString["Status"]);
                    if (status == 3)
                    {
                        ddlStatus.SelectedValue = "3";
                    }
                    else if (status == 5)
                    {
                        ddlStatus.SelectedValue = "5";
                    }
                    else if (status == 2)
                    {
                        ddlStatus.SelectedValue = "2";
                    }
                    else if (status == 1)
                    {
                        ddlStatus.SelectedValue = "1";
                    }
                    else
                    {
                        ddlStatus.SelectedValue = "0";
                    }
                }
                catch
                {
                }
            }
        }

        private void binddata()
        {
            BLServiceEngineer objEngineer = new BLServiceEngineer();
            Int64 ticketID = Convert.ToInt64(Utilities.EncryptDecrypt.DecryptText(Request.QueryString["TicketID"].ToString()));
            DataTable dt = objEngineer.GetTicketDetails(ticketID);
            if (dt != null && dt.Rows.Count > 0)
            {
                RptTickets.DataSource = dt;
                RptTickets.DataBind();
            }
            else
            {
                lblMsg.Text = "No TicketDetail";
            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            int roleID = Convert.ToInt32(Session[Constant.Session.Role]);
            if (roleID == 1)
            {
                Response.Redirect("ServiceTickets.aspx");
            }
            else
            {
                Response.Redirect("EnginerServiceTickets.aspx");
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Int64 ticketID = Convert.ToInt64(Utilities.EncryptDecrypt.DecryptText(Request.QueryString["TicketID"].ToString()));
            BLServiceEngineer obj = new BLServiceEngineer();
            int status = Convert.ToInt32(ddlStatus.SelectedValue);
            int result = obj.UpdateEngineerTicket(ticketID, Convert.ToInt64(Session[Constant.Session.AdminSession]), Convert.ToInt64(Session[Constant.Session.AdminSession]), status,txtReason.Text);
            if (result > 0)
            {
                if (ddlStatus.SelectedItem.Text == "Accept")
                {
                    Common objSend = new Common();
                    BLCustomerCare custom = new BLCustomerCare();
                    DataTable dtuser = custom.GetUSerByTicketID(ticketID, 1);
                    if (dtuser.Rows.Count > 0)
                    {
                        DataTable dt = objSend.GetUserInfoByID(Convert.ToInt64(dtuser.Rows[0]["UserID"]));
                        if (dt.Rows.Count > 0)
                        {
                            string firstname = Convert.ToString(dt.Rows[0]["firstname"]);
                            string emailid = Convert.ToString(dt.Rows[0]["Emailid"]);
                            string mobile = Convert.ToString(dt.Rows[0]["MNumber"]);
                            DataTable dtengineer = objSend.GetUserInfoByID(Convert.ToInt64(Session[Constant.Session.AdminSession]));
                            string engineerName = "";
                            string engno = string.Empty;
                            if (dtengineer.Rows.Count > 0)
                            {
                                engineerName = Convert.ToString(dtengineer.Rows[0]["firstname"]);
                                engno = Convert.ToString(dtengineer.Rows[0]["MNumber"]);
                            }
                            string message1 = "Hi," + firstname + "! Your Service ticket Id: " + ticketID + " has been Accepted by  Engineer name:" + engineerName +  ". and his Mobile number is:"+engno + " .He will serve you shortly. Please log in with your account details on our website(www.makenmake.in)/Mobile App to see the status of the ticket.Or call us at Helpline Nos:"+ReadConfig.helpLineNumber;

                            //Hi, .Name!Your Service ticket Id: XXXXXXX has been assigned to Engineer XXXXXXXX.He will serve you shortly.Please log in with your account details on our website(www.makenmake.co.in) to see the status of the ticket.


                     SendSms objSms1 = new SendSms();
                            try
                            {
                                int i = objSms1.SendSmsOnMobile(message1, mobile);
                                if (i != 1)
                                {
                                    BL.BLAdmin objAdmin = new BL.BLAdmin();
                                    objAdmin.AddNotSendSmsMail(Convert.ToInt64(dt.Rows[0]["UserID"]), 0, "Ticket Status to Client", 1);
                                }
                            }
                            catch (Exception ex)
                            {
                                BL.BLAdmin objAdmin = new BL.BLAdmin();
                                objAdmin.AddNotSendSmsMail(Convert.ToInt64(dt.Rows[0]["UserID"]), 0, "Error while Ticket Status to Client-Issue:-" + ex.Message, 1);
                            }

                            //MEmail.SendGMail(emailid, "Service Ticket Make 'N' Make", message1, "");

                            MEmail.SendGMail(emailid, "Service Ticket Status Make 'N' Make", "Hi," + firstname + "! Your Service ticket Id: " + ticketID + " has been Accepted by  Engineer name:" + engineerName +  ".and his Mobile number is:"+engno + ".He will serve you shortly. Please log in with your account details on our website(www.makenmake.in)/Mobile App to see the status of the ticket.Or Call us at Helpline Nos:"+ReadConfig.helpLineNumber, "");



                        }
                    }
                }
                    ddlStatus.SelectedValue = "0";
                txtReason.Text = string.Empty;
                int roleID = Convert.ToInt32(Session[Constant.Session.Role]);
                if (roleID == 1)
                {
                    Response.Redirect("ServiceTickets.aspx");
                }
                else
                {
                    Response.Redirect("EnginerServiceTickets.aspx");
                }
               // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You have changed the Ticket status') ;", true);
            }
            else if (result == -95)
            {
                ddlStatus.SelectedValue = "0";
                txtReason.Text = string.Empty;
                binddata();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('As the client has taken new plan so ticket status become completed') ;", true);
            }
            else if (result == -99)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You already " + ddlStatus.SelectedItem.Text + " the ticket') ;", true);
            }
            else if (result == -98)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You have not fill the service time for that ticketID') ;", true);
            }
            else if (result == -97)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot reject the ticket for which you have spent time ') ;", true);
            }
            else if (result == -96)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot mark it complete until you have add the checkout time on service time for that ticketID') ;", true);
            }
        }

        protected void RptTickets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblreasn = (Label)e.Item.FindControl("lblQuery");
                if (lblreasn.Text.Length > 25)
                {
                    lblreasn.Text = lblreasn.Text.Substring(0, 25);
                    LinkButton lnkBtnMore = (LinkButton)e.Item.FindControl("lnkBtnMore");
                    lnkBtnMore.Visible = true;
                    lnkBtnMore.OnClientClick = "ShowMsg(this)";
                }
                else
                {
                    LinkButton lnkBtnMore = (LinkButton)e.Item.FindControl("lnkBtnMore");
                    lnkBtnMore.Visible = false;
                }
            }
        }
    }
}
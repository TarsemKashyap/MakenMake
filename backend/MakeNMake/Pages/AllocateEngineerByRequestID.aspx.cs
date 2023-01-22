using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MakeNMake.BL;
using MakeNMake.CommomFunctions;

namespace MakeNMake
{
    public partial class AllocateEngineerByRequestID : System.Web.UI.Page
    {
        BLAdmin obj = new BLAdmin();
        BLEscalation objEsc = new BLEscalation();
        Int64 RequestID;
        Int64 EngineerID;
        int Status;
        protected void Page_Load(object sender, EventArgs e)
        {
            RequestID = Convert.ToInt64(Request.QueryString["RequestID"]);
            EngineerID = Convert.ToInt64(Request.QueryString["EngineerID"]);
            lblhstryTicketID.Text = Convert.ToString(RequestID);
            Status = Convert.ToInt32(Request.QueryString["Status"]);
            if (!IsPostBack)
            {
                try
                {
                    BindData(RequestID);
                    BindEngineer();
                    if (Status == 1)
                    {
                        ddlStatus.Items.Insert(0, new ListItem("--Select Status--", "0"));
                        ddlStatus.Items.Insert(1, new ListItem("Reject", "2"));
                    }
                    else if (Status == 2)
                    {
                        ddlStatus.Items.Insert(0, new ListItem("--Select Status--", "0"));
                        ddlStatus.Items.Insert(1, new ListItem("Assign", "1"));
                    }
                    else if (Status == 3)
                    {
                        ddlStatus.Items.Insert(0, new ListItem("--Job Completed--", "0"));
                    }
                    else if (Status == 4)
                    {
                        ddlStatus.Items.Insert(0, new ListItem("--Select Status--", "0"));
                        ddlStatus.Items.Insert(1, new ListItem("Assign", "1"));
                        ddlStatus.Items.Insert(1, new ListItem("Reject", "2"));
                    }
                    else if (Status == 5)
                    {
                        ddlStatus.Items.Insert(0, new ListItem("Job Accepted", "0"));
                        ddlStatus.Items.Insert(1, new ListItem("Reject", "2"));
                    }
                    else if (Status == 0)
                    {
                        ddlStatus.Items.Insert(0, new ListItem("--Select Status--", "0"));
                        ddlStatus.Items.Insert(1, new ListItem("Assign", "1"));
                        ddlStatus.Items.Insert(1, new ListItem("Reject", "2"));
                    }
                }
                catch(Exception ee)
                {
                    Response.Redirect("~/Error.aspx");
                }
            }
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlStatus.SelectedValue) == 1)
            {
                ddlEngineer.Enabled = true; ddlEngineer.Visible = true;
            }
            if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
            {
                ddlEngineer.Enabled = false; ddlEngineer.Visible = false;
            }
            DivComments.Visible = true;
        }
        protected void BindEngineer()
        {
            DataTable dt = obj.BindAllEngineer(RequestID);
            ddlEngineer.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlEngineer.DataTextField = "allEngineerName";
                ddlEngineer.DataValueField = "allEngineerID";
                ddlEngineer.DataBind();
                ddlEngineer.Items.Insert(0, new ListItem("--Select Engineer--", "0"));
                if (Status == 1)
                {
                    ddlEngineer.SelectedValue = EngineerID.ToString();
                    ddlEngineer.Enabled = false;
                }
                else if (Status == 3 || Status == 5)
                {
                    ddlEngineer.SelectedValue = EngineerID.ToString();
                    ddlEngineer.Enabled = false;
                    ddlStatus.Enabled = false;
                    btnSubmit.Visible = false;
                }
                else if (Status == 2)
                {
                    ddlEngineer.Items.Clear();
                    ddlEngineer.Items.Insert(0, new ListItem("", "0"));
                    ddlEngineer.Enabled = true;
                }
                else if (Status == 4)
                {
                    ddlEngineer.Items.Clear();
                    ddlEngineer.Items.Insert(0, new ListItem("", "0"));
                    ddlEngineer.Enabled = true;
                }
            }
            else
            {
                ddlEngineer.Items.Insert(0, new ListItem("--No Other Engineer Found in this Zone--", "0"));
            }
        }
        protected void ddlEngineer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DivComments.Visible = true;

        }
        private void BindData(Int64 RequestID)
        {
            DataTable dt = obj.GetRequestHistoryByRequestID(RequestID);
            if (dt != null && dt.Rows.Count > 0)
            {
               
                RptTickets.DataSource = dt;
                RptTickets.DataBind();
            }
            else
            {
                RptTickets.Visible = false;
             //   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No Commercial Request') ;", true);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int result = obj.AssignReqToEnginner(Convert.ToInt64(ddlEngineer.SelectedValue), Convert.ToInt64(Session[Constant.Session.AdminSession]),RequestID, txtComments.Text, Convert.ToInt32(ddlStatus.SelectedValue));
            if (result > 0)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Request Assigned Successfully') ;", true);
                Response.Redirect("CommercialRequest.aspx");
            }
            else if (result==-99)
            {
                BLCustomerCare care1 = new BLCustomerCare();
                BLAdmin admindl = new BLAdmin();
                DataTable dtadmin = admindl.GetAdmin();
                if (dtadmin.Rows.Count > 0)
                {
                    string message1 = "";

                    message1 = "Hi,Admin Commercial visit Request Id " + RequestID + " has been raised for your necessary action.  .Please log in with your account details on our website ( www.makenmake.in)/Mobile App to see the status of the ticket. ";

                       
                       
                    
                    SendSms objSms1 = new SendSms();
                    try
                    {
                        int i = objSms1.SendSmsOnMobile(message1, Convert.ToString(dtadmin.Rows[0]["MobileNumber"]));
                        if (i != 1)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            //objAdmin.AddNotSendSmsMail(Convert.ToInt64(UserID), 0, "Ticket Status to Client", 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(Session[Constant.Session.AdminSession]), 0, "Error while Ticket Status to Admin-Issue:-" + ex.Message, 1);
                    }

                    MEmail.SendGMail(Convert.ToString(dtadmin.Rows[0]["EmailID"]), "Service Ticket Make 'N' Make", message1, "");
                }

                DataTable dtescaltion = care1.GetEsclationMnagerByZoneid(Convert.ToInt32(RequestID));
                if (dtescaltion.Rows.Count > 0 && dtescaltion != null)
                {
                    string message1 = "";

                    message1 = "Hi," + dtescaltion.Rows[0]["EFirstName"] + "Commercial visit Request Id " + RequestID + " has been raised for your necessary action.  .Please log in with your account details on our website ( www.makenmake.in) to see the status of the ticket. ";

                       
                    SendSms objSms1 = new SendSms();
                    try
                    {
                        int i = objSms1.SendSmsOnMobile(message1, Convert.ToString(dtescaltion.Rows[0]["MobileNumber"]));
                        if (i != 1)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(Session[Constant.Session.AdminSession]), 0, "Ticket Status to Escation Manager", 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(Session[Constant.Session.AdminSession]), 0, "Error while Ticket Status to Client-Issue:-" + ex.Message, 1);
                    }

                    MEmail.SendGMail(Convert.ToString(dtescaltion.Rows[0]["EmailID"]), "Service Ticket Make 'N' Make", message1, "");
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot assign request because he has already holding five requests') ;", true);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("CommercialRequest.aspx");
        }

        protected void RptTickets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblreasn = (Label)e.Item.FindControl("lblcreated");
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
        //protected void FindEngineerStatus()
        //{
        //    DataTable dt = obj.FindEngineerStatus(Convert.ToInt64(ViewState["RequestID"]), "RequestID");
        //    int Status = Convert.ToInt32(dt.Rows[0]["Status"]);
        //    if (Status == 0 || Status == 1 || Status == 2 || Status == 4)
        //    {
        //        ddlEngineer.Enabled = true;
        //        ddlStatus.Enabled = true;
        //    }
        //    if (Status == 5)
        //    {
        //        ddlEngineer.Enabled = false;

        //        ddlStatus.Items.Insert(0, new ListItem("Accepted", "0"));
        //        ddlStatus.Enabled = false;
        //        divSubmitButton.Visible = false;
        //    }
        //    if (Status == 3)
        //    {
        //        ddlEngineer.Enabled = false;
        //        ddlStatus.Items.Insert(0, new ListItem("Completed", "0"));
        //        ddlStatus.Enabled = false;
        //        divSubmitButton.Visible = false;
        //    }
        //}
    }
}
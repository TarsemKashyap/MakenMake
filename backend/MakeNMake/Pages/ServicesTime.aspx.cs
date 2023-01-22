using AjaxControlToolkit;
using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.ServiceEngineer
{
    public partial class ServicesTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                BindTicketdata();
                BindTicketTime();
            }
        }
        public void BindNumbers(Dictionary<string,string > datasource)
        {
            rdbUserNumbers.DataSource = datasource;
            rdbUserNumbers.DataTextField = "Key";
            rdbUserNumbers.DataValueField = "Value";
            rdbUserNumbers.DataBind();

        }
        private void BindTicketTime()
        {
            BLServiceEngineer obj = new BLServiceEngineer();
            DataTable dt = obj.GetServiceTicketsByEngID(Convert.ToInt64(Session[Constant.Session.AdminSession]));
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
            BLServiceEngineer obj = new BLServiceEngineer();
            DataTable dt = obj.GetServiceTicketsForTiming(Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if (dt != null && dt.Rows.Count > 0)
            {
                dvticket.Visible = true;
                ddlticket.DataSource = dt;
                ddlticket.DataTextField = "TicketID";
                ddlticket.DataValueField = "ServiceID";
                ddlticket.DataBind();
                ddlticket.Items.Insert(0, new ListItem("--Select Ticket ID--", "0"));
            }
            else
            {
                dvticket.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No ticket is assigned to you so you cannot fill service time') ;", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }

        protected void btnSent_Click(object sender, EventArgs e)
        {

            string Otp = EncryptDecrypt.CreateRandomPassword(4);
            SendSms obj = new SendSms();
            BLCustomerCare objConsumer = new BLCustomerCare();


            string message = "Hi!" + hdnName.Value + ", Your OTP is " + Otp + " . Thanks MakeNake team";
            MEmail.SendGMail(hdnEmailid.Value, "Make n Make OTP", message, "");
            int i = obj.SendSmsOnMobile(message, rdbUserNumbers.SelectedValue);
            if (i == 1)
            {
                int result = objConsumer.AddUpdateUserOTP(Convert.ToInt64(hdnUserID.Value), Otp, hdnMobileNumber.Value, 1);
                dvVerifyOTP.Visible = true;
                dvOTP.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Some Problem occurs due to insufficient balance while sending message') ;", true);
            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            BL.BLCustomerCare objClient = new BL.BLCustomerCare();
            Int64 userID = Convert.ToInt64(hdnUserID.Value);
            int result = objClient.CheckUserOtp(userID, txtOTP.Text.Trim());
            if (result == 1)
            {
                int i = objClient.AddUpdateUserOTP(userID, string.Empty, string.Empty, 3);
                dvVerifyOTP.Visible = false;
                dvServiceTime.Visible = true;
                BLServiceEngineer obj = new BLServiceEngineer();
                int result1 = obj.CheckServiceStatus(Convert.ToInt64(hdntblID.Value));
                if (result1 > 0)
                {
                    hdntblID.Value = result1.ToString();
                    checkOutTime.Visible = true;
                    checkinTime.Visible = false;
                    txttimeto.Text = DateTime.Now.ToString("hh:mm");
                    txttimeto.Enabled = false;
                    ddlToTime.SelectedValue = DateTime.Now.ToString("tt");
                    ddlToTime.Enabled = false;
                }
                else if (result1 == -99)
                {
                    checkOutTime.Visible = false;
                    checkinTime.Visible = true;
                    txttimeFrom.Text = DateTime.Now.ToString("hh:mm");
                    txttimeFrom.Enabled = false;
                    ddlFromtime.SelectedValue = DateTime.Now.ToString("tt");
                    ddlFromtime.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Incorrect OTP') ;", true);
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string filenames = string.Empty;
            if (uploadFile.PostedFiles.Count > 0)
            {
               
                filenames += "<data>";
                int count = 0;
                string[] ArrayImages = NewImages.Value.Split(';');
                foreach (var file in uploadFile.PostedFiles)
                {
                    if (file.ContentLength  !=0)
                    { 
                        //string extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                        //if (extension == ".jpeg" || extension == ".jpg" || extension == ".png")
                        //{
                        if (ArrayImages.Contains(file.FileName))
                    {
                        string filename = DateTime.Now.ToString("ddMMyyyyhhmmss") + Path.GetFileName(file.FileName);
                        file.SaveAs(Server.MapPath("~/UserImages/Issues/") + filename);


                       System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("~/UserImages/Issues/") + filename);
                        System.Drawing.Image thumb = img.GetThumbnailImage(100, 90, null, IntPtr.Zero);
                        img.Dispose();

                        string thumbnailImage = "thumb" + (count++) + filename;
                        thumb.Save(Server.MapPath("~/UserImages/IssuesThumb/" + thumbnailImage));

                        filenames += "<child><thumb>" + thumbnailImage + "</thumb><image>" + filename + "</image></child>";
                    }
                  }
                }
                filenames += "</data>";
                NewImages.Value = string.Empty;
            }
            BLServiceEngineer obj = new BLServiceEngineer();
            int serviceID = Convert.ToInt32(Convert.ToInt64(ddlticket.SelectedValue.Substring(0, ddlticket.SelectedValue.IndexOf("$"))));
            Int64 ticketID = Convert.ToInt64(ddlticket.SelectedValue.Substring(ddlticket.SelectedValue.IndexOf("$") + 1));
            Int64 openedFor = Convert.ToInt64(obj.GetUserIDByTicketID(ticketID));
            int result = 0;
            if (checkOutTime.Visible)
            {
                result = obj.AddserviceTime(openedFor, ticketID, Convert.ToInt64(Session[Constant.Session.AdminSession]),
                serviceID, txttimeFrom.Text + " " + ddlFromtime.SelectedItem.Text, txttimeto.Text + " " +
                ddlToTime.SelectedItem.Text, filenames, Convert.ToInt64(hdntblID.Value), txtwork.Text.Replace("$",""));
            }
            else
            {
                result = obj.AddserviceTime(openedFor, ticketID, Convert.ToInt64(Session[Constant.Session.AdminSession]),
                   serviceID, txttimeFrom.Text + " " + ddlFromtime.SelectedItem.Text,
                   txttimeto.Text + " " + ddlToTime.SelectedItem.Text, filenames, 0, txtwork.Text.Replace("$", ""));
            }

            if (result > 0)
            {
                ddlticket.Enabled = true;
                ddlticket.SelectedValue = "0";
                txttimeFrom.Text = string.Empty;
                txttimeto.Text = string.Empty;
                dvServiceTime.Visible = false;
                BindTicketTime();
                txtOTP.Text = string.Empty;
                txtwork.Text = string.Empty;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Service Time Added') ;", true);
            }
            else if (result == -96)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot add service time Because client has changed the plan') ;", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Service Time for today was already Added') ;", true);
            }
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('" + NewImages.Value + "') ;", true);
           
        }

        protected void ddlticket_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdntblID.Value = string.Empty;
            if (ddlticket.SelectedValue != "0")
            {
                BLServiceEngineer objeng = new BLServiceEngineer();
                Int64 openedFor = Convert.ToInt64(objeng.GetUserIDByTicketID(Convert.ToInt64(ddlticket.SelectedValue.Substring(ddlticket.SelectedValue.IndexOf("$")+1))));
                Common obj = new Common();
                DataTable dt = obj.GetUserInfoByID(openedFor);
                string mnumber = Convert.ToString(dt.Rows[0]["MNumber"]);
                if (string.IsNullOrEmpty(mnumber))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please ask client to update his/her number') ;", true);
                }
                else
                {
                    hdntblID.Value = ddlticket.SelectedValue.Substring(ddlticket.SelectedValue.IndexOf("$")+1);
                    dvOTP.Visible = true;
                    hdnName.Value = Convert.ToString(dt.Rows[0]["firstname"]);
                    hdnUserID.Value = Convert.ToString(dt.Rows[0]["UserID"]);
                    hdnEmailid.Value = Convert.ToString(dt.Rows[0]["Emailid"]);
                    hdnMobileNumber.Value = mnumber;

                    string alternateNumbers = Convert.ToString(dt.Rows[0]["AlternateNumbers"]);
                    Dictionary<string, string> numbers = new Dictionary<string, string>();
                    if (string.IsNullOrEmpty(alternateNumbers) || alternateNumbers == "")
                    {
                        numbers.Add(hdnMobileNumber.Value, hdnMobileNumber.Value);
                    }
                    else
                    {
                        numbers.Add(hdnMobileNumber.Value, hdnMobileNumber.Value);
                        string[] splitNumbers = alternateNumbers.Split(',');
                        foreach (var phonenumber in splitNumbers)
                        {
                            numbers.Add(phonenumber, phonenumber);
                        }
                    }
                    BindNumbers(numbers);
                    ddlticket.Enabled = false;
                    BLServiceEngineer objj = new BLServiceEngineer();
                    int result1 = objj.CheckServiceStatus(Convert.ToInt64(hdntblID.Value));
                    if (result1 > 0)
                    {                        
                        btnSent.Text = "Send Check-Out Time OTP to Customer";
                    }
                    else if (result1 == -99)
                    {
                        btnSent.Text = "Send Check-In Time OTP to Customer";
                    }
                }
            }
            else
            {
                ddlticket.Enabled = true;
                ddlticket.SelectedValue = "0";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ddlFromtime.Enabled = true;
            ddlToTime.Enabled = true;
            ddlticket.Enabled = true;
            ddlticket.SelectedValue = "0";
            txttimeFrom.Enabled = true;
            txttimeto.Enabled = true;
            txttimeFrom.Text = string.Empty;
            txttimeto.Text = string.Empty;
            dvServiceTime.Visible = false;
        }

        protected void RptTickets_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "viewimage")
            {
                //  hdnCustomer.Value = Convert.ToString("../UserImages/Issues/"+e.CommandArgument);
                Response.Redirect("ViewImages.aspx?ImgID=" + Utilities.EncryptDecrypt.Encript(Convert.ToString(e.CommandArgument)));
            }
        }

        protected void RptTickets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblreasn = (Label)e.Item.FindControl("Label3");
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

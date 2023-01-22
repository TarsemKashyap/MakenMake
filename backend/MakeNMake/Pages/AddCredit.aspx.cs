using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MakeNMake.BL;
using System.Data;
using NLog;
using MakeNMake.CommomFunctions;

namespace MakeNMake
{
    public partial class AddCredit : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        int pagesize;
        BLAdmin objAdmin = new BLAdmin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataList();
            }
        }
        DataTable GetBindUserPayment(int pagesize, int currentpage)
        {
            DataTable dtable = objAdmin.GetClientList(pagesize, currentpage, txtSearchclient.Text);
            return dtable;
        }
        protected void clear()
        {
            Lblemail.Text = string.Empty;
            lblname.Text = string.Empty;
            txtamt.Text = string.Empty;
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
            DataTable dt = GetBindUserPayment(pagesize, CurrentPage);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / pagesize));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];

            if (dt != null && dt.Rows.Count > 0)
            {
                Rptclient.DataSource = dt;
                Rptclient.DataBind();
            }
            else
            {
                Rptclient.DataSource = null;
                Rptclient.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "PageReload()", true);
            }

            doPaging();
            RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

            return (Convert.ToInt32(dt.Rows.Count));

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
        protected void ddlIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindDataList();
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
        protected void RepeaterPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            LinkButton lnkPage = (LinkButton)e.Item.FindControl("Pagingbtn");
            if (lnkPage.CommandArgument.ToString() == CurrentPage.ToString())
            {
                lnkPage.Enabled = false;
                lnkPage.BackColor = System.Drawing.Color.FromName("#970915");
            }
        }

        protected void Rptclient_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AddCredit")
            {
                try
                {
                    
                    divlist.Visible = true;
                    hdnuserID.Value = Convert.ToString(e.CommandArgument);
                    // hdnuserID.Value = Convert.ToString(e.CommandArgument);
                    Label Label1 = (Label)e.Item.FindControl("Label1");
                    Label lblEmailID = (Label)e.Item.FindControl("lblEmailID");
                    Label lblmobilenumber = (Label)e.Item.FindControl("lblmobilenumber");
                    btnSubmit.Text = "Add Credit";
                    HiddenField hdninvoice = (HiddenField)e.Item.FindControl("hdninvoice");
                    lblname.Text = Label1.Text;
                    Lblemail.Text = lblEmailID.Text;
                    txtMobileNumber.Text = lblmobilenumber.Text;

                }
                catch (Exception ee)
                {
                }
            }
        }
        //public void SendSms()
        //{
        //    BLAdmin getUsers = new BLAdmin();
        //    DataTable dt = getUsers.GetUsersByRoleID(Convert.ToInt64(hdnuserID.Value));
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            string message = "Hi ," + lblname.Text + "!Amount of INR " + txtamt.Text + "has been credited to your account.Please log in with your account details on our website(www.makenmake.co.in) to see the status of the ticket.";
        //           // string message = txtmsg.Text;

        //            MEmail.SendGMail(Convert.ToString(dt.Rows[i]["EmailID"]), "Make n Make Annoucements", message, "");

        //            SendSms objSms = new SendSms();
        //            try
        //            {
        //                int length = Convert.ToString(dt.Rows[i]["MobileNumber"]).Length;
        //                if (length > 0)
        //                {
        //                    int j = objSms.SendSmsOnMobile(message, Convert.ToString(dt.Rows[i]["MobileNumber"]));
        //                    if (j != 1)
        //                    {
        //                        BL.BLAdmin objAdmin = new BL.BLAdmin();
        //                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(dt.Rows[i]["UserID"]), 0, "Sending Announcements", 1);
        //                    }
        //                }
        //                else
        //                {
        //                    BL.BLAdmin objAdmin = new BL.BLAdmin();
        //                    objAdmin.AddNotSendSmsMail(Convert.ToInt64(dt.Rows[i]["UserID"]), 0, "Number not Avaiable:-", 1);

        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                BL.BLAdmin objAdmin = new BL.BLAdmin();
        //                objAdmin.AddNotSendSmsMail(Convert.ToInt64(dt.Rows[i]["UserID"]), 0, "Error while Sending Announcements to Client-Issue:-" + ex.Message, 1);
        //            }
        //        }
        //    }
        //}
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSubmit.Text == "Add Credit")
                {
                    DateTime Created = DateTime.Now;
                    DateTime Modified = DateTime.Now;
                    BLAdmin objEdit = new BLAdmin();

                    int result = objEdit.AddUserPayment(0, Convert.ToInt64(hdnuserID.Value), Convert.ToDecimal(txtamt.Text), Convert.ToInt64(Session[Constant.Session.AdminSession]), Created, Modified, Convert.ToInt64(Session[Constant.Session.AdminSession]));
                   

                    if (result > 0)                                                                                                             
                    {
                        string message = "Hi ," + lblname.Text + "!Amount of INR " + txtamt.Text + " has been credited to your account.Please log in with your account details on our website(www.makenmake.in)/Mobile App to see the status of the ticket.Or call us at Helpline Nos." + ReadConfig.helpLineNumber;
                        SendSms objSms = new SendSms();
                        try
                        {
                            int i = objSms.SendSmsOnMobile(message, txtMobileNumber.Text);
                            if (i != 1)
                            {
                                BL.BLAdmin objAdmin = new BL.BLAdmin();
                                objAdmin.AddNotSendSmsMail(Convert.ToInt64(hdnuserID.Value), 0, "Updating Client Profile By Admin", 1);
                            }
                        }
                        catch (Exception ex)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(hdnuserID.Value), 0, "Error while Updating Client Profile By Admin -Issue:-" + ex.Message, 1);
                        }
                        MEmail.SendGMail(Lblemail.Text.Trim(),"Regarding Credit Amount", "Hi ," + lblname.Text + "!Amount of INR " + txtamt.Text + " has been credited to your account.Please log in with your account details "+ Lblemail.Text + "on our website(www.makenmake.in)/Mobile App to see the status of the ticket.Or call us at Helpline Nos." + ReadConfig.helpLineNumber,"");
                       // MEmail.SendGMail(txtEmailID.Text.Trim(), "Registration Make 'N' Make", "Dear User,<br><br> You have successfully registered with Make N Make . Login with your credentials :<br><br> User ID: " + txtEmailID.Text + "<br>Password: " + password + " <br><br> Click on this link: <a target='_blank' style='color:blue;'  href='" + VerifyUrl + "'>" + VerifyUrl + "</a><br><br>You can change the password after login to our site with these credentials.If your email system does not allow linking, please copy and paste the following into your browser. <br><br>Kind Regards:<br> Make 'N' Make Service Team", "");



                        //"Registration Make 'N' Make", "Dear User,<br><br> You have successfully registered with Make N Make . Login with your credentials :<br><br> User ID: " + txtEmailID.Text + "<br>Password: " + password + " <br><br> Click on this link: <a target='_blank' style='color:blue;'  href='" + VerifyUrl + "'>" + VerifyUrl + "</a><br><br>You can change the password after login to our site with these credentials.If your email system does not allow linking, please copy and paste the following into your browser. <br><br>Kind Regards:<br> Make 'N' Make Service Team", "");
                        clear();
                        divlist.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Credit Amount Updated sucessfully.') ;", true);
                        BindDataList();


                    }

                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                //  clear();
                logger.Error(logger.Name + ":" + ex.Message);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtSearchclient.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Enter name , emailid or mobilenumber ') ;", true);
            }
            else
            {
                BindDataList();
            }
        }
    }
}
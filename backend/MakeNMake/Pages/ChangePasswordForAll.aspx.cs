using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MakeNMake.BL;
using System.Data;
using MakeNMake.CommomFunctions;
using NLog;
using MakeNMake.Utilities;

namespace MakeNMake.Admin
{
    public partial class ChangePasswordForAll : System.Web.UI.Page
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
        DataTable GetBindAllUser(int pagesize, int currentpage, string UserName)
        {
            DataTable dtable = objAdmin.GetAllUserPassword(pagesize, currentpage, UserName);
            return dtable;
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
            DataTable dt = GetBindAllUser(pagesize, CurrentPage, txtSearchclient.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / pagesize));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];

            if (dt != null && dt.Rows.Count > 0)
            {
                  Rptpassword.Visible = true;
                  Rptpassword.DataSource = dt;
                  Rptpassword.DataBind();
                
                  
               }
            else
               {
                  Rptpassword.Visible = false;
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

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
         
            if (txtSearchclient.Text != "")
            {
                CurrentPage = 0;
                int x = BindDataList();
                if (x != 0)
                {
                    tblPaging.Visible = true;
                    divClientList.Visible = true;
                }
                else
                {
                    tblPaging.Visible = false;
                    divClientList.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('No Record Found!') ;", true);
                    txtSearchclient.Text = null;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please Enter the Name Or Mobile Number!') ;", true);
            }
        
        }

        protected void Rptpassword_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpdatePassword")
            {
                try
                {
                    divlist.Visible = true;
                    //hdnroleid.Value=Convert.ToString(e.CommandArgument);
                   // hdnuserID.Value = Convert.ToString(e.CommandArgument);
                    Label Label1 = (Label)e.Item.FindControl("Label1");
                    Label lblEmailID = (Label)e.Item.FindControl("lblEmailID");
                    Label lblmobilenumber = (Label)e.Item.FindControl("lblmobilenumber");

                    btnSubmit.Text = "Update Password";
                    HiddenField hdnID = (HiddenField)e.Item.FindControl("hdnID");
                    hdnMobileNumber.Value = lblmobilenumber.Text;

                    lblname.Text = Label1.Text;
                    Lblemail.Text = lblEmailID.Text;
                    //txttitle.Text = lbltitle.Text;

                }
                catch (Exception ee)
                {
                }
            }
        }
        protected void clear()
        {
            Lblemail.Text = string.Empty;
            lblname.Text = string.Empty;
            txtpass.Text = string.Empty;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSubmit.Text == "Update Password")
                {
                    BLAdmin objEdit = new BLAdmin();
                    string password = Utilities.EncryptDecrypt.Encript(txtpass.Text);
                    int result = objEdit.UpdateAllUserPassword(Lblemail.Text, password);
                    if (result > 0)
                    {
                        string VerifyUrl = string.Format("{0}Default.aspx", ReadConfig.SiteUrl);
                        MEmail.SendGMail(Lblemail.Text,
                            "Change Password Make 'N' Make", "Dear User,<br><br> You have successfully changed password with MakenMake .<br/><br/> Login with your credentials :<br/> User ID: " 
                            + Lblemail.Text + "<br>Password: " + txtpass.Text + " <br> <br> Click on this link: <a target='_blank' style='color:blue;'  href='" + VerifyUrl + "'>" +
                            VerifyUrl + "</a><br>You can change the password after login to our site with these credentials.If your email system does not allow linking, please copy and paste the following into your browser .For any queries or complaints, you have our ears at Helpline Nos : "+ReadConfig.helpLineNumber+"  or log in with your account details on our website (www.makenmake.in)/Mobile App <br><br>Kind Regards:<br> Make 'N' Make Service Team", "");
                        if (hdnMobileNumber.Value != "")
                        {
                            string message1 = "Dear User,You have successfully changed password with MakenMake Account .For any queries or complaints, you have our ears at Helpline Nos : " + ReadConfig.helpLineNumber + " or log in with your account details on our website (www.makenmake.in)/Mobile App";
                            SendSms objSms = new SendSms();
                            try
                            {
                                int i = objSms.SendSmsOnMobile(message1, hdnMobileNumber.Value);
                                if (i != 1)
                                {
                                    BL.BLAdmin objAdmin = new BL.BLAdmin();
                                    objAdmin.AddNotSendSmsMail(result, 0, "Sent to Client when Change pAssword", 1);
                                }
                            }
                            catch (Exception ex)
                            {
                                BL.BLAdmin objAdmin = new BL.BLAdmin();
                                objAdmin.AddNotSendSmsMail(result, 0, "Error while Sent to Client when Change pAssword-Issue:-" + ex.Message, 1);
                            }
                        }
                        else
                        {
                            MEmail.SendGMail(Lblemail.Text, "Update MobileNumber Make n Make", "Dear User , <br/><br/>Please update you mobile number through MakenMake portal to enjoy the services", "");
                        }
                        BindDataList();
                        clear();
                        divlist.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "PageReload()", true);
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }
    }
}
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
using System.Text;
using System.Globalization;
namespace MakeNMake.Admin
{
    public partial class AddUser : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        BLAdmin addUser = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        int pagesize;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRoles();
                BindCountry();
                BindZones(); LoadDDL();
                BindDataList();
                
                rdbStatus.SelectedValue = "1";
            }
        }
        DataTable GetData(int pagesize, int currentpage, string UserName)
        {
            DataTable dtable = addUser.Getuserinfo(pagesize, currentpage, UserName);
            return dtable;
        }
        public void bindalternateno(Int64 userid)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            DataTable dt = obj.GetUseralternateno(userid);
            if (dt.Rows.Count > 0)
            {
                int rowcount = dt.Rows.Count;
                if (rowcount > 0)
                {
                    txtAltMob1.Text = Convert.ToString(dt.Rows[0]["ContactNumber"]);
                }
                if (rowcount > 1)
                {
                    txtAltMob2.Text = Convert.ToString(dt.Rows[1]["ContactNumber"]);
                }
                if (rowcount > 2)
                {
                    txtAltMob3.Text = Convert.ToString(dt.Rows[2]["ContactNumber"]);
                }
                if (rowcount > 3)
                {
                    txtAltMob4.Text = Convert.ToString(dt.Rows[3]["ContactNumber"]);
                }
            }

        }
        protected void RptAllUser_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "delete")
                {
                    HiddenField UserID = (HiddenField)e.Item.FindControl("hdnID");
                    BLAdmin objDelete = new BLAdmin();
                    int result = objDelete.DeleteUser(Convert.ToInt64(UserID.Value), Convert.ToInt64(Session[Constant.Session.AdminSession]));
                    if (result == 1)
                    {
                        BindDataList();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Deleted') ;", true);
                    }
                    else if (result == -98)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You cannot delete this user because of existence in tickets or appointments') ;", true);
                    }
                }
                else if (e.CommandName == "edit")
                {

                    divuser.Visible = false;
                    divedituser.Visible = true;
                    hdnuserID.Value = Convert.ToString(e.CommandArgument);
                    HiddenField hdnRoleID = (HiddenField)e.Item.FindControl("hdnRoleID");
                    Label lbladdress = (Label)e.Item.FindControl("lbladdress");
                    HiddenField zone = (HiddenField)e.Item.FindControl("hdnZone");
                    HiddenField subZone = (HiddenField)e.Item.FindControl("hdnSubZone");
                    HiddenField hdncountry = (HiddenField)e.Item.FindControl("hdncountry");
                    HiddenField hdnstate = (HiddenField)e.Item.FindControl("hdnstate");
                    HiddenField hdndistrict = (HiddenField)e.Item.FindControl("hdndistrict");
                    HiddenField hdncity = (HiddenField)e.Item.FindControl("hdncity");
                    HiddenField hdndob = (HiddenField)e.Item.FindControl("hdndob");
                    HiddenField hdnaddress = (HiddenField)e.Item.FindControl("hdnaddress");

                    if (hdnRoleID.Value == "4")
                    {
                        dvAlternateMobile.Visible = true;
                        bindalternateno(Convert.ToInt64(hdnuserID.Value));
                    }
                    else
                    {
                        dvAlternateMobile.Visible = false;
                    }
                    //if (hdnRoleID.Value == "2" || hdnRoleID.Value == "3" || hdnRoleID.Value == "4")
                    //{
                    dvaddress.Visible = true;
                    divadd2.Visible = true;
                    dvcountry.Visible = true;
                    dvstate.Visible = true;
                    dvdistrict.Visible = true;
                    dvcity.Visible = true;
                    dvZone.Visible = true;
                    dvSubZone.Visible = true;
                    ddlZone.SelectedValue = zone.Value;
                    BindSubZones(Convert.ToInt32(zone.Value));
                    ddlSubZone.SelectedValue = subZone.Value;
                    BindCountry();
                    if (hdncountry.Value == "")
                    {
                        ddlCountry.Items.Add(new ListItem("--Select Country--", "0"));
                        ddlState.Items.Add(new ListItem("--Select State--", "0"));
                        ddlDistrict.Items.Add(new ListItem("--Select District--", "0"));
                        ddlCity.Items.Add(new ListItem("--Select City--", "0"));
                    }
                    else
                    {
                        ddlCountry.SelectedValue = hdncountry.Value;
                        BindState(Convert.ToInt32(hdncountry.Value));
                        if (hdnstate.Value == "")
                        {
                            ddlState.Items.Add(new ListItem("--Select State--", "0"));
                        }
                        else
                        {
                            ddlState.SelectedValue = hdnstate.Value;
                            BindDistrict(Convert.ToInt32(hdnstate.Value));
                            if (hdndistrict.Value == "")
                            {
                                ddlDistrict.Items.Add(new ListItem("--Select District--", "0"));
                            }
                            else
                            {
                                ddlDistrict.SelectedValue = hdndistrict.Value;
                                BindCity(Convert.ToInt32(hdndistrict.Value));
                                if (hdncity.Value == "")
                                {
                                    ddlCity.Items.Add(new ListItem("--Select City--", "0"));
                                }
                                else
                                {
                                    ddlCity.SelectedValue = hdncity.Value;
                                }
                            }
                        }
                    }


                    txtDob.Text = hdndob.Value == "" ? "" : Convert.ToDateTime(hdndob.Value) == null ? "" : Convert.ToDateTime(hdndob.Value).ToString("dd/MM/yyyy");
                    string[] values = hdnaddress.Value.Split('+');
                    if (values.Length > 0)
                    {
                        txtaddresslocality.Text = values[0].Trim();
                        txtstreet.Text = values[1].Trim();
                    }
                    else
                    {
                        txtaddresslocality.Text = hdnaddress.Value;
                    }
                    //}
                    //else
                    //{
                    //    dvcountry.Visible = false;
                    //    dvstate.Visible = false;
                    //    dvdistrict.Visible = false;
                    //    dvcity.Visible = false;
                    //    dvZone.Visible = false;
                    //    dvSubZone.Visible = false;
                    //    ddlSubZone.Items.Clear();
                    //}
                    ddlRole.SelectedValue = hdnRoleID.Value;

                    Label lblName = (Label)e.Item.FindControl("lblname");
                    Label lbllname = (Label)e.Item.FindControl("lbllname");

                    Label lblEmail = (Label)e.Item.FindControl("lblEmailid");
                    Label lblrole = (Label)e.Item.FindControl("Lblrolename");
                    LinkButton lblmobile = (LinkButton)e.Item.FindControl("lblmobile");
                    Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                    Label lblModified = (Label)e.Item.FindControl("lblModified");

                    modifiedDate.Visible = true;
                    txtModified.Text = lblModified.Text;
                    txtModified.Enabled = false;
                    btnSubmit.Text = "Edit";
                    txtfirstname.Text = lblName.Text;
                    txtlastname.Text = lbllname.Text;
                    txtEmailID.Text = lblEmail.Text;
                    rdbStatus.SelectedValue = lblStatus.Text == "Active" ? "1" : "0";
                    txtMobile.Text = lblmobile.Text;
                    txtfirstname.Enabled = false;
                    ddlRole.Enabled = false;
                    // txtEmailID.Enabled = false;

                    dvPages.Visible = true;
                    treeMap.Nodes.Clear();
                    BindTree(Convert.ToInt32(hdnRoleID.Value), 0);
                    GetUserRolesPermission(Convert.ToInt64(hdnuserID.Value));
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "callingMYFunction", "rdFunction() ;", true);
                }
            }
            catch (Exception ex)
            {
                clear();
                logger.Error(logger.Name + ":" + ex.Message);
            }
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
            DataTable dt = GetData(pagesize, CurrentPage, txtSearchclient.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / pagesize));
            }
            pgsource.DataSource = dt.DefaultView;
            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];
            RptAllUser.DataSource = dt;
            RptAllUser.DataBind();
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
        public void BindZones()
        {
            addUser.GetZones(ddlZone);
        }
        public void BindSubZones(int ZoneID)
        {
            ddlSubZone.Items.Clear();
            addUser.GetSubZoneDistrict(ddlSubZone, ZoneID);
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSubmit.Text.ToLower() == "add")
                {
                    string password = EncryptDecrypt.CreateRandomPassword(6);
                    int status = Convert.ToInt32(rdbStatus.SelectedValue);
                    StringBuilder parentRole = new StringBuilder();
                    StringBuilder childRole = new StringBuilder();
                    int roleCount = 0;
                    int childRoleCount = 0;
                    bool isChildRoleChecked = true;
                    string role = string.Empty;
                    foreach (TreeNode node in treeMap.Nodes)
                    {
                        if (node.Checked)
                        {
                            roleCount++;
                            parentRole.Append(node.Value + ",");
                            role = node.Text;
                            foreach (var childnodes in node.ChildNodes)
                            {
                                if (((TreeNode)childnodes).Checked)
                                {
                                    childRoleCount++;
                                    childRole.Append(((TreeNode)childnodes).Value + ",");
                                }
                            }
                            if (childRoleCount < 1)
                            {
                                isChildRoleChecked = false;
                                break;
                            }
                        }
                        childRoleCount = 0;
                    }
                    if (isChildRoleChecked == false)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select subrole for " + role + " role') ;", true);
                    }

                    else
                    {
                        string parent = Convert.ToString(parentRole);
                        string child = Convert.ToString(childRole);
                        string alternatemobileno1 = "";
                        string alternatemobileno2 = "";
                        string alternatemobileno3 = "";
                        string alternatemobileno4 = "";
                        var resultchekcmobilenoexists = addUser.checkAnyphonenoexists(txtMobile.Text);
                        if (resultchekcmobilenoexists.Rows.Count > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Mobile number that you entered already exists as the primary number of another user') ;", true);
                        }
                        else
                        {
                            if (ddlRole.SelectedItem.Text == "Customer")
                            {
                                var resultphonenoexists = addUser.checkphonenoexistsinMobileno(txtAltMob1.Text + "," + txtAltMob2.Text + "," + txtAltMob3.Text + "," + txtAltMob4.Text);
                                if (resultphonenoexists.Rows.Count > 0)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('One of the alternate mobile number that you entered already exists as the primary number of another user') ;", true);
                                }
                                else
                                {
                                    var result1 = addUser.checkphonenoexists(txtAltMob1.Text + "," + txtAltMob2.Text + "," + txtAltMob3.Text + "," + txtAltMob4.Text);

                                    if (result1.Rows.Count > 0)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('One of the alternate mobile number that you entered already exists as the primary number of another user') ;", true);
                                    }
                                    else
                                    {
                                        alternatemobileno1 = txtAltMob1.Text;

                                        alternatemobileno2 = txtAltMob2.Text;

                                        alternatemobileno3 = txtAltMob3.Text;


                                        alternatemobileno4 = txtAltMob4.Text;
                                        if (roleCount > 0)
                                        {
                                            //string dob = Convert.ToDateTime(txtDob.Text).ToString("MM/dd/yyyy");
                                            string dob = DateTime.ParseExact(txtDob.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                                            string address = txtaddresslocality.Text + "+" + txtstreet.Text;
                                            int result = addUser.AddUsers(txtfirstname.Text, txtlastname.Text, txtEmailID.Text, EncryptDecrypt.Encript(password), Convert.ToDateTime(dob),address,
                                                Convert.ToInt32(ddlCountry.SelectedItem.Value),
                                                Convert.ToInt64(ddlState.SelectedItem.Value), Convert.ToInt64(ddlDistrict.SelectedItem.Value), Convert.ToInt64(ddlCity.SelectedItem.Value),
                                                Convert.ToInt32(ddlRole.SelectedItem.Value), Convert.ToInt32(ddlZone.SelectedItem.Value),
                                                Convert.ToInt32(ddlSubZone.SelectedItem == null ? "0" : ddlSubZone.SelectedItem.Value),
                                                Convert.ToInt64(Session[Constant.Session.AdminSession]), status, parent.Substring(0, parent.Length - 1), child.Substring(0, child.Length - 1), txtMobile.Text, alternatemobileno1, alternatemobileno2, alternatemobileno3, alternatemobileno4, Convert.ToInt64(Session[Constant.Session.AdminSession]));

                                            if (result > 0)
                                            {
                                                BindDataList();
                                                string VerifyUrl = string.Format("{0}Default.aspx", ReadConfig.SiteUrl);
                                                if (ddlRole.SelectedValue == "4")
                                                {
                                                    string message = "Hi , " + txtfirstname.Text + " ! Thanks for Registering with us. A warm welcome to the MakenMake family. Give us a chance to serve you. Your credentials have been sent to registered E-mail Id or directly be in touch with us through our Helpline No : " + ReadConfig.helpLineNumber;
                                                    SendSms objSms = new SendSms();
                                                    try
                                                    {
                                                        int i = objSms.SendSmsOnMobile(message, txtMobile.Text);
                                                        if (i != 1)
                                                        {
                                                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                                                            objAdmin.AddNotSendSmsMail(result, 0, "Sent to AdminUser when SignUp", 1);
                                                        }

                                                        txtMobile.Text = string.Empty;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                                                        objAdmin.AddNotSendSmsMail(result, 0, "Error while Sent to AdminUser when SignUp-Issue:-" + ex.Message, 1);
                                                    }
                                                    MEmail.SendGMail(txtEmailID.Text.Trim(), "Registration Make 'N' Make", "Dear User,<br><br> You have successfully registered with Make N Make . Login with your credentials :<br><br> User ID: " + txtEmailID.Text + "<br>Password: " + password + " <br><br> Click on this link: <a target='_blank' style='color:blue;'  href='" + VerifyUrl + "'>" + VerifyUrl + "</a><br><br>You can change the password after login to our site with these credentials.If your email system does not allow linking, please copy and paste the following into your browser. <br><br>Kind Regards:<br> Make 'N' Make Service Team", "");
                                                }
                                            }

                                            clear();
                                            dvPages.Visible = false;
                                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('" + Constant.ErrorCodeForAdminUser(result) + "') ;", true);
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please Select atleast one role') ;", true);
                                        }




                                    }
                                }


                            }

                            else
                            {
                                if (roleCount > 0)
                                {

                                    //string dob = Convert.ToDateTime(txtDob.Text).ToString("MM/dd/yyyy");
                                    string dob = DateTime.ParseExact(txtDob.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                                    string address = txtaddresslocality.Text + "+" + txtstreet.Text;
                                    int result = addUser.AddUsers(txtfirstname.Text, txtlastname.Text, txtEmailID.Text, EncryptDecrypt.Encript(password), Convert.ToDateTime(dob),address,
                                        Convert.ToInt32(ddlCountry.SelectedItem.Value),
                                        Convert.ToInt64(ddlState.SelectedItem.Value), Convert.ToInt64(ddlDistrict.SelectedItem.Value), Convert.ToInt64(ddlCity.SelectedItem.Value),
                                        Convert.ToInt32(ddlRole.SelectedItem.Value), Convert.ToInt32(ddlZone.SelectedItem.Value),
                                        Convert.ToInt32(ddlSubZone.SelectedItem == null ? "0" : ddlSubZone.SelectedItem.Value),
                                        Convert.ToInt64(Session[Constant.Session.AdminSession]), status, parent.Substring(0, parent.Length - 1), child.Substring(0, child.Length - 1), txtMobile.Text, alternatemobileno1, alternatemobileno2, alternatemobileno3, alternatemobileno4, Convert.ToInt64(Session[Constant.Session.AdminSession]));

                                    if (result > 0)
                                    {
                                       
                                        //string VerifyUrl = string.Format("{0}Default.aspx", ReadConfig.SiteUrl);
                                        //string message = "Hi , " + txtfirstname.Text + " ! Thanks for Registering with us. A warm welcome to the MakenMake family. Give us a chance to serve you. Your credentials have been sent to registered E-mail Id or directly be in touch with us through our Helpline No : " + ReadConfig.helpLineNumber;
                                        //SendSms objSms = new SendSms();
                                        //try
                                        //{
                                        //    int i = objSms.SendSmsOnMobile(message, txtMobile.Text);
                                        //    if (i != 1)
                                        //    {
                                        //        BL.BLAdmin objAdmin = new BL.BLAdmin();
                                        //        objAdmin.AddNotSendSmsMail(result, 0, "Sent to AdminUser when SignUp", 1);
                                        //    }

                                        //    txtMobile.Text = string.Empty;
                                        //}
                                        //catch (Exception ex)
                                        //{
                                        //    BL.BLAdmin objAdmin = new BL.BLAdmin();
                                        //    objAdmin.AddNotSendSmsMail(result, 0, "Error while Sent to AdminUser when SignUp-Issue:-" + ex.Message, 1);
                                        //}
                                        //MEmail.SendGMail(txtEmailID.Text.Trim(), "Registration Make 'N' Make", "Dear User,<br><br> You have successfully registered with Make N Make . Login with your credentials :<br><br> User ID: " + txtEmailID.Text + "<br>Password: " + password + " <br><br> Click on this link: <a target='_blank' style='color:blue;'  href='" + VerifyUrl + "'>" + VerifyUrl + "</a><br><br>You can change the password after login to our site with these credentials.If your email system does not allow linking, please copy and paste the following into your browser. <br><br>Kind Regards:<br> Make 'N' Make Service Team", "");
                                        BindDataList();
                                    }

                                    clear();
                                    dvPages.Visible = false;
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('" + Constant.ErrorCodeForAdminUser(result) + "') ;", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please Select atleast one role') ;", true);
                                }
                            }
                        }

                    }
                }

                else
                {
                    BL.BLAdmin objService = new BL.BLAdmin();

                    StringBuilder parentRole = new StringBuilder();
                    StringBuilder childRole = new StringBuilder();
                    int roleCount = 0;
                    int childRoleCount = 0;
                    bool isChildRoleChecked = true;
                    string role = string.Empty;
                    foreach (TreeNode node in treeMap.Nodes)
                    {
                        if (node.Checked)
                        {
                            roleCount++;
                            parentRole.Append(node.Value + ",");
                            role = node.Text;
                            foreach (var childnodes in node.ChildNodes)
                            {
                                if (((TreeNode)childnodes).Checked)
                                {
                                    childRoleCount++;
                                    childRole.Append(((TreeNode)childnodes).Value + ",");
                                }
                            }
                            if (childRoleCount < 1)
                            {
                                isChildRoleChecked = false;
                                break;
                            }
                        }
                        childRoleCount = 0;
                    }
                    if (isChildRoleChecked == false)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please select subrole for " + role + " role') ;", true);
                    }
                    else
                    {
                        string parent = Convert.ToString(parentRole);
                        string child = Convert.ToString(childRole);
                        string alternatemobileno1 = "";
                        string alternatemobileno2 = "";
                        string alternatemobileno3 = "";
                        string alternatemobileno4 = "";


                        if (roleCount > 0)
                        {
                            //string dob = Convert.ToDateTime(txtDob.Text).ToString("MM/dd/yyyy");
                            string dob = DateTime.ParseExact(txtDob.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                            string address = txtaddresslocality.Text + "+" + txtstreet.Text;
                            int result = objService.UpdateUserlist(Convert.ToInt64(hdnuserID.Value), txtEmailID.Text, txtlastname.Text, Convert.ToDateTime(dob), address,
                               Convert.ToInt32(ddlCountry.SelectedItem.Value), Convert.ToInt64(ddlState.SelectedItem.Value), Convert.ToInt64(ddlDistrict.SelectedItem.Value), Convert.ToInt64(ddlCity.SelectedItem.Value),
                                Convert.ToInt32(ddlRole.SelectedValue), Convert.ToInt32(ddlZone.SelectedValue),
                                Convert.ToInt64(ddlSubZone.SelectedValue == "" ? "0" : ddlSubZone.SelectedValue), Convert.ToInt32(rdbStatus.SelectedItem.Value),
                                 parent.Substring(0, parent.Length - 1), child.Substring(0, child.Length - 1), txtMobile.Text, Convert.ToInt64(Session[Constant.Session.AdminSession]));

                            if (result == 1)
                            {
                                var resultphonenoexists = addUser.checkphonenoexistsinMobileno(txtAltMob1.Text + "," + txtAltMob2.Text + "," + txtAltMob3.Text + "," + txtAltMob4.Text);
                                if (resultphonenoexists.Rows.Count > 0)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Any Alternate Mobile no  is already exists in users  primary no') ;", true);
                                }
                                else
                                {
                                    if (ddlRole.SelectedItem.Text == "Customer")
                                    {
                                        alternatemobileno1 = txtAltMob1.Text;
                                        alternatemobileno2 = txtAltMob2.Text;
                                        alternatemobileno3 = txtAltMob3.Text;
                                        alternatemobileno4 = txtAltMob4.Text;
                                        Common objc = new Common();
                                        int resultaltno = objc.UpdateUserAlternateno(alternatemobileno1, alternatemobileno2, alternatemobileno3, alternatemobileno4, Convert.ToInt64(hdnuserID.Value));
                                    }
                                }
                                string message = "Hi ," + txtfirstname.Text + "! Your details have been updated as per your request. For any queries or complaints, you have our ears at Helpline No :" +
                                    ReadConfig.helpLineNumber + " or log in with your account details on our website (www.makenmake.in)/Mobile App";
                                SendSms objSms = new SendSms();
                                try
                                {
                                    int i = objSms.SendSmsOnMobile(message, txtMobile.Text);
                                    if (i != 1)
                                    {
                                        BL.BLAdmin objAdmin = new BL.BLAdmin();
                                        objAdmin.AddNotSendSmsMail(Convert.ToInt64(hdnuserID.Value), 0, "Updating Client Profile By CustomerCare", 1);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    BL.BLAdmin objAdmin = new BL.BLAdmin();
                                    objAdmin.AddNotSendSmsMail(Convert.ToInt64(hdnuserID.Value), 0, "Error while Updating Client Profile By CustomerCare -Issue:-" + ex.Message, 1);
                                }
                                modifiedDate.Visible = false;
                                txtModified.Text = string.Empty;
                                txtModified.Enabled = true;
                                treeMap.Nodes.Clear();
                                dvPages.Visible = false;
                                btnSubmit.Text = "Add";
                                divuser.Visible = true;
                                divedituser.Visible = false;
                                txtfirstname.Enabled = true;
                                txtstreet.Text = string.Empty;
                                txtaddresslocality.Text = string.Empty;
                                ddlRole.Enabled = true;
                                txtEmailID.Enabled = true;
                                clear();
                                txtMobile.Text = string.Empty;
                                BindDataList();
                                hdnuserID.Value = string.Empty;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Successfully Updated') ;", true);
                            }
                            else if (result == -98)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Choose Another Emailid, user with this emailid already exists') ;", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Problem occurs') ;", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please Select atleast one role') ;", true);
                        }



                    }
                }
            }
            catch (Exception ex)
            {
               // clear();
                logger.Error(logger.Name + ":" + ex.Message);
            }
        }
        protected void ddlIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindDataList();
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
        private void BindRoles()
        {
            BLAdmin getRoles = new BLAdmin();
            getRoles.GetRoles(ddlRole);
        }
        private void clear()
        {
            txtfirstname.Text = string.Empty;
            txtlastname.Text = string.Empty;
            txtEmailID.Text = string.Empty;
            txtaddresslocality.Text = string.Empty;
            txtstreet.Text = string.Empty;
            txtDob.Text = string.Empty;
            txtAltMob1.Text = string.Empty;
            txtAltMob2.Text = string.Empty;
            txtAltMob3.Text = string.Empty;
            txtAltMob4.Text = string.Empty;
            dvAlternateMobile.Visible = false;
            ddlCity.SelectedValue = "0";
            ddlCountry.SelectedValue = "0";
            ddlState.SelectedValue = "0";
            ddlDistrict.SelectedValue = "0";
            ddlRole.SelectedValue = "0";
            ddlZone.SelectedValue = "0";
            ddlSubZone.Items.Clear();
            dvSubZone.Visible = false;
            dvZone.Visible = false;

        }
        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlZone.SelectedValue != "0")
            {
                BindSubZones(Convert.ToInt32(ddlZone.SelectedValue));
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "callingMYFunction", "rdFunction() ;", true);
            }
            else
            {
                ddlSubZone.Items.Clear();
            }
        }
        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRole.SelectedValue == "0")
            {
                //dvcountry.Visible = false;
                //dvstate.Visible = false;
                //dvdistrict.Visible = false;
                //dvcity.Visible = false;
                dvZone.Visible = false;
                dvPages.Visible = false;
                dvSubZone.Visible = false;
            }
            else if (ddlRole.SelectedValue == "2" || ddlRole.SelectedValue == "3" || ddlRole.SelectedValue == "4")
            {
                if (ddlRole.SelectedValue == "4")
                {
                    dvAlternateMobile.Visible = true;
                }
                else
                {
                    dvAlternateMobile.Visible = false;
                }
                dvPages.Visible = true;
                dvcountry.Visible = true;
                dvstate.Visible = true;
                dvdistrict.Visible = true;
                dvcity.Visible = true;
                // dvaddress.Visible = true;
                dvaddress.Visible = true;
                divadd2.Visible = true;
                dvZone.Visible = true;
                dvSubZone.Visible = true;
                BindTree(Convert.ToInt32(ddlRole.SelectedValue), 1);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "callingMYFunction", "rdFunction() ;", true);
            }

            else
            {
                dvPages.Visible = true;
                dvcountry.Visible = true;
                dvstate.Visible = true;
                dvdistrict.Visible = true;
                dvcity.Visible = true;
                dvaddress.Visible = true;
                divadd2.Visible = true;
                dvZone.Visible = true;
                dvSubZone.Visible = true;
                BindTree(Convert.ToInt32(ddlRole.SelectedValue), 1);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "callingMYFunction", "rdFunction() ;", true);
            }

        }
        public void BindTree(int roleID, int action)
        {
            string rolename = string.Empty;
            if (roleID == 1)
            {
                rolename = "administrator";
            }
            else if (roleID == 2)
            {
                rolename = "service engineer";
            }
            else if (roleID == 3)
            {
                rolename = "customer care";
            }
            else if (roleID == 4)
            {
                rolename = "customer";
            }
            else if (roleID == 5)
            {
                rolename = "mis";
            }
            else if (roleID == 6)
            {
                rolename = "account manager";
            }
            else if (roleID == 7)
            {
                rolename = "escalation manager";
            }
            else if (roleID == 8)
            {
                rolename = "inventory manager";
            }
            
            BLAdmin bl = new BLAdmin();
            DataTable dt = GetMasterData(roleID);
            treeMap.Nodes.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string parentRole = dt.Rows[i]["ParentName"].ToString();
                TreeNode parent = new TreeNode(parentRole, dt.Rows[i]["ParentNodeID"].ToString());
                treeMap.Nodes.Add(parent);
                if (action == 1)
                {
                    parentRole = parentRole.Substring(0, parentRole.IndexOf("-")).ToLower();
                    if (parentRole == rolename)
                    {
                        parent.Checked = true;
                    }
                }
                DataTable dtChild = bl.GetChildPagesByNodeID(Convert.ToInt64(dt.Rows[i]["ParentNodeID"]));
                for (int j = 0; j < dtChild.Rows.Count; j++)
                {
                    TreeNode child = new TreeNode(dtChild.Rows[j]["PageTitle"].ToString(), dtChild.Rows[j]["tblChildID"].ToString());
                    if (action == 1)
                    {
                        // child.Checked = true;
                        parentRole = parentRole.ToLower();
                        if (parentRole == rolename)
                        {
                            child.Checked = true;
                        }
                    }
                    parent.ChildNodes.Add(child);
                }
            }
        }
        private DataTable GetMasterData(int roleID)
        {
            BLAdmin bl = new BLAdmin();
            return bl.GetPageByRoleID(roleID);
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }
        protected void Button1_Click(object sender, EventArgs e)
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "PageReload()", true);
                    txtSearchclient.Text = null;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please Enter the Name Or Mobile Number!') ;", true);
            }
        }
        private void GetUserRolesPermission(Int64 userID)
        {
            BLAdmin obj = new BLAdmin();
            DataTable dt = obj.GetRolesByUserID(userID);
            if (dt != null)
            {
                string parent = Convert.ToString(dt.Rows[0]["RoleParentPages"]);
                string child = Convert.ToString(dt.Rows[0]["RoleChildPages"]);
                foreach (var parentItem in parent.Split(','))
                {
                    var node = treeMap.FindNode(parentItem);
                    node.Checked = true;
                    foreach (TreeNode i in node.ChildNodes)
                    {
                        string isValueExists = Convert.ToString(i.Value);
                        string[] childArray;
                        childArray = child.Split(',');
                        foreach (var p in childArray.Where(c => c == isValueExists))
                        {
                            i.Checked = true;
                        }
                    }
                }
            }
        }
        private void BindCountry()
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetCountry(ddlCountry);
            BindState(Convert.ToInt32(ddlCountry.SelectedValue));
        }
        private void BindState(int countryID)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetStatesByCountryID(ddlState, countryID);
            BindDistrict(Convert.ToInt32(ddlState.SelectedValue));
            
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue != "0")
            {
                ddlState.Items.Clear();
                ddlDistrict.Items.Clear();
                ddlCity.Items.Clear();
                BindState(Convert.ToInt32(ddlCountry.SelectedValue));
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "callingMYFunction", "rdFunction() ;", true);
            }
            else
            {
                ddlState.Items.Clear();
                ddlDistrict.Items.Clear();
                ddlCity.Items.Clear();
            }
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue != "0")
            {
                ddlDistrict.Items.Clear();
                ddlCity.Items.Clear();
                BindDistrict(Convert.ToInt64(ddlState.SelectedValue));
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "callingMYFunction", "rdFunction() ;", true);
            }
            else
            {
                ddlDistrict.Items.Clear();
                ddlCity.Items.Clear();
            }
        }
        private void BindDistrict(Int64 StateID)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetDistrictsByID(ddlDistrict, StateID);

        }
        private void BindCity(Int64 districtID)
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetCityByDistrictID(ddlCity, districtID);
        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDistrict.SelectedValue != "0")
            {
                ddlCity.Items.Clear();
                BindCity(Convert.ToInt64(ddlDistrict.SelectedValue));
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "callingMYFunction", "rdFunction() ;", true);
            }
            else
            {
                ddlCity.Items.Clear();
            }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearchclient.Text = string.Empty;
            CurrentPage = 0;
           
            BindDataList();
        }

        protected void RptAllUser_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdcustid = (HiddenField)e.Item.FindControl("hdnID");
                LinkButton lbaltrnate = (LinkButton)e.Item.FindControl("lblmobile");
                MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
                DataTable dt = obj.GetUseralternateno(Convert.ToInt64(hdcustid.Value));
                string alternateno = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int a = i + 1;
                        //lbaltrnate.Text = lbaltrnate.Text + " " + dt.Rows[i]["ContactNumber"].ToString();
                        if (alternateno == "")
                        {
                            alternateno = "Alternate number " + a + "-" + dt.Rows[i]["ContactNumber"].ToString();
                        }
                        else
                        {
                            alternateno = alternateno + ", Alternate number  " + a + "-" + dt.Rows[i]["ContactNumber"].ToString();
                        }
                    }
                    
                    lbaltrnate.OnClientClick = "javascript:alert('" + alternateno + "')";
                }
                else
                {
                    lbaltrnate.OnClientClick = "javascript:alert('No Alternate Number ')";
                }
            }
        }

        protected void btngeneratereport_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsersReport.aspx");
        }

      
    }
}
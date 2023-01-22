using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace MakeNMake.CustomerCare
{
    public partial class AddClient : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        BLAdmin addUser = new BLAdmin();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rdbStatus.SelectedValue = "1";
                BindCountry();
                ViewState["Session_UserID"] = Convert.ToString(Session[Constant.Session.AdminSession]);
                BindDataList();
                BindZone();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                   string alternatemobileno1 = "";
                        string alternatemobileno2 = "";
                        string alternatemobileno3 = "";
                        string alternatemobileno4 = "";

                if (btnSubmit.Text.ToLower() == "add")
                {
                    Int64 Userid = Convert.ToInt64(Session[Constant.Session.AdminSession]);
                    string password = EncryptDecrypt.CreateRandomPassword(6);
                    BL.BLConsumer createUser = new BL.BLConsumer();
                    
                        var resultphonenoexists = addUser.checkphonenoexistsinMobileno(txtAltMob1.Text + "," + txtAltMob2.Text + "," + txtAltMob3.Text + "," + txtAltMob4.Text);
                        if (resultphonenoexists.Rows.Count > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Any Alternate Mobile no  is already exists in users  primary no') ;", true);
                        }
                        else
                        {
                            var result1 = addUser.checkphonenoexists(txtAltMob1.Text + "," + txtAltMob2.Text + "," + txtAltMob3.Text + "," + txtAltMob4.Text);

                            if (result1.Rows.Count > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Any Alternate Mobile no  is alredy exists.') ;", true);
                            }
                            else
                            {
                                alternatemobileno1 = txtAltMob1.Text;

                                alternatemobileno2 = txtAltMob2.Text;

                                alternatemobileno3 = txtAltMob3.Text;


                                alternatemobileno4 = txtAltMob4.Text;
                            }
                        
                    }
                        string dob = Convert.ToDateTime(txtDob.Text).ToString("MM/dd/yyyy");
                    string address = txtaddresslocality.Text + "+" + txtstreet.Text;
                    int result = createUser.SignUpUser(txtfirstname.Text, txtlastname.Text, txtEmailID.Text, password, txtMobileNumber.Text, ddlGender.SelectedValue, Convert.ToDateTime(dob),address, Convert.ToInt64(ddlCountry.SelectedValue), Convert.ToInt64(ddlState.SelectedValue), Convert.ToInt64(ddlDistrict.SelectedValue), Convert.ToInt64(ddlCity.SelectedValue), Convert.ToInt64(Session[Constant.Session.AdminSession]), Convert.ToInt32(rdbStatus.SelectedItem.Value), Convert.ToInt64(ddlZone.SelectedValue), Convert.ToInt64(ddlSubZone.SelectedValue), alternatemobileno1, alternatemobileno2, alternatemobileno3, alternatemobileno4);
                    if (result > 0)
                    {
                        BindDataList();
                        

                        
                        //string content = MakeNMake.Utilities.EncryptDecrypt.Encript(txtEmailID.Text + ":" + txtfirstname.Text + " " + txtlastname.Text + ":" + result + ":" + password);
                        //string VerifyUrl = string.Format("{0}EmailConfirmation.aspx?Content={1}", ReadConfig.SiteUrl, content);
                        //MEmail.SendGMail(txtEmailID.Text.Trim(), "Please activate your Make 'N' Make", "Dear User,<br><br> Thanks for registering with us and we try to always take care of your convenance. Please find the below Activation Link and Login with your credentials for activating your account.:<br><br> User ID: " + txtEmailID.Text
                        //    + " <br> Click on this link: <a target='_blank' style='color:blue;'  href='" + VerifyUrl + "'>" +
                        //    VerifyUrl + "</a><br>You can change the password after login to our site.If your email system does not allow linking, please copy and paste the following into your browser. <br><br>Kind Regards:<br> Make 'N' Make Service Team", "");
                        string VerifyUrl = string.Format("{0}Default.aspx", ReadConfig.SiteUrl);
                        MEmail.SendGMail(txtEmailID.Text.Trim(), "Registration Make 'N' Make", "Dear User,<br><br> You have successfully registered with Make N Make . Login with your credentials :<br><br> User ID: " + txtEmailID.Text + "<br>Password: " + password + " <br> Click on this link: <a target='_blank' style='color:blue;'  href='" + VerifyUrl + "'>" + VerifyUrl + "</a><br>You can change the password after login to our site with these credentials.If your email system does not allow linking, please copy and paste the following into your browser. <br><br>Kind Regards:<br> Make 'N' Make Service Team", "");
                        
                        string salutation = ddlGender.SelectedValue == "M" ? "Mr." : "Ms.";

                        string message = "Hi," + salutation + txtfirstname.Text + "! Thanks for Registering with us. A warm welcome to the “MakenMake” family." +
                            "Give us a chance to serve you. Your credentials have been sent to registered E-mail Id or directly be in touch with " +
                            "us through our Helpline No." + ReadConfig.helpLineNumber;
                        SendSms obj = new SendSms();
                        int i = obj.SendSmsOnMobile(message, txtMobileNumber.Text);
                        if (i != 1)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(result, 0, "Signup of Client By Customer Care", 1);
                        }
                        clear();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('User has been created') ;", true);
                    }
                    else if (result == -99)
                    {
                        txtEmailID.Text = string.Empty;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('User with this emailid already exists') ;", true);
                    }
                }
                else if (btnSubmit.Text.ToLower() == "edit")
                {
                    string address = txtaddresslocality.Text + "+" + txtstreet.Text;
                    int result1 = addUser.updateUserslist(Convert.ToInt64(hdnUserID.Value), txtfirstname.Text, txtlastname.Text, address, string.Empty,
                        Convert.ToChar(ddlGender.SelectedValue), Convert.ToDateTime(txtDob.Text), txtMobileNumber.Text, Convert.ToInt32(ddlCountry.SelectedValue),
                        Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlCity.SelectedValue)
                        , Convert.ToInt32(rdbStatus.SelectedValue), Convert.ToInt64(ddlZone.SelectedValue), Convert.ToInt64(ddlSubZone.SelectedValue));
                    if (result1 > 0)
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
                                int resultaltno = objc.UpdateUserAlternateno(alternatemobileno1, alternatemobileno2, alternatemobileno3, alternatemobileno4, Convert.ToInt64(hdnUserID.Value));
                            }
                        }
                        string salutation = ddlGender.SelectedValue == "M" ? "Mr." : "Ms.";
                        string message = "Hi ," + salutation + txtfirstname + "! Your details have been updated as per your request. For any queries or complaints, you have our ears at Helpline No :" +
                            ReadConfig.helpLineNumber + " or log in with your account details on our website (www.makenmake.in)/Mobile App";
                        SendSms objSms = new SendSms();
                        try
                        {
                            int i = objSms.SendSmsOnMobile(message, txtMobileNumber.Text);
                            if (i != 1)
                            {
                                BL.BLAdmin objAdmin = new BL.BLAdmin();
                                objAdmin.AddNotSendSmsMail(Convert.ToInt64(hdnUserID.Value), 0, "Updating Client Profile By Admin", 1);
                            }
                        }
                        catch (Exception ex)
                        {
                            BL.BLAdmin objAdmin = new BL.BLAdmin();
                            objAdmin.AddNotSendSmsMail(Convert.ToInt64(hdnUserID.Value), 0, "Error while Updating Client Profile By Admin -Issue:-" + ex.Message, 1);
                        }
                        rdbStatus.SelectedValue = "1";
                       // dvStatus.Visible = false;
                        hdnUserID.Value = string.Empty;
                        btnSubmit.Text = "Add";
                        txtEmailID.Enabled = true;
                        ddlZone.SelectedValue = "0";
                        ddlSubZone.Items.Clear();
                        clear();
                        BindDataList();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('updated sucessfully') ;", true);
                    }
                }
            }
            catch (Exception ex)
            {
                clear();
                logger.Error(logger.Name + ":" + ex.Message);
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
                    divconsumer.Visible = false;
                    diveditCustomer.Visible = true;

                    HiddenField custid = (HiddenField)e.Item.FindControl("hdnID");

                    hdnUserID.Value = custid.Value;

                    Label lblfName = (Label)e.Item.FindControl("lblname");
                    Label lblLName = (Label)e.Item.FindControl("lblastname");
                    Label lbladdress = (Label)e.Item.FindControl("lbladdress");
                    Label lblemail = (Label)e.Item.FindControl("lblemail");
                    Label lblgen = (Label)e.Item.FindControl("Lblgender");
                    Label lbldob = (Label)e.Item.FindControl("lblDOB");
                    Label lblcountry = (Label)e.Item.FindControl("lblcntryID");

                    Label lblState = (Label)e.Item.FindControl("lblstateID");
                    Label lbldisname = (Label)e.Item.FindControl("lbldstrictID");
                    Label lblcity = (Label)e.Item.FindControl("lblctyID");
                    LinkButton lblmobile = (LinkButton)e.Item.FindControl("lblmobile");
                    Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                    Label lblzoneid = (Label)e.Item.FindControl("lblzoneid");
                    Label lblsubzone = (Label)e.Item.FindControl("lblsubzone");

                    HiddenField hdndob = (HiddenField)e.Item.FindControl("hdndob");

                    ddlZone.SelectedValue = lblzoneid.Text;
                    BindsubZone(Convert.ToInt64(lblzoneid.Text));
                    ddlSubZone.SelectedValue = lblsubzone.Text;
                    bindalternateno(Convert.ToInt64(custid.Value));
                    dvStatus.Visible = true;
                    if (lblStatus.Text.ToLower() == "active")
                    {
                        rdbStatus.SelectedValue = "1";
                    }
                    else
                    {
                        rdbStatus.SelectedValue = "0";
                    }


                    btnSubmit.Text = "Edit";

                    txtfirstname.Text = lblfName.Text;
                    txtlastname.Text = lblLName.Text;
                    string[] values = Convert.ToString(lbladdress.Text).Split('+');
                    if (values.Length > 0)
                    {
                        txtaddresslocality.Text = values[0].Trim();
                        txtstreet.Text = values[1].Trim();
                    }
                    else
                    {
                        txtaddresslocality.Text = Convert.ToString(lbladdress.Text);
                    }
                   // txtaddress.Text = lbladdress.Text;
                    txtEmailID.Text = lblemail.Text;
                    txtMobileNumber.Text = lblmobile.Text;
                    ddlGender.SelectedValue = lblgen.Text == "NA" ? "0" : lblgen.Text == "Male" ? "M" : "F";
                    //  DateTime dt = Convert.ToDateTime(hdndob.Value);

                    txtDob.Text = hdndob.Value == "" ? "" : Convert.ToDateTime(hdndob.Value) == null ? "" : Convert.ToDateTime(hdndob.Value).ToString("MM/dd/yyyy");

                    ddlCountry.SelectedValue = lblcountry.Text == "" ? "0" : lblcountry.Text;
                    BindState(Convert.ToInt32(ddlCountry.SelectedValue));
                    ddlState.SelectedValue = lblState.Text == "" ? "0" : lblState.Text;
                    BindDistrict(Convert.ToInt64(ddlState.SelectedValue));
                    ddlDistrict.SelectedValue = lbldisname.Text == "" ? "0" : lbldisname.Text;
                    BindCity(Convert.ToInt64(ddlDistrict.SelectedValue));
                    ddlCity.SelectedValue = lblcity.Text == "" ? "0" : lblcity.Text;
                    ddlRole.SelectedValue = "4";
                    //  txtcustid.Text = Convert.ToString(custid);
                    txtEmailID.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                clear();
                logger.Error(logger.Name + ":" + ex.Message);
            }
        }

        private void clear()
        {
            txtfirstname.Text = string.Empty;
            txtlastname.Text = string.Empty;
            txtEmailID.Text = string.Empty;
            ddlRole.SelectedValue = "0";
            txtMobileNumber.Text = string.Empty;
            ddlGender.SelectedValue = "0";
            ddlZone.SelectedValue = "0";
            txtDob.Text = string.Empty;
            txtstreet.Text = string.Empty;
            txtaddresslocality.Text = string.Empty;
            ddlCountry.SelectedValue = "0";
            ddlState.SelectedValue = "0";
            ddlDistrict.SelectedValue = "0";
            ddlCity.SelectedValue = "0";
            ddlRole.SelectedValue = "0";
        }
        // Today code here---------------------------------------------------
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
        private void BindZone()
        {
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetZones(ddlZone);

        }
        private void BindsubZone(Int64 StateID)
        {
            ddlSubZone.Items.Clear();
            MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
            obj.GetSubZoneDistrict(ddlSubZone, Convert.ToInt32( StateID));
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
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue != "0")
            {
                ddlState.Items.Clear();
                BindState(Convert.ToInt32(ddlCountry.SelectedValue));
            }
            else
            {
                ddlState.Items.Clear();
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
            }
            else
            {
                ddlDistrict.Items.Clear();
                ddlCity.Items.Clear();
            }
        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDistrict.SelectedValue != "0")
            {
                ddlCity.Items.Clear();
               BindCity(Convert.ToInt64(ddlDistrict.SelectedValue));
            }
            else
            {
                ddlCity.Items.Clear();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashBoard.aspx");
        }
        DataTable GetClientData(int currentpage, string userid, string clientName)
        {
            DataTable dtable = addUser.GetClientdatavalue(currentpage, userid, clientName);
            if (dtable != null && dtable.Rows.Count > 0)
            {
                tblPaging.Visible = true;
            }
            else
            {
                tblPaging.Visible = false;
            }
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
        private int BindDataList()
        {
            pgsource.CurrentPageIndex = CurrentPage;
            DataTable dt = GetClientData(CurrentPage, ViewState["Session_UserID"].ToString(), txtSearchclient.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["totpage"] = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["totalCount"]) / 10));
            }
            pgsource.DataSource = dt.DefaultView;


            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + ViewState["totpage"];

            RptAllUser.DataSource = dt;
            RptAllUser.DataBind();

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
        protected void RepeaterPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            LinkButton lnkPage = (LinkButton)e.Item.FindControl("Pagingbtn");
            if (lnkPage.CommandArgument.ToString() == CurrentPage.ToString())
            {
                lnkPage.Enabled = false;
                lnkPage.BackColor = System.Drawing.Color.FromName("#970915");
            }
        }

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlZone.SelectedValue != "0")
            {
                ddlSubZone.Items.Clear();
                MakeNMake.BL.BLAdmin obj = new BL.BLAdmin();
                obj.GetDistrictsByID(ddlSubZone, Convert.ToInt64(ddlZone.SelectedValue));
            }
            else
            {
                ddlSubZone.Items.Clear();
            }
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
                            alternateno = alternateno + ",Alternate number " + a + "-" + dt.Rows[i]["ContactNumber"].ToString();
                        }
                    }
                    
                    lbaltrnate.OnClientClick = "javascript:alert('" + alternateno + "')";
                }
                else
                {
                    lbaltrnate.OnClientClick = "javascript:alert('No Alternate Number')";
                }
            }
        }

    }
}
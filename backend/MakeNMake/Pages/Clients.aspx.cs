using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using MakeNMake.Utilities;
using System.Data;

namespace MakeNMake.CustomerCare
{
    public partial class Clients : System.Web.UI.Page
    {
        BLCustomerCare objConsumer = new BLCustomerCare();
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["Session_UserID"] = Convert.ToString(Session[Constant.Session.AdminSession]);
                BindDataList();
            }
        }

        DataTable GetClientData(int currentpage, string userid, string clientName)
        {
            DataTable dtable = objConsumer.GetConsumer(currentpage, userid, clientName);

            return dtable;
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

            RptClient.DataSource = dt;
            RptClient.DataBind();

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



        protected void RptClient_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "package")
            {
                Int64 UserID = Convert.ToInt64(e.CommandArgument);
                LinkButton lblMobile = (LinkButton)e.Item.FindControl("lblMobile");
                Label lblName = (Label)e.Item.FindControl("lblName");
                if (lblMobile.Text != "")
                {
                    string Otp = EncryptDecrypt.CreateRandomPassword(4);
                    SendSms obj = new SendSms();
                    string message = "Hi ! " + lblName.Text + ", Your OTP is " + Otp + " .Thanks MakeNake team";
                    int i = obj.SendSmsOnMobile(message, lblMobile.Text);
                    if (i == 1)
                    {
                        int result = objConsumer.AddUpdateUserOTP(UserID, Otp, lblMobile.Text, 1);
                        Label lblEmail = (Label)e.Item.FindControl("lblEmail");
                        MEmail.SendGMail(lblEmail.Text, "One Time Password  Make 'N' Make", message, "");
                        Response.Redirect(Constant.Pages.PackageSelection + Constant.Separators.QuestionMark + Constant.QueryString.ClientID + Constant.Separators.EqualTo +
                           EncryptDecrypt.Encript(Convert.ToString(UserID)), false);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Some Problem occurs due to insufficient balance while sending message') ;", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You need to update your personal information first to buy the services') ;", true);
                }
            }
            else if (e.CommandName == "updateInfo")
            {
                Int64 UserID = Convert.ToInt64(e.CommandArgument);
                Response.Redirect(Constant.Pages.UpdateInfo + Constant.Separators.QuestionMark + Constant.QueryString.ClientID + Constant.Separators.EqualTo +
                         EncryptDecrypt.Encript(Convert.ToString(UserID)) + "&" + Constant.QueryString.Confirmed + Constant.Separators.EqualTo + "jdqwu832g7dtwb3297", false);
            }
            else if (e.CommandName == "complaint")
            {
                Int64 UserID = Convert.ToInt64(e.CommandArgument);
                Common newObj = new Common();
                int result = newObj.CheckUserAddress(UserID);
                if (result > 0)
                {
                    Response.Redirect(Constant.Pages.Complaint + Constant.Separators.QuestionMark + Constant.QueryString.ClientID + Constant.Separators.EqualTo +
                             EncryptDecrypt.Encript(Convert.ToString(UserID)), false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('You need to update your personal information first to log the complaint') ;", true);
                }
            }
            else if (e.CommandName == "contract")
            {
                Response.Redirect("PayContractAmount.aspx?UserID=" + EncryptDecrypt.Encript(Convert.ToString(e.CommandArgument)));
            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
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

        protected void RptClient_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdcustid = (HiddenField)e.Item.FindControl("hdnID");
                LinkButton lbaltrnate = (LinkButton)e.Item.FindControl("lblMobile");
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
                            alternateno ="Alternate number "+a+"-" +dt.Rows[i]["ContactNumber"].ToString();
                        }
                        else
                        {
                            alternateno = alternateno + ", Alternate number " + a + "-" +dt.Rows[i]["ContactNumber"].ToString();
                        }
                    }
                    
                    lbaltrnate.OnClientClick = "javascript:alert('"+alternateno+"')";
                }
                else
                {
                    lbaltrnate.OnClientClick = "javascript:alert('No Alternate number')";
                }
            }
        }
    }
}
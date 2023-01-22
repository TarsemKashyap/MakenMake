using MakeNMake.BL;
using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MakeNMake.Admin
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                slideShow.Visible = false;
                int roleID = Convert.ToInt32(Session[Constant.Session.Role]);
                if (roleID == 3)
                {
                    slideShow.Visible = false;
                    FillGeeralInfo();
                    GreetMesage();
                }
                else if (roleID == 4)
                {
                    string confirm = Convert.ToString(Request.QueryString.Get("Confirm"));
                    if (string.IsNullOrEmpty(confirm))
                    {
                        slideShow.Visible = true;
                        FillGeeralInfo();
                        CheckServices();
                        GreetMesage();
                    }
                    else
                    {
                        Common obj = new Common();
                        DataTable dt = obj.GetUserInfoByID(Convert.ToInt64(Session[Constant.Session.AdminSession]));
                        string data = string.Empty;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            data += "MobileNumber : " + Convert.ToString(dt.Rows[0]["MNumber"]) + " & ";
                            data += "DOB : " + Convert.ToString(Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("MM/dd/yyyy")) + " & ";
                            data += "Address : " + Convert.ToString(dt.Rows[0]["UserAddress"]) + " & ";
                            data += "EmailID : " + Convert.ToString(dt.Rows[0]["Emailid"]) + " & ";
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "ConfirmUser('" + data + "') ;", true);
                    }
                }
                else if (roleID == 2)
                {
                    slideShow.Visible = false;

                    GreetMesage();
                }
                else
                {
                    GreetMesage();
                }
            }
        }
        private string BindLoginUserName(Int64 UserID)
        {
            BL.BLAdmin obj = new BL.BLAdmin();
            return obj.GetLoginuserName(UserID);

        }
        private void GreetMesage()
        {
            string firsttime = Convert.ToString(Request.QueryString.Get("FirstTime"));
            if (!string.IsNullOrEmpty(firsttime))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Hi " + BindLoginUserName(Convert.ToInt64(Session[Constant.Session.AdminSession])) + ", Good to see you  :)') ;", true);
            }            
        }
   
        private void CheckServices()
        {
            Common getInfo = new Common();
            int result = getInfo.GetBalanceEnquiry(Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if (result == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Hi, Your services has been expired,please buy new services') ;", true);
            }
            else if (result == -1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Hi, Your services has been expired due to balance out') ;", true);
            }
        }
        private void FillGeeralInfo()
        {
            Common getInfo = new Common();
            int result = getInfo.GetInfo(Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if (result == -99)
            {
                hdnIsUpdated.Value = "1";
            }
            else
            {
                hdnIsUpdated.Value = "0";
            }
        }
    }
}
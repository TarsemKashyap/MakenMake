using MakeNMake.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.UserControl
{
    public partial class UserInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public void BindData(Int64 UserID)
        {
            Common objClient = new Common();
            DataTable dt = objClient.GetCustomerByID(UserID);
            if (dt != null)
            {
                lblName.Text = Convert.ToString(dt.Rows[0]["name"]);
                lblEmailID.Text = Convert.ToString(dt.Rows[0]["Emailid"]);
                lblMobileNumber.Text = Convert.ToString(dt.Rows[0]["MNumber"]);
                lblAddress.Text = Convert.ToString(dt.Rows[0]["UserAddress"] + "</br>" + dt.Rows[0]["cityname"] +
                    "</br>" + dt.Rows[0]["districtname"] + "</br>" + dt.Rows[0]["statename"]+ "</br>" + dt.Rows[0]["countryname"]);
            }
        }
    }
}
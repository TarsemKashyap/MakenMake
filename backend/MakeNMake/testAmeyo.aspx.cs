using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake
{
    public partial class testAmeyo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("MakenMakeCustomer.aspx?GetCustomerBy=emailid&CustomerData=" + TextBox1.Text + "");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("MakenMakeCustomer.aspx?GetCustomerBy=mobilenumber&CustomerData=" + TextBox2.Text + "");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("MakenMakeCustomer.aspx?GetCustomerBy=userid&CustomerData=" + TextBox3.Text + "");
        }
    }
}
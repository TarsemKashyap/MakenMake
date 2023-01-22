using AjaxControlToolkit;
using MakeNMake.CommomFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.Pages
{
    public partial class CustomerAddress2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTicket();
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ToolkitScriptManager masterlbl = (ToolkitScriptManager)Master.FindControl("ToolkitScriptManager1");
            masterlbl.EnablePartialRendering = false;
        }
        private void BindTicket()
        {
            BL.BLServiceEngineer obj = new BL.BLServiceEngineer();
            DataTable dt = obj.GetTicketIDsAcceptedByEngID(Convert.ToInt64(Session[Constant.Session.AdminSession]));
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlticket.DataSource = dt;
                ddlticket.DataTextField = "TicketID";
                ddlticket.DataValueField = "TicketID";
                ddlticket.DataBind();
                ddlticket.Items.Insert(0, new ListItem("Select Ticket ID", "0"));
            }
            else
            {
                ddlticket.Items.Insert(0, new ListItem("Select Ticket ID", "0"));
            }
        }
        protected void ddlticket_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlticket.SelectedValue != "0")
            {
                ddlticket.Enabled = false;
                clientinfo.Visible = true;
                BL.BLServiceEngineer obj = new BL.BLServiceEngineer();
                DataTable dt = obj.GetCustomerAddressByTicketID(Convert.ToInt64(ddlticket.SelectedValue));
                if (dt != null && dt.Rows.Count > 0)
                {
                    clientinfo.Visible = true;
                    txtname.Text = Convert.ToString(dt.Rows[0]["name"]);
                    txtmobileNumber.Text = Convert.ToString(dt.Rows[0]["MobileNumber"]) == null ? " Not available" : Convert.ToString(dt.Rows[0]["MobileNumber"]);
                    // txtaddress.Text = Convert.ToString(dt.Rows[0]["CustomerAddress"]) == null ? " Not available" : Convert.ToString(dt.Rows[0]["CustomerAddress"]);
                    string[] values = Convert.ToString(dt.Rows[0]["CustomerAddress"]).Split('+');
                    string rd = "";
                    int len = values.Length;

                    for (int i = 0; i < len; i++)
                    {
                        if (i < len - 1)
                        {
                            rd = rd + values[i] + ",";

                        }
                        else
                        {
                            rd = rd + values[i];
                        }

                    }


                    if (values.Length > 0)
                    {
                        // Convert.ToString(dt.Rows[0]["CustomerAddress"]) == null ? " Not available" : Convert.ToString(dt.Rows[0]["CustomerAddress"]);
                        txtaddress2.Text = Convert.ToString(dt.Rows[0]["CustomerAddress"]) == null ? " Not available" : rd.Trim();
                        //if (values.Length == 1)
                        //{

                        //    txtaddress.Text = Convert.ToString(dt.Rows[0]["CustomerAddress"]) == null ? " Not available" : Convert.ToString(dt.Rows[0]["CustomerAddress"]).Trim();
                        //}
                    }
                    else
                    {
                        txtaddress2.Text = Convert.ToString(dt.Rows[0]["CustomerAddress"]);
                    }




                    txtname.Enabled = false;
                    txtaddress2.Enabled = false;
                    txtmobileNumber.Enabled = false;
                }
            }
            else
            {
               
                clientinfo.Visible = false;
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            //dvmap.Visible = false;
            clientinfo.Visible = false;
            ddlticket.Enabled = true;
            txtname.Enabled = true;
            txtaddress2.Enabled = true;
            txtmobileNumber.Enabled = true;
        }
    }
}
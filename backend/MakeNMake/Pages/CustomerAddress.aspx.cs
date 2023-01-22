using MakeNMake.CommomFunctions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace MakeNMake.ServiceEngineer
{
    public partial class CustomerAddress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTicket();
            }
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

        private void bindMap(Int64 TicketID, string lat, string log, string address1)
        {

            string url = "https://api.mapmyindia.com/v3?fun=geocode&lic_key=9zzhzm4joibbs2w9dplb29x8qjtmaxri&q=" + txtaddress.Text;
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string jsonData = reader.ReadToEnd();
                    var finaldata = JsonConvert.DeserializeObject<List<GeocodeData>>(jsonData);
                    string lat1 = string.Empty;
                    string log1 = string.Empty;
                    string address2 = string.Empty;
                    foreach (var items in finaldata)
                    {
                        lat1 = items.lat;
                        log1 = items.lng;
                        address2 = items.formatted_address;
                        break;
                    }
                    if (finaldata.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "BindMap(" + Convert.ToDecimal(lat) + "," + Convert.ToDecimal(log) + "," + Convert.ToDecimal(lat1) + "," + Convert.ToDecimal(log1) + ",'" + address1 + "','" + address2 + "') ;", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "get_route_result();alert('No location found by Map My India for the customer address :- " + txtaddress.Text + "')", true);
                    }
                }
            }
        }
        private void DrawMap(string latitude1, string longitude1, string latitude2, string longitude2)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                dvmap.Visible = true;
                string url = "https://api.mapmyindia.com/v3?fun=geocode&lic_key=9zzhzm4joibbs2w9dplb29x8qjtmaxri&q=" + txtaddresss.Text;
                //+ "&callback=display_geocode_result";
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        string jsonData = reader.ReadToEnd();
                        var finaldata = JsonConvert.DeserializeObject<List<GeocodeData>>(jsonData);
                        foreach (var items in finaldata)
                        {
                            bindMap(Convert.ToInt64(ddlticket.SelectedValue), items.lat, items.lng, items.formatted_address);
                            break;
                        }
                    }
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Map Api is not working');) ;", true);
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
                    string rd="";
                    int len = values.Length;

                    for (int i=0; i < len; i++)
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
                        txtaddress.Text = Convert.ToString(dt.Rows[0]["CustomerAddress"]) == null ? " Not available" : rd.Trim();
                        //if (values.Length == 1)
                        //{

                        //    txtaddress.Text = Convert.ToString(dt.Rows[0]["CustomerAddress"]) == null ? " Not available" : Convert.ToString(dt.Rows[0]["CustomerAddress"]).Trim();
                        //}
                    }
                    else
                    {
                        txtaddress.Text = Convert.ToString(dt.Rows[0]["CustomerAddress"]);
                    }




                    txtname.Enabled = false;
                    txtaddress.Enabled = false;
                    txtmobileNumber.Enabled = false;
                }
            }
            else
            {
                dvmap.Visible = false;
                clientinfo.Visible = false;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            dvmap.Visible = false;
            clientinfo.Visible = false;
            ddlticket.Enabled = true;
            txtname.Enabled = true;
            txtaddress.Enabled = true;
            txtmobileNumber.Enabled = true;
        }
    }
    public class GeocodeData
    {
        public string country { get; set; }
        public string city { get; set; }
        public string area { get; set; }
        public string PLZ { get; set; }
        public string street{ get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string formatted_address { get; set; }
    }
}
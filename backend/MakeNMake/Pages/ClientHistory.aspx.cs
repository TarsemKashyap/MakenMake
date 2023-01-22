using MakeNMake.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MakeNMake.Datatable;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace MakeNMake.Admin
{
    public partial class ClientHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static ClientHIstory BindData()
        {
            ClientHIstory obj = new ClientHIstory();
            BL.BLAdmin objAdmin = new BL.BLAdmin();
            DataTable dt = objAdmin.GetAllClient();
            obj.draw = 1;
            obj.recordsFiltered = 15;
            obj.recordsTotal = 15;
            List<Clients> lstClient = new List<Clients>();
            var ClientData = from items in dt.AsEnumerable()
                             select new
                             {
                                 id = Convert.ToInt64(items["UserID"]),
                                 Name = Convert.ToString(items["Name"]),
                             };

            foreach (var data in ClientData)
            {

                lstClient.Add(new Clients
                {
                    UserID = data.id,
                    Name = data.Name,


                });
            }
            obj.ClientId = lstClient;
            return obj;
        }
        [WebMethod]
        public static List<ClientDetail> BindTicketData(Int64 UserID)
        {
            DataTable dtdetail = null;
            BLAdmin obj = new BLAdmin();
            dtdetail = obj.GetClientDetails(UserID);
            var clientdteail = from items in dtdetail.AsEnumerable()
                               select new
                               {
                                   id = Convert.ToInt64(items["UserID"]),
                                   ServiceName = Convert.ToString(items["ServiceName"]),
                                   NoCalls = Convert.ToInt32(items["Quantity"]),
                                   Plan = Convert.ToString(items["PlanType"]),
                                   category = Convert.ToString(items["ServiceCategory"]),
                                   IssueDate = Convert.ToString(items["IssueDate"]),
                                   PlanExpirationDate = Convert.ToString(items["ExpirationDate"]),
                                   Amount = Convert.ToString(items["Amount"])
                               };
            List<ClientDetail> lstdetails = new List<ClientDetail>();
            foreach (var ddetails in clientdteail)
            {
                lstdetails.Add(new ClientDetail { ServiceName = ddetails.ServiceName, Calls = ddetails.NoCalls, ServiceCategory = ddetails.category, PlanType = ddetails.Plan, IssueDate = ddetails.IssueDate, ExpirationDate = ddetails.PlanExpirationDate, Amount = ddetails.Amount });
            }
            return lstdetails;
        }


        [WebMethod]
        public static List<ClientProfileDetail> BindProfileData(Int64 UserID)
        {
            DataTable profile = null;
            BLAdmin obj = new BLAdmin();
            profile = obj.GetClientProfile(UserID);
            var clientdteail = from items in profile.AsEnumerable()
                               select new
                               {
                                   //   id = Convert.ToInt64(items["UserID"]),
                                   Reference = Convert.ToString(items["Reference"]),
                                   Status = Convert.ToString(items["Status"]),
                                   Source = Convert.ToString(items["Source"]),
                                   ServiceDate = Convert.ToString(items["ServiceDate"]),
                                   //Amount = Convert.ToString(items["Amount"])
                               };
            List<ClientProfileDetail> lstdetails = new List<ClientProfileDetail>();

            foreach (var ddetails in clientdteail)
            {
                lstdetails.Add(new ClientProfileDetail { Reference = ddetails.Reference, Status = ddetails.Status, Source = ddetails.Source, ServiceDate = ddetails.ServiceDate });
            }
            return lstdetails;
        }

        [WebMethod]
        public static List<ClienBillDetail> BindBillData(Int64 UserID)
        {
            DataTable profile = null;
            BLAdmin obj = new BLAdmin();
            profile = obj.GetClientBill(UserID);
            var clientBill = from items in profile.AsEnumerable()
                             select new
                             {
                                 id = UserID,
                                 Name = Convert.ToString(items["Name"]),
                                 InvoiceNumber = Convert.ToString(items["InvoiceNumber"]),
                                 InvoiceDate = Convert.ToString(items["InvoiceDate"]),
                                 Amount = Convert.ToString(items["Amount"]),
                                 //Amount = Convert.ToString(items["Amount"])
                             };
            List<ClienBillDetail> lstdetails = new List<ClienBillDetail>();

            foreach (var ddetails in clientBill)
            {
                lstdetails.Add(new ClienBillDetail { UserID = ddetails.id, Name = ddetails.Name, InvoiceNumber = ddetails.InvoiceNumber, InvoiceDate = ddetails.InvoiceDate, Amount = ddetails.Amount });
            }
            return lstdetails;
        }


    }

}
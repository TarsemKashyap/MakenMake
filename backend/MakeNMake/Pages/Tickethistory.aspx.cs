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
    public partial class Tickethistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        [WebMethod]
        public static  JqDataTableTickets BindData()
        {
            JqDataTableTickets obj = new JqDataTableTickets();
            BL.BLAdmin objAdmin = new BL.BLAdmin();
            DataTable dt = objAdmin.GetAllTickets();
            obj.draw = 1;
            obj.recordsFiltered = 15;
            obj.recordsTotal = 15;
            List<Tickets> lstTickets = new List<Tickets>();
            var ticketData = from items in dt.AsEnumerable()
                             select new
                             {
                                 id = Convert.ToInt64(items["TicketID"]),
                                 ticketType = Convert.ToString(items["TicketType"]),
                                 customerID = Convert.ToInt64(items["CustomerId"]),
                                 name = Convert.ToString(items["Name"]),
                                 created = Convert.ToDateTime(items["created"]).ToString("MM/dd/yyyy hh:mm"),
                                 status = Convert.ToString(items["Status"]),
                                 closure = Convert.ToString(items["closure"]).ToLower() == "opened" ? "opened" :Convert.ToString( Convert.ToDateTime(items["closure"]).ToString("MM/dd/yyyy hh:mm")),
                             };
           
            foreach (var data in ticketData)
            {
               
                lstTickets.Add(new Tickets
                {
                    ticketID = data.id,
                    ticketType = data.ticketType,
                    customerName = data.name,
                    status = data.status,
                    created =data.created,
                    closure = data.closure,
                    details=null
                });
            }
            obj.Tickets = lstTickets;
            return obj;
        }
        [WebMethod]
        public static List<TicketDetails> BindTicketData(Int64 ticketID)
        {
            DataTable dtdetail = null;
            BLServiceEngineer objEngineer = new BLServiceEngineer();
            List<TicketDetails> lstdetails = new List<TicketDetails>();
            dtdetail = objEngineer.GetTicketDetails(ticketID);
            if (dtdetail != null && dtdetail.Rows.Count > 0)
            {
                var ticketDetail = from items in dtdetail.AsEnumerable()
                                   select new
                                   {
                                       id = Convert.ToInt64(items["TicketID"]),
                                       ServiceName = Convert.ToString(items["ServiceName"]),
                                       description = Convert.ToString(items["description"]),
                                       category = Convert.ToString(items["Category"]),
                                       Plans = Convert.ToString(items["Plans"]),
                                       servedBy = Convert.ToString(items["ServedBy"]),
                                       servedByID = Convert.ToInt64(items["servedByID"])
                                   };

                foreach (var ddetails in ticketDetail)
                {
                    lstdetails.Add(new TicketDetails { serviceName = ddetails.ServiceName, description = ddetails.description, plan = ddetails.Plans, servedBy = ddetails.servedBy, servedByID = ddetails.servedByID });
                }
            }
            return lstdetails;
        }


        [WebMethod]
        public static List<EngProfileDetail> BindProfileData(Int64 UserID)
        {
            DataTable profile = null;
            BLAdmin obj = new BLAdmin();

            List<EngProfileDetail> lstdetails = new List<EngProfileDetail>();
            string EngID = Convert.ToString(UserID);
            profile = obj.GetAllEngineerskills(0, "", EngID);
            if (profile != null && profile.Rows.Count > 0)
            {
                var clientdteail = from items in profile.AsEnumerable()
                                   select new
                                   {
                                       //   id = Convert.ToInt64(items["UserID"]),
                                       name = Convert.ToString(items["name"]),
                                       EmailId = Convert.ToString(items["EmailId"]),
                                       TotalSkill = Convert.ToString(items["TotalSkills"]),
                                       Zone = Convert.ToString(items["Zone"]),
                                       SubZone = Convert.ToString(items["SubZone"]),
                                       TotalTicket = Convert.ToString(items["TotalTicket"]),
                                       TotalAppointement = Convert.ToString(items["TotalAppointments"]),

                                       //Amount = Convert.ToString(items["Amount"])
                                   };

                foreach (var ddetails in clientdteail)
                {
                    lstdetails.Add(new EngProfileDetail { name = ddetails.name, EmailId = ddetails.EmailId, TotalSkill = ddetails.TotalSkill, Zone = ddetails.Zone, SubZone = ddetails.SubZone, TotalTicket = ddetails.TotalTicket, TotalAppointement = ddetails.TotalAppointement });
                }
            }
            return lstdetails;
        }

        [WebMethod]
        public static List<TicketWorkHistory> BindWorkHistory(Int64 UserID, Int64 TicketID)
        {
            BLServiceEngineer obj = new BLServiceEngineer();
            List<TicketWorkHistory> lstdetails = new List<TicketWorkHistory>();

            string EngID = Convert.ToString(UserID);
            DataTable dt = obj.GetDiaryByTicketID(TicketID);
            if (dt != null && dt.Rows.Count > 0)
            {
                var clientdteail = from items in dt.AsEnumerable()
                                   select new
                                   {
                                       TicketID = Convert.ToInt64(items["TicketID"]),
                                       Opendate = Convert.ToString(items["Opendate"]),
                                       ServiceStart = Convert.ToString(items["ServiceStart"]),
                                       ServiceEnd = Convert.ToString(items["ServiceEnd"]),
                                       WorkDEscCheckIn = Convert.ToString(items["WorkDEscCheckIn"]),
                                       WorkDescCheckOut = Convert.ToString(items["WorkDescCheckOut"]),
                                   };
              
                foreach (var ddetails in clientdteail)
                {
                    lstdetails.Add(new TicketWorkHistory
                    {
                        TicketID = ddetails.TicketID,
                        Opendate = ddetails.Opendate,
                        ServiceStart = ddetails.ServiceStart,
                        ServiceEnd = ddetails.ServiceEnd,
                        WorkDEscCheckIn = ddetails.WorkDEscCheckIn,
                        WorkDescCheckOut = ddetails.WorkDescCheckOut
                    });
                }
            }
            return lstdetails;
        }


    }
}
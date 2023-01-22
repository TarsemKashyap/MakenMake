using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MakeNMake.BL;
using System.Data;
namespace makenMakeApi.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public  JqDataTableTickets GetAllTickets()
        {
            BLAdmin objAdmin = new BLAdmin();
            DataTable dt = objAdmin.GetAllTickets();


            JqDataTableTickets obj = new JqDataTableTickets();
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
                                 closure = Convert.ToString(items["closure"]).ToLower() == "opened" ? "opened" : Convert.ToString(Convert.ToDateTime(items["closure"]).ToString("MM/dd/yyyy hh:mm")),
                             };

            foreach (var data in ticketData)
            {

                lstTickets.Add(new Tickets
                {
                    ticketID = data.id,
                    ticketType = data.ticketType,
                    customerName = data.name,
                    status = data.status,
                    created = data.created,
                    closure = data.closure,
                    details = null
                });
            }
            obj.Tickets = lstTickets;
            return obj;
        }
    }
    public class JqDataTableTickets
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<Tickets> Tickets;
    }
    public class Tickets
    {
        public Int64 ticketID { get; set; }
        public string ticketType { get; set; }
        public Int64 customerID { get; set; }
        public string customerName { get; set; }
        public string status { get; set; }
        public string created { get; set; }
        public string closure { get; set; }
        public List<TicketDetails> details { get; set; }
    }
    public class TicketDetails
    {
        public string serviceName { get; set; }
        public string description { get; set; }
        public string plan { get; set; }
        public string servedBy { get; set; }
        public string Created { get; set; }
    }
}

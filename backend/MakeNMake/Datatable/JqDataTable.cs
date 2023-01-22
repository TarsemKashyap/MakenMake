using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeNMake.Datatable
{
    public  class JqDataTableTickets
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<Tickets> Tickets;
    }
    public  class Tickets
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
        public Int64 servedByID { get; set; }
    }


    public class EngProfileDetail
    {
        public string name { get; set; }
        public string EmailId { get; set; }
        public string TotalSkill { get; set; }
        public string Zone { get; set; }
        public string SubZone { get; set; }
        public string TotalTicket { get; set; }
        public string TotalAppointement { get; set; }
        
    }
    public class TicketWorkHistory
    {
        public Int64 TicketID { get; set; }
        public string Opendate { get; set; }
        public string ServiceStart { get; set; }
        public string ServiceEnd { get; set; }
        public string WorkDEscCheckIn { get; set; }
        public string WorkDescCheckOut { get; set; }

    }




}
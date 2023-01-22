using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeNMake.Datatable
{
    public class ClientHIstory
    {
        
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<Clients> ClientId;
    }
    public class Clients
    {
        public Int64 UserID { get; set; }
        public string Name{ get; set; }
       public List<ClientProfileDetail> detail { get; set; }
       public List<ClienBillDetail> detai { get; set; }
        public List<ClientDetail> details { get; set; }
    }
    public class ClientDetail
    {
        public string ServiceName { get; set; }
        public string PlanType { get; set; }
        public int Calls { get; set; }
        public string IssueDate { get; set; }
        public string ServiceCategory { get; set; }
        public string ExpirationDate { get; set; }
        public string Amount { get; set; }
    }
    public class ClientProfileDetail
    {
        public string Reference { get; set; }
        public string ServiceDate { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
    }
    public class ClienBillDetail
    {
        public Int64 UserID { get; set; }
        public string Name { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string Amount { get; set; }
       // public string Amount { get; set; }
    }
}
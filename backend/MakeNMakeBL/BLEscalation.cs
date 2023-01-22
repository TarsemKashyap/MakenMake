using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeNMake.BL
{
   public class BLEscalation
    {

       MakeNMake.DL.Escalation obj = new MakeNMake.DL.Escalation();
       public DataTable GetTicketHistoryByTicketID(int pageNumber,Int64 ticketID)
       {
           return obj.GetTicketHistoryByTicketID(pageNumber,ticketID);
       }
       public DataTable GetAllTicketsInProcess(int currentpage,string clientName)
       {
           return obj.GetAllTicketsInProcess(currentpage, clientName);
       }
       public DataTable GetAllTicketsNotINProcess(int currentpage)
       {
           return obj.GetAllTicketsNotINProcess(currentpage);
       }
       public DataTable GetAllAppoinments(int currentpage, int status, string clientName)
       {
           return obj.GetAllAppointmentList(currentpage, status, clientName);
       }

       public DataTable AllAppointmentHistory(int currentpage, Int64 AppointmentID)
       {
           return obj.GetAllAppointmentHistoryList(currentpage, AppointmentID);
       }
       public DataTable BindAllEngineer(Int64 Appoint_TicketID, string APPoint_ticket)
       {
           return obj.BindAllEngineerList(Appoint_TicketID, APPoint_ticket);
       }

       public DataTable FindEngineerStatus(Int64 Appoint_TicketID, string APPoint_ticket)
       {
           return obj.FindEngineerStatus(Appoint_TicketID, APPoint_ticket);
       }

       public int AssignTicketToEnginner(Int64 EngineerID, Int64 session, Int64 TicketID, string Comments, int status)
       {
           return obj.AssignTicketToEnginner(EngineerID, session, TicketID, Comments, status);
       }
       public DataTable AssignAppointmentsToEnginner(Int64 EngineerID, Int64 session, Int64 AppointmentID, string Comments, int status)
       {
           return obj.AssignAppointmentsToEnginner(EngineerID, session, AppointmentID, Comments, status);
       }
    }
}

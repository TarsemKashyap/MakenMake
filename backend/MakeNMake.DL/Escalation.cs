using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeNMake.DL
{
   public class Escalation
   {
       public string connectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["MakeNMake"]);

       public DataTable GetTicketHistoryByTicketID(int pageNumber, Int64 ticketID)
       {
           SqlParameter[] parameter = { new SqlParameter("@TicketID",ticketID),new SqlParameter ("@PageNumber",pageNumber) };
           return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetTicketHistoryByTicketID", parameter);
       }
       public DataTable GetAllTicketsInProcess(int currentpage,string search)
       {
           SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), new SqlParameter("@client_name",search) };
           return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetAllTicketsInProcess", parameter);

       }
       public DataTable GetAllTicketsNotINProcess(int currentpage)
       {
           SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage) };
           return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetAllTicketsNotINProcess", parameter);
       }
       public DataTable GetAllAppointmentList(int currentpage, int AppoinmentStatus, string clientName)
       {
           SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), new SqlParameter("@AppoinmentStatus", AppoinmentStatus), new SqlParameter("@client_name", clientName) };
           return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "UspGetAppointmentsByStatus", parameter);
       }
       public DataTable GetAllAppointmentHistoryList(int currentpage, Int64 AppointmentID)
       {
           SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), new SqlParameter("@AppointmentID", AppointmentID) };
           return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "UspGetAppointmentByStatus", parameter);
       }
       public DataTable BindAllEngineerList(Int64 Appoint_TicketID, string APPoint_ticket)
       {
           SqlParameter[] parameter = { new SqlParameter("@Appoint_TicketID", Appoint_TicketID), new SqlParameter("@APPoint_ticket", APPoint_ticket) };
           return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "UspGetAllEngineersList", parameter);
       }

       public DataTable FindEngineerStatus(Int64 Appoint_TicketID, string APPoint_ticket)
       {
           SqlParameter[] parameter = { new SqlParameter("@Appoint_TicketID", Appoint_TicketID), new SqlParameter("@APPoint_ticket", APPoint_ticket) };
           return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "UspGetFindEngineerStatus", parameter);
       }


       public int AssignTicketToEnginner(Int64 EngineerID, Int64 session, Int64 TicketID, string Comments, int status)
       {
           MethodHelper objHelper = new MethodHelper();
           SqlParameter[] parameter = { new SqlParameter("@EngineerID", EngineerID), new SqlParameter("@sesssion", session),
                                       new SqlParameter("@TicketID", TicketID),new SqlParameter("@Comments", Comments)
                                        ,new SqlParameter("@status", status)};
           return objHelper.ExcuteNonQuery(connectionString, "UspGetAssignTicketToEnginner", parameter);
       }
       public DataTable AssignAppointmentsToEnginner(Int64 EngineerID, Int64 session, Int64 AppointmentID, string Comments, int status)
       {
           SqlParameter[] parameter = { new SqlParameter("@EngineerID", EngineerID), new SqlParameter("@sesssion", session),
                                       new SqlParameter("@AppointmentID", AppointmentID),new SqlParameter("@Comments", Comments)
                                        ,new SqlParameter("@status", status)};
           return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "UspGetAssignAppointmentsToEnginner", parameter);
       }
    }
}

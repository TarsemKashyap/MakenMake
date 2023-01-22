using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace MakeNMake.BL
{
    public class BLCustomerCare
    {
        MakeNMake.DL.CustomerCare obj = new DL.CustomerCare();

        public int AddUpdateUserOTP(Int64 userID, string OTP, string mobileNUmber, int status)
        {
            return obj.AddUpdateUserOTP(userID, OTP, mobileNUmber, status);
        }
        public int CheckUserOtp(Int64 UserID,string OTP)
        {
            return obj.CheckUserOTP(UserID,OTP);
        }
        public int AddAppoinment(Int64 UserID, Int64 CustomerCareID, string date, string time, int status)
        {
            return obj.AddAppoinment(UserID, CustomerCareID, date, time, status);
        }
       public int AddComplaint(Int64 userID, Int64 createdFor,Int64 agreementID, string services, string type, string desc)
       {
           return obj.AddComplaint(userID, createdFor, agreementID, services, type, desc);
       }
        public int ticketStatusByTicketId(Int64 ticketID)
        {
            return obj.ticketStatusByTicketId(ticketID);
        }
        public DataTable GetEsclationMnagerByZoneidticket(int zoneid)
        {
            DataTable dt;
            dt = obj.GetEsclaionMangerticket(zoneid);

            return dt;
        }
        public DataTable GetEsclationMnagerByZoneid(int zoneid)
       {
           DataTable dt;
           dt = obj.GetEsclaionManger(zoneid);

           return dt;
       }
       public DataTable GetEsclationMnagerByTicketID(int zoneid)
       {
           DataTable dt;
           dt = obj.GetEsclaionMangerbyTicketID(zoneid);

           return dt;
       }
       public DataTable GetConsumer(int CurrentPage, string userID, string clientName)
       {
           DataTable dt;
           dt = obj.GetClient(CurrentPage, userID, clientName);

           return dt;
       }
        public DataTable GetConsumerByID(Int64 UserID)
        {
            return obj.GetClientByID(UserID);
        }
        public DataTable GetEngineerByAppointmentD(Int64 AppointmentID)
        {
            return obj.GetEngineerByAppointmentD(AppointmentID);
        }
        public DataTable GetEngineerByTicketID(Int64 TicketID)
        {
            return obj.GetEngineerByTicketID(TicketID);
        }
        public DataTable GetUSerByTicketID(Int64 TicketID,int status)
        {
            return obj.GetUSerByTicketID(TicketID,status);
        }
    }
}

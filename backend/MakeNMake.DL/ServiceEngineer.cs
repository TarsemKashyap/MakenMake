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
    public class ServiceEngineer
    {
        MethodHelper objHelper = new MethodHelper();
        public string connectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["MakeNMake"]);

        public DataTable GetSkills()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServices");
        }


        public int EngineerAppointment(Int64 appointmentID,Int64 engineerID,Int64 ModifiedBy, int status,string reason)
        {
            SqlParameter[] parameter = {new SqlParameter("@EngineerID",engineerID), new SqlParameter("@AppointmentID",appointmentID),
                                           new SqlParameter("@UserID", ModifiedBy), 
                                           new SqlParameter("@Status",status) ,new SqlParameter("@Reason",reason)      };
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateAppointmentStatus", parameter);
        }
        public int UpdateEngineerTicket(Int64 ticktID, Int64 engineerID, Int64 ModifiedBy, int status,string reason)
        {
            SqlParameter[] parameter = {new SqlParameter("@TicketID",ticktID),new SqlParameter("@EngineerID",engineerID), 
                                           new SqlParameter("@ModifiedBy", ModifiedBy), 
                                           new SqlParameter("@Status",status) ,  new SqlParameter("@Reason",reason)      };
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateTicketStatus", parameter);
        }
        public int AddServiceTime(Int64 UserID, Int64 ticketID, Int64 Engineer, Int32 serviceid, string from, string to,string ImageName,Int64 tblID,string work)
        {
            SqlParameter[] parameter = {new SqlParameter("@EngineerID", Engineer), new SqlParameter("@UserID",UserID), new SqlParameter("@TicketID", ticketID), 
                                           new SqlParameter("@ServiceID",serviceid),new SqlParameter("@servicefrom",from),new SqlParameter("@Image",ImageName),
                                       new SqlParameter("@serviceto",to),new SqlParameter("@tblID",tblID),
                                       new SqlParameter("@work",work)};

            return objHelper.ExcuteNonQuery(connectionString, "uspAddServiceTime", parameter);
        }
        public int Addskill(Int64 UserID, int skill, int skillrate, char pskill, Int64 CreatedBy, Int64 ModifiedBy)
        {
            SqlParameter[] parameter = {new SqlParameter("@EngineerID", UserID), new SqlParameter("@SkillID", skill), new SqlParameter("@Rate", skillrate), 
                                           new SqlParameter("@SkillType",pskill),new SqlParameter("@CreatedBy",CreatedBy) ,new SqlParameter("@ModifiedBy",ModifiedBy)     };

            return objHelper.ExcuteNonQuery(connectionString, "uspAddSkill", parameter);
        }
        public int ApplyLeave(Int64 engineerid,string reason,DateTime leaveon,DateTime created,int status)
        {
            SqlParameter[] parameter = {new SqlParameter("@engineerid", engineerid), new SqlParameter("@reason", reason),
                                           new SqlParameter("@leaveon",leaveon),new SqlParameter("@created",created) ,new SqlParameter("@status",status)     };

            return objHelper.ExcuteNonQuery(connectionString, "uspApplyLeavebyServiceEngineer", parameter);
        }
       
        public int deleteskill(Int64 UserID, Int64 SkillID)
        {
            SqlParameter[] parameter = { new SqlParameter("@EngineerID", UserID), new SqlParameter("@SkillID", SkillID) };

            return objHelper.ExcuteNonQuery(connectionString, "uspDeleteskill", parameter);
        }
        public int AddDailyDiary(Int64 UserID, string Title, string Description, DateTime moddate, int ReportTo, DateTime Created,Int64 ticktID)
        {
            SqlParameter[] parameter = {new SqlParameter("@UserID", UserID), new SqlParameter("@Title", Title), 
                                           new SqlParameter("@Description",Description) , new SqlParameter("@Modified",moddate),
                                        new SqlParameter("@ReportTo",ReportTo) , new SqlParameter("@Created",Created),
                                        new SqlParameter("@TicketID",ticktID)};

            return objHelper.ExcuteNonQuery(connectionString, "uspAddDailyDiary", parameter);
        }
        public DataTable Getskill(Int64 EngineerID)
        {
            SqlParameter[] parameter = { new SqlParameter("@@EngineerID", EngineerID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetEngineerSkills",parameter);
        }

        public DataTable GetTotalSkills(Int64 EngineerID)
        {
            SqlParameter[] parameter = { new SqlParameter("@EngineerID", EngineerID)};
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetTotalSkillsByID", parameter);
        }

        public DataTable Getskill(int currentpage, Int64 EngineerID)
        {
            SqlParameter[] parameter = { new SqlParameter("@EngineerID", EngineerID), new SqlParameter("@PageNumber", currentpage) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetEngineerSkills", parameter);
        }
       
        public DataTable GetAppoinments(int currentpage, Int64 EngID)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), new SqlParameter("@EngineerID", EngID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspEngineerAppointmentStatusByID", parameter);
        }
        public DataTable GetAppliedLeave(Int64 engineerId)
        {
            SqlParameter[] parameter = { new SqlParameter("@engineerId", engineerId) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspgetserviceengineerleavebyId", parameter);
        }
        public DataTable GetTickets(int CurrentPage, Int64 EngID)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", CurrentPage), new SqlParameter("@EngineerID", EngID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspEngineerTicketStatusByID", parameter);
        }
        public DataTable SearchTickets(Int64 EngID,string TicketIDOrName,int inspectionType,int findwhat)
        {
            SqlParameter[] parameter = { new SqlParameter("@EngineerID", EngID), new SqlParameter("@TicketIDorCustomerName", TicketIDOrName)
                                       , new SqlParameter("@FindWhat", findwhat), new SqlParameter("@TicketType", inspectionType)};
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspSearchEngineerTicketStatusByID", parameter);
        }
        public DataTable GetTicketDetails(Int64 TicketID)
        {
            SqlParameter[] parameter = { new SqlParameter("@TicketID", TicketID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetTicketDetails", parameter);
        }
        public DataTable GetUserid(Int64 EngineerID)
        {
            SqlParameter[] parameter = { new SqlParameter("@EngineerID", EngineerID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetUserId", parameter);
        }
        public DataTable GetDailyDiary(Int64 EngineerID)
        {
            SqlParameter[] parameter = { new SqlParameter("@EngineerID", EngineerID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetDailyDiary", parameter);
        }
        public DataTable GetServiceTicketsForTiming(Int64 EngineerID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", EngineerID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServiceForTiming", parameter);
        }
        public DataTable GetServiceTicketsByEngID(Int64 EngineerID)
        {
            SqlParameter[] parameter = { new SqlParameter("@EngID", EngineerID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServiceTimeByUserID", parameter);
        }
        public Int64 GetUserIDByTicketID(Int64 ticktID)
        {
            SqlParameter[] parameter = { new SqlParameter("@ticketID", ticktID) };
            return Convert.ToInt64( SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, "uspGetUserIDByTicletID", parameter));
        }
        public DataTable GetCustomerAddressByTicketID(Int64 TicketID)
        {
            SqlParameter[] parameter = { new SqlParameter("@TicketID", TicketID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetCustomerAddress", parameter);
        }
        public DataTable GetTicketIDsAcceptedByEngID(Int64 EngineerID)
        {
            SqlParameter[] parameter = { new SqlParameter("@EngineerID", EngineerID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetTicketIDByID", parameter);
        }
        public DataTable GetImagesByID(Int64 ServiceTimeID)
        {
            SqlParameter[] parameter = { new SqlParameter("@ServiceTimeID", ServiceTimeID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServiceTimesImages", parameter);
        }
        public int DeleteImage(Int64 ImageID)
        {
            SqlParameter[] parameter = { new SqlParameter("@ImageID",ImageID)};

            return objHelper.ExcuteNonQuery(connectionString, "uspDeleteServiceTimeImages", parameter);
        }
        public int CheckServiceStatus(Int64 ticketID)
        {
            SqlParameter[] parameter = { new SqlParameter("@TicketID", ticketID) };

            return objHelper.ExcuteNonQuery(connectionString, "uspCheckServiceTimeStatus", parameter);
        }
        public Int64 UpdateRequestData(Int64 RequestID, Int64 engineerID, int status, string reason)
        {
            SqlParameter[] parameter = { new SqlParameter("@userId", engineerID), new SqlParameter("@Status", status), new SqlParameter("@reason",reason) 
                                       ,new SqlParameter("@RequestID", RequestID)};
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateRequestData", parameter);
        }
        public DataTable GetAssesmentForm(Int64 requestID)
        {
            SqlParameter[] parameter = { new SqlParameter("@RequestID", requestID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetAssesmentForm", parameter);       
        }
        public Int64 AddAssesmentForm(Int64 RequestID, int serviceID, Int64 engineerID, string formData, string reason)
        {
            SqlParameter[] parameter = { new SqlParameter("@RequstID", RequestID), new SqlParameter("@ServiceID",serviceID), new SqlParameter("@EngineerID",engineerID) ,
                                       new SqlParameter("@FormData",formData) ,new SqlParameter("@reason",reason) };
            return objHelper.ExcuteNonQuery(connectionString, "uspAddAssesmentData", parameter);
        }
        public DataSet GetAssesmentData(Int64 requestID)
        {
            SqlParameter[] parameter = { new SqlParameter("@RequestID", requestID) };
            return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, "uspGetAssessmentData", parameter);       
        }
        public DataTable GetDiaryByTicketID(Int64 ticktID)
        {

            SqlParameter[] parameter = { new SqlParameter("@ticketID", ticktID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetDiaryByTicletID", parameter);
        }
    }
}

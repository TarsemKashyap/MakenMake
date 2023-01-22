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
    public class CustomerCare
    {
        MethodHelper objHelper = new MethodHelper();
        public string connectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["MakeNMake"]);

        #region Insert_delete_Update Method
        public int AddUpdateUserOTP(Int64 userID, string OTP, string mobileNUmber, int status)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", userID), new SqlParameter("@OTP", OTP), new SqlParameter("@Status", status), new SqlParameter("@MobileNumber", mobileNUmber) };
            return objHelper.ExcuteNonQuery(connectionString, "uspAddUpdateOtp", parameter);
        }
        public int AddAppoinment(Int64 UserID, Int64 CustomerCareID, string date, string time, int status)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID), new SqlParameter("@CustomerCareID", CustomerCareID), new SqlParameter("@Date", date),
                                           new SqlParameter("@time", time) ,new SqlParameter("@Status", status) };
            return objHelper.ExcuteNonQuery(connectionString, "uspAddAppointment", parameter);
        }

        public int AddServiceToBasket(Int64 userID, Int64 createdBy, string services, string plan, int calls, int duration, decimal amount)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserId", userID), new SqlParameter("@CreatedBy", createdBy),
                                        new SqlParameter("@Services", services), new SqlParameter("@Plan", plan)
                                       , new SqlParameter("@Calls", calls), new SqlParameter("@duration", duration),new SqlParameter("@Amount",amount)};
            return objHelper.ExcuteNonQuery(connectionString, "uspAddServiceToBasket", parameter);
        }
        public int EditServiceContractToBasket(Int64 userID, Int64 createdBy, string services, string plan, int calls, int duration, decimal amount)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserId", userID), new SqlParameter("@CreatedBy", createdBy),
                                        new SqlParameter("@Services", services), new SqlParameter("@Plan", plan)
                                       , new SqlParameter("@Calls", calls), new SqlParameter("@duration", duration),new SqlParameter("@Amount",amount)};
            return objHelper.ExcuteNonQuery(connectionString, "uspEditServicesToBasket", parameter);
        }
        public int AddContractToBasket(Int64 userID, Int64 createdBy, string services)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserId", userID), new SqlParameter("@CreatedBy", createdBy),
                                        new SqlParameter("@Services", services)};
            return objHelper.ExcuteNonQuery(connectionString, "uspCreateServiceContractToBasket", parameter);
        }

        public int AddComplaint(Int64 userID, Int64 createdFor, Int64 agreementID, string services, string type, string desc)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserId", userID), new SqlParameter("@CreatedFor",createdFor),
                                           new SqlParameter("@AgreementID",agreementID),
                                        new SqlParameter("@Desc", desc), new SqlParameter("@Type", type)
                                       , new SqlParameter("@Services",services)};
            return objHelper.ExcuteNonQuery(connectionString, "uspAddConsumerTicket", parameter);
        }

        public int ticketStatusByTicketId(Int64 ticketID)
        {
            SqlParameter[] parameter = { new SqlParameter("@ticketID", ticketID)};
            return objHelper.ExcuteNonQuery(connectionString, "uspticketStatusByTicketId", parameter);
        }
        public int DeleteBasketItem(Int64 orderID)
        {
            SqlParameter[] parameter = { new SqlParameter("@OrderID", orderID) };
            return objHelper.ExcuteNonQuery(connectionString, "uspDeleteBasketItem", parameter);
        }
        public int Payment(string invoicenumber, Int64 userid, Int64 createdBy, int paymentMode, int paymentStatus,
            decimal Amount, int changePlan, decimal balance, decimal withdraw,
            int paymentMethod, decimal citrusAmount, string merchantID, string citrusTransID, string citrusTransStatus, string issueReference,
            int status, string plan,string type)
        {
            SqlParameter[] parameter = { new SqlParameter("@InvoiceNumber", invoicenumber), new SqlParameter("@UserID", userid),
                                           new SqlParameter("@WalletWithdrawal", withdraw),
                                       new SqlParameter("@CreatedBy", createdBy),new SqlParameter("@PaymentMode", paymentMode),
                                       new SqlParameter("@PaymentStatus", paymentStatus)
                                       ,new SqlParameter("@amount", Amount),new SqlParameter("@ChangePlan", changePlan)
                                       ,new SqlParameter("@PaymentMethod", paymentMethod)
                                       ,new SqlParameter("@balance", balance)
                                       ,new SqlParameter("@MerchantID", merchantID)
                                       ,new SqlParameter("@CitrusTransID", citrusTransID)
                                       ,new SqlParameter("@CitrusTransStatus", citrusTransStatus)
                                       ,new SqlParameter("@IssueRefNo", issueReference)
                                           ,new SqlParameter("@Status", status)
                                               ,new SqlParameter("@ServicePlan", plan)
                                                   ,new SqlParameter("@ServiceType", type)
        };
            return objHelper.ExcuteNonQuery(connectionString, "uspUserPayment", parameter);
        }
        public int CommercialRequest(string serviceid, string planId, string unlimitedID, string firstname, string lastname, Int64 userid, string mobile, string landline, string EmilID, string address, string totalSteArea, string buildUpArea, DateTime prefereedtime, int contactble, string contact_Fname, string contact_Lname, string contact_Emailid, string contact_mobile, string Conctable_Address)
        {
            CustomerCare objBasket = new CustomerCare();
            {
                SqlParameter[] parameter = { new SqlParameter("@firstName", firstname), new SqlParameter("@lastname", lastname),
                                       new SqlParameter("@Userid", userid),new SqlParameter("@mobile", mobile),
                                       new SqlParameter("@landline", landline),new SqlParameter("@EmilID", EmilID),new SqlParameter("@Address", address),
                                       new SqlParameter("@TotalbuildArea",totalSteArea),new SqlParameter("@BuildArea", buildUpArea),
                                       new SqlParameter("@prefereedTime", prefereedtime),new SqlParameter("@contactble", contactble)
                                       ,new SqlParameter("@contact_Fname", contact_Fname),new SqlParameter("@contact_Lname", contact_Lname)
                                       ,new SqlParameter("@contact_Emailid", contact_Emailid),new SqlParameter("@contact_mobile", contact_mobile),
                                      new SqlParameter("@Contact_Address", Conctable_Address) ,new SqlParameter("@serviceID", serviceid) 
                                           ,new SqlParameter("@PlanID", planId) ,new SqlParameter("@UnlimitedPlanID", unlimitedID) };
                return objHelper.ExcuteNonQuery(connectionString, "uspCreateCommercialRequest", parameter);
            }
        }

        public DataTable GetEngineerByAppointmentD(Int64 AppointmentID)
        {
            SqlParameter[] parameter = { new SqlParameter("@AppoinmentID", AppointmentID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetEngineerByAppointmentD", parameter);
        }
        public DataTable GetEngineerByTicketID(Int64 TicketID)
        {
            SqlParameter[] parameter = { new SqlParameter("@TicketID", TicketID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetEngineerByTicketID", parameter);
        }
        public DataTable GetUSerByTicketID(Int64 TicketID, int status)
        {
            SqlParameter[] parameter = { new SqlParameter("@TicketID", TicketID), new SqlParameter("@Status", status) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetUserTicketID", parameter);
        }
        #endregion

        #region Select Method
        public DataTable GetClient(int CurrentPage, string userID, string clientName)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", CurrentPage), new SqlParameter("@userid", userID), new SqlParameter("@client_name", clientName) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetConsumer", parameter);
        }
        public DataTable GetEsclaionMangerticket(int zoneid)
        {
            SqlParameter[] parameter = { new SqlParameter("@zoneid", zoneid) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "UspGetEscalationmanagerbyZoneTicket", parameter);
        }
        public DataTable GetEsclaionManger(int zoneid)
        {
            SqlParameter[] parameter = { new SqlParameter("@zoneid", zoneid) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "getEscalationmanagerbyZone", parameter);
        }
        public DataTable GetEsclaionMangerbyTicketID(int zoneid)
        {
            SqlParameter[] parameter = { new SqlParameter("@zoneid", zoneid) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "getEscalationmanagerbyTicketID", parameter);
        }
        public DataTable GetClientByID(Int64 userID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", userID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetConsumerByID", parameter);
        }
        public DataSet Getpayment(Int64 userID,int status ,string plan, string type)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", userID), new SqlParameter("@Status", status), new SqlParameter("@SPlan", plan),
                                       new SqlParameter("@SType",type)};
            return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, "uspGetPayment", parameter);
        }
        public int CheckUserOTP(Int64 userID, string OTP)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", userID), new SqlParameter("@OTp", OTP) };
            return objHelper.ExcuteNonQuery(connectionString, "uspCheckOTP", parameter);
        }
        public DataSet GetServiceAccordingToUser(Int64 userID, string category, int TicketType)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", userID), new SqlParameter("@Category", category), new SqlParameter("@TicketType", TicketType) };
            return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, "uspGetServicesAccordingToUser", parameter);
        }
        public int CheckUserAddress(Int64 userID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", userID) };
            int i = Convert.ToInt32(SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, "uspGetAddressByUserID", parameter));
            return i;
        }
        public DataTable GetInspectionServices(string plan, string category)
        {
            SqlParameter[] parameter = { new SqlParameter("@Plan", plan), new SqlParameter("@Category", category) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetInspectionService", parameter);
        }

        public DataTable GetContractForClient(string plan,Int64 userID , string stype)
        {
            SqlParameter[] parameter = { new SqlParameter("@plan", plan), new SqlParameter("@userID", userID), new SqlParameter("@sType", stype) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetContractForClient", parameter);
        }
        
        #endregion
    }
}

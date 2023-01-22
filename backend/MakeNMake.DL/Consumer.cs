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
    public class Consumer
    {
        MethodHelper objHelper = new MethodHelper();
        public string connectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["MakeNMake"]);

        #region Insert_Update_Delete_Method

        public int RegisterConsumer(string firstname, string lastname, string emalid, string password, string mobile, string gender, DateTime DOB, string address, Int64 country, Int64 state, Int64 district, Int64 city,Int64 createdBY,int status, Int64 zonID , Int64 subZonID,string alternatemobileNumber1,string alternatemobileNumber2,string alternatemobileNumber3,string alternatemobileNumber4)
        {
            SqlParameter[] parameter = { new SqlParameter("@FirstName", firstname), new SqlParameter("@mobile", mobile), 
                                           new SqlParameter("@gender", gender), new SqlParameter("@LastName", lastname), 
                                            new SqlParameter("@DOB", DOB),new SqlParameter("@address", address), new SqlParameter("@country", country),
                                            new SqlParameter("@state", state),new SqlParameter("@district", district),
                                            new SqlParameter("@city", city),new SqlParameter("@EmailID",emalid),
                                            new SqlParameter("@password",password),
                                       new SqlParameter("@CreatedBy",createdBY),new SqlParameter("@status",status)
                                       ,new SqlParameter("@ZoneID",zonID),new SqlParameter("@SubzoneID",subZonID)
                                        ,new SqlParameter("@alternatephoneno1",alternatemobileNumber1),
                                       new SqlParameter("@alternatephoneno2",alternatemobileNumber2),
                                       new SqlParameter("@alternatephoneno3",alternatemobileNumber3),
                                       new SqlParameter("@alternatephoneno4",alternatemobileNumber4)
                                       };
            return objHelper.ExcuteNonQuery(connectionString, "uspAddCustomer", parameter);
        }

        public int RegisterSignUpConsumer(string firstname, string lastname, string emalid, string password, string mobile, string gender, string DOB, string address, Int64 country, Int64 state, Int64 district, Int64 city, Int64 createdBY)
        {
            SqlParameter[] parameter = { new SqlParameter("@FirstName", firstname), new SqlParameter("@mobile", mobile), new SqlParameter("@gender", gender), new SqlParameter("@LastName", lastname), 
                                            new SqlParameter("@DOB", DOB),new SqlParameter("@address", address), new SqlParameter("@country", country),
                                            new SqlParameter("@state", state),new SqlParameter("@district", district),
                                            new SqlParameter("@city", city),new SqlParameter("@EmailID",emalid),
                                            new SqlParameter("@password",password),
                                       new SqlParameter("@CreatedBy",createdBY)};
            return objHelper.ExcuteNonQuery(connectionString, "uspAddSignUpCustomer", parameter);
        }
        public int ConfirmConsumer(string emalid)
        {
            SqlParameter[] parameter = { new SqlParameter("@EmailID", emalid) };
            return objHelper.ExcuteNonQuery(connectionString, "uspConfirmCustomer", parameter);
        }
        public int ValiDateuser(string emailid, string password, out int roleID)
        {
            SqlParameter[] parameter = { new SqlParameter("@EmailID", emailid), new SqlParameter("@Password", password) };
            return objHelper.ExcuteNonQueryMultipleOutput(connectionString, "uspCheckLogin", "@RoleID", out roleID, parameter);
        }
        public Int64 AmeyoUser(string getcustomerBy, string customerdata)
        {
            SqlParameter[] parameter = { new SqlParameter("@GetCustomer", getcustomerBy), new SqlParameter("@Customerdata", customerdata) };
            return objHelper.ExcuteNonQuery(connectionString, "uspGetCustomerForAmeyo", parameter);
        }
        public DataTable GetServicePurchased(Int64 userid)
        {
            SqlParameter[] parameter = { new SqlParameter("@userid", userid) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetClientAgreement", parameter);
        }
        public DataTable GetClientService(Int64 agreementID)
        {
            SqlParameter[] parameter = { new SqlParameter("@AgreementID", agreementID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "UspGetClientService", parameter);
        }
        public DataTable GetBalance(Int64 UserID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserId", UserID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServiceBalancehistory", parameter);
        }
        public int GetcustomerFeedback(Int64 customer, Int64 ticketID, int Satisfied, int Rating, int Status, string Description)
        {
            SqlParameter[] parameter = { new SqlParameter("@customerID", customer), new SqlParameter("@ticketID", ticketID)
                                       ,new SqlParameter("@Satisfied", Satisfied),new SqlParameter("@Rating", Rating),new SqlParameter("@Status", Status),
                                       new SqlParameter("@Description", Description)};
            return objHelper.ExcuteNonQuery(connectionString, "UspAddCustomerFeedback", parameter);
        }

        public DataTable GetFeedbackData(Int64 customer)
        {
            SqlParameter[] parameter = { new SqlParameter("@customerID", customer) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "UspGetShowFeedback", parameter);
        }

        public DataTable GetEngineerTicketsData(Int64 customer)
        {
            SqlParameter[] parameter = { new SqlParameter("@customerID", customer) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "UspGetEngineerTickets", parameter);
        }
        public DataTable GetTicketsByUserID(Int64 customerID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", customerID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetTicketsByID", parameter);
        }
        public DataTable GetInvoiceByUserID(Int64 customerID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", customerID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetInvoiceForCustomer", parameter);
        }

        #endregion
    }
}

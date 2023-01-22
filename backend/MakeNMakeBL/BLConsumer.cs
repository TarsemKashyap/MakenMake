using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeNMake.DL;
using System.Data;

namespace MakeNMake.BL
{
   public class BLConsumer
    {
       Consumer obj = new Consumer();

       public int SignUpUser(string firstname, string lastname, string emalid, string password, string mobile, string gender, DateTime DOB, string address, Int64 country, Int64 state, Int64 district, Int64 city,Int64 CreatedBY,int status,Int64 zoneID, Int64 subzoneid,string alternatemobileNumber1,string alternatemobileNumber2,string alternatemobileNumber3,string alternatemobileNumber4)
       {
           return obj.RegisterConsumer(firstname, lastname, emalid, password, mobile, gender, DOB, address, country, state, district, city, CreatedBY,status,zoneID,subzoneid,alternatemobileNumber1,alternatemobileNumber2,alternatemobileNumber3,alternatemobileNumber4);
       }
       public int RegisterSignUpConsumer(string firstname, string lastname, string emalid, string password, string mobile, string gender, string DOB, string address, Int64 country, Int64 state, Int64 district, Int64 city, Int64 CreatedBY)
       {
           return obj.RegisterSignUpConsumer(firstname, lastname, emalid, password, mobile, gender, DOB, address, country, state, district, city, CreatedBY);
       }
       public int ConfirmUser(string emailid)
       {
           return obj.ConfirmConsumer(emailid);
       }
       public Int64 AmeyoUser(string getcustomerBy, string customerdata)
       {
           return obj.AmeyoUser(getcustomerBy,customerdata);
       }
       public int ValidateUser(string emailid,string password,out int roleID)
       {
           return obj.ValiDateuser(emailid,password,out roleID);
       }
       public DataTable GetPurchasedClient(Int64 userid)
        {
            return obj.GetServicePurchased(userid);
        }

         public DataTable GetClientService(Int64  AgreementID)
         {
             return obj.GetClientService(AgreementID);
         }
         public DataTable GetBalance(Int64 UserID)
         {
             return obj.GetBalance(UserID);
         }
         public int CustomerFeedback(Int64 customer, Int64 ticketID, int Satisfied, int Rating, int Status, string Description)
         {
             return obj.GetcustomerFeedback(customer, ticketID, Satisfied, Rating, Status, Description);
         }
         public DataTable GetCustomerFeedbackData(Int64 customer)
         {
             return obj.GetFeedbackData(customer);
         }

         public DataTable GetEngineerTicketsData(Int64 customer)
         {
             return obj.GetEngineerTicketsData(customer);
         }
         public DataTable GetTicketsByUserID(Int64 customerID)
         {
             return obj.GetTicketsByUserID(customerID);
         }
         public DataTable GetInvoiceByUserID(Int64 customerID)
         {
             return obj.GetInvoiceByUserID(customerID);
         }
    }
}

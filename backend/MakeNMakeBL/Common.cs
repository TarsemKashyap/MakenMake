using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeNMake.DL;
using System.Web.UI.WebControls;
using System.Data;

namespace MakeNMake.BL
{
    public class Common
    {
        Admin obj = new Admin();
        public int ChangePassword(Int64 UserID,string oldPassword,string newPassword)
        {
            return obj.ChangeUserPassword(UserID, oldPassword, newPassword);
        }
        public int UpdateUserInfo(string mobileNumber, string address, int country, DateTime dob, Int64 state, Int64 district, Int64 city, string gender, Int64 userID, Int64 modifiedBY)
        {
            return obj.UpdateUserInfo(mobileNumber, address, country, state, district, city, dob, gender, userID, modifiedBY);
        }
        public int UpdateUserAlternateno(string altrno1,string altrno2,string altrno3,string altrno4,Int64 userid)
        {
            return obj.UpdateUserAlternateno(altrno1, altrno2, altrno3, altrno4,userid);
        }
        public int AddServiceToBasket( Int64 userID,Int64 createdBy,string services,string plan,int calls, int duration ,decimal amount)
        {
            CustomerCare objBasket = new CustomerCare();
            return objBasket.AddServiceToBasket(userID, createdBy, services, plan, calls, duration, amount);
        }
        public int EditServiceContractToBasket(Int64 userID, Int64 createdBy, string services, string plan, int calls, int duration, decimal amount)
        {
            CustomerCare objBasket = new CustomerCare();
            return objBasket.EditServiceContractToBasket(userID, createdBy, services, plan, calls, duration, amount);
        }
        public int AddContractToBasket(Int64 userID, Int64 createdBy, string services)
        {
            CustomerCare objBasket = new CustomerCare();
            return objBasket.AddContractToBasket(userID, createdBy, services);
        }
        public int Updatesubscriberinfo(DateTime DOB, string AlternateMobileNumber, string AlternateEmailID, string HearFromWhere, string NearMilestone, int SmsEnabled, string PermanentAddress, string ServiceType, string ServicePlan, string CurrentAddress, int CurrentCountry, int CurrentState, int CurrentDistrict, int CurrentCity, int PermanentCountry, int PermanentState, int PermanentDistrict, int PermanentCity, Int64 UserID, Int64 modifiedBy)
        {
            return obj.Updatesubscriberinfo(DOB, AlternateMobileNumber, AlternateEmailID, HearFromWhere, NearMilestone, SmsEnabled, PermanentAddress, ServiceType, ServicePlan, CurrentAddress, CurrentCountry, CurrentState, CurrentDistrict, CurrentCity, PermanentCountry, PermanentState, PermanentDistrict, PermanentCity, UserID, modifiedBy);
        }
        public int Payment(string invoicenumber, Int64 userid, Int64 createdBy, int paymentMode, int paymentStatus, decimal Amount,int changePlan,decimal balance,decimal withdraw,  int paymentMethod, decimal citrusAmount, string merchantID, string citrusTransID,string citrusTransStatus,string issueReference,int status, string plan,string type)
        {
            CustomerCare objBasket = new CustomerCare();
            return objBasket.Payment(invoicenumber, userid, createdBy, paymentMode, paymentStatus, Amount, changePlan, balance, withdraw, paymentMethod, citrusAmount, merchantID, citrusTransID, citrusTransStatus, issueReference, status, plan, type);
        }

        public DataTable GetSubscriberInfo(Int64 UserID)
        {
            return obj.GetSubscriberInfo(UserID);
        }
        public int CommercialRequest(string serviceid, string planId, string unlimitedID, string firstname, string lastname, Int64 userid, string mobile, string landline, string EmilID, string address, string totalSteArea, string buildUpArea, DateTime prefereedtime, int contactble, string contact_Fname, string contact_Lname, string contact_Emailid, string contact_mobile, string Conctable_Address)
        {
            CustomerCare objBasket = new CustomerCare();
            return objBasket.CommercialRequest(serviceid,planId,unlimitedID, firstname, lastname, userid, mobile, landline, EmilID, address, totalSteArea, buildUpArea, prefereedtime, contactble, contact_Fname, contact_Lname, contact_Emailid, contact_mobile, Conctable_Address);
        }

        public int DeleteBasketItem(Int64 orderID)
        {
            CustomerCare objBasket = new CustomerCare();
            return objBasket.DeleteBasketItem(orderID);
        }
        public int GetInfo(Int64 UserID)
        {
            return obj.GetGeneralInfo(UserID);
        }
        public int GetBalanceEnquiry(Int64 UserID)
        {
            return obj.GetBalanceEnquiry(UserID);
        }
        public int CheckEmailID(string emailID)
        {
            return obj.CheckEmailiExistence(emailID);
        }
        public DataTable GetUserInfoByID(Int64 UserID)
        {
            return obj.GetUserByID(UserID);
        }
        public DataTable GetUserInfoByEmail(string emailid)
        {
            return obj.GetUserByEmailid(emailid);
        }
        public int ForgotPassword(string emailID,string guid)
        {
            return obj.ForgotPassword(emailID, guid);
        }
        public int SetNewPassword(string emailID, string password)
        {
            return obj.SetUpNewPassword(emailID, password);
        }
        public DataSet GetPayment(Int64 UserID,int status,string plan ,string type)
        {
            CustomerCare objBasket = new CustomerCare();
            return objBasket.Getpayment(UserID,status,plan,type);
        }
      
       public DataSet GetServicesForUser(string category,  string plan)
       {
           return obj.GetServicesForuser(category,plan);
        }
       public DataTable GetServicesForRequest(string category, string plan,string type)
       {
           return obj.GetServicesForRequest(category, plan,type);
       }
        public DataTable GetServicesAfterDiscount(Int64 userid)
        {
            return obj.GetServicesAfterDiscount(userid);
        }
        public DataTable GetEditServicesAfterDiscount(Int64 userid)
        {
            return obj.GetEditServicesAfterDiscount(userid);
        }
        public DataTable GetCustomerByID(Int64 UserID)
        {
            return obj.GetCustomerByID(UserID);
        }
        public DataSet GetServiceAccordingToUser(Int64 userID, string category,int ticketType)
        {
            CustomerCare objCare = new CustomerCare();
            return objCare.GetServiceAccordingToUser(userID, category, ticketType);
        }
        public int CheckUserAddress(Int64 userID)
        {
            CustomerCare objCare = new CustomerCare();
            return objCare.CheckUserAddress(userID);
        }
        public DataTable GetInspectionServices(string plan, string category)
        {
            CustomerCare objCare = new CustomerCare();
            return objCare.GetInspectionServices(plan,category);
        }
        public DataTable GetMessageCirculatD(Int32 Roleid)
        {
            return obj.GetMessageCirculatD(Roleid);
        }

        public DataTable GetContractForClient(string plan, Int64 userID, string stype)
        {
            CustomerCare objCare = new CustomerCare();
            return objCare.GetContractForClient(plan, userID, stype);
        }
        
    }
}


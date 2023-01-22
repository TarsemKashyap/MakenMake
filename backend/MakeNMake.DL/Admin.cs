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
    public class Admin
    {
        MethodHelper objHelper = new MethodHelper();
        public string connectionString=Convert.ToString(ConfigurationManager.ConnectionStrings["MakeNMake"]);

        #region Insert_Update_Delete_Method
        public int AddUser(string firstname, string lastname, string emalid, string password, DateTime Dob, string address, int country, Int64 state, Int64 district, Int64 city, int roleID, int ZoneID, int subZoneID, Int64 userID, int status, string parentRole, string childRole, string mobileNumber,string alternatemobileNumber1,string alternatemobileNumber2,string alternatemobileNumber3,string alternatemobileNumber4, Int64 usermodified)
        {
            SqlParameter[] parameter = { new SqlParameter("@FirstName", firstname), new SqlParameter("@LastName", lastname), 
                                           new SqlParameter("@EmailID",emalid),new SqlParameter("@Password",password)
                                           ,new SqlParameter("@DOB", Dob)
                                           ,new SqlParameter("@Address", address),
                                            new SqlParameter("@Country", country),
                                            new SqlParameter("@State", state),
                                            new SqlParameter("@District", district),
                                            new SqlParameter("@CityID", city),          
                                           new SqlParameter("@RoleID", roleID),
                                       new SqlParameter("@ZoneID", ZoneID),new SqlParameter("@SubzoneID", subZoneID),new SqlParameter ("@CreatedBy",userID),
                                       new SqlParameter ("@Status",status),new SqlParameter ("@ParentRole",parentRole),new SqlParameter ("@ChildRole",childRole)
                                       ,new SqlParameter ("@Mobile",mobileNumber),new SqlParameter ("@ModifiedBy",usermodified)
                                       ,new SqlParameter("@alternatephoneno1",alternatemobileNumber1),
                                       new SqlParameter("@alternatephoneno2",alternatemobileNumber2),
                                       new SqlParameter("@alternatephoneno3",alternatemobileNumber3),
                                       new SqlParameter("@alternatephoneno4",alternatemobileNumber4)
                                       };
            return objHelper.ExcuteNonQuery(connectionString, "uspAdminAddUser", parameter);
        }
        public int AddOverheadInfo(string propertyname, string PropertyDescription,decimal value,Int64 createdBy,DateTime created,Int64 modifiedby,DateTime modified)
        {
            SqlParameter[] parameter = { new SqlParameter("@propertyName",propertyname), new SqlParameter("@propertyDecription",PropertyDescription), 
                                           new SqlParameter("@value",value),new SqlParameter("@createdby",createdBy)
                                           ,new SqlParameter("@created", created)
                                           ,new SqlParameter("@modifiedby",modifiedby),
                                       new SqlParameter("@modified",modified)};
                                       
                                           
            return objHelper.ExcuteNonQuery(connectionString, "uspAddOverheadInfo", parameter);
        }
        public int AddOverheadInfo(int overheadid,string propertyname, string PropertyDescription, decimal value, Int64 modifiedby, DateTime modified)
        {
            SqlParameter[] parameter = { new SqlParameter("@property",propertyname), new SqlParameter("@description",PropertyDescription), 
                                           new SqlParameter("@value",value),new SqlParameter("@overheadid",overheadid)
                                          
                                           ,new SqlParameter("@modifiedby",modifiedby),
                                       new SqlParameter("@modified",modified)};


            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateOverheadinfo", parameter);
        }
        public int UpdateUserList(Int64 Userid, string emailid, string L_Name, DateTime Dob, string address, int country, Int64 state, Int64 district, Int64 city, int roleid, int zoneid, Int64 subzoneid, int Status, string parent, string child, string mobile,Int64 ModifiedBy)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", Userid),new SqlParameter("@emailid", emailid),new SqlParameter("@L_Name", L_Name),
                                           new SqlParameter("@Status",Status), new SqlParameter("@DOB",Dob),
                                            new SqlParameter("@Address",address)
                                           , new SqlParameter("@Country",country)
                                           , new SqlParameter("@State",state)
                                           , new SqlParameter("@District",district)
                                           , new SqlParameter("@City",city)
                                           ,new SqlParameter("@roleID", roleid),
                                       new SqlParameter("@zoneID", zoneid),new SqlParameter("@subZoneid", subzoneid),new SqlParameter("@parent", parent)
                                       ,new SqlParameter("@child", child),new SqlParameter ("@mobile",mobile),
                                      
                                       new SqlParameter ("@ModifiedBy",ModifiedBy)};
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateUserlist", parameter);
        }
      
        public int updateUserslist(Int64 Userid, string firstname, string lastname, string address, string emalid, char gender, DateTime dob,
            string Mob_no, Int32 country, Int32 state, Int32 distict, Int32 city, int status, Int64 zone, Int64 subzone)
        {
            SqlParameter[] parameter = {new SqlParameter("@UserID", Userid),new SqlParameter("@FirstName",firstname),
                                           new SqlParameter("@LastName",lastname),new SqlParameter("@CurrentAddress",address),
                                           new SqlParameter("@EmailID", emalid),new SqlParameter("@Gender",gender),
                                        new SqlParameter("@DOB", dob),new SqlParameter("@MobileNumber",Mob_no),
                                         new SqlParameter("@CurrentCountry", country),new SqlParameter("@CurrentState",state),
                                       new SqlParameter("@CurrentDistrict", distict),new SqlParameter("@CurrentCity",city)
                                       ,new SqlParameter("@Status",status),new SqlParameter("@zone",zone)
                                       ,new SqlParameter("@subzone",subzone)};
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateClientlist", parameter);
        }
        public DataTable GetCountStateList(int currentpage)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetcountryState", parameter);
            return dt;
        }
        public DataTable GetshowengineerList(int currentpage)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetshowEngineer", parameter);
            return dt;
        }
        public int InsertCountry(string CountryName, Int64 CreatedBy, Int64 ModifiedBy)
        {
            SqlParameter[] parameter = { new SqlParameter("@CountryName", CountryName),
                                        new SqlParameter("@CreatedBy", CreatedBy),
                                        new SqlParameter("@ModifiedBy", ModifiedBy)};
            return objHelper.ExcuteNonQuery(connectionString, "uspAddCountry", parameter);
        }

        public int CheckUserPages(Int64 userID, string pages)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID",userID),
                                           new SqlParameter("@pagename", pages) };
            return objHelper.ExcuteNonQuery(connectionString, "uspCheckUserPages", parameter);
        }
        public int InsertZone(string zoneName,Int64 userID,string states)
        {
            SqlParameter[] parameter = { new SqlParameter("@ZoneName", zoneName), new SqlParameter("@UserID",userID),
                                           new SqlParameter("@States", states) };
            return objHelper.ExcuteNonQuery(connectionString, "uspAddZones", parameter);
        }
        public int InsertZoneSubZone(Int64 zoneID, Int64 subZoneID, Int64 userID, int zStatus, int sStatus, Int64 ModifyBy)
        {
            SqlParameter[] parameter = { new SqlParameter("@ZoneID", zoneID), new SqlParameter("@SubZone",subZoneID),
                                           new SqlParameter("@CreatedBy", userID),new SqlParameter("@ZoneStatus", zStatus) ,
                                       new SqlParameter("@SubZoneStatus", sStatus), new SqlParameter("@ModifiedBy", ModifyBy) };
            return objHelper.ExcuteNonQuery(connectionString, "uspAddZoneSubZone", parameter);
        }
        public int UpdateZoneSubZonelist(Int64 zoneID, Int64 subZoneID, int zStatus, int sStatus)
        {
            SqlParameter[] parameter = { new SqlParameter("@ZoneID", zoneID), new SqlParameter("@SubZoneID",subZoneID),
                                       new SqlParameter("@ZoneStatus", zStatus), new SqlParameter("@SubZoneStatus", sStatus)};
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateZoneSubZone", parameter);
        }
        public int InsertSubZone(string zoneName, int zoneID,Int64 userID, string cities)
        {
            SqlParameter[] parameter = { new SqlParameter("@SubZone", zoneName),new SqlParameter("@ZoneID", zoneID), new SqlParameter("@UserID",userID),
                                           new SqlParameter("@Cities", cities) };
            return objHelper.ExcuteNonQuery(connectionString, "uspAddSubZones", parameter);
        }
        public int AddServices(string servicename, string serviceDesc, string fromTime, string ToTime, string category, string type, Int64 userID, int status)
        {
            SqlParameter[] parameter = { new SqlParameter("@Name", servicename),new SqlParameter("@Desc",serviceDesc), new SqlParameter("@UserID",userID),
                                           new SqlParameter("@Fromtime", fromTime),new SqlParameter("@ToTime", ToTime),new SqlParameter("@Category", category),
                                       new SqlParameter("@Type", type),new SqlParameter("@status", status)};
            return objHelper.ExcuteNonQuery(connectionString, "uspAddService", parameter);
        }
        public int UpdateServices(int serviceID, Int64 session, string serviceDesc, string fromTime, string ToTime, string category, string type, int status)
        {
            SqlParameter[] parameter = { new SqlParameter("@ServiceID", serviceID),new SqlParameter("@session", session),new SqlParameter("@Desc",serviceDesc),
                                           new SqlParameter("@Fromtime", fromTime),new SqlParameter("@ToTime", ToTime),new SqlParameter("@Category", category),
                                       new SqlParameter("@Type", type),new SqlParameter("@status", status)};
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateService", parameter);
        }
        public int AddServicePlan(int serviceID, string plan, string plansForunlimited, int calls, int duration, decimal amount, int visitRequired, Int64 session, decimal discount)
        {
            SqlParameter[] parameter = { new SqlParameter("@ServiceID", serviceID),new SqlParameter("@PlanForUnlimited",plansForunlimited),new SqlParameter("@Plan",plan), new SqlParameter("@Calls",calls),
                                           new SqlParameter("@Duration", duration),new SqlParameter("@Amount", amount),new SqlParameter("@VisitRequired ", visitRequired),new SqlParameter("@session", session),new SqlParameter("@FDiscount", discount)};
            return objHelper.ExcuteNonQuery(connectionString, "uspAddServicePlan", parameter);
        }
        public int UpdateServicePlan(int serviceID, int planID, int calls, int duration, decimal amount, Int32 visitRequired, Int64 session, decimal discount)
        {
            SqlParameter[] parameter = { new SqlParameter("@ServiceID", serviceID),new SqlParameter("@PlanID",planID), new SqlParameter("@Calls",calls),
                                           new SqlParameter("@Duration", duration),new SqlParameter("@Amount", amount),
                                       new SqlParameter("@VisitRequired",visitRequired),new SqlParameter("@session",session),
                                       new SqlParameter("@discount",discount)};
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateServicePlan", parameter);

        }
        public int AddServiceDiscount(string servicePlan, int isfixed, int fromcall, int tocall, int status, decimal discount, Int64 session)
        {
            SqlParameter[] parameter = { new SqlParameter("@ServicePlan", servicePlan),new SqlParameter("@IsFixed",isfixed),
                                           new SqlParameter("@Fromcall",fromcall),new SqlParameter("@toCall",tocall),
                                           new SqlParameter("@Discount", discount),new SqlParameter("@Status",status),new SqlParameter("@session",session)};
            return objHelper.ExcuteNonQuery(connectionString, "uspAddServiceDiscount", parameter);
        }
        public int UpdateServiceDiscount(int DiscountID, int Isfixed, int fromcall, int tocall, int status, decimal discount, Int64 session)
        {
            SqlParameter[] parameter = {new SqlParameter("@DiscountID", DiscountID),new SqlParameter("@Isfixed",Isfixed),
                                           new SqlParameter("@Fromcall",fromcall),new SqlParameter("@toCall",tocall),
                                           new SqlParameter("@Discount", discount),new SqlParameter("@Status",status),new SqlParameter("@session",session)};
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateServiceDiscount", parameter);
        }

        public int AddServiceHorizontalDiscount(int quantfrom, int quantto, int status, decimal discount, Int64 session)
        {
            SqlParameter[] parameter = { new SqlParameter("@QuantFrom", quantfrom), new SqlParameter("@QuantTo",quantto),
                                           new SqlParameter("@Discount", discount),new SqlParameter("@Status",status),new SqlParameter("@session",session)};
            return objHelper.ExcuteNonQuery(connectionString, "uspAddServiceHoriZontalDiscount", parameter);
        }
        public int UpdateSkill(Int64 skillid,int Pskill, Int64 engineerID, string skilltype, Int32 rate, Int64 ModifiedBy)
        {
            SqlParameter[] parameter = { new SqlParameter("@SkillID", skillid),new SqlParameter("@Skills", Pskill),new SqlParameter("@EngineerID", engineerID), new SqlParameter("@SkillType",skilltype),
                                       new SqlParameter("@Rate", rate),new SqlParameter("@ModifiedBy", ModifiedBy)};
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateSkill", parameter);
        }
        public DataTable GetAllEngineerskills(int currentpage, string userid, string clientName)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage),  new SqlParameter("@client_name", clientName)
                                       ,new SqlParameter("@userid", userid)};
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServiceEngineersWithSkilss", parameter);
        }
        public int UpdateServiceHorizontalDiscount(int DisID, int quantfrom, int quantto, int status, decimal discount, Int64 session)
        {
            SqlParameter[] parameter = { new SqlParameter("@DisID",DisID),new SqlParameter("@QuantFrom", quantfrom), new SqlParameter("@QuantTo",quantto),
                                           new SqlParameter("@Discount", discount),new SqlParameter("@Status",status),new SqlParameter("@session",session)};
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateServiceHoriZontalDiscount", parameter);
        }
        public int UpdateUserAlternateno(string altrno1, string altrno2, string altrno3, string altrno4,Int64 userid)
        {
            SqlParameter[] parameter = {  new SqlParameter ("@alternateno1",altrno1),
                                        new SqlParameter ("@alternateno2",altrno2)
                                         , new SqlParameter ("@alternateno3",altrno3)
                                          , new SqlParameter ("@alternateno4",altrno4)
                                        , new SqlParameter ("@UserID",userid)
                                       };
            return objHelper.ExcuteNonQuery(connectionString, "uspaddUserAlternateno", parameter);
        }
        public int UpdateUserInfo(string mobileNumber, string address, int country, Int64 state, Int64 district, Int64 city, DateTime dob, string gender, Int64 userID, Int64 modifiedBy)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", userID), new SqlParameter("@MobileNumber",mobileNumber),
                                           new SqlParameter("@Address", address),new SqlParameter("@Country",country),new SqlParameter("@Dob",dob),
                                           new SqlParameter ("@District",district),
                                       new SqlParameter("@City",city),new SqlParameter("@Gender",gender),new SqlParameter("@State",state),
                                       new SqlParameter ("@modifiedBy",modifiedBy)
                                       ////, new SqlParameter ("@alternateno1",alternateno1)
                                       //// , new SqlParameter ("@alternateno2",alternateno2)
                                       ////  , new SqlParameter ("@alternateno3",alternateno3)
                                       ////   , new SqlParameter ("@alternateno4",alternateno4)
                                       };
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateUserInfo", parameter);
       
        }
        public int AddState(int countryID, string stateName, Int64 CreatedBy, Int64 ModifiedBy)
        {
            SqlParameter[] parameter = { new SqlParameter("@CountryID", countryID),
                                           new SqlParameter("@StateName", stateName),
                                            new SqlParameter("@CreatedBy", CreatedBy),
                                             new SqlParameter("@ModifiedBy", ModifiedBy)
                                             };

            return objHelper.ExcuteNonQuery(connectionString, "uspAddState", parameter);
        }
        public int AddDistrict(Int64 StateID, string District, Int64 CreatedBy, Int64 ModifiedBy)
        {
            SqlParameter[] parameter = { new SqlParameter("@StateID", StateID),
                                           new SqlParameter("@District", District),
                                           new SqlParameter("@CreatedBy", CreatedBy),
                                           new SqlParameter("@ModifiedBy", ModifiedBy)
                                             };

            return objHelper.ExcuteNonQuery(connectionString, "uspAddDistrict", parameter);
        }
        public int AddCty(Int64 DistrictID, string CityName, string CityCode, Int64 CreatedBy, Int64 ModifiedBy)
        {
            SqlParameter[] parameter = { new SqlParameter("@DistrictID", DistrictID),
                                           new SqlParameter("@CityName", CityName),
                                           new SqlParameter("@CityCode", CityCode),
                                           new SqlParameter("@CreatedBy", CreatedBy),
                                           new SqlParameter("@ModifiedBy", ModifiedBy)
                                             };

            return objHelper.ExcuteNonQuery(connectionString, "uspAddCity", parameter);
        }
        public int DeleteZone(Int64 zoneID, Int64 subZoneID)
        {
            SqlParameter[] parameter = { new SqlParameter("@ZoneID", zoneID),
                                           new SqlParameter("@SubZoneID", subZoneID),
                                             };

            return objHelper.ExcuteNonQuery(connectionString, "uspDeleteZone", parameter);
        }
        public int Updatesubscriberinfo(DateTime DOB, string AlternateMobileNumber, string AlternateEmailID, string HearFromWhere, string NearMilestone, int SmsEnabled, string PermanentAddress, string ServiceType, string ServicePlan, string CurrentAddress, int CurrentCountry, int CurrentState, int CurrentDistrict, int CurrentCity, int PermanentCountry, int PermanentState, int PermanentDistrict, int PermanentCity, Int64 UserID, Int64 modifiedBy)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID),
                                           new SqlParameter("@DOB",DOB),
                                           new SqlParameter("@AlternateMobileNumber",AlternateMobileNumber),
                                           new SqlParameter("@AlternateEmailID", AlternateEmailID),new SqlParameter("@HearFromWhere",HearFromWhere),
                                           new SqlParameter("@NearMileStone",NearMilestone),
                                           new SqlParameter("@SmsEnabled",SmsEnabled),
                                           new SqlParameter ("@PermanentAddress",PermanentAddress),
                                       new SqlParameter("@ServiceType",ServiceType),new SqlParameter("@ServicePlan",ServicePlan),new SqlParameter("@CurrentAddress",CurrentAddress),
                                       new SqlParameter("@CurrentCountry",CurrentCountry),
                                       new SqlParameter("@CurrentState",CurrentState),
                                       new SqlParameter("@CurrentDistrict",CurrentDistrict),
                                       new SqlParameter("@CurrentCity",CurrentCity),
                                       new SqlParameter ("@PermanentCountry",PermanentCountry), 
                                       new SqlParameter ("@PermanentState",PermanentState), 
                                       new SqlParameter ("@PermanentDistrict",PermanentDistrict), 
                                       new SqlParameter ("@PermanentCity",PermanentCity),
                                       new SqlParameter ("@ModifiedBy",modifiedBy)};
            return objHelper.ExcuteNonQuery(connectionString, "Updatesubscriberinfo", parameter);

        }
        public int ChangeUserPassword(Int64 UserID, string oldPassword, string newPassword)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID),
                                           new SqlParameter("@Oldpassword", oldPassword),
                                           new SqlParameter("@Password",newPassword)
                                             };

            return objHelper.ExcuteNonQuery(connectionString, "uspUpdatePassword", parameter);
        }
        public int CheckEmailiExistence(string emailid)
        {
            SqlParameter[] parameter = { new SqlParameter("@EmailID", emailid) };
            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, "uspCheckEmailID", parameter));
            return result;
        }
        public int ForgotPassword(string emailid, string guid)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", emailid), new SqlParameter("@GuiD", guid) };
            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, "uspForgotPassword", parameter));
            return result;
        }
        public int SetUpNewPassword(string emailid, string password)
        {
            SqlParameter[] parameter = { new SqlParameter("@EmailID", emailid), new SqlParameter("@NewPassword", password) };
            return objHelper.ExcuteNonQuery(connectionString, "uspSetNewPassword", parameter); 
        }
        public int DeleteService(int serviceID, int servicePlanID)
        {
            SqlParameter[] parameter = { new SqlParameter("@ServiceID", serviceID), new SqlParameter("@ServicePlanID", servicePlanID) };
            return objHelper.ExcuteNonQuery(connectionString, "uspDeleteService", parameter);
        }
        public int DeleteServicePlan(int serviceID, int servicePlanID)
        {
            SqlParameter[] parameter = { new SqlParameter("@ServiceID", serviceID), new SqlParameter("@ServicePlanID", servicePlanID) };
            return objHelper.ExcuteNonQuery(connectionString, "uspDeleteServicePlan", parameter);
        }
        public int DeleteHoriZontalDIscount(int discountID)
        {
            SqlParameter[] parameter = { new SqlParameter("@DisID", discountID) };
            return objHelper.ExcuteNonQuery(connectionString, "uspDeletehorizontalDiscount", parameter);
       
        }
        public int DeleteDiscount(int servicePlanID,int discountID)
        {
            SqlParameter[] parameter = { new SqlParameter("@ServicePlanID", servicePlanID),new SqlParameter("@DiscountID",discountID) };
            return objHelper.ExcuteNonQuery(connectionString, "uspDeleteDiscount", parameter);
        }
         public DataTable GetUnlimitedServiceList(int CurrentPage, int ServiceplanID)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", CurrentPage), new SqlParameter("@ServiceplanID", ServiceplanID) };
             DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetUnlimitedServicesWithPlanDiscount", parameter);
             return dt;
        }

         public int UpdateUnlimitedServicePlan(int serviceID, int planID, string planUnlimited, Int64 session)
         {
             SqlParameter[] parameter = { new SqlParameter("@ServiceID", serviceID),new SqlParameter("@PlanID",planID),new SqlParameter("@PlanForUnlimited",planUnlimited)
                                           ,new SqlParameter("@session",session)};
             return objHelper.ExcuteNonQuery(connectionString, "uspUpdateUnlimitedServicePlan", parameter);
         }
        public int UpdateUserStatus(Int64 UserID, int Status)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID), new SqlParameter("@Status", Status) };
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateUserStatus", parameter);
        }
        public int CheckUserStatus(Int64 UserID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID)};
            return objHelper.ExcuteNonQuery(connectionString, "uspCheckUserLoginStatus", parameter);
        }
        public int DeleteUser(Int64 UserID, Int64 DeletedBY)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID), 
                                            new SqlParameter("@DeletedBY", DeletedBY) };
            return objHelper.ExcuteNonQuery(connectionString, "uspDeleteUserlist", parameter);
        }
        public int DeleteOverhead(Int64 OverheadId)
        {
            SqlParameter[] parameter = { new SqlParameter("@OverheadId", OverheadId) 
                                            };
            return objHelper.ExcuteNonQuery(connectionString, "uspDeleteOverhead", parameter);
        }

        public int AddQuotation(Int64 RequestID, Int64 createdBy, decimal price, string title, string consumes, string paymentmode, string activationTime)
        {
            SqlParameter[] parameter = { new SqlParameter("@RequestID",RequestID),new SqlParameter("@CreatedBy", createdBy),new SqlParameter("@Price", price),
                                           new SqlParameter("@QuotationTitle",title)
                                           ,new SqlParameter("@Consumes", consumes),
                                       new SqlParameter("@PaymentMode", paymentmode),new SqlParameter("@ActivationTime ", activationTime)};
            return objHelper.ExcuteNonQuery(connectionString, "uspAddQuotation", parameter);
        }

        #endregion


        #region SelectMethods
        public DataTable GetCountry()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetCountry");
        }
        public DataTable GetRoles()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetRoles");
        }
        public DataTable GetStates()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetStates");
        }
        public DataTable ChekcPhoneno(string phono)
        {
            SqlParameter[] parameter = { new SqlParameter("@alternateno", phono)};
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspCheckphoneno",parameter);
        }
        public DataTable ChekcPhonenoexistsinmobileno(string phono)
        {
            SqlParameter[] parameter = { new SqlParameter("@alternatenos", phono) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspCheckPrimaryNoexists", parameter);
        }
        public DataTable ChekcAnyPhonenoexistsinmobileno(string phono)
        {
            SqlParameter[] parameter = { new SqlParameter("@phoneno", phono) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspCheckPhonenoexists", parameter);
        }
        public DataTable GetAllLeave()
        {

            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspgetserviceengineerleave");
        }
        public DataTable GetDistricts()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetDistrict");
        }
        public DataTable GetCities()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetCities");
        }
        public DataTable GetStatesByID(int countryID)
        {
            SqlParameter[] parameter = { new SqlParameter("@CountryID",countryID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetStatesyCountryID", parameter);
        }
        public DataTable GetDistrictsByID(Int64 stateID)
        {
            SqlParameter[] parameter = { new SqlParameter("@StateID", stateID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetDistrictsByStateID", parameter);
        }
         public DataTable GetCitiesByID(Int64 districtID)
        {
            SqlParameter[] parameter = { new SqlParameter("@DistrictID", districtID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetCitiesByDistrictID", parameter);
        }
         public DataTable GetCitiesByDistrictID(int currentpage, Int64 districtID)
         {
             SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), new SqlParameter("@DistrictID", districtID) };
             return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetCitiesViaDistrictID", parameter);
         }
         public DataTable GetDistrictByID(Int64 stateID)
         {
             SqlParameter[] parameter = { new SqlParameter("@ZoneID", stateID) };
             return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetSubZonesDistrictByID", parameter);
         }
        
         public DataTable GetDistrictByZoneID(Int64 zoneid)
         {
             SqlParameter[] parameter = { new SqlParameter("@zoneid  ", zoneid) };
             return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetZonesByID", parameter);
         }
         public DataTable GetsubzoneBysubZoneID(Int64 subzoneid)
         {
             SqlParameter[] parameter = { new SqlParameter("@subzoneid  ", subzoneid) };
             return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetSubZonesDistrictBybySubzoneid", parameter);
         }
         public DataTable GetUseralternateno(Int64 userid)
         {
             SqlParameter[] parameter = { new SqlParameter("@userid", userid) };
             return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspgetalternateno", parameter);
         }
         public DataTable GetUserList(int pagesize, int currentPage, string UserName)
         {
             SqlParameter[] parameter = { new SqlParameter("@RowsPerPage", pagesize), new SqlParameter("@PageNumber", currentPage), new SqlParameter("@client_name", UserName) };
             DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetAllUsers", parameter);
             return dt;
         }
         public DataTable GetUserListReport(int pagesize, int currentPage, string UserName)
         {
             SqlParameter[] parameter = { new SqlParameter("@RowsPerPage", pagesize), new SqlParameter("@PageNumber", currentPage), new SqlParameter("@client_name", UserName) };
             DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetAllUsersReport", parameter);
             return dt;
         }
        public DataTable GetUserListReportFilterDate(int pagesize, int currentPage, string UserName,DateTime fromdate,DateTime todate)
         {
             SqlParameter[] parameter = { new SqlParameter("@RowsPerPage", pagesize), new SqlParameter("@PageNumber", currentPage), new SqlParameter("@client_name", UserName), new SqlParameter("@todate", todate), new SqlParameter("@fromdate", fromdate) };
             DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetAllUsersReportFilerdate", parameter);
             return dt;
         }
         

         public DataTable GetUserQuotation()
         {
             //SqlParameter[] parameter = { new SqlParameter("@RowsPerPage", pagesize), new SqlParameter("@PageNumber", currentPage), new SqlParameter("@client_name", UserName) };
             DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetUserQuotation");
             return dt;
         }
         public DataSet GetRequestServices(Int64 RequestID)
         {
             SqlParameter[] parameter = { new SqlParameter("@requestID", RequestID) };
             return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, "uspGetUserQuotationByRequestID", parameter);
         }
         public DataTable GetRequestidByName(string name)
         {
             SqlParameter[] parameter = { new SqlParameter("@servicename", name) };
             return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServiceIdByName", parameter);
         }
        public DataTable GetZones()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetZones");
        }
        public DataTable GetSubZones()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetSubZones");
        }
        public DataTable GetZoneSubZoneList(int currentpage, string Zonename)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), new SqlParameter("@client_name", Zonename) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetSubZonesDistrict", parameter);
        }
        public string GetUserIDByEmailID(string emailid)
        {
            string name = "";
            SqlParameter[] parameter = { new SqlParameter("@emailID", emailid) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetUserIDByEmailID", parameter);
            if (dt.Rows.Count > 0)
            {
                name = dt.Rows[0]["UserID"] + ":" + dt.Rows[0]["Name"];
               
            }
            return name;
        }
        public string GetUserIDByEmailIDFomTemp(string emailid)
        {
            string name = "";
            SqlParameter[] parameter = { new SqlParameter("@emailID", emailid) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetUserIDByEmailIDTemp", parameter);
            if (dt.Rows.Count > 0)
            {
                name = dt.Rows[0]["TempID"] + ":" + dt.Rows[0]["Name"];

            }
            return name;
        }
        public DataTable GetServices()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServices");
            return dt;
        }
        public DataTable GetZoneList()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetZoneList");
            return dt;
        }
        
        public DataTable GetStateList(int zoneID)
        {
            SqlParameter[] parameter = { new SqlParameter("@ZoneID", zoneID) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetStateList",parameter);
            return dt;
        }
        public DataTable GetSubZoneList()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetSubZoneList");
            return dt;
        }


        public DataTable GetServiceList(int CurrentPage, string UserName)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", CurrentPage), new SqlParameter("@client_name", UserName) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServicesWithPlanDiscount", parameter);
            return dt;
        }
        public DataTable GetServiceListDiscount()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServicesWithVerticalDiscount");
            return dt;
        }
        public int GetGeneralInfo(Int64 UserID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID) };
            return objHelper.ExcuteNonQuery(connectionString,"uspIsGeneralInfoFilled",parameter);
           
        }
        public int GetBalanceEnquiry(Int64 UserID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID) };
            return objHelper.ExcuteNonQuery(connectionString, "uspBalanceEnquiry", parameter);
        }
        public DataSet GetServicesForuser(string category, string plan)
        {
            SqlParameter[] parameter = { new SqlParameter("@Servicecategory", category), new SqlParameter("@ServicePlan", plan)};
            return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, "uspGetServicesForUsers", parameter);
        }
        public DataTable GetServicesForRequest(string category, string plan, string type)
        {
            SqlParameter[] parameter = { new SqlParameter("@Servicecategory", category), new SqlParameter("@ServicePlan", plan), new SqlParameter("@ServiceType", type) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServicesForRequest", parameter);
            return dt;
        }
        public DataTable GetServicesAfterDiscount(Int64 userid)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", userid)};
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServicesAfterDiscount", parameter);
        }
        public DataTable GetEditServicesAfterDiscount(Int64 userid)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", userid) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspEditServicesAfterDiscount", parameter);
        }
        public DataTable GetHorizontalDiscount()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGethorizontalDiscount");
        }

        public DataTable GetCommercialRequest(int currentpage)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetCommercialRequest", parameter);
        }
        public DataTable GetUserByID(Int64 UserID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetUserInfoByID", parameter);
        }
        public DataTable GetUserByEmailid(string email)
        {
            SqlParameter[] parameter = { new SqlParameter("@emailid", email) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetUserInfobyEmail", parameter);
        }
        public DataTable GetCustomerByID(Int64 UserID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetCustomerInfo", parameter);
        }
        public DataTable GetAllTickets()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetAllTicketStatus");
        }
        public DataTable GetAllEngineerskills(int currentpage)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServiceEngineersWithSkilss", parameter);
        }

        public DataTable GetAllTickets(int currentpage, string UserName)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), new SqlParameter("@client_name", UserName) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetAllTicketData", parameter);
        }
        public DataTable GetAdmin()
        {

            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetAdmin");
        }
        public DataTable GetAllTicketsReport(int currentpage, string UserName)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), new SqlParameter("@client_name", UserName) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetAllTicketDataReport", parameter);
        }
        public DataTable GetAllTicketsReportFilterbyDate(int currentpage, string UserName,DateTime dtfrom,DateTime dtto)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), new SqlParameter("@client_name", UserName), new SqlParameter("@fromdate", dtfrom), new SqlParameter("@todate", dtto) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetAllTicketDataReportFilerbyDate", parameter);
        }
        public DataTable GetAllTicketsEngineerReport(int currentpage, string UserName)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), new SqlParameter("@client_name", UserName) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServiceEngineerDataReport", parameter);
        }
        public DataTable GetAllTicketsEngineerReportFilterbyDate(int currentpage, string UserName, DateTime dtfrom, DateTime dtto)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), new SqlParameter("@client_name", UserName), new SqlParameter("@fromdate", dtfrom), new SqlParameter("@todate", dtto) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServiceEngineerDataReportFilterbydate", parameter);
        }
        public DataTable GetAllAppointments(int currentpage, string clientName)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), new SqlParameter("@client_name", clientName) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetEngineerAppointmentStatus", parameter);
        }
        public DataTable GetAllTicketDetails()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetAllTicketDetails");
        }
        public DataTable GetClientDataList(int currentpage, string userid)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), new SqlParameter("@userid", userid) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspShowClientCreatedByCustomerCare", parameter);
            return dt;
        }
        public DataTable GetClientDetails(Int64 UserID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetClientServiceHistory", parameter);
        }
        public DataTable GetClientProfile(Int64 UserID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetProfile", parameter);
        }
        public DataTable GetClientBill(Int64 UserID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetClientBillInfo", parameter);
        }
        public DataTable GetAllClient()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetAllClient");
        }
        public DataTable GetOverheadData()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetOverheadinfo");
        }
        public DataTable GetDistrictViaID(int currentpage, Int64 stateID)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), new SqlParameter("@ZoneID", stateID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetSubZonesDistrictViaID", parameter);
        }
        public DataTable GetAllFeedbackList(int currentpage)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetShowFeedbackByAdmin", parameter);
        }
        public DataTable GetClientDataList(int currentpage, string userid, string clientName)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), new SqlParameter("@userid", userid), new SqlParameter("@client_name", clientName) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspShowClientCreatedByCustomerCare", parameter);
            return dt;
        }
        public int DeleteParentPage(int ParentNodeID)
        {
            SqlParameter[] parameter = { new SqlParameter("@ParentNodeID", ParentNodeID) };
            return objHelper.ExcuteNonQuery(connectionString, "uspDeleteParentPage", parameter);

        }
        public int DeleteChildPage(int tblChildID)
        {
            SqlParameter[] parameter = { new SqlParameter("@tblChildID", tblChildID) };
            return objHelper.ExcuteNonQuery(connectionString, "uspDeleteChildPage", parameter);

        }
        public int UpdateParentPage(int ParentNodeID, string ParentName, int RoleID)
        {
            SqlParameter[] parameter = { new SqlParameter("@ParentNodeID", ParentNodeID), new SqlParameter("@ParentName", ParentName), new SqlParameter("@RoleID", RoleID) };
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateParentPage", parameter);
        }
        public int UpdateChildPage(int tblChildID, int ParentNodeID, string PageName, string PageTitle, string PageDescription, string LinkedPages)
        {
            SqlParameter[] parameter = { new SqlParameter("@tblChildID", tblChildID), new SqlParameter("@ParentNodeID", ParentNodeID),
                                           new SqlParameter("@PageName", PageName),
                                           new SqlParameter("@PageTitle", PageTitle),
                                           new SqlParameter("@PageDescription", PageDescription)
                                           ,new SqlParameter("@LinkedPages", LinkedPages)
                                       };
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateChildPage", parameter);
        }
        public int AddPage(int ParentID, string PageName, string PageTitle, string PageDescription, string LinkedPages)
        {
            SqlParameter[] parameter = { new SqlParameter("@ParentNodeID", ParentID),new SqlParameter("@PageName", PageName),new SqlParameter("@PageTitle", PageTitle),
                                           new SqlParameter("@PageDescription", PageDescription),
                                            new SqlParameter("@LinkedPages", LinkedPages)
                                             };

            return objHelper.ExcuteNonQuery(connectionString, "uspAddPageRole", parameter);
        }

        public int AddParentPage(string ParentName, int RoleID)
        {
            SqlParameter[] parameter = { new SqlParameter("@ParentName", ParentName),new SqlParameter("@RoleID", RoleID)
                                             };

            return objHelper.ExcuteNonQuery(connectionString, "uspAddParentPageRole", parameter);
        }

        public DataTable GetParentNode(int currentpage)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetParentPage", parameter);
            return dt;
        }
        public DataTable GetChildNode(int currentpage)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetChildParentPage", parameter);
            return dt;
        }
        public DataTable GetRolesByUserID(Int64 UserID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetUserRolesbyUserID", parameter);
            return dt;
        }
        public DataTable GetPageByRoleID(int ParentNodeID)
        {
            SqlParameter[] parameter = { new SqlParameter("@ParentNodeID", ParentNodeID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetSPageByRoleID", parameter);
        }
        public DataTable GetChildPagesByNodeID(Int64 ParentNodeID)
        {
            SqlParameter[] parameter = { new SqlParameter("@NodeID", ParentNodeID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetChildPages", parameter);
        }
        public DataTable GetPageRole()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "UspGetPageRole");
        }

        public DataTable GetRoleWisePagesByUserID(Int64 UserID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetRoleWisePagesByUserID", parameter);
        }
        public int UpdateAllUserPassword(string Userid, string Password)
        {
            SqlParameter[] parameter = { new SqlParameter("@EmailID", Userid), new SqlParameter("@Password", Password) };
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateUserPassword", parameter);
        }
        public int UpdateLeaveStatus(int leaveid,int status)
        {
            SqlParameter[] parameter = { new SqlParameter("@leaveid", leaveid), new SqlParameter("@status", status) };
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateLeaveStatus", parameter);
        }
        public DataTable GetAllUserPassword(int pagesize, int currentPage, string UserName)
        {
            SqlParameter[] parameter = { new SqlParameter("@RowsPerPage", pagesize), new SqlParameter("@PageNumber", currentPage), new SqlParameter("@client_name", UserName) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetAllUsersPassword", parameter);
            return dt;
        }
        public int AddNotSendSmsMail(Int64 Userid, int sent,string purpose,int media)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", Userid), new SqlParameter("@Sent", sent) ,
                                       new SqlParameter("@Purpose", purpose) ,new SqlParameter("@Media", media) };
            return objHelper.ExcuteNonQuery(connectionString, "uspAddNotSendSmsMail", parameter);
        }
        public DataTable GetSubscriberInfo(Int64 UserID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetSubscriberById", parameter);
        }
        public string GetLoginuserName(Int64 UserID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", UserID) };
            return Convert.ToString( SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, "uspGetUserNameByID", parameter));        
        }
        public DataTable BindAllEngineerList(Int64 RequestID)
        {
            SqlParameter[] parameter = { new SqlParameter("@RequestID", RequestID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "UspGetAllEngineersListReq", parameter);
        }
        public int AssignRequestToEnginner(Int64 AssignedTo, Int64 ModifiedBy, Int64 RequestID, string Reason, int Status)
        {
            MethodHelper objHelper = new MethodHelper();
            SqlParameter[] parameter = { new SqlParameter("@AssignedTo", AssignedTo), new SqlParameter("@ModifiedBy", ModifiedBy),
                                       new SqlParameter("@RequestID", RequestID),new SqlParameter("@Reason", Reason)
                                        ,new SqlParameter("@Status", Status)};
            return objHelper.ExcuteNonQuery(connectionString, "UspGetEngineerStatusByRequestID", parameter);
        }
        public DataTable GetRequestHistoryByRequestID(Int64 RequestID)
        {
            SqlParameter[] parameter = { new SqlParameter("@RequestID", RequestID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetRequestHistoryByRequestId", parameter);
        }
        public DataTable GetRequestdata(int currentpage, Int64 userID)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage), 
                                        new SqlParameter("@UserID", userID)};
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetRequestdata", parameter);
        }

        public int AddServicesAssesment(int Serviceid, int planID,string PropertyName, string validation, DateTime created, Int64 userID)
        {
            SqlParameter[] parameter = {new SqlParameter("@ServiceID", Serviceid),new SqlParameter("@PropertyName",PropertyName), new SqlParameter("@Validation",validation),
                                          new SqlParameter("@Created", created),new SqlParameter("@CreatedBy", userID)
                                       ,new SqlParameter("@PlanID", planID)};
            return objHelper.ExcuteNonQuery(connectionString, "uspAddServiceAssesment", parameter);
        }
        public int UpdateServicesAssesment(int ProprtyID, int Serviceid,string PropertyName, string validation)
        {
            SqlParameter[] parameter = { new SqlParameter("@PropertyID", ProprtyID),new SqlParameter("@ServiceID", Serviceid),
                                           new SqlParameter("@PropertyName", PropertyName),
                                           new SqlParameter("@Validation",validation)
                                         };
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateServiceAssesment", parameter);
        }



        public DataTable GetBasicCommercialoServiceList(int CurrentPage)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", CurrentPage) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetCommercialBasicServices", parameter);
            return dt;
        }

        public DataTable GetBasicServices()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetBasicServices");
            return dt;
        }

        public int DeleteServiceAssesment(int PropertyID)
        {
            SqlParameter[] parameter = { new SqlParameter("@PropertyID", PropertyID) };
            return objHelper.ExcuteNonQuery(connectionString, "uspDeleteServiceAssesment", parameter);
        }
        public DataTable GetWorkHistory(Int64 TicketID)
        {
            SqlParameter[] parameter = { new SqlParameter("@TicketID", TicketID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetWorkHistory", parameter);
        }
        public DataTable GetAllEngineerskill(int currentpage, string clientName)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage),  new SqlParameter("@client_name", clientName)
                                      };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetServiceEngineersSkill", parameter);
        }

        public DataTable GetClientList(int pagesize, int currentPage,string searchString)
        {
            SqlParameter[] parameter = { new SqlParameter("@RowsPerPage", pagesize), new SqlParameter("@PageNumber", currentPage), new SqlParameter("@SearchString", searchString) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetClientList", parameter);
        }

        public int AddUserPayment(Int64 InvoiceID, Int64 UserID, decimal newAmount, Int64 CreatedBy, DateTime Created, DateTime Modified, Int64 ModifiedBy)
        {
            SqlParameter[] parameter = { 
                                           new SqlParameter("@InvoiceID",InvoiceID), 
                                           new SqlParameter("@UserID", UserID)
                                       ,new SqlParameter("@newAmount", newAmount)                                      
                                       ,new SqlParameter("@CreatedBy", CreatedBy), 
                                       new SqlParameter("@Created", Created)
                                      ,new SqlParameter("@Modified", Modified), 
                                       new SqlParameter("@ModifiedBy", ModifiedBy)
                                       };

            return objHelper.ExcuteNonQuery(connectionString, "UspAddUserPayment", parameter);
        }
        public DataTable GetMessage(int currentpage)
        {
            SqlParameter[] parameter = { new SqlParameter("@PageNumber", currentpage) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetMessages", parameter);
            return dt;
        }
        public int DeleteMessage(int RoleId, Int64 MessageId)
        {
            SqlParameter[] parameter = { new SqlParameter("@RoleId", RoleId),
                                           new SqlParameter("@MessageId", MessageId),
                                             };

            return objHelper.ExcuteNonQuery(connectionString, "uspDeleteMessage", parameter);
        }

        public int UpdateMessage(Int16 RoleId, String Message, int status, int sendsms)
        {
            SqlParameter[] parameter = { new SqlParameter("@RoleId", RoleId), new SqlParameter("@Message",Message),
                                           new SqlParameter("@Status",status),
                                           new SqlParameter("@Sendsms",sendsms)
                                 };
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateMessage", parameter);
        }


        public int SaveMessage(Int64 CreatedBy, Int64 ModifyBy, int RoleId, String message, int Status, int SendSms)
        {
            SqlParameter[] parameter = { new SqlParameter("@RoleId", RoleId),new SqlParameter("@Message", message), new SqlParameter("@CreatedBy",CreatedBy),
                                           new SqlParameter("@ModifiedBy", ModifyBy) ,new SqlParameter("@Status", Status),new SqlParameter("@SendSms", SendSms)};
            return objHelper.ExcuteNonQuery(connectionString, "uspADDMessage", parameter);
        }

        public DataTable GetMessageCirculatD(Int32 Roleid)
        {
            SqlParameter[] parameter = { new SqlParameter("@RoleID", Roleid) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetMessageCirculate", parameter);
        }
        public DataTable GetCommercialRequestByID(Int64 requestID)
        {
            SqlParameter[] parameter = { new SqlParameter("@ReQuestID", requestID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetCommercialRequestByID", parameter);
        }

        public DataTable GetEngineerHistoryListByDate(int curntpage, DateTime date, Int64 EngineerID)
        {
            SqlParameter[] parameter = { new SqlParameter("@EngineerID", EngineerID), new SqlParameter("@date", date), new SqlParameter("@PageNumber", curntpage) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetShowEngineerHistory", parameter);
        }
        public DataTable GetAllEngineers()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetAllEngineers");
        }
        public DataTable GetUsersByRoleID(int roleid)
        {
            SqlParameter[] parameter = { new SqlParameter("@RoleID", roleid) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetUsersByRoleId",parameter);
        }
        public DataTable GetUserHistoryByID(Int64 clientID)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserId", clientID) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetClientHistoryByID", parameter);
        }
        public int DeleteTool(int Toolid)
        {
            SqlParameter[] parameter = { new SqlParameter("@ToolId", Toolid) };
            return objHelper.ExcuteNonQuery(connectionString, "uspDeleteTool", parameter);
        }

        public int UpdateTool(int ToolID, string ToolName, int tooltype, int quantity, int status, string description, DateTime created, Int64 userID)
        {
            SqlParameter[] parameter = { new SqlParameter("@ToolId", ToolID), new SqlParameter("@ToolName",ToolName), new SqlParameter("@ToolType",tooltype), new SqlParameter("@Quantity",quantity),
                                           new SqlParameter("@Status",status),
                                         
                                            new SqlParameter("@Description",description), new SqlParameter("@Created",created),new SqlParameter("@CreatedBy",userID),

                                 };
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateTool", parameter);
        }


        public int AddTool(string ToolName, int tooltype, int quantity, int status, string description, DateTime created, Int64 userID)
        {
            SqlParameter[] parameter = {new SqlParameter("@ToolName", ToolName),new SqlParameter("@ToolType",tooltype), new SqlParameter("@Quantity",quantity),new SqlParameter("@Status",status),new SqlParameter("@Description", description),
                                          new SqlParameter("@Created", created),new SqlParameter("@CreatedBy", userID)
                                       };
            return objHelper.ExcuteNonQuery(connectionString, "uspAddTools", parameter);
        }
        public DataTable GetTool(int pagesize, int currentPage)
        {

             SqlParameter[] parameter = { new SqlParameter("@RowsPerPage", pagesize), new SqlParameter("@PageNumber", currentPage) };
             return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetTools", parameter);
       
        }

        public int AddToolAssignment(int toolid, Int64 engid, Int64 ticketid, int status, Int64 Createdby, string remark)
        {
            SqlParameter[] parameter = { 
                                            new SqlParameter("@ToolId", toolid), 
                                            new SqlParameter("@AssignedTo",engid),
                                            new SqlParameter("@TicketID",ticketid),
                                            new SqlParameter("@Status", status),
                                            new SqlParameter("@Createdby", Createdby),
                                            new SqlParameter("@Remark", remark)
                                         };
            return objHelper.ExcuteNonQuery(connectionString, "uspAddToolAssignment", parameter);

        }

        public int DeleteToolAssignment(int Assignmentid)
        {
            SqlParameter[] parameter = { new SqlParameter("@AssignmentID", Assignmentid)
                                             };

            return objHelper.ExcuteNonQuery(connectionString, "uspDeleteToolAssignment", parameter);
        }

        public int UpdateToolAssignment(int assignmentid, int status)//, string remark)
        {
            SqlParameter[] parameter = { new SqlParameter("@AssignmentID", assignmentid),
                                           //new SqlParameter("@ToolTypeID", tooltype), new SqlParameter("@ToolId", toolid),
                                           //new SqlParameter("@AssignedTo", engid),
                                           //new SqlParameter("@TicketID", ticketid),
                                           new SqlParameter("@Status", status),
                                           //new SqlParameter("@Remark", remark)
                                       };
            return objHelper.ExcuteNonQuery(connectionString, "uspUpdateToolAssignment", parameter);
        }
        public DataTable GetTicketByEng(Int64 Engid)
        {
            SqlParameter[] parameter = { new SqlParameter("@AssignedTo", Engid) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "GetTicketByEngineerID", parameter);
        }


        public DataTable GetToolByToolType(int ToolType)
        {
            SqlParameter[] parameter = { new SqlParameter("@ToolType", ToolType) };
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetTool", parameter);
        }

        public DataTable GetToolAssignment(int pagesize, int currentPage)
        {
            SqlParameter[] parameter = { new SqlParameter("@RowsPerPage", pagesize), new SqlParameter("@PageNumber", currentPage) };
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetToolAssignmentList", parameter);
            return dt;
        }
        public DataTable GetAllToolType()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetToolType");
        }

        public DataTable GetGraceTime()
        {
            return SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "uspGetGraceTime");
        }

        public int AddGraceTime(Int64 AddedBy,int Graceminutes)
        {
            SqlParameter[] parameter = { new SqlParameter("@AddedBy", AddedBy),
                                           new SqlParameter("@Graceminutes", Graceminutes),
                                       };
            return objHelper.ExcuteNonQuery(connectionString, "uspAddGraceMinutes", parameter);
        }
        public int UpdateGraceTime(Int64 Byadded, int gracetime)
        {
            SqlParameter[] parameter = { new SqlParameter("@ModifiedBy", Byadded),
                                           new SqlParameter("@Graceminutes", gracetime)                                           
                                       };
            return objHelper.ExcuteNonQuery(connectionString, "uspEditGraceMinutes", parameter);
        }
        public int IsRequestDone(Int64 requestID)
        {
            SqlParameter[] parameter = { new SqlParameter("@requestID", requestID) };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, "uspIsRequestDone", parameter));

        }
        public int PaymentByAdmin(Int64 userid, Int64 createdBy, string paymentInfo, decimal amount)
        {
            SqlParameter[] parameter = { new SqlParameter("@UserID", userid), new SqlParameter("@CreatedBy",createdBy),
                                       new SqlParameter("@paymentInfo", paymentInfo),new SqlParameter("@amount", amount)};
            return objHelper.ExcuteNonQuery(connectionString, "uspUserPaymentFromAdmin", parameter);

        }
        #endregion
    }
}


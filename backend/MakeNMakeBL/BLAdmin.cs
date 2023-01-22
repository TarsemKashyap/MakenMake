using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeNMake.DL;
using System.Data;
using System.Web.UI.WebControls;
using MakeNMake.Utilities;

namespace MakeNMake.BL
{
    public class BLAdmin
    {
        Admin objAdmin = new Admin();
        #region Insert_Update_Delete-Methods
        public int AddUsers(string firstname, string lastname, string emalid, string password, DateTime Dob, string address, int country, Int64 state, Int64 district, Int64 city, int roleID, int ZoneID, int subZoneID, Int64 userID, int status, string parentRole, string childRole, string mobileNumber,string alternatemobileNumber1,string alternatemobileNumber2,string alternatemobileNumber3,string alternatemobileNumber4, Int64 usermodified)
        {
            return objAdmin.AddUser(firstname, lastname, emalid, password, Dob, address, country, state, district, city, roleID, ZoneID, subZoneID, userID, status, parentRole, childRole, mobileNumber, alternatemobileNumber1,alternatemobileNumber2,alternatemobileNumber3,alternatemobileNumber4, usermodified);
        }
        public int AddOverheadInfo(string property, string description,decimal value,Int64 createdby, DateTime created,Int64 modifiedby,DateTime modified)
        {
            return objAdmin.AddOverheadInfo(property,description,value,createdby,created,modifiedby,modified);
        }
        public int UpdateOverheadInfo(int overheadid,string property, string description, decimal value, Int64 modifiedby, DateTime modified)
        {
            return objAdmin.AddOverheadInfo(overheadid, property, description, value, modifiedby, modified);
        }
        public int UpdateUserlist(Int64 Userid, string emailid, string L_Name, DateTime Dob, string address, int country, Int64 state, Int64 district, Int64 city, int roleid, int zoneid, Int64 subzoneid, int Status, string parent, string child, string mobile,Int64 ModifiedBy)
        {
            return objAdmin.UpdateUserList(Userid, emailid, L_Name, Dob, address, country, state, district, city, roleid, zoneid, subzoneid, Status, parent, child, mobile,ModifiedBy);
        }
        public DataTable GetCountState(int currentpage)
        {
            DataTable dt = objAdmin.GetCountStateList(currentpage);
            return dt;
        }
        public DataTable GetDistrictViaStateID(int currentpage, Int64 StateID)
        {
            DataTable dt = objAdmin.GetDistrictViaID(currentpage, StateID);
            return dt;
        }
        public DataTable GetAllTicketDetails()
        {
            return objAdmin.GetAllTicketDetails();
        }
        public DataTable GetshowengineerList(int currentpage)
        {
            DataTable dt = objAdmin.GetshowengineerList(currentpage);
            return dt;
        }
        public int CheckPages( Int64 userID, string pagename)
        {
            return objAdmin.CheckUserPages(userID, pagename);
        }
        public int updateUserslist(Int64 Userid, string firstname, string lastname, string address, string emalid, char gender, DateTime dob, string Mob_no, Int32 country, Int32 state, Int32 distict, Int32 city,int status,Int64 zone,Int64 subzone)
        {
            return objAdmin.updateUserslist(Userid, firstname, lastname, address, emalid, gender, dob, Mob_no, country, state, distict, city,status,zone,subzone);
        }
        public int AddCountry(string CountryName, Int64 CreatedBy, Int64 ModifiedBy)
        {
            return objAdmin.InsertCountry(CountryName, CreatedBy, ModifiedBy);
        }
        public int AddZones(string zone,Int64 userID,string stateID)
        {
            return objAdmin.InsertZone(zone,userID,stateID);
        }
        public int InsertZoneSubZone(Int64 zoneID, Int64 subZoneID, Int64 userID, int zStatus, int sStatus, Int64 ModifyBy)
        {
            return objAdmin.InsertZoneSubZone(zoneID, subZoneID, userID, zStatus, sStatus, ModifyBy);
        }
        public int UpdateZoneSubZone(Int64 zoneID, Int64 subZoneID, int zStatus, int sStatus)
        {
            return objAdmin.UpdateZoneSubZonelist(zoneID, subZoneID, zStatus, sStatus);
        }
        public int AddSubZones(string subZone,int zoneID, Int64 userID, string cities)
        {
            return objAdmin.InsertSubZone(subZone, zoneID, userID, cities); 
        }
        public int AddServices(string servicename, string serviceDesc, string fromTime, string ToTime, string category, string type, Int64 userID, int status)
        {
            return objAdmin.AddServices(servicename, serviceDesc, fromTime, ToTime, category, type, userID, status);
        }
        public int UpdateServices(int serviceID, Int64 session, string serviceDesc, string fromTime, string ToTime, string category, string type, int status)
        {
            return objAdmin.UpdateServices(serviceID, session, serviceDesc, fromTime, ToTime, category, type, status);
        }

        public int AddServicePlan(int serviceID, string planType, string planForUnlimited, int calls, int duration, decimal amount, int visitRequired, Int64 session,decimal discount)
        {
            return objAdmin.AddServicePlan(serviceID, planType, planForUnlimited, calls, duration, amount, visitRequired, session,discount);
        }
        public int UpdateServicePlan(int serviceID, int planID, int calls, int duration, decimal amount, Int32 visitrquired, Int64 session,decimal discount)
        {
            return objAdmin.UpdateServicePlan(serviceID, planID, calls, duration, amount, visitrquired, session,discount);
        }
        public int DeleteDiscount(int serviceplanID,int discountID)
        {
            return objAdmin.DeleteDiscount(serviceplanID, discountID);
        }
        public DataTable GetUnlimitedServiceList(int CurrentPage, int ServiceplanID)
        {
            DataTable dt = objAdmin.GetUnlimitedServiceList(CurrentPage, ServiceplanID);
            return dt;
        }
        public DataTable GetAllEngineerskills(int currentpage, string userid, string clientName)
        {
            return objAdmin.GetAllEngineerskills(currentpage, userid, clientName);
        }
        public int UpdateSkill( Int64 skillid, int Pskill, Int64 engineerID, string skilltype, Int32 rate, Int64 ModifiedBy)
        {
            return objAdmin.UpdateSkill(skillid,Pskill, engineerID, skilltype, rate, ModifiedBy);
        }
        public int UpdateUnlimitedServicePlan(int serviceID, int planID, string planUnlimited, Int64 session)
        {
            return objAdmin.UpdateUnlimitedServicePlan(serviceID, planID, planUnlimited, session);
        }
        public int AddServiceDiscount(string serviceplan, int isfixed, int fromcall, int tocall, int status, decimal discount, Int64 session)
        {
            return objAdmin.AddServiceDiscount(serviceplan, isfixed, fromcall, tocall, status, discount, session);
        }
        public int UpdateServiceDiscount(int DiscountID, int Isfixed, int fromcall, int tocall, int status, decimal discount, Int64 session)
        {
            return objAdmin.UpdateServiceDiscount(DiscountID, Isfixed, fromcall, tocall, status, discount, session);
        }
        public int AddServiceHorizontalDiscount(int quantfrom, int quantto, int status, decimal discount, Int64 session)
        {
            return objAdmin.AddServiceHorizontalDiscount(quantfrom, quantto, status, discount, session);
        }

        public int Addcity(Int64 DistrictID, string CityName, string CityCode, Int64 CreatedBy, Int64 ModifiedBy)
        {
            Admin objAdmin = new Admin();
            return objAdmin.AddCty(DistrictID, CityName, CityCode, CreatedBy, ModifiedBy);
        }
        public int AddDistrict(Int64 StateID, string District, Int64 CreatedBy, Int64 ModifiedBy)
        {
            Admin objAdmin = new Admin();
            return objAdmin.AddDistrict(StateID, District, CreatedBy, ModifiedBy);
        }
        public int AddState(int countryID, string stateName, Int64 CreatedBy, Int64 ModifiedBy)
        {
            return objAdmin.AddState(countryID, stateName, CreatedBy, ModifiedBy);
        }
        public int DeleteService(int serviceID, int servicePlanID)
        {
            return objAdmin.DeleteService(serviceID, servicePlanID);
        }
        public int DeleteServicePlan(int serviceID, int servicePlanID)
        {
            return objAdmin.DeleteServicePlan(serviceID, servicePlanID);
        }
        public int DeleteHoriZontalDIscount(int discountID)
        {
            return objAdmin.DeleteHoriZontalDIscount(discountID);
        }
        public int UpdateServiceHorizontalDiscount(int DisID, int quantfrom, int quantto, int status, decimal discount, Int64 session)
        {
            return objAdmin.UpdateServiceHorizontalDiscount(DisID, quantfrom, quantto, status, discount, session);
        }
        public int UpdateUserStatus(Int64 UserID, int Status)
        {
            return objAdmin.UpdateUserStatus(UserID, Status);
        }
        public int CheckUserStatus(Int64 UserID)
        {
            return objAdmin.CheckUserStatus(UserID);
        }
        public int DeleteUser(Int64 UserID, Int64 deletedBY)
        {
            return objAdmin.DeleteUser(UserID, deletedBY);
        }
        public int DeleteOverhead(Int64 UserID)
        {
            return objAdmin.DeleteOverhead(UserID);
        }
        public int DeleteZone(Int64 zoneID, Int64 subZoneID)
        {
            return objAdmin.DeleteZone(zoneID, subZoneID);
        }
        public int AddQuotation(Int64 RequestID, Int64 createdBy,decimal price,string title,string consumes,string paymentmode,string activationTime)
        {
            return objAdmin.AddQuotation(RequestID, createdBy, price, title, consumes, paymentmode, activationTime);
        }        
        #endregion

        #region SelectMethods
        public void GetRoles(DropDownList dropdown)
        {
            DataTable dt = objAdmin.GetRoles();
            dropdown.DataSource = dt;
            dropdown.DataTextField = "RoleName";
            dropdown.DataValueField = "RoleID";
            dropdown.DataBind();
            dropdown.Items.Insert(0, new ListItem("--Select Role--", "0"));
        }
        public void GetCountry(DropDownList ddlcountry)
        {
            DataTable dt = objAdmin.GetCountry();
            ddlcountry.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlcountry.DataTextField = "CountryName";
                ddlcountry.DataValueField = "CountryID";
                ddlcountry.DataBind();
                ddlcountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
                ddlcountry.SelectedValue = Convert.ToString(dt.Rows[0]["CountryID"]);
            }
            else
            {
                ddlcountry.Items.Insert(0, new ListItem("--No Country mentioned by Admin--", "0"));
            }
        }
        public void GetStates(ListBox lstBox)
        {
            DataTable dt = objAdmin.GetStates();
            lstBox.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                lstBox.DataTextField = "StateName";
                lstBox.DataValueField = "StateID";
                lstBox.DataBind();
                lstBox.Items.Insert(0, new ListItem("--Select States--", "0"));
            }
            else
            {
                lstBox.Items.Insert(0, new ListItem("--No States mentioned by Admin--", "0"));
            }
        }
        public DataTable checkphonenoexists(string phono)
        {
            DataTable result = objAdmin.ChekcPhoneno(phono);
            return result;
        }
        public DataTable checkphonenoexistsinMobileno(string phono)
        {
            DataTable result = objAdmin.ChekcPhonenoexistsinmobileno(phono);
            return result;
        }
        public DataTable checkAnyphonenoexists(string phono)
        {
            DataTable result = objAdmin.ChekcAnyPhonenoexistsinmobileno(phono);
            return result;
        }
        public DataTable GetServiceEngineerLeave()
        {
            DataTable result = objAdmin.GetAllLeave();
            return result;
        }
        public void GetStates(DropDownList lstBox)
        {
            DataTable dt = objAdmin.GetStates();
            lstBox.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                lstBox.DataTextField = "StateName";
                lstBox.DataValueField = "StateID";
                lstBox.DataBind();
                lstBox.Items.Insert(0, new ListItem("--Select States--", "0"));
            }
            else
            {
                lstBox.Items.Insert(0, new ListItem("--No States mentioned by Admin--", "0"));
            }
        }
        public void GetStatesByCountryID(DropDownList dropdown,int countryID)
        {
            DataTable dt = objAdmin.GetStatesByID(countryID);
            dropdown.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                dropdown.DataTextField = "StateName";
                dropdown.DataValueField = "StateID";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("--Select States--", "0"));
                dropdown.SelectedValue =Convert.ToString(dt.Rows[0]["StateID"]);
            }
            else
            {
                dropdown.Items.Insert(0, new ListItem("--No States mentioned by Admin--", "0"));
            }
        }
        public DataTable Getuserinfo(int pagesize, int currentPage, string UserName)
        {
            DataTable dt = objAdmin.GetUserList(pagesize, currentPage, UserName);
            return dt;
        }
        public DataTable Getuserreport(int pagesize, int currentPage, string UserName)
        {
            DataTable dt = objAdmin.GetUserListReport(pagesize, currentPage, UserName);
            return dt;
        }
        public DataTable GetuserreportFilterbydate(int pagesize, int currentPage, string UserName,DateTime fromdate,DateTime todate)
        {
            DataTable dt = objAdmin.GetUserListReportFilterDate(pagesize, currentPage, UserName,fromdate,todate);
            return dt;
        }
        public DataTable GetQuotationAdmin()
        {
            DataTable dt = objAdmin.GetUserQuotation();
            return dt;
        }
        public DataTable GetRequestIdbyName(string name)
        {
            DataTable dt = objAdmin.GetRequestidByName(name);
            return dt;
        }
        public DataSet GetRequests(Int64 RequestID)
        {
            //DataTable dt;
            return objAdmin.GetRequestServices(RequestID);

        }
        public void GetDistrictByStateID(DropDownList dropdown, Int64 stateID)
        {
            DataTable dt = objAdmin.GetDistrictByID(stateID);
            dropdown.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                dropdown.DataTextField = "DistrictName";
                dropdown.DataValueField = "DistrictID";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("--Select District--", "0"));
            }
            else
            {
                dropdown.Items.Insert(0, new ListItem("--No District mentioned by Admin--", "0"));
            }
        }
        public DataTable GetDistrictByStateID(Int64 stateID)
        {
            DataTable dt = objAdmin.GetDistrictByID(stateID);
            return dt;
        }
        public DataTable GetUseralternateno(Int64 userid)
        {
            DataTable dt = objAdmin.GetUseralternateno(userid);
            return dt;
        }
        public void GetDistrictsByID(DropDownList dropdown, Int64 stateID)
        {
            DataTable dt = objAdmin.GetDistrictsByID(stateID);
            dropdown.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                dropdown.DataTextField = "DistrictName";
                dropdown.DataValueField = "DistrictID";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("--Select District--", "0"));
            }
            else
            {
                dropdown.Items.Insert(0, new ListItem("--No District mentioned by Admin--", "0"));
            }
        }
        public void GetCityByDistrictID(DropDownList dropdown, Int64 districtID)
        {
            DataTable dt = objAdmin.GetCitiesByID(districtID);
            dropdown.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                dropdown.DataTextField = "CityName";
                dropdown.DataValueField = "CityID";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("--Select Cities--", "0"));
            }
            else
            {
                dropdown.Items.Insert(0, new ListItem("--No Cities mentioned by Admin--", "0"));
            }
        }
        
        public DataTable GetCityByDistrictID(int currentpage, Int64 districtID)
        {
            DataTable dt = objAdmin.GetCitiesByDistrictID(currentpage, districtID);

            return dt;
        }
        public void GetZones(DropDownList dropdown)
        {
            DataTable dt = objAdmin.GetZones();
            dropdown.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                dropdown.DataTextField = "ZoneName";
                dropdown.DataValueField = "ZoneID";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("--Select Zones--", "0"));
            }
            else
            {
                dropdown.Items.Insert(0, new ListItem("--No Zone mentioned by Admin--", "0"));
            }
        }
        public void GetSubZones(DropDownList dropdown)
        {
            DataTable dt = objAdmin.GetZones();
            dropdown.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                dropdown.DataTextField = "ZoneName";
                dropdown.DataValueField = "ZoneID";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("--Select SubZones--", "0"));
            }
            else
            {
                dropdown.Items.Insert(0, new ListItem("--No SubZone mentioned by Admin--", "0"));
            }
        }
        public DataTable  getZonenameByid(int ZoneID)
        {
            DataTable dtzone = objAdmin.GetDistrictByZoneID(ZoneID);
            return dtzone;
        }
        public DataTable getSubZonenameByid(int SubZoneID)
        {
            DataTable dtsubzone = objAdmin.GetsubzoneBysubZoneID(SubZoneID);
            return dtsubzone;
        }
        
        
        public void GetSubZoneDistrict(DropDownList lstBox, int ZoneID)
        {
            DataTable dt = objAdmin.GetDistrictByID(ZoneID);
            lstBox.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                lstBox.DataTextField = "DistrictName";
                lstBox.DataValueField = "DistrictId";
                lstBox.DataBind();
                lstBox.Items.Insert(0, new ListItem("--Select Subzone--", "0"));
            }
            else
            {
                lstBox.Items.Insert(0, new ListItem("--No district mentioned by Admin--", "0"));
            }
        }
        public void GetDistricts(DropDownList lstBox)
        {
            DataTable dt = objAdmin.GetDistricts();
            lstBox.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                lstBox.DataTextField = "DistrictName";
                lstBox.DataValueField = "DistrictId";
                lstBox.DataBind();
                lstBox.Items.Insert(0, new ListItem("--Select DistrictName--", "0"));
            }
            else
            {
                lstBox.Items.Insert(0, new ListItem("--No district mentioned by Admin--", "0"));
            }
        }
        public void GetCites(DropDownList lstBox)
        {
            DataTable dt = objAdmin.GetCities();
            lstBox.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                lstBox.DataTextField = "CityName";
                lstBox.DataValueField = "CityId";
                lstBox.DataBind();
                lstBox.Items.Insert(0, new ListItem("--Select CityName--", "0"));
            }
            else
            {
                lstBox.Items.Insert(0, new ListItem("--No City mentioned by Admin--", "0"));
            }
        }
        public string GetUserID(string emailID)
        {
            return objAdmin.GetUserIDByEmailID(emailID);
        }
        public string GetUserTempID(string emailID)
        {
            return objAdmin.GetUserIDByEmailIDFomTemp(emailID);
        }
        public void GetServices(DropDownList dropdown)
        {
            DataTable dt = objAdmin.GetServices();
            dropdown.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                dropdown.DataTextField = "ServiceName";
                dropdown.DataValueField = "ServiceID";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("--Select Service--", "0"));
            }
            else
            {
                dropdown.Items.Insert(0, new ListItem("--No Service mentioned by Admin--", "0"));
            }
        }
        public void GetServices(ListBox dropdown)
        {
            DataTable dt = objAdmin.GetServices();
            dropdown.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                dropdown.DataTextField = "ServiceName";
                dropdown.DataValueField = "ServiceID";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("--Select Service--", "0"));
            }
            else
            {
                dropdown.Items.Insert(0, new ListItem("--No Service mentioned by Admin--", "0"));
            }
        }
        public DataTable GetZoneList()
        {
            DataTable dt = objAdmin.GetZoneList();
            return dt;
        }
         public DataTable GetZoneSubZoneList(int currentpage, string Zonename)
        {
            DataTable dt = objAdmin.GetZoneSubZoneList(currentpage, Zonename);
            return dt;
        }
        public DataTable GetStateList(int ZoneID)
        {
            DataTable dt = objAdmin.GetStateList(ZoneID);
            return dt;
        }
        public DataTable GetSubZoneList()
        {
            DataTable dt = objAdmin.GetSubZoneList();
            return dt;
        }

        public DataTable GetServiceList(int CurrentPage, string UserName)
        {
            DataTable dt = objAdmin.GetServiceList(CurrentPage, UserName);
           
            return dt;
        }
        public DataTable GetServiceListDiscount()
        {
            DataTable dt = objAdmin.GetServiceListDiscount();
            return dt;
        }
        
        public DataTable GetHorizontalDiscount()
        {
            DataTable dt = objAdmin.GetHorizontalDiscount();
            return dt;
        }
        public DataTable GetCommercialRequest(int currentpage)
        {
            DataTable dt = objAdmin.GetCommercialRequest(currentpage);
            return dt;
        }
        public DataTable GetCommercialRequestByID(Int64 requestID)
        {
            DataTable dt = objAdmin.GetCommercialRequestByID(requestID);
            return dt;
        }
        public DataTable GetAllTickets()
        {
            return objAdmin.GetAllTickets();
        }
        public DataTable GetAllEngineerskills(int currentpage)
        {
            return objAdmin.GetAllEngineerskills(currentpage);
        }
        public DataTable GetAllTickets(int currentpage, string UserName)
        {
            return objAdmin.GetAllTickets(currentpage, UserName);
        }
        public DataTable GetAllTicketsReport(int currentpage, string UserName)
        {
            return objAdmin.GetAllTicketsReport(currentpage, UserName);
        }
        public DataTable GetAllTicketsReportFIlterbydate(int currentpage, string UserName,DateTime dtfrom,DateTime dtto)
        {
            return objAdmin.GetAllTicketsReportFilterbyDate(currentpage, UserName, dtfrom, dtto);
        }
        public DataTable GetAllTicketsEngineerReport(int currentpage, string UserName)
        {
            return objAdmin.GetAllTicketsEngineerReport(currentpage, UserName);
        }
        public DataTable GetAllTicketsEngineerReportFilterbyDate(int currentpage, string UserName, DateTime dtfrom, DateTime dtto)
        {
            return objAdmin.GetAllTicketsEngineerReportFilterbyDate(currentpage, UserName, dtfrom, dtto);
        }
        public DataTable GetAdmin()
        {
            return objAdmin.GetAdmin();
        }
        public DataTable GetAllAppointments(int currentpage, string clientName)
        {
            return objAdmin.GetAllAppointments(currentpage, clientName);
        }
        public DataTable GetClientdatavalue(int currentpage, string userid)
        {
            DataTable dt = objAdmin.GetClientDataList(currentpage, userid);
            return dt;
        }
        public DataTable GetClientDetails(Int64 UserID)
        {
            return objAdmin.GetClientDetails(UserID);
        }
        public DataTable GetClientProfile(Int64 UserID)
        {
            return objAdmin.GetClientProfile(UserID);
        }
        public DataTable GetClientBill(Int64 UserID)
        {
            return objAdmin.GetClientBill(UserID);
        }
        public DataTable GetAllClient()
        {
            return objAdmin.GetAllClient();
        }
        public DataTable GetAllFeedback(int currentpage)
        {
            DataTable dt = objAdmin.GetAllFeedbackList(currentpage);
            return dt;
        }
        public DataTable GetClientdatavalue(int currentpage, string userid, string clientName)
        {
            DataTable dt = objAdmin.GetClientDataList(currentpage, userid, clientName);
            return dt;
        }
        public DataTable GetOverheadvalue()
        {
            DataTable dt = objAdmin.GetOverheadData();
            return dt;
        }
        public int AddParentPage(string ParentName, int RoleID)
        {
            return objAdmin.AddParentPage(ParentName, RoleID);
        }
        public void GetPageByRoleID(DropDownList dropdown, int ParentNodeID)
        {
            DataTable dt = objAdmin.GetPageByRoleID(ParentNodeID);
            dropdown.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                dropdown.DataTextField = "ParentName";
                dropdown.DataValueField = "ParentNodeID";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("--Select ParentNode--", "0"));
            }
            else
            {
                dropdown.Items.Insert(0, new ListItem("--No Page mentioned by Admin--", "0"));
            }
        }
        public DataTable GetPageByRoleID(int RoleID)
        {
            return objAdmin.GetPageByRoleID(RoleID);
        }

        public DataTable GetChildPagesByNodeID(Int64 ParentNodeID)
        {
            return objAdmin.GetChildPagesByNodeID(ParentNodeID);
        }
        public DataTable GetRoleWisePagesByUserID(Int64 UserID)
        {
            return objAdmin.GetRoleWisePagesByUserID(UserID);
        }
        public int AddPage(int ParentID, string PageName, string PageTitle, string PageDescription,string LinkedPages)
        {
            return objAdmin.AddPage(ParentID, PageName, PageTitle, PageDescription,LinkedPages);
        }
        public int DeleteParentPage(int ParentNodeID)
        {
            return objAdmin.DeleteParentPage(ParentNodeID);
        }
        public int DeleteChildPage(int tblChildID)
        {
            return objAdmin.DeleteChildPage(tblChildID);
        }
        public int UpdateParentPage(int ParentNodeID, string ParentName, int RoleID)
        {
            return objAdmin.UpdateParentPage(ParentNodeID, ParentName, RoleID);
        }
        public int UpdateChildPage(int tblChildID, int ParentNodeID, string PageName, string PageTitle, string PageDescription, string LinkedPages)
        {
            return objAdmin.UpdateChildPage(tblChildID, ParentNodeID, PageName, PageTitle, PageDescription,LinkedPages);
        }
        public void GetPageRole(DropDownList ddlParent)
        {
            DataTable dt = objAdmin.GetPageRole();
            ddlParent.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlParent.DataTextField = "ParentName";
                ddlParent.DataValueField = "ParentNodeID";
                ddlParent.DataBind();
                ddlParent.Items.Insert(0, new ListItem("--Select Parent--", "0"));
            }
            else
            {
                ddlParent.Items.Insert(0, new ListItem("--No Parent Page Defined by Admin--", "0"));
            }
        }
        public DataTable GetParentNode(int currentpage)
        {
            DataTable dt = objAdmin.GetParentNode(currentpage);
            return dt;
        }
        public DataTable GetChildNode(int currentpage)
        {
            DataTable dt = objAdmin.GetChildNode(currentpage);
            return dt;
        }
        public DataTable GetRolesByUserID(Int64 UserID)
        {
            DataTable dt = objAdmin.GetRolesByUserID(UserID);
            return dt;
        }
        public int UpdateAllUserPassword(string Userid, string Password)
        {
            return objAdmin.UpdateAllUserPassword(Userid, Password);
        }
        public int UpdateLeaveStatus(int leaveid,int status)
        {
            return objAdmin.UpdateLeaveStatus(leaveid,status);
        }
        public DataTable GetAllUserPassword(int pagesize, int currentPage, string UserName)
        {
            return objAdmin.GetAllUserPassword(pagesize, currentPage, UserName);
        }
        public int AddNotSendSmsMail(Int64 Userid, int sent, string purpose, int media)
        {
            return objAdmin.AddNotSendSmsMail(Userid, sent,purpose,media);
        }
        public string GetLoginuserName(Int64 UserID)
        {
            return objAdmin.GetLoginuserName(UserID);
        }

        public DataTable BindAllEngineer(Int64 RequestID)
        {
            return objAdmin.BindAllEngineerList(RequestID);
        }
        public int AssignReqToEnginner(Int64 AssignedTo, Int64 ModifiedBy, Int64 RequestID, string Reason, int Status)
        {
            return objAdmin.AssignRequestToEnginner(AssignedTo, ModifiedBy, RequestID, Reason, Status);
        }
        public DataTable GetRequestHistoryByRequestID(Int64 RequestID)
        {
            return objAdmin.GetRequestHistoryByRequestID(RequestID);
        }
        public DataTable GetRequestdata(int currentpage, Int64 userid)
        {
            DataTable dt = objAdmin.GetRequestdata(currentpage, userid);
            return dt;
        }

        public int AddServicesAssesment(int Serviceid,int planid, string PropertyName, string validation, DateTime created, Int64 userID)
        {
            return objAdmin.AddServicesAssesment(Serviceid, planid, PropertyName, validation, created, userID);
        }
        public int UpdateServicesAssesment(int ProprtyID, int Serviceid, string PropertyName, string validation)
        {
            return objAdmin.UpdateServicesAssesment(ProprtyID, Serviceid, PropertyName, validation);
        }
        public int DeleteServiceAssesment(int PropertyID)
        {
            return objAdmin.DeleteServiceAssesment(PropertyID);
        }

        public void GetBasicServiced(DropDownList dropdown)
        {
            DataTable dt = objAdmin.GetBasicServices();
            dropdown.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                dropdown.DataTextField = "ServiceName";
                dropdown.DataValueField = "ServiceID";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("--Select Service--", "0"));
            }
            else
            {
                dropdown.Items.Insert(0, new ListItem("--No Service mentioned by Admin--", "0"));
            }
        }

        public DataTable GetBasicCommercialoServiceList(int CurrentPage)
        {
            DataTable dt = objAdmin.GetBasicCommercialoServiceList(CurrentPage);
            return dt;
        }
        public DataTable GetWorkHistory(Int64 TicketID)
        {
            return objAdmin.GetWorkHistory(TicketID);
        }
        public DataTable GetAllEngineerskill(int currentpage, string clientName)
        {
            return objAdmin.GetAllEngineerskill(currentpage, clientName);
        }
        public DataTable GetAllEngineers()
        {
            return objAdmin.GetAllEngineers();
        }
        public int AddUserPayment(Int64 InvoiceID, Int64 UserID, decimal newAmount, Int64 CreatedBy, DateTime Created, DateTime Modified, Int64 ModifiedBy)
        {
            return objAdmin.AddUserPayment(InvoiceID, UserID, newAmount, CreatedBy, Created, Modified, ModifiedBy);
        }
        public DataTable GetClientList(int pagesize, int currentPage, string searchString)
        {
            DataTable dt = objAdmin.GetClientList(pagesize, currentPage,searchString);
            return dt;
        }
        public DataTable GetMessage(int currentpage)
        {
            DataTable dt = objAdmin.GetMessage(currentpage);
            return dt;
        }
        public int DeleteMessage(int RoleId, Int64 MessageId)
        {
            return objAdmin.DeleteMessage(RoleId, MessageId);
        }

        public int UpdateMessage(Int16 RoleId, string Message, int status, int sendsms)
        {
            return objAdmin.UpdateMessage(RoleId, Message, status, sendsms);
        }

        public int SaveMessage(Int64 CreatedBy, Int64 ModifyBy, int RoleId, String message, int Status, int SendSms)
        {
            return objAdmin.SaveMessage(CreatedBy, ModifyBy, RoleId, message, Status, SendSms);
        }

        public DataTable GetEngineerHistoryByDate(int curntpage, DateTime date, Int64 EngineerID)
        {
            DataTable dt = objAdmin.GetEngineerHistoryListByDate(curntpage, date, EngineerID);
            return dt;
        }
        public DataTable GetUsersByRoleID(int roleid)
        {
            DataTable dt = objAdmin.GetUsersByRoleID(roleid);
            return dt;
        }
        public DataTable GetUserHistoryByID(Int64 clientID)
        {
            DataTable dt = objAdmin.GetUserHistoryByID(clientID);
            return dt;
        }
        public int AddTool(string ToolName, int tooltype, int quantity, int status, string description, DateTime created, Int64 userID)
        {
            return objAdmin.AddTool(ToolName, tooltype, quantity, status, description, created, userID);
        }
        public int UpdateTool(int ToolID, string ToolName, int tooltype, int quantity, int status, string description, DateTime created, Int64 userID)
        {
            return objAdmin.UpdateTool(ToolID, ToolName, tooltype, quantity, status, description, created, userID);
        }
        public int DeleteTool(int Toolid)
        {
            return objAdmin.DeleteTool(Toolid);
        }
        public DataTable GetTool(int pagesize, int currentPage)
        {
            DataTable dt = objAdmin.GetTool(pagesize, currentPage);
            return dt;

        }

        public int AddToolAssignment(Int32 toolid, Int64 engid, Int64 ticketid, int status,  Int64 Createdby, string remark)
        {
            return objAdmin.AddToolAssignment( toolid, engid, ticketid, status, Createdby, remark);
        }
        public int DeleteToolAssignment(int Assignmentid)
        {
            return objAdmin.DeleteToolAssignment(Assignmentid);
        }

        public int UpdateToolAssignment(int assignmentid, int status)//, string remark)
        {
            return objAdmin.UpdateToolAssignment(assignmentid,status);
        }
        public void GetTicketByEng(DropDownList ddl, Int64 Engid)
        {
            DataTable dt = objAdmin.GetTicketByEng(Engid);
            ddl.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl.DataTextField = "TicketID";
                ddl.DataValueField = "TicketID";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select Ticket--", "0"));
            }
            else
            {
                ddl.Items.Insert(0, new ListItem("--No Ticket--", "0"));
            }
        }


        public void GetToolByToolType(DropDownList ddl, int ToolType)
        {
            DataTable dt = objAdmin.GetToolByToolType(ToolType);
            ddl.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl.DataTextField = "ToolName";
                ddl.DataValueField = "ToolId";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select Tool--", "0"));
            }
            else
            {
                ddl.Items.Insert(0, new ListItem("--No Tool mentioned by Admin--", "0"));
            }
        }


        public DataTable GetToolAssignment(int pagesize, int currentpage)
        {

            DataTable dt = objAdmin.GetToolAssignment(pagesize, currentpage);
            return dt;

        }
        public DataTable GetAllToolType()
        {
            return objAdmin.GetAllToolType();
        }
        public DataTable GetGraceTime()
        {
            return objAdmin.GetGraceTime();
        }
        public int AddGraceTime(Int64 AddedBy, int gracetime)
        {
            return objAdmin.AddGraceTime(AddedBy, gracetime);
        }
        public int UpdateGraceTime(Int64 AddedBy, int gracetime)
        {
            return objAdmin.UpdateGraceTime(AddedBy,gracetime);
        }
        public int IsRequestDOne(Int64 requestID)
        {
            return objAdmin.IsRequestDone(requestID);
        }
        public int PaymentByAdmin(Int64 userid, Int64 createdBy, string paymentInfo, decimal amount)
        {
            return objAdmin.PaymentByAdmin(userid, createdBy, paymentInfo, amount);
        }

        #endregion

        
    }
}

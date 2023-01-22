using MakeNMake.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace MakeNMake.BL
{
    public class BLServiceEngineer
    {
        ServiceEngineer objAdmin = new ServiceEngineer();

        public void GetPrimaryskill(DropDownList dropdown)
        {
            DataTable dt = objAdmin.GetSkills();
            dropdown.DataSource = dt;
            dropdown.DataTextField = "ServiceName";
            dropdown.DataValueField = "ServiceID";
            dropdown.DataBind();
            dropdown.Items.Insert(0, new ListItem("Select Skills", "0"));
        }
        //public void GetConsumer(Repeater rpt,Int64 EngineerID)
        //{
        //    DataTable dt;
        //    dt = objAdmin.Getskill(EngineerID);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        rpt.DataSource = dt;
        //        rpt.DataBind();
        //    }
        //    else
        //    {
        //        rpt.Visible = false;
        //    }
        //}
        public DataTable GetConsumer(int currentpage, Int64 EngineerID)
        {
            //DataTable dt;
            return objAdmin.Getskill(currentpage, EngineerID);

        }
        
        public DataTable GetTotalSkills(Int64 EngineerID)
        {
            //DataTable dt;
            return objAdmin.GetTotalSkills(EngineerID);

        }

        public int Addskill(Int64 UserID, int skill, int skilrate, char skilltype, Int64 CreatedBy, Int64 ModifiedBy)
        {
            return objAdmin.Addskill(UserID, skill, skilrate, skilltype, CreatedBy, ModifiedBy);
        }
        public int ApplyLeave(Int64 engineerid,string reason,DateTime leaveon,DateTime  created, int status)
        {
            return objAdmin.ApplyLeave(engineerid,reason,leaveon,created,status);
        }
        public int deleteskill(Int64 UserID, Int64 SkillID)
        {
            return objAdmin.deleteskill(UserID, SkillID);
        }
        public int EngineerAppointment(Int64 appointmentID, Int64 engineerID, Int64 ModifiedBy, int status,string reason)
        {
            return objAdmin.EngineerAppointment(appointmentID, engineerID, ModifiedBy, status,reason);
        }
        public int UpdateEngineerTicket(Int64 ticktID, Int64 engineerID, Int64 ModifiedBy, int status,string reason)
        {
            return objAdmin.UpdateEngineerTicket(ticktID, engineerID, ModifiedBy, status, reason);
        }
        public DataTable GetAppoinments(int currentpage, Int64 EngID)
        {
            return objAdmin.GetAppoinments(currentpage, EngID);
        }
        public DataTable GetAppliedleave(Int64 engineerid)
        {
            return objAdmin.GetAppliedLeave(engineerid);
        }
        public DataTable GetTickets(int CurrentPage, Int64 EngID)
        {
            return objAdmin.GetTickets(CurrentPage, EngID);
        }
        public DataTable SearchTickets(Int64 EngID, string TicketIDOrName, int inspectionType, int findwhat)
        {
            return objAdmin.SearchTickets(EngID,TicketIDOrName,inspectionType,findwhat);
        }
        public DataTable GetTicketDetails(Int64 TicketID)
        {
            return objAdmin.GetTicketDetails(TicketID);
        }
        public int AddDailyDiary(int UserID, string Title, string Description, DateTime moddate, int ReportTo, DateTime Created,Int64 ticktid)
        {
            return objAdmin.AddDailyDiary(UserID, Title, Description, moddate, ReportTo, Created, ticktid);
        }

        public void GetUserId(DropDownList dropdown, Int64 EngineerID)
        {
            DataTable dt = objAdmin.GetUserid(EngineerID);
            dropdown.DataSource = dt;
            dropdown.DataTextField = "TicketID";
            dropdown.DataValueField = "TicketID";
            dropdown.DataBind();
        }


        public void GetDailyDiary(Repeater rpt, Int64 EngineerID)
        {
            DataTable dtDiary = objAdmin.GetDailyDiary(EngineerID);
            if (dtDiary != null && dtDiary.Rows.Count > 0)
            {
                rpt.Visible = true;
                rpt.DataSource = dtDiary;
                rpt.DataBind();
            }
            else
            {
                rpt.Visible = false;
            }
        }
        public DataTable GetServiceTicketsForTiming(Int64 EngineerID)
        {
            return objAdmin.GetServiceTicketsForTiming(EngineerID);
        }
        public DataTable GetServiceTicketsByEngID(Int64 EngineerID)
        {
            return objAdmin.GetServiceTicketsByEngID(EngineerID);
        }
        public int AddserviceTime(Int64 UserID, Int64 ticketID, Int64 Engineer, Int32 serviceid, string from, string to,string image,Int64 tblID,string work)
        {
            return objAdmin.AddServiceTime(UserID, ticketID, Engineer, serviceid, from, to, image, tblID,work);
        }
        public Int64 GetUserIDByTicketID(Int64 ticktID)
        {
            return objAdmin.GetUserIDByTicketID(ticktID);
        }
        public int CheckServiceStatus(Int64 ticketID)
        {
            return objAdmin.CheckServiceStatus(ticketID);
        }
        public DataTable GetCustomerAddressByTicketID(Int64 TicketID)
        {
            return objAdmin.GetCustomerAddressByTicketID(TicketID);
        }
        public DataTable GetTicketIDsAcceptedByEngID(Int64 EngineerID)
        {
            return objAdmin.GetTicketIDsAcceptedByEngID(EngineerID);
        }
        public DataTable GetImagesByID(Int64 ServiceTimeID)
        {
            return objAdmin.GetImagesByID(ServiceTimeID);
        }
        public int DeleteImage(Int64 ImageID)
        {
            return objAdmin.DeleteImage(ImageID);
        }
        public Int64 UpdateRequestData(Int64 RequestID, Int64 engineerID, int status, string reason)
        {
            return objAdmin.UpdateRequestData(RequestID, engineerID, status, reason);
        }
        public DataTable GetAssesmentForm(Int64 RequestID)
        {
            return objAdmin.GetAssesmentForm(RequestID);
        }
        public Int64 AddAssesmentForm(Int64 RequestID, int serviceID,Int64 engineerID,string formData, string reason)
        {
            return objAdmin.AddAssesmentForm(RequestID, serviceID, engineerID, formData, reason);
        }
        public DataSet  GetAssesmentData(Int64 requestID)
        {
            return objAdmin.GetAssesmentData(requestID);
        }
        public DataTable GetDiaryByTicketID(Int64 TicketID)
        {
            DataTable dtDiary = objAdmin.GetDiaryByTicketID(TicketID);
            return dtDiary;
        }
    }
}

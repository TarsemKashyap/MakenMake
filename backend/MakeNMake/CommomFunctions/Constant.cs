using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MakeNMake.CommomFunctions
{
    public static class Constant
    {

        #region Users
        public enum Users
        {
            Administrator=1,
            ServiceEngineer=2,
            CustomerCare=3,
            Consumer=4,
            MIS=5,
            AccountManager=6,
            ExclationManager=7,
            InventoryManager=8
        }
        #endregion

        #region Sessions
        public static class Session
        {
            public static string Role { get { return "Role"; } }
            public static string AdminSession { get { return "Admin"; } }
            //public static string AdminSession { get { return "CustomerCare"; } }
            //public static string UserSession { get { return "User";}}
            //public static string AdminSession { get { return "Escalation"; } }
            //public static string AccountSession { get { return "Account"; } }
            //public static string MISSession { get { return "MIS"; } }
            //public static string InventorySession { get { return "Inventory"; } }
        }

        #endregion

        #region pages
        public static class Pages
        {
            public static string SignUp { get { return "~/SignUp.aspx"; } }
            public static string ForgotPassword { get { return "~/ForgotPassword.aspx"; } }
            public static string Customer { get { return "~/Pages/"; } }
            public static string Admin { get { return "~/Pages/"; } }
            public static string ServiceEngineer { get { return "~/Pages/"; } }
            public static string CustomerCare { get { return "~/Pages/"; } }
            public static string Exclation { get { return "~/Pages/"; } }
            public static string Inventory { get { return "~/Pages/"; } }
            public static string Account { get { return "~/Pages/"; } }
            public static string MIS { get { return "~/Pages/"; } }
            public static string Confirmation { get { return "Confirmation.aspx"; } }
            public static string Login { get { return "Default.aspx"; } }
            public static string Error { get { return "Error.aspx"; } }
            public static string CustomerDashBoard { get { return "Dashboard.aspx"; } }
            public static string DashBoard { get { return "DashBoard.aspx"; } }
            public static string PackageSelection { get { return "PackageSelection.aspx"; } }
            public static string Client { get { return "Clients.aspx"; } }
            public static string Services { get { return "SUserServices.aspx"; } }
            public static string AddOnServices { get { return "AddOnServices.aspx"; } }
            public static string Complaint { get { return "Complaint.aspx"; } }
            public static string UpdateInfo { get { return "CustomerUpdateInfo.aspx"; } }
        }

        #endregion

        
        #region QueryString
        public static class QueryString
        {
            public static string Confirmed { get { return "CID";}}
            public static string ClientID { get { return "ClientID"; } }
        }
        #endregion

      #region Separators
        public static class Separators
        {
            public static string QuestionMark { get { return "?";}}
            public static string EqualTo { get { return "=";}}
            public static string Backspace { get { return "/";}}
        }
        #endregion

        public static string AlertSucessFull { get { return "alert('Data Successfully Added') ;";}}
        public static string AlertError { get { return "alert('Some Fatal Error Occurs') ;";}}

        
        public static string ErrorCodeForAdminUser(int errorCode)
        {
            switch (errorCode)
            {
                case -99:
                    return "EmailID already exists. Please enter a different EmailID.";
                case -1:
                    return "fatal error occur";
                default:
                    return "Sucessfully Added";
            }
        }

        public static class AuthenticationType
        {
            public static string Facebook { get { return "Facebook";}}
            public static string Twitter { get { return "Twitter";}}
            public static string Google { get { return "Google";}}
            public static string LinkedIn { get { return "LinkedIn";}}
            public static string MicroSoft { get { return "Microsoft";}}
            public static string Yahoo { get { return "Yahoo";}}
        }
        public static DateTime? ConvertStringToDateTime(string dateTime)
        {
            DateTime outputValue;
            if (DateTime.TryParse(dateTime, out outputValue))
                return outputValue;
            return null;
        }
    }
}
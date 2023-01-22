using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for SendSms
/// </summary>
public class SendSms
{
    public SendSms()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public int SendSmsOnMobile( string message,string mobilenumber)
    {
        try
        {
            string endPoint = "http://121.241.247.222:7501/failsafe/HttpLink?aid=573257&pin=mnm@12&mnumber=91" + mobilenumber + "&message=" + message;
            var client = new RestClient(endPoint);

            var json = client.MakeRequest("&message="+message);
            if (json.StartsWith("Message Accepted for Request ID"))//confirm response from service provide..also debuge to check returns...
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch //(Exception objException)
        {
            return -99;
        }
    }
 
    public int SendSmsOnMobileForApointment(string UserName, string mobilenumber, int isinterested, string avaiabledate, string avaiabletime)
    {
        try
        {
            string endPoint = "http://121.241.247.222:7501/failsafe/HttpLink?aid=573257&pin=mnm@12&mnumber=91" + mobilenumber;
            var client = new RestClient(endPoint);
            string avaiable = string.Empty;
            if (isinterested == 1)
            {
                avaiable = " Your appointment is fixed on : " + avaiabledate + " at " + avaiabletime;
            }
            var json = client.MakeRequest("&message=Hi " + UserName +
                    ", "+ avaiable + ". MakenMake Team");

            if (json.StartsWith("Message Accepted for Request ID"))//confirm response from service provide..also debuge to check returns...
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        catch (Exception objException)
        {
            return -99;
        }

    }
    public int SendSmsOnMobileForEngineerConfirmation(string UserName, string mobilenumber, string otp)
    {
        try
        {
            string endPoint = "http://121.241.247.222:7501/failsafe/HttpLink?aid=573257&pin=mnm@12&mnumber=91" + mobilenumber;
            var client = new RestClient(endPoint);
  var json = client.MakeRequest("&message=Hi " + UserName +
                    ", Your OTP number for engineer confirmation is :" + otp + ". MakenMake Team");
            if (json.StartsWith("Message Accepted for Request ID"))//confirm response from service provide..also debuge to check returns...
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch //(Exception objException)
        {
            return -99;
        }

    }
  
}
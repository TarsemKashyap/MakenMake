using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Web.Mail;
using System.Net.Mime;

/// <summary>
/// Summary description for MEmail
/// </summary>
public class MEmail
{
    public static void SendGMail(string toMail, string subject, string body, string path)
    {
        const string fromPassword = "Shalini21#";
        var fromAddress = new MailAddress("mohit@makenmake.in", "Make-N-Make Admin");
        string[] toMailA = toMail.Split('|');
        var toAddress = new MailAddress(toMailA[0], (toMailA.Length == 2) ? toMailA[1] : toMailA[0]);
        var smtp = new SmtpClient
        {
            Host = "a2plcpnl0309.prod.iad2.secureserver.net",
            Port = 587,
            EnableSsl = false,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = true,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };
        using (var message = new System.Net.Mail.MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
        {
            message.IsBodyHtml = true;
            if (path.Length > 4)
            {
                Attachment attachment = new Attachment(path, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = attachment.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(path);
                disposition.ModificationDate = File.GetLastWriteTime(path);
                disposition.ReadDate = File.GetLastAccessTime(path);
                disposition.FileName = Path.GetFileName(path);
                disposition.Size = new FileInfo(path).Length;
                disposition.DispositionType = DispositionTypeNames.Attachment;
                message.Attachments.Add(attachment);
            }
            smtp.Send(message);
        }
    }
}
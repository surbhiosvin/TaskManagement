using System;
using System.Configuration;
using System.Net.Mail;

namespace Providers.Helper
{
    public class Email
    {
        public static bool SendEmail(string ToEmail, string body, string Subject)
        {
            try
            {
                string Host = Convert.ToString(ConfigurationManager.AppSettings["Host"]);
                int Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                string FromEmail = Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]);
                string FromPassword = Convert.ToString(ConfigurationManager.AppSettings["FromPassword"]);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(FromEmail);
                mail.To.Add(ToEmail);
                mail.Subject = string.IsNullOrWhiteSpace(Subject)?"":Subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient Smtp = new SmtpClient();
                Smtp.Host = Host;
                Smtp.Port = Port;
                Smtp.Credentials = new System.Net.NetworkCredential(FromEmail, FromPassword);
                Smtp.EnableSsl = false;
                Smtp.Send(mail);
                return true;
            }
            catch(Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }
    }
}
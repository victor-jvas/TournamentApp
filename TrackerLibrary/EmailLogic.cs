using System.Collections.Generic;
using System.Net.Mail;

namespace TrackerLibrary
{
    public static class EmailLogic
    {
        public static void SendEmail(string to, string subject, string body)
        {
            var fromMailAddress = new MailAddress("EmailAddress@email.com", null);

            var mail = new MailMessage();
            mail.To.Add(to);
            mail.From = fromMailAddress;
            mail.Subject = subject;
            mail.Body = body;

            var client = new SmtpClient();
            client.Send(mail);
        }
    }
}
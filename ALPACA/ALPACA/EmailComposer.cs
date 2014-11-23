using System;
using System.Net;
using System.Net.Mail;

namespace ALPACA
{
    public static class EmailComposer
    {
        public static void SendEmail(AlpacaUser currentUser, string emailBody)
        {
            var fromAddress = new MailAddress("kodamn@gmail.com", "Dave the Rave");
            string fromPassword = currentUser.EmailPassword;
            string subject = "Subject";

            var smtp = new SmtpClient
            {
                Host = currentUser.EmailServer,
                Port = Convert.ToInt32(currentUser.EmailPort),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage { From = fromAddress, Subject = subject, Body = emailBody })
            {
                foreach (var contact in currentUser.Contacts)
                    message.To.Add(contact);

                smtp.Send(message);
            }
        }
    }
}

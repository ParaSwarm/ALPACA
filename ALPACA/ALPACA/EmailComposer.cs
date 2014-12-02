using System;
using System.Net;
using System.Net.Mail;

namespace ALPACA
{
    public static class EmailComposer
    {
        public static bool SendEmail(AlpacaUser currentUser, string subject, string emailBody)
        {
            var fromAddress = new MailAddress(currentUser.Email);
            string fromPassword = currentUser.EmailPassword;
            SmtpClient smtp;
            try
            {
                smtp = new SmtpClient
                {
                    Host = currentUser.EmailServer,
                    Port = Convert.ToInt32(currentUser.EmailPort),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

            }catch(Exception e)
            {
                return false;
            }

            foreach (var contact in currentUser.Contacts)
            {

                using (var message = new MailMessage { From = fromAddress, Subject = subject, Body = emailBody, IsBodyHtml = true })
                {
                    try
                    {
                        message.To.Add(contact);
                        smtp.Send(message);
                    }catch(SmtpException e)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ALPACA
{
    public static class EmailComposer
    {
        private static readonly Regex InlineImageRegex = new Regex("<img src=\\\"data:image\\/(?<FileType>\\w*);base64,(?<Data>.*?)\" \\/>");

        public static bool SendEmail(AlpacaUser currentUser, string subject, string emailBody, IList<string> attachments)
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
                    Timeout = 5000,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

            }
            catch(Exception e)
            {
                return false;
            }
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            foreach (var contact in currentUser.Contacts)
            {
                using (var message = new MailMessage { From = fromAddress, Subject = subject, Body = emailBody, IsBodyHtml = true })
                {
                    try
                    {
                        message.To.Add(contact);
                        var attachmentList = new List<Attachment>();
                        foreach (var attachment in attachments)
                        {
                            var pathToFile = Path.Combine(appDataPath, attachment);
                            FileStream fileToAttach = File.Open(pathToFile, FileMode.Open);
                            attachmentList.Add(new Attachment(fileToAttach, attachment));
                        }
                        foreach(var file in attachmentList)
                        {
                            message.Attachments.Add(file);
                        }

                        EmbedInlineImages(message);

                        smtp.Send(message);
                    }
                    catch(SmtpException e)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void EmbedInlineImages(this MailMessage email)
        {
            //Reads base64 encoded image tags, attaches them to the email as binary, and replaces the tag with a reference to the attached file.
            //Necessary due to Outlook not displaying base64-encoded images.

            var matches = InlineImageRegex.Matches(email.Body);

            for (int i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                var fileType = match.Groups["FileType"].Value;
                var data = match.Groups["Data"].Value;

                byte[] imageBytes = Convert.FromBase64String(data);
                var filename = String.Format("image{0}.{1}", i, fileType);
                email.Attachments.Add(new Attachment(new MemoryStream(imageBytes), filename));
                email.Body = InlineImageRegex.Replace(email.Body, "<img src=\"cid:" + filename + "\" />", 1, i);
            }
        }
    }
}

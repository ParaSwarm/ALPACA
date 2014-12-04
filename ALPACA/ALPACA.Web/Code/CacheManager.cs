using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using ALPACA.Utility;
using System.IO;

namespace ALPACA.Web.Code
{
    public class CacheManager
    {
        [Inject]
        public HttpContextBase HttpContext { get; set; }
        [Inject]
        public IMainBusiness MainBusiness { get; set; }

        private AlpacaUser _currentUser;
        public AlpacaUser CurrentUser
        {
            get
            {
                if (_currentUser != null) return _currentUser;

                _currentUser = HttpContext.Session["CurrentUser"] as AlpacaUser;

                if (_currentUser != null) return _currentUser;

                _currentUser = MainBusiness.GetUser(HttpContext.User.Identity.Name);
                if (_currentUser == null)
                {
                    _currentUser = new AlpacaUser();
                }
                else
                {
                    HttpContext.Session["CurrentUser"] = _currentUser;
                }

                return _currentUser;
            }
            set { }
        }

        public void NullifySession()
        {
            HttpContext.Session.Abandon();
        }

        public void AddAttachment(string attachment)
        {
            var attachments = GetAttachments();
                        attachments.Add(attachment);
            CacheSession("Attachments", attachments);
        }

        public void RemoveAttachment(string attachmentName)
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var attachments = GetAttachments();
            foreach(var fileName in attachments)
            {
                var fileToDelete = Path.Combine(appDataPath, fileName);
                File.Delete(fileToDelete);
            }
            attachments.RemoveAll(x => x.Contains(attachmentName));
            if (!attachments.Any())
                attachments = null;

            CacheSession("Attachments", attachments);
        }

        public void RemoveAllAttachments()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var attachments = GetAttachments();
            foreach(var fileName in attachments)
            {
                var fileToDelete = Path.Combine(appDataPath, fileName);
                File.Delete(fileToDelete);
            }


            CacheSession("Attachments", null);
        }

        public IList<string> GetAttachments()
        {
            return Get<List<string>>("Attachments") ?? new List<string>();
        }

        public T Get<T>(string key)
        {
            object data;
            try
            {
                data = HttpContext.Session[key] ?? HttpRuntime.Cache[key];
            }
            catch (Exception)
            {
                data = null;
            }

            return (T)data;
        }

        public object CacheSession(string key, object obj)
        {
            HttpContext.Session[key] = obj;
            return obj;
        }
    }
}
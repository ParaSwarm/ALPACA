﻿using ALPACA.Entities;
using ALPACA.Web.Controllers.Base;
using ALPACA.Web.ViewModels;
using Ninject;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Mail;
using System.Net.Mime;
using System;

namespace ALPACA.Web.Controllers
{
    [Authorize]
    [ValidateInput(false)]
    public class HomeController : AlpacaControllerBase
    {
        [Inject]
        public IMainBusiness MainBusiness { get; set; }

        public ActionResult Index()
        {
            var model = new MainViewModel
            {
                Email = new EmailViewModel
                {
                    DraftNames = MainBusiness.GetDrafts().Select(x => x.Name)
                },
                Users = new UserViewModel
                {
                    Users = MainBusiness.GetUsers().Select(x => x.UserName)
                },
                Contacts = new ContactsViewModel
                {
                    Contacts = CurrentUser.Contacts
                }
            };
            ViewBag.IsAdmin = CurrentUser.AdminFlag;
            return View(model);
        }

        public string GetDraftBody(string draftName)
        {
            return MainBusiness.GetDraftBody(draftName);
        }

        [HttpPost]
        public JsonResult SaveDraft(string draftName, string draftBody)
        {
            MainBusiness.SaveDraft(draftName, draftBody);

            return Json(new { draftNames = MainBusiness.GetDrafts() });
        }

        [HttpPost]
        public JsonResult DeleteDraft(string draftName)
        {
            MainBusiness.DeleteDraft(draftName);

            return Json(new { draftNames = MainBusiness.GetDrafts() });
        }

        public JsonResult UploadFile(IEnumerable<HttpPostedFileBase> files, UploadType uploadType)
        {
            string result = string.Empty;

            foreach (HttpPostedFileBase file in files)
            {
                StreamReader reader = new StreamReader(file.InputStream);
                IList<string> fileContents = new List<string>();
                while (!reader.EndOfStream)
                {
                    fileContents.Add(reader.ReadLine());
                }
                //previous call always adds an empty string to the list, getting rid of it here
                //fileContents.RemoveAt(fileContents.Count - 1);

                switch (uploadType)
                {
                    case UploadType.Add:
                        result += MainBusiness.AddToList(fileContents);
                        break;
                    case UploadType.Remove:
                        result += MainBusiness.RemoveFromList(fileContents);
                        break;
                }
            }

            return Json(new { Success = true });
        }
        public JsonResult GetUserInfo()
        {
            return Json(new
            {
                userName = CurrentUser.UserName,
                fName = CurrentUser.FirstName,
                lName = CurrentUser.LastName,
                email = CurrentUser.Email,
                emailPassword = CurrentUser.EmailPassword,
                emailServer = CurrentUser.EmailServer,
                emailPort = CurrentUser.EmailPort,
                pass = CurrentUser.PasswordHash,
                adminFlag = CurrentUser.AdminFlag
            });
        }
        public JsonResult GetUserByName(string username)
        {
            var user = MainBusiness.GetUser(username);
            return Json(new
            {
                userName = user.UserName,
                fName = user.FirstName,
                lName = user.LastName,
                email = user.Email,
                emailPassword = user.EmailPassword,
                emailServer = user.EmailServer,
                emailPort = user.EmailPort,
                pass = user.PasswordHash,
                adminFlag = user.AdminFlag
            });

        }
        public JsonResult SaveUserInfo(UserViewModel model, bool samePass)
        {
            AlpacaUser userBeingUpdated = null;
            bool success;
            if (model.username == CurrentUser.UserName)
            {
                CurrentUser.Email = model.email;
                CurrentUser.EmailPassword = model.emailPass;
                CurrentUser.FirstName = model.fName;
                CurrentUser.LastName = model.lName;
                CurrentUser.EmailServer = model.emailServer;
                CurrentUser.EmailPort = model.emailPort;
                CurrentUser.AdminFlag = model.adminFlag;
                var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                if (!samePass)
                {
                    CurrentUser.PasswordHash = manager.PasswordHasher.HashPassword(model.pass);
                }
                success = MainBusiness.SaveUser(CurrentUser);
                return Json(new
                {
                    usernames = MainBusiness.GetUsers().Select(x => x.UserName),
                    passHash = CurrentUser.PasswordHash,
                    success = success
                });
            }
            else
            {
                userBeingUpdated = MainBusiness.GetUser(model.username);
            }
            AlpacaUser newUser = new AlpacaUser();
            if (userBeingUpdated == null)
            {
                newUser.UserName = model.username;
                newUser.Email = model.email;
                newUser.EmailPassword = model.emailPass;
                newUser.FirstName = model.fName;
                newUser.LastName = model.lName;
                newUser.PasswordHash = model.pass;
                newUser.EmailServer = model.emailServer;
                newUser.EmailPort = model.emailPort;
                newUser.AdminFlag = model.adminFlag;
                var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var result = manager.CreateAsync(newUser, model.pass);
                while (!result.IsCompleted) { /*wait for task to complete to avoid DB session conflict*/}
                success = MainBusiness.SaveUser(newUser);

            }
            else
            {
                userBeingUpdated.UserName = model.username;
                userBeingUpdated.Email = model.email;
                userBeingUpdated.EmailPassword = model.emailPass;
                userBeingUpdated.FirstName = model.fName;
                userBeingUpdated.LastName = model.lName;
                userBeingUpdated.EmailServer = model.emailServer;
                userBeingUpdated.EmailPort = model.emailPort;
                userBeingUpdated.AdminFlag = model.adminFlag;
                var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                if (!samePass)
                {
                    userBeingUpdated.PasswordHash = manager.PasswordHasher.HashPassword(model.pass);
                }
                success = MainBusiness.SaveUser(userBeingUpdated);
            }
            var moddedUser = userBeingUpdated;
            if (userBeingUpdated == null)
            {
                moddedUser = newUser;
            }
            return Json(new { usernames = MainBusiness.GetUsers().Select(x => x.UserName), passHash = moddedUser.PasswordHash, success = success });
        }

        public JsonResult DeleteUser(string username)
        {
            var user = MainBusiness.GetUser(username);
            MainBusiness.DeleteUser(user);
            return Json(new { usernames = MainBusiness.GetUsers().Select(x => x.UserName) });
        }

        public JsonResult SendEmail(string emailBody, string emailSubject)
        {
            if (string.IsNullOrWhiteSpace(emailBody))
                return Json(new { success = false });

            var emailSent = MainBusiness.SendEmail(emailBody, emailSubject, CacheManager.GetAttachments());
            if (emailSent)
                CacheManager.RemoveAllAttachments();

            return Json(new { success = emailSent });
        }

        public FileResult ExportContacts()
        {
            string toExport = MainBusiness.ExportContacts();
            return File(new System.Text.UTF8Encoding().GetBytes(toExport), "text/csv", "contacts.csv");
        }

        public JsonResult GetContacts()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = CurrentUser.Contacts;
            return result;
        }

        public ContentResult UploadAttachment(IEnumerable<HttpPostedFileBase> attachments)
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            
            foreach (var file in attachments)
            {
                var path = Path.Combine(appDataPath, file.FileName);
                file.SaveAs(path);

                CacheManager.AddAttachment(file.FileName);
            }

            return Content(string.Empty);
        }

        public ContentResult RemoveAttachment(string[] fileNames)
        {
            foreach (var fileName in fileNames)
                CacheManager.RemoveAttachment(fileName);

            return Content(string.Empty);
        }
        public int CheckForAttachments()
        {
            return CacheManager.GetAttachments().Count;
        }
    }

    public enum UploadType
    {
        Add,
        Remove
    }
}
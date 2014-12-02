using ALPACA.Entities;
using ALPACA.Web.Controllers.Base;
using ALPACA.Web.ViewModels;
using Ninject;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                    Users = MainBusiness.GetUsers().Select(x  => x.UserName)
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
                string stringFromFile = reader.ReadToEnd();
                IList<string> fileContents = stringFromFile.Split('\n').ToList();
                //previous call always adds an empty string to the list, getting rid of it here
                fileContents.RemoveAt(fileContents.Count - 1);

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
                                adminFlag = CurrentUser.AdminFlag});
        }
        public JsonResult GetUserByName(string username)
        {
            var user = MainBusiness.GetUser(username);
            return Json(new {userName = user.UserName,
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
        public JsonResult SaveUserInfo(string username, string email, string fName, string lName, string pass,
                                        string emailPass, string emailServer, string emailPort, bool adminFlag)
        {
            AlpacaUser userBeingUpdated = null;
            if (username == CurrentUser.UserName)
            {
                CurrentUser.Email = email;
                CurrentUser.EmailPassword = emailPass;
                CurrentUser.FirstName = fName;
                CurrentUser.LastName = lName;
                CurrentUser.PasswordHash = pass;
                CurrentUser.EmailServer = emailServer;
                CurrentUser.EmailPort = emailPort;
                CurrentUser.AdminFlag = adminFlag;
                MainBusiness.SaveUser(CurrentUser);
                return Json(new { usernames = MainBusiness.GetUsers().Select(x => x.UserName) });
            }
            else
            {
            userBeingUpdated = MainBusiness.GetUser(username);
            }
            if(userBeingUpdated == null)
            {
                AlpacaUser newUser = new AlpacaUser();
                newUser.UserName = username;
                newUser.Email = email;
                newUser.EmailPassword = emailPass;
                newUser.FirstName = fName;
                newUser.LastName = lName;
                newUser.PasswordHash = pass;
                newUser.EmailServer = emailServer;
                newUser.EmailPort = emailPort;
                newUser.AdminFlag = adminFlag;
                MainBusiness.SaveUser(newUser);

            }
            else
            {
                userBeingUpdated.UserName = username;
                userBeingUpdated.Email = email;
                userBeingUpdated.EmailPassword = emailPass;
                userBeingUpdated.FirstName = fName;
                userBeingUpdated.LastName = lName;
                userBeingUpdated.PasswordHash = pass;
                userBeingUpdated.EmailServer = emailServer;
                userBeingUpdated.EmailPort = emailPort;
                userBeingUpdated.AdminFlag = adminFlag;
                MainBusiness.SaveUser(userBeingUpdated);
            }

            return Json(new { usernames = MainBusiness.GetUsers().Select(x => x.UserName) });
        }

        public JsonResult DeleteUser(string username)
        {
            var user = MainBusiness.GetUser(username);
            MainBusiness.DeleteUser(user);
            return Json(new { usernames = MainBusiness.GetUsers().Select(x => x.UserName) });
        }

        public JsonResult SendEmail(string emailBody)
        {
            if(string.IsNullOrWhiteSpace(emailBody))
                return Json(new { success = false });

            var emailSent = MainBusiness.SendEmail(emailBody);

            return Json(new { success = true });
        }
    }

    public enum UploadType
    {
        Add,
        Remove
    }
}
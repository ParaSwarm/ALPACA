using ALPACA.Entities;
using ALPACA.Web.ViewModels;
using Ninject;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALPACA.Web.Controllers
{
    //[Authorize]
    [ValidateInput(false)]
    public class HomeController : Controller
    {
        [Inject]
        public AlpacaUser CurrentUser { get; set; }
        [Inject]
        public IMainBusiness MainBusiness { get; set; }

        public ActionResult Index()
        {
            var model = new MainViewModel
            {
                Email = new EmailViewModel
                {
                    DraftNames = MainBusiness.GetDrafts(CurrentUser.Id).Select(x => x.Name)
                }
            };

            return View(model);
        }

        public string GetDraftBody(string draftName) 
        {
            return MainBusiness.GetDraftBody(CurrentUser.Id, draftName);
        }
        [HttpPost]
        public JsonResult SaveDraft(string draftName, string draftBody)
        {
            MainBusiness.SaveDraft(CurrentUser.Id, draftName, draftBody);

            return Json(new { draftNames = MainBusiness.GetDrafts(CurrentUser.Id) });
        }
        [HttpPost]
        public JsonResult DeleteDraft(string draftName)
        {
            MainBusiness.DeleteDraft(CurrentUser.Id, draftName);

            return Json(new { draftNames = MainBusiness.GetDrafts(CurrentUser.Id) });
        }
        public JsonResult UploadFile(IEnumerable<HttpPostedFileBase> files, UploadType uploadType)
        {
            if(uploadType == UploadType.Add)
            {
                //Add - get the list from the file and pass it to the MainBusiness AddToList method
                foreach (HttpPostedFileBase file in files) 
                {
                    StreamReader reader = new StreamReader(file.InputStream);
                    string stringFromFile = reader.ReadToEnd();
                    IEnumerable<string> fileContents = stringFromFile.Split('\n').ToList();
                    string result = MainBusiness.AddToList(fileContents);
                }
            }
            else
            {
                //Remove - get the list from the file and pass it to the MainBusiness RemoveFromList method
                foreach (HttpPostedFileBase file in files)
                {
                    StreamReader reader = new StreamReader(file.InputStream);
                    string stringFromFile = reader.ReadToEnd();
                    IEnumerable<string> fileContents = stringFromFile.Split('\n').ToList();
                    string result = MainBusiness.RemoveFromList(fileContents);
                }
            }

            return Json(new { result = "failed" });
        }
    }

    public enum UploadType
    {
        Add,
        Remove
    }

}
using ALPACA.Entities;
using ALPACA.Web.ViewModels;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALPACA.Web.Controllers
{
    //[Authorize]
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
        public ContentResult SaveDraft()
        {
            return Content("stuff");
        }
        public ContentResult UploadFile(IEnumerable<HttpPostedFileBase> files, UploadType uploadType)
        {
            if(uploadType == UploadType.Add)
            {
                //Add
            }
            else
            {
                //Remove
            }

            return Content("Successful");
        }
    }

    public enum UploadType
    {
        Add,
        Remove
    }

}
using ALPACA.Domain.Entities;
using ALPACA.Web.ViewModels;
using Ninject;
using System.Web.Mvc;
using System.Linq;

namespace ALPACA.Web.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        [Inject]
        public User CurrentUser { get; set; }
        [Inject]
        public IMainBusiness MainBusiness { get; set; }

        public ActionResult Index()
        {
            var model = new MainViewModel
            {
                Email = new EmailViewModel
                {
                    Drafts = MainBusiness.GetDrafts(CurrentUser.Id).Select(x => x.Name)
                }
            };

            return View(model);
        }
    }
}
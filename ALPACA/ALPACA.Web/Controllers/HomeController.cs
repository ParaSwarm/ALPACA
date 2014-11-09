using ALPACA.Web.ViewModels;
using System.Web.Mvc;

namespace ALPACA.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new MainViewModel();

            return View(model);
        }
    }
}
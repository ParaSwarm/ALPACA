using ALPACA.Entities;
using Ninject;
using System.Web.Mvc;

namespace ALPACA.Web.Controllers.Base
{
    public class AlpacaControllerBase : Controller
    {
        [Inject]
        public AlpacaUser CurrentUser { get; set; }
    }
}
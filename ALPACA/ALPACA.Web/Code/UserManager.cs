using ALPACA.Entities;
using Ninject;
using System.Web;

namespace ALPACA.Web.Code
{
    public class UserManager
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

                _currentUser = MainBusiness.GetUser("dselinger");
                
                HttpContext.Session["CurrentUser"] = _currentUser;



                return _currentUser;
            }
            set { }
        }
    }
}
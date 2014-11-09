using ALPACA.Domain.Entities;
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

        private User _currentUser;
        public User CurrentUser
        {
            get
            {
                if (_currentUser != null) return _currentUser;

                _currentUser = HttpContext.Session["CurrentUser"] as User;

                if (_currentUser != null) return _currentUser;

                _currentUser = new User();//MainBusiness.GetUser(HttpContext.User.Identity.Name);
                
                HttpContext.Session["CurrentUser"] = _currentUser;



                return _currentUser;
            }
            set { }
        }
    }
}
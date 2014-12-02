
namespace ALPACA.Web.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Email = new EmailViewModel();
            Users = new UserViewModel();
            Contacts = new ContactsViewModel();
        }

        public EmailViewModel Email { get; set; }
        public UserViewModel Users { get; set; }
        public ContactsViewModel Contacts { get; set; }
    }
}
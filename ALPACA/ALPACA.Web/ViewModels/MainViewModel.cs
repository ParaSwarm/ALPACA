
namespace ALPACA.Web.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Email = new EmailViewModel();
        }

        public EmailViewModel Email { get; set; }
    }
}
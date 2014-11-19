using System.Collections.Generic;

namespace ALPACA.Web.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Users = new List<string>();
        }
        public string SelectedUser { get; set; }
        public IEnumerable<string> Users { get; set; }

	}
}
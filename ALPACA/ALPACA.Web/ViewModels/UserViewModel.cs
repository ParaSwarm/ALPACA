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
        public string username { get; set; }
        
        public string email{ get; set; }
        public string fName{ get; set; }
        public string lName{ get; set; }
        public string pass{ get; set; }
        public string emailPass{ get; set; }
        public string emailServer{ get; set; }
        public string emailPort{ get; set; }
        public bool adminFlag{ get; set; }
        public IEnumerable<string> Users { get; set; }

	}
}
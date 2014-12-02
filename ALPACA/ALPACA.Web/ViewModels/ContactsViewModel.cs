using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALPACA.Web.ViewModels
{
    public class ContactsViewModel
    {
        public ContactsViewModel()
        {
            Contacts = new List<string>();
        }

        public string SelectedContact { get; set; }
        public IEnumerable<string> Contacts{ get; set; }
    }
}
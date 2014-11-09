using System.Collections.Generic;

namespace ALPACA.Web.ViewModels
{
    public class EmailViewModel
    {
        public EmailViewModel()
        {
            Drafts = new List<string>();
        }

        public string EmailBody { get; set; }
        public IEnumerable<string> Drafts { get; set; }
    }
}
using System.Collections.Generic;

namespace ALPACA.Web.ViewModels
{
    public class EmailViewModel
    {
        public EmailViewModel()
        {
            DraftNames = new List<string>();
        }

        public string EmailBody { get; set; }
        public string SelectedDraft { get; set; }
        public IEnumerable<string> DraftNames { get; set; }
    }
}
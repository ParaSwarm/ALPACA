using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALPACA.Entities
{
    public class AlpacaUser
    {
        public virtual int Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string AccountName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        
        public virtual IList<string> Contacts { get; set; }
    }
}

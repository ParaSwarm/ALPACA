using System.Collections.Generic;

namespace ALPACA.Entities
{
    public class AlpacaUser
    {
        public virtual int Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string AccountName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual List<string> Contacts { get; set; }
    }
}

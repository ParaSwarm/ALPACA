using System.Collections.Generic;

namespace ALPACA.Entities
{
    public class AlpacaUser
    {
        public virtual int Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string AccountName { get; set; }
        public virtual string AccountPassword{ get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string EmailPassword { get; set; }
        public virtual string EmailServer{ get; set; }
        public virtual string EmailPort{ get; set; }
        public virtual bool AdminFlag { get; set; }
        
        public virtual IList<string> Contacts { get; set; }
    }
}

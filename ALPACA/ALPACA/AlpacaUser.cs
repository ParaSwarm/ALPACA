using Microsoft.AspNet.Identity;
using NHibernate.AspNet.Identity;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ALPACA
{
    public class AlpacaUser : IdentityUser
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string EmailPassword { get; set; }
        public virtual string EmailServer { get; set; }
        public virtual string EmailPort { get; set; }
        public virtual bool AdminFlag { get; set; }

        public virtual IList<string> Contacts { get; set; }

        public virtual async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AlpacaUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}
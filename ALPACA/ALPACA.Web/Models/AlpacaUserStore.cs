using ALPACA.Entities;
using Microsoft.AspNet.Identity;
using NHibernate;
using NHibernate.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace ALPACA.Web.Models
{
    public class AlpacaUserStore : UserStore<AlpacaUser>, IUserEmailStore<AlpacaUser>
    {
        public AlpacaUserStore(ISession session) : base(session) { }

        public Task<AlpacaUser> FindByEmailAsync(string email)
        {
            var getUser = Context.QueryOver<AlpacaUser>().Where(x => x.Email == email).SingleOrDefault();

            return Task.FromResult(getUser);
        }

        public Task<string> GetEmailAsync(AlpacaUser user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(AlpacaUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(AlpacaUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(AlpacaUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }
    }
}

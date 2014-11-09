using ALPACA.Domain.Entities;
using NHibernate;
using Ninject;
using System.Collections.Generic;

namespace ALPACA
{
    public class MainBusiness : IMainBusiness
    {
        [Inject]
        public ISession Session { get; set; }

        public User GetUser(string accountName)
        {
            return Session.QueryOver<User>().Where(x => x.AccountName == accountName).SingleOrDefault();
        }

        public IEnumerable<EmailDraft> GetDrafts(int userId)
        {
            return Session.QueryOver<EmailDraft>().Where(x => x.UserId == userId).List();
        }
    }

    public interface IMainBusiness
    {
        User GetUser(string loginName);
        IEnumerable<EmailDraft> GetDrafts(int userId);
    }
}

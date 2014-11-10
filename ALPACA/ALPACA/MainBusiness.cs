using ALPACA.Entities;
using NHibernate;
using Ninject;
using System.Collections.Generic;

namespace ALPACA
{
    public class MainBusiness : IMainBusiness
    {
        public static ListManager ListManager = new ListManager();

        [Inject]
        public ISession Session { get; set; }

        public AlpacaUser GetUser(string accountName)
        {
            return Session.QueryOver<AlpacaUser>().Where(x => x.AccountName == accountName).SingleOrDefault();
        }

        public IEnumerable<EmailDraft> GetDrafts(int userId)
        {
            return Session.QueryOver<EmailDraft>().Where(x => x.UserId == userId).List();
        }

        public string GetDraftBody(int userId, string draftName)
        {
            return Session.QueryOver<EmailDraft>()
                            .Where(x => x.UserId == userId)
                            .And(x => x.Name == draftName)
                            .SingleOrDefault().Body;
        }

        public string AddToList(IEnumerable<string> listToAdd)
        {
            //ListManager.MergeList(listToAdd);

            return "";
        }
    }

    public interface IMainBusiness
    {
        AlpacaUser GetUser(string loginName);
        IEnumerable<EmailDraft> GetDrafts(int userId);
        string GetDraftBody(int userId, string draftName);
    }
}

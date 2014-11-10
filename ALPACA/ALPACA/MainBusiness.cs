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
            AlpacaUser ToReturn = Session.QueryOver<AlpacaUser>().Where(x => x.AccountName == accountName).SingleOrDefault();
            return ToReturn;
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
        public string SaveDraft(int userId, string draftName, string draftBody)
        {
            EmailDraft DraftToSave = new EmailDraft();
            DraftToSave.UserId = userId;
            DraftToSave.Name = draftName;
            DraftToSave.Body = draftBody;
            Session.SaveOrUpdate(DraftToSave);
            return "WE DID IT!";
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

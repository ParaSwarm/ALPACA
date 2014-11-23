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

        public AlpacaUser GetUser(string userName)
        {
            return Session.QueryOver<AlpacaUser>().Where(x => x.UserName == userName).SingleOrDefault();
        }

        public IEnumerable<EmailDraft> GetDrafts(string userId)
        {
            return Session.QueryOver<EmailDraft>().Where(x => x.UserId == userId).List();
        }
        public IEnumerable<AlpacaUser> GetUsers()
        {
            return Session.QueryOver<AlpacaUser>().List();
        }

        public string GetDraftBody(string userId, string draftName)
        {
            return Session.QueryOver<EmailDraft>()
                            .Where(x => x.UserId == userId)
                            .And(x => x.Name == draftName)
                            .SingleOrDefault().Body;
        }

        public string SaveDraft(string userId, string draftName, string draftBody)
        {
            EmailDraft draft = Session.QueryOver<EmailDraft>()
                                                .Where(x => x.Name == draftName).SingleOrDefault();
            if(draft != null)
            {
                if(draftBody == string.Empty)   //empty body indicates user wishes to delete draft
                {
                    Session.Delete(draft);
                }
                else
                {
                    draft.Body = draftBody;
                    Session.SaveOrUpdate(draft);
                }
            }
            else
            {
                EmailDraft DraftToSave = new EmailDraft();
                DraftToSave.UserId = userId;
                DraftToSave.Name = draftName;
                DraftToSave.Body = draftBody;
                Session.SaveOrUpdate(DraftToSave);
            }
            return "success";
        }

        public string DeleteDraft(string userId, string draftName)
        {
            EmailDraft draft = Session.QueryOver<EmailDraft>()
                                    .Where(x => x.UserId == userId)
                                    .And(x => x.Name == draftName)
                                    .SingleOrDefault();
            if(draft != null)
            {
                Session.Delete(draft);

                return "success";
            }
            return "fail";
        }

        public string AddToList(string userId, IList<string> listToAdd)
        {
            //ListManager.MergeList(listToAdd);

            return "";
        }

        public string RemoveFromList(string userId, IList<string> listToRemove)
        {
            //ListManageer.RemoveList(listToAdd);

            return "";
        }
        public string SaveUser(AlpacaUser user)
        {
            Session.SaveOrUpdate(user);

            return "yaaaay";
        }
        public string DeleteUser(AlpacaUser user)
        {
            Session.Delete(user);

            return "deleted";
        }
    }

    public interface IMainBusiness
    {
        AlpacaUser GetUser(string userName);
        IEnumerable<EmailDraft> GetDrafts(string userId);
        string GetDraftBody(string userId, string draftName);
        string SaveDraft(string userId, string draftName, string draftBody);
        string DeleteDraft(string userId, string draftName);
        string AddToList(string userId, IList<string> listToAdd);
        string RemoveFromList(string userId, IList<string> listToRemove);
        string SaveUser(AlpacaUser user);
        string DeleteUser(AlpacaUser user);
        IEnumerable<AlpacaUser> GetUsers();
    }
}

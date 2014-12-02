using ALPACA.Entities;
using NHibernate;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;

namespace ALPACA
{
    public class MainBusiness : IMainBusiness
    {
        public static ListManager ListManager = new ListManager();

        [Inject]
        public Lazy<AlpacaUser> CurrentUser { get; set; }
        [Inject]
        public ISession Session { get; set; }


        public AlpacaUser GetUser(string userName)
        {
            return Session.QueryOver<AlpacaUser>().Where(x => x.UserName == userName).SingleOrDefault();
        }

        public IEnumerable<EmailDraft> GetDrafts()
        {
            return Session.QueryOver<EmailDraft>().Where(x => x.UserId == CurrentUser.Value.Id).List();
        }

        public IEnumerable<AlpacaUser> GetUsers()
        {
            return Session.QueryOver<AlpacaUser>().List();
        }

        public string GetDraftBody(string draftName)
        {
            return Session.QueryOver<EmailDraft>()
                            .Where(x => x.UserId == CurrentUser.Value.Id)
                            .And(x => x.Name == draftName)
                            .SingleOrDefault().Body;
        }

        public string SaveDraft(string draftName, string draftBody)
        {
            EmailDraft draft = Session.QueryOver<EmailDraft>()
                                        .Where(x => x.UserId == CurrentUser.Value.Id)
                                        .And(x => x.Name == draftName)
                                        .SingleOrDefault();
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
                DraftToSave.UserId = CurrentUser.Value.Id;
                DraftToSave.Name = draftName;
                DraftToSave.Body = draftBody;
                Session.SaveOrUpdate(DraftToSave);
            }
            return "success";
        }

        public string DeleteDraft(string draftName)
        {
            EmailDraft draft = Session.QueryOver<EmailDraft>()
                                    .Where(x => x.UserId == CurrentUser.Value.Id)
                                    .And(x => x.Name == draftName)
                                    .SingleOrDefault();
            if(draft != null)
            {
                Session.Delete(draft);

                return "success";
            }
            return "fail";
        }

        public string AddToList(IList<string> listToAdd)
        {
            CurrentUser.Value.Contacts = ListManager.MergeList((List<string>)CurrentUser.Value.Contacts,
                                                                    (List<string>)listToAdd);
            Session.SaveOrUpdate(CurrentUser.Value);
            return "";
        }

        public string RemoveFromList(IList<string> listToRemove)
        {
            CurrentUser.Value.Contacts = ListManager.RemoveList((List<string>)CurrentUser.Value.Contacts,
                                                                    (List<string>)listToRemove);
            Session.SaveOrUpdate(CurrentUser.Value);
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
        public bool SendEmail(string emailBody, string emailSubject)
        {
            return EmailComposer.SendEmail(CurrentUser.Value, emailSubject, emailBody);
        }
        public string ExportContacts()
        {
            StringWriter output = new StringWriter();
            List<string> currContacts =(List<string>)CurrentUser.Value.Contacts;
            currContacts.Sort();
            foreach(var contact in currContacts)
            {
                output.WriteLine(contact);
            }

            return output.ToString(); 
        }
    }

    public interface IMainBusiness
    {
        AlpacaUser GetUser(string userName);
        IEnumerable<EmailDraft> GetDrafts();
        string GetDraftBody(string draftName);
        string SaveDraft(string draftName, string draftBody);
        string DeleteDraft(string draftName);
        string AddToList(IList<string> listToAdd);
        string RemoveFromList(IList<string> listToRemove);
        string SaveUser(AlpacaUser user);
        string DeleteUser(AlpacaUser user);
        IEnumerable<AlpacaUser> GetUsers();
        bool SendEmail(string emailBody, string emailSubject);
        string ExportContacts();
    }
}

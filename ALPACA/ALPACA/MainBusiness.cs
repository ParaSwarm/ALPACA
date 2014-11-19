﻿using ALPACA.Entities;
using NHibernate;
using NHibernate.Exceptions;
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
            EmailDraft draft = Session.QueryOver<EmailDraft>()
                                                .Where(x => x.Name == draftName).SingleOrDefault();
            if(draft != null)
            {
                if(draftBody =="")//empty body indicates user wishes to delete draft
                {
                    Session.Delete(draft);
                    Session.Flush();
                }
                else
                {
                    draft.Body = draftBody;
                    Session.SaveOrUpdate(draft);
                    Session.Flush();
                }
            }
            else
            {
                EmailDraft DraftToSave = new EmailDraft();
                DraftToSave.UserId = userId;
                DraftToSave.Name = draftName;
                DraftToSave.Body = draftBody;
                Session.SaveOrUpdate(DraftToSave);
                Session.Flush();
            }
            return "success";
        }
        public string DeleteDraft(int userId, string draftName)
        {
            EmailDraft draft = Session.QueryOver<EmailDraft>()
                                                .Where(x => x.Name == draftName).SingleOrDefault();
            if(draft != null)
            {
                Session.Delete(draft);
                Session.Flush();
                return "success";
            }
            return "fail";
        }

        public string AddToList(IList<string> listToAdd)
        {
            //ListManager.MergeList(listToAdd);

            //temp code
            var user = new AlpacaUser
            {
                AccountName = "CoolBro",
                FirstName = "Cool",
                LastName = "Bro",
                Contacts = listToAdd,
                Email = "cooolbro@hotmail.com"
            };

            Session.Save(user);
            Session.Flush();

            return "";
        }

        public string RemoveFromList(IList<string> listToRemove)
        {
            //ListManageer.RemoveList(listToAdd);

            return "";
        }
    }

    public interface IMainBusiness
    {
        AlpacaUser GetUser(string loginName);
        IEnumerable<EmailDraft> GetDrafts(int userId);
        string GetDraftBody(int userId, string draftName);
        string SaveDraft(int userId, string draftName, string draftBody);
        string DeleteDraft(int userId, string draftName);
        string AddToList(IList<string> listToAdd);
        string RemoveFromList(IList<string> listToRemove);
    }
}

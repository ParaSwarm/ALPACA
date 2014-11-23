﻿using ALPACA.Entities;
using NHibernate;
using Ninject;
using System;
using System.Collections.Generic;

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
            //ListManager.MergeList(listToAdd);

            return "";
        }

        public string RemoveFromList(IList<string> listToRemove)
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
        public string SendEmail(string emailBody)
        {
            EmailComposer.SendEmail(CurrentUser.Value, emailBody);

            return "deleted";
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
        string SendEmail(string emailBody);
    }
}

using NHibernate;
using Ninject;
using System;
using System.Data;
using System.Web.Mvc;

namespace ALPACA.Web.Filters
{
    public sealed class UnitOfWorkFilter : IActionFilter, IDisposable
    {
        [Inject]
        public ISession Session { get; set; }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Begin();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            End();
        }

        public void Begin()
        {
            Session.BeginTransaction(IsolationLevel.Serializable);
        }

        public void End()
        {
            CommitTransaction();
            Session.Dispose();
        }

        public void Dispose()
        {
            End();
        }

        private void CommitTransaction()
        {
            if (!Session.IsOpen || Session.Transaction == null || !Session.Transaction.IsActive) return;
            try
            {
                Session.Transaction.Commit();
            }
            catch (Exception)
            {
                Session.Transaction.Rollback();
                throw;
            }
        }
    }
}
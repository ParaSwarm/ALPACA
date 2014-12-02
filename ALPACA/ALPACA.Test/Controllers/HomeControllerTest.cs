using ALPACA.Entities;
using ALPACA.Web.Controllers;
using ALPACA.Web.ViewModels;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace ALPACA.Test.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private HomeController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new HomeController
            {
                MainBusiness = A.Fake<IMainBusiness>(),
                CurrentUser = A.Fake<AlpacaUser>()
            };
        }

        [Test]
        public void Index_ReturnsCorrectViewAndModel()
        {
            _controller.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<MainViewModel>();
        }

        [Test]
        public void Index_CanGetDrafts()
        {
            //var emailDrafts = Builder.Build<List<EmailDraft>>();

            //A.CallTo(() => _controller.MainBusiness.GetDrafts()).Returns(emailDrafts);

            var model = _controller.Index();

            A.CallTo(() => _controller.MainBusiness.GetDrafts()).MustHaveHappened();
        }

        [Test]
        public void Index_CanGetUsers()
        {
            _controller.Index();

            A.CallTo(() => _controller.MainBusiness.GetUsers()).MustHaveHappened();
        }
    }
}

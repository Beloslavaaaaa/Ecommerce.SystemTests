using Ecommerce.Infrastructure.Factories;
using Ecommerce.Infrastructure.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Tests
{
    [TestFixture]
    public class LoginTests : BaseTest 
    {
        [Test]
        public void Login_WithValidCredentials_ShouldSucceed()
        {
            var loginPage = new LoginPage(Driver);
            Driver.Navigate().GoToUrl("https://automationexercise.com/login");

            Driver.SwitchTo().DefaultContent();

            loginPage.Login(UserFactory.GetStandardUser());

            Assert.That(loginPage.IsUserLoggedIn(), Is.True,
                "The logout button was not found, indicating the user is not logged in.");
        }
    }
}
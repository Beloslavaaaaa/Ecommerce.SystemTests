using Ecommerce.Infrastructure.Factories;
using Ecommerce.Infrastructure.Pages;
using NUnit.Framework;

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

            loginPage.Login(UserFactory.GetStandardUser());

            Assert.That(loginPage.IsUserLoggedIn(), Is.True);
        }

        [Test]
        public void Login_WithInvalidCredentials_ShouldShowError()
        {
            var loginPage = new LoginPage(Driver);
            Driver.Navigate().GoToUrl("https://automationexercise.com/login");

            loginPage.Login(UserFactory.GetInvalidUser());

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsUserLoggedIn(), Is.False);
                Assert.That(loginPage.IsErrorMessageDisplayed(), Is.True);
            });
        }
    }
}
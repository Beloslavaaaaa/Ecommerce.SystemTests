using NUnit.Framework;
using Ecommerce.Infrastructure.Pages;
using Ecommerce.Infrastructure.Factories;
using OpenQA.Selenium;

namespace Ecommerce.Tests
{
    [TestFixture]
    public class RegistrationTests : BaseTest
    {
        [Test]
        public void Register_NewUser_ShouldSucceed()
        {
            var signupPage = new SignupPage(Driver);
            var testUser = UserFactory.GetRegistrationUser();

            Driver.Navigate().GoToUrl("https://automationexercise.com/login");
            signupPage.WaitForPageToLoad();

            signupPage.FillInitialSignup(testUser.Name, testUser.Email);
            signupPage.WaitForPageToLoad();
            signupPage.CompleteRegistration(testUser);

            Assert.Multiple(() =>
            {
                Assert.That(signupPage.GetSuccessText(), Is.EqualTo("ACCOUNT CREATED!"));
                Assert.That(Driver.Url, Does.Contain("account_created"));
            });
        }

        [Test]
        public void Register_ExistingEmail_ShouldShowError()
        {
            var signupPage = new SignupPage(Driver);
            var existingUser = UserFactory.GetExistingUser();

            Driver.Navigate().GoToUrl("https://automationexercise.com/login");
            signupPage.WaitForPageToLoad();

            signupPage.FillInitialSignup(existingUser.Name, existingUser.Email);

            var errorMsg = Driver.FindElement(By.XPath("//p[contains(text(), 'Email Address already exist')]"));
            Assert.That(errorMsg.Displayed, Is.True);
        }

        [Test]
        public void Register_InvalidEmailFormat_ShouldFail()
        {
            var signupPage = new SignupPage(Driver);

            Driver.Navigate().GoToUrl("https://automationexercise.com/login");
            signupPage.WaitForPageToLoad();

            signupPage.FillInitialSignup("User", "invalid-email-format");

            Assert.That(Driver.Url, Does.Not.Contain("signup"));
        }
    }
}
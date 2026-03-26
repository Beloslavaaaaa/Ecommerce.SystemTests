using NUnit.Framework;
using Ecommerce.Infrastructure.Pages;
using Ecommerce.Infrastructure.Factories;

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
                Assert.That(signupPage.GetSuccessText(), Is.EqualTo("ACCOUNT CREATED!"),
                    "Requirement 13: Success message text mismatch.");
                Assert.That(Driver.Url, Does.Contain("account_created"),
                    "Requirement 13: The URL did not redirect to account_created page.");
            });
        }
    }
}
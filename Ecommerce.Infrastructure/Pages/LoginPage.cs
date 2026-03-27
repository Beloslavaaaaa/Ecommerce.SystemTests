using Ecommerce.Infrastructure.DTOs;
using OpenQA.Selenium;
using System.Threading;

namespace Ecommerce.Infrastructure.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        private By EmailInput => By.XPath("//div[@class='login-form']//input[@data-qa='login-email']");
        private By PasswordInput => By.XPath("//div[@class='login-form']//input[@data-qa='login-password']");
        private By LoginButton => By.XPath("//div[@class='login-form']//button[@data-qa='login-button']");
        private By LogoutButton => By.XPath("//a[contains(@href, 'logout')]");
        private By ErrorMessage => By.XPath("//p[contains(text(), 'incorrect')]");
        private By LoginHeader => By.XPath("//h2[text()='Login to your account']");

        public void Login(UserDto user)
        {
            WaitForPageToLoad();
            Thread.Sleep(1000);
            ForceType(EmailInput, user.Email);
            ForceType(PasswordInput, user.Password);
            ForceClick(LoginButton);
        }

        public bool IsUserLoggedIn()
        {
            try
            {
                return Wait.Until(d => d.FindElement(LogoutButton).Displayed);
            }
            catch { return false; }
        }

        public bool IsErrorMessageDisplayed()
        {
            try
            {
                return Wait.Until(d => d.FindElement(ErrorMessage).Displayed);
            }
            catch { return false; }
        }
    }
}
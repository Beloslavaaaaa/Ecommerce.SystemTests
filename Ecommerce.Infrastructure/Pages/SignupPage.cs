using OpenQA.Selenium;
using Ecommerce.Infrastructure.DTOs;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace Ecommerce.Infrastructure.Pages
{
    public class SignupPage : BasePage
    {
        public SignupPage(IWebDriver driver) : base(driver) { }
        private By NameInput => By.XPath("//div[@class='signup-form']//input[@data-qa='signup-name']");
        private By EmailInput => By.XPath("//div[@class='signup-form']//input[@data-qa='signup-email']");
        private By SignupButton => By.XPath("//div[@class='signup-form']//button[@data-qa='signup-button']");
        private By PasswordInput => By.Id("password");
        private By DaysSelect => By.Id("days");
        private By FirstNameInput => By.Id("first_name");
        private By LastNameInput => By.Id("last_name");
        private By AddressInput => By.Id("address1");
        private By StateInput => By.Id("state");
        private By CityInput => By.Id("city");
        private By ZipcodeInput => By.Id("zipcode");
        private By MobileInput => By.Id("mobile_number");
        private By CreateAccountButton => By.XPath("//button[@data-qa='create-account']");
        private By SuccessMessage => By.XPath("//b[contains(text(), 'Account Created')]");

        public void FillInitialSignup(string name, string email)
        {
            ForceType(NameInput, name);
            ForceType(EmailInput, email);
            ForceClick(SignupButton);
        }

        public void CompleteRegistration(UserDto user)
        {
            Wait.Until(d => d.FindElement(PasswordInput).Displayed);

            ForceType(PasswordInput, user.Password);

            var daysElement = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(DaysSelect));
            new SelectElement(daysElement).SelectByValue(user.BirthDay);

            ForceType(FirstNameInput, user.FirstName);
            ForceType(LastNameInput, user.LastName);
            ForceType(AddressInput, user.Address);
            ForceType(StateInput, user.State);
            ForceType(CityInput, user.City);
            ForceType(ZipcodeInput, user.ZipCode);
            ForceType(MobileInput, user.MobileNumber);
            ForceClick(CreateAccountButton);
        }

        public string GetSuccessText()
        {
            var element = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(SuccessMessage));
            return element.Text;
        }
    }
}
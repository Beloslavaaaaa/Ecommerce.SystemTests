using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;

namespace Ecommerce.Tests
{
    public class BaseTest
    {
        protected IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
            var options = new EdgeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-notifications");
            options.AddExcludedArgument("enable-automation");
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);

            Driver = new EdgeDriver(options);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        [TearDown]
        public void Teardown()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
            }
        }
    }
}
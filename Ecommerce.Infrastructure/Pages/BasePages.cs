using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace Ecommerce.Infrastructure.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }
        public void KillPopups()
        {
            try
            {
                By consentBtn = By.XPath("//button[contains(@class, 'fc-button')] | //button[text()='Consent'] | //p[text()='Consent'] | //button[contains(@class, 'fc-confirm-button')]");

                var popups = Driver.FindElements(consentBtn);
                if (popups.Count > 0 && popups[0].Displayed)
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                    js.ExecuteScript("arguments[0].click();", popups[0]);

                    Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(consentBtn));
                    Thread.Sleep(500);
                }
            }
            catch { }
        }

        public void WaitForPageToLoad()
        {
            KillPopups();
            Wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").ToString() == "complete");
        }

        protected void ForceClick(By locator)
        {
            KillPopups();

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    var element = Wait.Until(ExpectedConditions.ElementExists(locator));
                    IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;

                    js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);
                    Thread.Sleep(500); 

                    js.ExecuteScript("arguments[0].click();", element);
                    return;
                }
                catch (Exception)
                {
                    KillPopups(); 
                    Thread.Sleep(1000);
                }
            }
        }

        protected void ForceType(By locator, string text)
        {
            KillPopups();
            var element = Wait.Until(ExpectedConditions.ElementExists(locator));
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            string script = @"
                var el = arguments[0];
                el.focus();
                el.value = arguments[1];
                el.dispatchEvent(new Event('input', { bubbles: true }));
                el.dispatchEvent(new Event('change', { bubbles: true }));
                el.dispatchEvent(new Event('blur', { bubbles: true }));
                if ('onpropertychange' in el) el.fireEvent('onpropertychange');";

            js.ExecuteScript(script, element, text);
            Thread.Sleep(500);
        }
        public void AcceptCookies() => KillPopups();

        protected IWebElement WaitUntilClickable(By locator)
        {
            KillPopups();
            return Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        protected void ClickViaJS(By locator) => ForceClick(locator);

        protected void SendKeysViaJS(By locator, string text) => ForceType(locator, text);

        protected void SafeClick(By locator)
        {
            try { ForceClick(locator); }
            catch { }

            if (Driver.Url.Contains("#google_vignette"))
            {
                Driver.Navigate().Refresh();
            }
        }

        public void HandleAdOverlay()
        {
            if (Driver.Url.Contains("#google_vignette"))
            {
                Driver.Navigate().Refresh();
            }
        }

        public void ResetFocus() => Driver.SwitchTo().DefaultContent();
    }
}
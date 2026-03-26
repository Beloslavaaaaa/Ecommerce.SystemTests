using OpenQA.Selenium;
using System.Threading;

namespace Ecommerce.Infrastructure.Pages
{
    public class ProductsPage : BasePage
    {
        public ProductsPage(IWebDriver driver) : base(driver) { }

        private By FirstAddToCartBtn => By.XPath("(//a[contains(text(),'Add to cart')])[1]");
        private By ViewCartLink => By.XPath("//u[contains(text(),'View Cart')]");

        public void AddFirstProductToCart()
        {
            ForceClick(FirstAddToCartBtn);

            Thread.Sleep(2000);

            Driver.Navigate().GoToUrl("https://automationexercise.com/view_cart");
        }

        public void GoToCartFromModal()
        {
            if (Driver.Url.Contains("view_cart")) return;

            SafeClick(ViewCartLink);
        }
    }
}
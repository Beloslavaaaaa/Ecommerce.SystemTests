using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading;

namespace Ecommerce.Infrastructure.Pages
{
    public class SearchPage : BasePage
    {
        public SearchPage(IWebDriver driver) : base(driver) { }

        private By SearchInput => By.Id("search_product");
        private By SubmitSearch => By.Id("submit_search");
        private By ProductNamesText => By.XPath("//div[@class='productinfo text-center']//p");

        public void PerformSearch(string term)
        {
            WaitForPageToLoad();

            var input = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(SearchInput));

            ForceType(SearchInput, term);
            ForceClick(SubmitSearch);

            Wait.Until(d => d.Url.Contains("search"));

            Wait.Until(d => {
                var elements = d.FindElements(ProductNamesText);
                return elements.Count > 0 && !string.IsNullOrWhiteSpace(elements[0].Text);
            });
        }

        public List<string> GetProductNames()
        {
            Thread.Sleep(1500);

            var elements = Driver.FindElements(ProductNamesText);

            var names = elements
                .Select(e => e.Text.Trim())
                .Where(t => !string.IsNullOrEmpty(t))
                .ToList();

            return names;
        }
    }
}
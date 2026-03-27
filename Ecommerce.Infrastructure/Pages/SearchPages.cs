using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using Ecommerce.Infrastructure.DTOs;

namespace Ecommerce.Infrastructure.Pages
{
    public class SearchPage : BasePage
    {
        public SearchPage(IWebDriver driver) : base(driver) { }

        private By SearchInput => By.XPath("//input[@id='search_product']");
        private By SubmitSearch => By.XPath("//button[@id='submit_search']");
        private By ResultsTitle => By.XPath("//h2[contains(@class, 'title') and text()='Searched Products']");
        private By ProductItems => By.XPath("//div[@class='single-products']");
        private By ProductNames => By.XPath("//div[@class='productinfo text-center']//p");
        private By ProductPrices => By.XPath("//div[@class='productinfo text-center']//h2");
        private By ViewProductBtn => By.XPath("//a[text()='View Product']");
        private By SidebarTitle => By.XPath("//div[@class='left-sidebar']/h2");
        private By CategoryWomen => By.XPath("//a[@href='#Women']");
        private By CategoryMen => By.XPath("//a[@href='#Men']");
        private By CategoryKids => By.XPath("//a[@href='#Kids']");

        public void PerformSearch(SearchOptionsDto options)
        {
            WaitForPageToLoad();
            ForceType(SearchInput, options.SearchTerm);
            ForceClick(SubmitSearch);
            Wait.Until(d => d.Url.Contains("search"));
        }

        public List<ProductDto> GetDisplayedProducts()
        {
            KillPopups();
            var names = Driver.FindElements(ProductNames);
            var prices = Driver.FindElements(ProductPrices);

            var results = new List<ProductDto>();
            for (int i = 0; i < names.Count; i++)
            {
                results.Add(new ProductDto
                {
                    Name = names[i].Text.Trim(),
                    Price = prices[i].Text.Trim()
                });
            }
            return results;
        }
    }
}
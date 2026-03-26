using OpenQA.Selenium;
using Ecommerce.Infrastructure.DTOs;
using System.Collections.Generic;

namespace Ecommerce.Infrastructure.Pages
{
    public class CartPage : BasePage
    {
        public CartPage(IWebDriver driver) : base(driver) { }

        private By CartRows => By.XPath("//table[@id='cart_info_table']/tbody/tr");

        public List<ProductDto> GetProductsInCart()
        {
            var products = new List<ProductDto>();
            var rows = Driver.FindElements(CartRows);

            foreach (var row in rows)
            {
                products.Add(new ProductDto
                {
                    Name = row.FindElement(By.XPath(".//h4/a")).Text,
                    Price = row.FindElement(By.XPath(".//td[@class='cart_price']/p")).Text,
                    Quantity = row.FindElement(By.XPath(".//td[@class='cart_quantity']/button")).Text
                });
            }
            return products;
        }
    }
}
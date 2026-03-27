using OpenQA.Selenium;
using Ecommerce.Infrastructure.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Infrastructure.Pages
{
    public class CartPage : BasePage
    {
        public CartPage(IWebDriver driver) : base(driver) { }

        private By CartRows => By.XPath("//table[@id='cart_info_table']/tbody/tr");
        private By ProductName(int row) => By.XPath($"//table[@id='cart_info_table']/tbody/tr[{row}]//h4/a");
        private By ProductPrice(int row) => By.XPath($"//table[@id='cart_info_table']/tbody/tr[{row}]//td[@class='cart_price']/p");
        private By ProductQuantity(int row) => By.XPath($"//table[@id='cart_info_table']/tbody/tr[{row}]//td[@class='cart_quantity']/button");
        private By RemoveBtn(string productName) => By.XPath($"//a[text()='{productName}']/ancestor::tr//a[@class='cart_quantity_delete']");
        private By ProceedToCheckoutBtn => By.XPath("//a[text()='Proceed To Checkout']");
        private By EmptyCartMsg => By.XPath("//span[@id='empty_cart']//b[text()='Cart is empty!']");
        private By RegisterLoginLink => By.XPath("//u[text()='Register / Login']");
        private By DeliveryAddressHeader => By.XPath("//h2[text()='Address Details']");
        private By PlaceOrderBtn => By.XPath("//a[@href='/payment']");
        private By DescriptionTextArea => By.XPath("//textarea[@name='message']");

        public List<ProductDto> GetProductsInCart()
        {
            var products = new List<ProductDto>();
            var rows = Driver.FindElements(CartRows);

            for (int i = 1; i <= rows.Count; i++)
            {
                products.Add(new ProductDto
                {
                    Name = Driver.FindElement(ProductName(i)).Text,
                    Price = Driver.FindElement(ProductPrice(i)).Text,
                    Quantity = Driver.FindElement(ProductQuantity(i)).Text
                });
            }
            return products;
        }

        public void RemoveProduct(string name)
        {
            ForceClick(RemoveBtn(name));
            Wait.Until(d => d.FindElements(By.XPath($"//a[text()='{name}']")).Count == 0);
        }

        public void ProceedToCheckout() => ForceClick(ProceedToCheckoutBtn);

        public void PlaceOrder(string comment)
        {
            ForceType(DescriptionTextArea, comment);
            ForceClick(PlaceOrderBtn);
        }
    }
}
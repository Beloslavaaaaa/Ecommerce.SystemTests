using Ecommerce.Infrastructure.DTOs;
using Ecommerce.Infrastructure.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;

namespace Ecommerce.Tests
{
    [TestFixture]
    public class CartTests : BaseTest
    {
        [Test]
        public void AddProductToCart_ShouldMatchProductDetails()
        {
            var cartPage = new CartPage(Driver);
            var productsPage = new ProductsPage(Driver);

            var expectedProduct = new ProductDto { Name = "Blue Top", Price = "Rs. 500" };

            Driver.Navigate().GoToUrl("https://automationexercise.com/products");

            productsPage.AddFirstProductToCart();
            productsPage.GoToCartFromModal();

            var actualProducts = cartPage.GetProductsInCart();
            var actualProduct = actualProducts.FirstOrDefault(p => p.Name == expectedProduct.Name);

            Assert.Multiple(() =>
            {
                Assert.That(actualProduct, Is.Not.Null, "Product not found in cart.");
                Assert.That(actualProduct, Is.EqualTo(expectedProduct), "DTO mismatch!");
            });
        }
    }
}

using Ecommerce.Infrastructure.DTOs;
using Ecommerce.Infrastructure.Pages;
using Ecommerce.Infrastructure.Factories;
using NUnit.Framework;
using System.Linq;

namespace Ecommerce.Tests
{
    [TestFixture]
    public class CartTests : BaseTest
    {
        [Test]
        public void AddProduct_ShouldMatchDtoDetails()
        {
            var cartPage = new CartPage(Driver);
            var productsPage = new ProductsPage(Driver);
            var expected = ProductFactory.GetBlueTop();

            Driver.Navigate().GoToUrl("https://automationexercise.com/products");
            productsPage.AddFirstProductToCart();
            productsPage.GoToCartFromModal();

            var actual = cartPage.GetProductsInCart().FirstOrDefault(p => p.Name == expected.Name);

            Assert.Multiple(() =>
            {
                Assert.That(actual, Is.Not.Null);
                Assert.That(actual.Price, Is.EqualTo(expected.Price));
            });
        }

        [Test]
        public void RemoveProduct_ShouldUpdateCartCount()
        {
            var cartPage = new CartPage(Driver);
            var productsPage = new ProductsPage(Driver);

            Driver.Navigate().GoToUrl("https://automationexercise.com/products");
            productsPage.AddFirstProductToCart();
            productsPage.GoToCartFromModal();

            var initialProducts = cartPage.GetProductsInCart();
            var productToRemove = initialProducts.First().Name;

            cartPage.RemoveProduct(productToRemove);
            var finalProducts = cartPage.GetProductsInCart();

            Assert.That(finalProducts.Count, Is.LessThan(initialProducts.Count));
        }

        [Test]
        public void PurchaseFlow_ShouldReachPaymentPage()
        {
            var cartPage = new CartPage(Driver);
            var productsPage = new ProductsPage(Driver);
            var loginPage = new LoginPage(Driver);
            var user = UserFactory.GetValidUser();

            Driver.Navigate().GoToUrl("https://automationexercise.com/login");
            loginPage.Login(user);

            Driver.Navigate().GoToUrl("https://automationexercise.com/products");
            productsPage.AddFirstProductToCart();
            productsPage.GoToCartFromModal();

            cartPage.ProceedToCheckout();
            cartPage.KillPopups();
            cartPage.PlaceOrder("Testing order flow.");

            Assert.That(Driver.Url, Does.Contain("payment"));
        }

        [Test]
        [TestCase("Dress")]
        public void AddSpecificProduct_UsingFactory_ShouldVerifyDetails(string term)
        {
            var cartPage = new CartPage(Driver);
            var searchPage = new SearchPage(Driver);
            var productsPage = new ProductsPage(Driver);
            var criteria = SearchFactory.GetSearchCriteria(term);

            Driver.Navigate().GoToUrl("https://automationexercise.com/products");
            searchPage.PerformSearch(criteria);
            productsPage.AddFirstProductToCart();
            productsPage.GoToCartFromModal();

            var results = cartPage.GetProductsInCart();
            Assert.That(results.Any(p => p.Name.ToLower().Contains(term.ToLower())), Is.True);
        }
    }
}
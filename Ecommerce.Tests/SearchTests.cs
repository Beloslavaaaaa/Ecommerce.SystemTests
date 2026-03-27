using NUnit.Framework;
using Ecommerce.Infrastructure.Pages;
using Ecommerce.Infrastructure.Factories;
using Ecommerce.Infrastructure.DTOs;
using System.Linq;
using System.Collections.Generic;

namespace Ecommerce.Tests
{
    [TestFixture]
    public class SearchTests : BaseTest
    {
        [Test]
        [TestCase("Dress")]
        [TestCase("Tshirt")]
        [TestCase("Jean")]
        [TestCase("Sleeveless")]
        [TestCase("Cotton")]
        [TestCase("Blue Top")]
        public void Search_ByCriteria_ShouldReturnMatchingProducts(string term)
        {
            var criteria = SearchFactory.GetSearchCriteria(term);
            var searchPage = new SearchPage(Driver);

            Driver.Navigate().GoToUrl("https://automationexercise.com/products");

            searchPage.PerformSearch(criteria);

            List<ProductDto> actualProducts = searchPage.GetDisplayedProducts();

            Assert.Multiple(() =>
            {
                Assert.That(actualProducts.Count, Is.GreaterThan(0),
                    $"Search for '{term}' failed on URL: {Driver.Url}");

                if (term == "Blue Top")
                {
                    var expectedProduct = ProductFactory.GetBlueTop();
                    var actualBlueTop = actualProducts.FirstOrDefault(p => p.Name == expectedProduct.Name);

                    Assert.That(actualBlueTop, Is.Not.Null, "Blue Top DTO was not found in results.");
                    Assert.That(actualBlueTop.Price, Is.EqualTo(expectedProduct.Price));
                }

                bool matchFound = actualProducts.Any(p =>
                    p.Name.ToLower().Contains(term.ToLower()));

                Assert.That(matchFound, Is.True,
                    $"No products in the DTO list matched '{term}'");
            });
        }
    }
    }
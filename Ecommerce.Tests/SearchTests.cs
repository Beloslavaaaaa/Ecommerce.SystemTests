using NUnit.Framework;
using Ecommerce.Infrastructure.Pages;
using System.Linq;

namespace Ecommerce.Tests
{
    [TestFixture]
    public class SearchTests : BaseTest
    {
        [Test]
        [TestCase("Dress")]
        [TestCase("Tshirt")]
        public void Search_ByTerm_ShouldReturnRelevantProducts(string term)
        {
            var searchPage = new SearchPage(Driver);
            Driver.Navigate().GoToUrl("https://automationexercise.com/products");

            searchPage.PerformSearch(term);

            var results = searchPage.GetProductNames();

            Assert.Multiple(() =>
            {
                Assert.That(results.Count, Is.GreaterThan(0), $"Search results empty on: {Driver.Url}");

                bool matchFound = results.Any(n => n.ToLower().Contains(term.ToLower()));

                Assert.That(matchFound, Is.True, $"No results contained '{term}'. Found: {string.Join(", ", results.Take(3))}");
            });
        }
    }
}
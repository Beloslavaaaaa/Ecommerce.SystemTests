using System.Collections.Generic;
using Ecommerce.Infrastructure.DTOs;
using Ecommerce.Infrastructure.Factories;

namespace Ecommerce.Infrastructure.Factories
{
    public static class SearchTestDataFactory
    {
        public static IEnumerable<SearchOptionsDto> GetSixSearchQueries()
        {
            yield return SearchFactory.GetSearchCriteria("Blue Top");
            yield return SearchFactory.GetSearchCriteria("Dress");
            yield return SearchFactory.GetSearchCriteria("Tshirt");
            yield return SearchFactory.GetSearchCriteria("Jean");
            yield return SearchFactory.GetSearchCriteria("Sleeveless");
            yield return SearchFactory.GetSearchCriteria("Cotton");
        }
    }
}
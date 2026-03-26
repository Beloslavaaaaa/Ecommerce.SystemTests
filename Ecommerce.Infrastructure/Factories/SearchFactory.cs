namespace Ecommerce.Infrastructure.Factories
{
    using Ecommerce.Infrastructure.DTOs;

    public static class SearchFactory
    {
        public static SearchOptionsDto GetSearchCriteria(string term)
        {
            return new SearchOptionsDto
            {
                SearchTerm = term
            };
        }
    }
}
namespace Ecommerce.Infrastructure.Factories
{
    using Ecommerce.Infrastructure.DTOs;

    public static class ProductFactory
    {
        public static ProductDto GetBlueTop()
        {
            return new ProductDto
            {
                Name = "Blue Top",
                Price = "Rs. 500",
                Quantity = "1"
            };
        }
    }
}
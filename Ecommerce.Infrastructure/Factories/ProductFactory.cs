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

        public static ProductDto GetFancyGreenDress()
        {
            return new ProductDto
            {
                Name = "Fancy Green Dress",
                Price = "Rs. 1600",
                Quantity = "1"
            };
        }
    }
}
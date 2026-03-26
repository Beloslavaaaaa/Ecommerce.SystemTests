namespace Ecommerce.Infrastructure.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is ProductDto other)
            {
                return Name == other.Name && Price == other.Price;
            }
            return false;
        }

        public override int GetHashCode() => (Name, Price).GetHashCode();
    }
}
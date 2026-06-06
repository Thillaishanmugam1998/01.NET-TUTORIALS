using ProducesConsumesDemo.Models;

namespace ProducesConsumesDemo.Data
{
    public static class ProductRepository
    {
        private static readonly List<Product> Products =
        [
            new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 65000, InStock = true },
            new Product { Id = 2, Name = "Keyboard", Category = "Accessories", Price = 1200, InStock = true },
            new Product { Id = 3, Name = "Office Chair", Category = "Furniture", Price = 8500, InStock = false }
        ];

        public static IEnumerable<Product> GetAll()
        {
            return Products;
        }

        public static Product? GetById(int id)
        {
            return Products.FirstOrDefault(product => product.Id == id);
        }

        public static Product Add(ProductCreateRequest request)
        {
            var product = new Product
            {
                Id = Products.Max(product => product.Id) + 1,
                Name = request.Name,
                Category = request.Category,
                Price = request.Price,
                InStock = request.InStock
            };

            Products.Add(product);
            return product;
        }
    }
}

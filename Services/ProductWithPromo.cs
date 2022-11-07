using PostgreSQL.Models;
using PostgreSQL.Products;
using Services.Models;
using System.Diagnostics;

namespace Services
{
    public class ProductWithPromo : IProductWithPromo
    {

        private IReadProduct _readProduct;
        private IReadPromo _readPromo;
        public ProductWithPromo(IReadProduct readProduct, IReadPromo readPromo)
        {
            _readProduct = readProduct;
            _readPromo = readPromo;
        }
        public async Task<IEnumerable<PromoProductSer>> GetProductsWithPromo()
        {
            var products = await _readProduct.GetProductsWithPromo();
            return products.Select(product => new PromoProductSer
               {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    IsInStock = product.IsInStock,
                    PromoPrice = product.Percentage
                }
           );
        }

    }
}
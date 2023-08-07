using PostgreSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL.Products
{
    public interface IReadProduct
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProduct(Guid productId);
        public Task<bool> IsProductInStock(Guid productId);
        public Task<IEnumerable<PromoProduct>> GetProductsWithPromo();
    }
}

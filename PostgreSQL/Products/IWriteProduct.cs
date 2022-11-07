using PostgreSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL.Products
{
    public interface IWriteProduct
    {
        public Task AddProduct(NewProduct product);
        public Task UpdateProduct(Product product);
        public Task DeleteProduct(Guid productId);
    }
}

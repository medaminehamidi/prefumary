using PostgreSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL.Products
{
    public interface IWritePromo
    {
        public Task AddPromo(NewPromo product);
        public Task UpdatePromo(Promo product);
        public Task DeletePromo(Guid productId);
    }
}

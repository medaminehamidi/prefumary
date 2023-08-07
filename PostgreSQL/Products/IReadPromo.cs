using PostgreSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL.Products
{
    public interface IReadPromo
    {

        public Task<IEnumerable<Promo>> GetPromos();
        public Task<Promo> GetPromo(Guid promoId);
    }
}

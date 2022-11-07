using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL.Models
{
    public class PromoProduct
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public int IsInStock { get; set; }
        public int Percentage { get; set; }
    }
}


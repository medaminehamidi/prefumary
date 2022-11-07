using Npgsql;
using Dapper;
using PostgreSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PostgreSQL.Products
{
    public class ReadProduct : IReadProduct
    {
        private readonly IConfiguration _configuration;
        private NpgsqlConnection connection;
        private const string TABLE_NAME = "products";

        public ReadProduct(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = new NpgsqlConnection(_configuration.GetConnectionString("ProductAppCon"));
            connection.Open();
        }

        public async Task<Product> GetProduct(Guid Id)
        {
            string commandText = $"SELECT * FROM {TABLE_NAME} WHERE Id = @Id";
            var queryArgs = new { Id = Id };
            return await connection.QueryFirstAsync<Product>(commandText, queryArgs);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            string commandText = $"SELECT * FROM {TABLE_NAME}";
            return await connection.QueryAsync<Product>(commandText);
        }

        public async Task<bool> IsProductInStock(Guid productId)
        {
            string commandText = $"SELECT * FROM {TABLE_NAME} WHERE Id = @Id";
            var queryArgs = new { Id = productId };
            var result = await connection.QueryFirstAsync<Product>(commandText, queryArgs);
            return result.IsInStock != 0;
        }
        public async Task<IEnumerable<PromoProduct>> GetProductsWithPromo()
        {
            string commandText = $"SELECT p.id, p.price, p.\"name\", p.isinstock, s.percentage FROM products p FULL OUTER JOIN promos s ON p.id = s.productid;";
            return await connection.QueryAsync<PromoProduct>(commandText);
        }
    }
}

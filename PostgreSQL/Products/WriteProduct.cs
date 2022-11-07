using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using PostgreSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL.Products
{
    public class WriteProduct : IWriteProduct
    {
        private readonly IConfiguration _configuration;
        private NpgsqlConnection connection;
        private const string TABLE_NAME = "products";

        public WriteProduct(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = new NpgsqlConnection(_configuration.GetConnectionString("ProductAppCon"));
            connection.Open();
        }
        public async Task AddProduct(NewProduct product)
        {
            string commandText = $"INSERT INTO {TABLE_NAME} (Id, Name, Price, IsInStock) VALUES (@id, @name, @price, @isInStock)";

            var queryArguments = new
            {
                id = Guid.NewGuid(),
                name = product.Name,
                price = product.Price,
                isInStock = product.IsInStock
            };

            await connection.ExecuteAsync(commandText, queryArguments);
        }

        public async Task DeleteProduct(Guid productId)
        {
            string commandText = $"DELETE FROM {TABLE_NAME} WHERE ID=(@id)";

            var queryArguments = new { id = productId };

            await connection.ExecuteAsync(commandText, queryArguments);
        }

        public async Task UpdateProduct(Product product)
        {
            var commandText = $@"UPDATE {TABLE_NAME}
                SET Name = @name, Price = @price, IsInStock = @isInStock
                WHERE id = @id";

            var queryArgs = new
            {
                id = product.Id,
                name = product.Name,
                price = product.Price,
                isInStock = product.IsInStock
            };

            await connection.ExecuteAsync(commandText, queryArgs);
        }
    }
}

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
    public class WritePromo : IWritePromo
    {
        private readonly IConfiguration _configuration;
        private NpgsqlConnection connection;
        private const string TABLE_NAME = "promos";

        public WritePromo(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = new NpgsqlConnection(_configuration.GetConnectionString("ProductAppCon"));
            connection.Open();
        }
        public async Task AddPromo(NewPromo promo)
        {
            string commandText = $"INSERT INTO {TABLE_NAME} (Id, Name, Percentage, StartDate, EndDate, ProductId) VALUES (@id, @name, @percentage, @startDate, @endDate, @productId)";

            var queryArguments = new
            {
                id = Guid.NewGuid(),
                name = promo.Name,
                percentage = promo.Percentage,
                startDate = promo.StartDate,
                endDate = promo.EndDate,
                productId = promo.ProductId
            };

            await connection.ExecuteAsync(commandText, queryArguments);
        }

        public async Task DeletePromo(Guid promoId)
        {

            string commandText = $"DELETE FROM {TABLE_NAME} WHERE ID=(@id)";

            var queryArguments = new { id = promoId };

            await connection.ExecuteAsync(commandText, queryArguments);
        }

        public async Task UpdatePromo(Promo promo)
        {
            var commandText = $@"UPDATE {TABLE_NAME}
                SET Name = @name, Percentage = @percentage, startDate = @StartDate, endDate = @EndDate, productId = @ProductId
                WHERE id = @id";

            var queryArgs = new
            {
                id = promo.Id,
                name = promo.Name,
                percentage = promo.Percentage,
                startDate = promo.StartDate,
                endDate = promo.EndDate,
                productId = promo.ProductId
            };

            await connection.ExecuteAsync(commandText, queryArgs);
        }
    }
}

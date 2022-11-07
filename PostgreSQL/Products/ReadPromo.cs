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
    public class ReadPromo : IReadPromo
    {
        private readonly IConfiguration _configuration;
        private NpgsqlConnection connection;
        private const string TABLE_NAME = "promos";

        public ReadPromo(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = new NpgsqlConnection(_configuration.GetConnectionString("ProductAppCon"));
            connection.Open();
        }
        public async Task<IEnumerable<Promo>> GetPromos()
        {
            string commandText = $"SELECT * FROM {TABLE_NAME}";
            return await connection.QueryAsync<Promo>(commandText);
        }

        public async Task<Promo> GetPromo(Guid id)
        {
            string commandText = $"SELECT * FROM {TABLE_NAME} WHERE productId = @Id";
            var queryArgs = new { Id = id };
            return await connection.QueryFirstAsync<Promo>(commandText, queryArgs);
        }
    }
}

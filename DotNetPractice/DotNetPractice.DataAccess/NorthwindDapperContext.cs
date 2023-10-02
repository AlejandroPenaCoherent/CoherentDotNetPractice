using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetPractice.DataAccess
{
    public class NorthwindDapperContext
    {
        private readonly string _connectionString;

        public NorthwindDapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Northwind");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<List<TResult>> GetAsync<TResult>(string commandText, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = CreateConnection();

            var result = await connection.ExecuteReaderAsync(commandText, param: parameters, commandType: commandType);

            return result
                .Parse<TResult>()
                .AsList();
        }

        public async Task<int> ExecuteAsync(string commandText, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = CreateConnection();

            var numberOfRowsAffected = await connection.ExecuteAsync(commandText, param: parameters, commandType: commandType);

            return numberOfRowsAffected;
        }
    }
}

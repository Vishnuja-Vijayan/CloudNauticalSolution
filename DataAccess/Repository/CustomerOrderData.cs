
using System.Data;
using CloudNauticalSolution.Domain.AbstractClasses.IRepository;
using CloudNauticalSolution.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CloudNauticalSolution.DataAccess.Repository
{
    public class CustomerOrderData : ICustomerOrderData
    {
        private readonly string _connectionString;

        public CustomerOrderData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<CustomerOrderDetails>> GetRecentOrder(string customerId)
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<CustomerOrderDetails>("sp_GetLatestOrder",new { CustomerId = customerId },commandType: CommandType.StoredProcedure);
        }

    }
}

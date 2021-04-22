using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System.Collections.Generic;
using System.Data;

namespace MISA.Infrastructure.Repository
{
    public class CustomerGroupRepository : ICustomerGroupRepository
    {
        private IConfiguration _configuration;
        private string _connectionString;

        public CustomerGroupRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("database");
        }

        /// <summary>
        /// Lấy tất cả danh sách nhóm khách hàng.
        /// </summary>
        /// <returns>Danh sách nhóm khách hàng.</returns>
        public IEnumerable<CustomerGroup> GetCustomerGroups()
        {
            // Khởi tạo kết nối.
            // Thiết lập kết nối cơ sở dữ liệu.
            var connection = new MySqlConnection(_connectionString);

            var customerGroups = connection.Query<CustomerGroup>("Proc_GetCustomerGroups", commandType: CommandType.StoredProcedure);
            return customerGroups;
        }
    }
}

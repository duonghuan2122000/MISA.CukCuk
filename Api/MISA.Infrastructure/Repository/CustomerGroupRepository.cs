using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System.Collections.Generic;
using System.Data;

namespace MISA.Infrastructure.Repository
{
    /// <summary>
    /// Repository của nhóm khách hàng.
    /// </summary>
    /// CreatedBy: dbhuan (28/04/2021)
    public class CustomerGroupRepository : BaseRepository<CustomerGroup>, ICustomerGroupRepository
    {
        public CustomerGroupRepository(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Lấy tất cả danh sách nhóm khách hàng.
        /// </summary>
        /// <returns>Danh sách nhóm khách hàng.</returns>
        /// CreatedBy: dbhuan (28/04/2021)
        public IEnumerable<CustomerGroup> GetCustomerGroups()
        {
            // Khởi tạo kết nối.
            // Thiết lập kết nối cơ sở dữ liệu.
            var connection = new MySqlConnection(_connectionString);

            // Lấy dữ liệu và trả về.
            var customerGroups = connection.Query<CustomerGroup>("Proc_GetCustomerGroups", commandType: CommandType.StoredProcedure);
            return customerGroups;
        }
    }
}

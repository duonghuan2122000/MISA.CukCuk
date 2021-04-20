using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System.Collections.Generic;
using System.Data;

namespace MISA.Infrastructure.Repository
{
    public class CustomerGroupRepository : ICustomerGroupRepository
    {
        public IEnumerable<CustomerGroup> GetCustomerGroups()
        {
            // Khởi tạo kết nối.
            // Thiết lập kết nối cơ sở dữ liệu.
            var connectionString = "Server=47.241.69.179;Database=MF0_NVManh_CukCuk02;Uid=dev;Pwd=12345678;";
            var connection = new MySqlConnection(connectionString);

            var customerGroups = connection.Query<CustomerGroup>("Proc_GetCustomerGroups", commandType: CommandType.StoredProcedure);
            return customerGroups;
        }
    }
}

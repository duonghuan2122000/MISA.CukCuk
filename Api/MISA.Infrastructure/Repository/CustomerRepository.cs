using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Data;
using System.Linq;

namespace MISA.Infrastructure.Repository
{
    /// <summary>
    /// Repository của khách hàng.
    /// </summary>
    /// CreatedBy: dbhuan (20/04/2021)
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        /* private IConfiguration _configuration;
         private string _connectionString;*/

        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {

        }
        /// <summary>
        /// Kiểm tra sự tồn tại của mã khách hàng trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="customerCode">Mã khách hàng cần kiểm tra.</param>
        /// <param name="customerId">Id của khách hàng cần loại trừ.</param>
        /// <returns>Có hoặc không tồn tại mã khách hàng.</returns>
        /// CreatedBy: dbhuan (20/04/2021)
        public bool CheckCustomerCodeExist(string customerCode, Guid? customerId = null)
        {
            // Khởi tạo kết nối.
            // Thiết lập kết nối cơ sở dữ liệu.
            var connection = new MySqlConnection(_connectionString);

            // Check dữ liệu.
            var isCustomerCodeExist = connection.QueryFirstOrDefault<bool>("Proc_CheckCustomerCodeExists", new { customerCode, customerId }, commandType: CommandType.StoredProcedure);

            return isCustomerCodeExist;
        }

        /// <summary>
        /// Lấy danh sách khách hàng có lọc
        /// </summary>
        /// <param name="customerFilter">Điều kiện lọc danh sách khách hàng.</param>
        /// <returns>Danh sách khách hàng.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
        public Paging<Customer> GetCustomers(CustomerFilter customerFilter)
        {
            // Thiết lập kết nối cơ sở dữ liệu.
            var connection = new MySqlConnection(_connectionString);

            // Tính tổng số khách hàng có điều kiện.
            var totalRecord = connection.QueryFirstOrDefault<int>("Proc_GetTotalCustomers", customerFilter, commandType: CommandType.StoredProcedure);

            // Lấy danh sách khách hàng có phân trang.
            var customers = connection.Query<Customer>("Proc_GetCustomers", customerFilter, commandType: CommandType.StoredProcedure);

            // trả dữ liệu.
            var paging = new Paging<Customer>()
            {
                totalRecord = totalRecord,
                data = customers,
                page = customerFilter.page,
                pageSize = customerFilter.pageSize
            };
            return paging;
        }
    }
}

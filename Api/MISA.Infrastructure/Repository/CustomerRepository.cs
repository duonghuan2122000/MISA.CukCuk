using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Data;

namespace MISA.Infrastructure.Repository
{
    /// <summary>
    /// Repository của khách hàng.
    /// </summary>
    /// CreatedBy: dbhuan (20/04/2021)
    public class CustomerRepository : ICustomerRepository
    {
        private IConfiguration _configuration;
        private string _connectionString;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("database");
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
            var isCustomerCodeExist = connection.QueryFirstOrDefault<bool>("Proc_H_CheckCustomerCodeExists", new { customerCode, customerId }, commandType: CommandType.StoredProcedure);

            return isCustomerCodeExist;
        }

        /// <summary>
        /// Xóa một khách hàng.
        /// </summary>
        /// <param name="customerId">Id một khách hàng.</param>
        /// <returns>Số khách hàng bị xóa.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
        public int Delete(Guid customerId)
        {
            // Thiết lập kết nối cơ sở dữ liệu.
            var connection = new MySqlConnection(_connectionString);

            var rowsAffect = connection.Execute("Proc_DeleteCustomer", new { CustomerId = customerId }, commandType: CommandType.StoredProcedure);

            return rowsAffect;
        }

        /// <summary>
        /// Lấy thông tin một khách hàng theo id.
        /// </summary>
        /// <param name="customerId">Id khách hàng.</param>
        /// <returns>Thông tin một khách hàng.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
        public Customer GetCustomer(Guid customerId)
        {
            // Thiết lập kết nối cơ sở dữ liệu.
            var connection = new MySqlConnection(_connectionString);

            // Lấy thông tin khách hàng.
            var customer = connection.QueryFirstOrDefault<Customer>("Proc_H_GetCustomerById", new { customerId }, commandType: CommandType.StoredProcedure);
            return customer;
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
            var totalRecord = connection.QueryFirstOrDefault<int>("Proc_H_GetTotalCustomers", customerFilter, commandType: CommandType.StoredProcedure);

            // Tính tổng số trang.
            var totalPages = Math.Ceiling((decimal)totalRecord / customerFilter.pageSize);

            // Lấy danh sách khách hàng có phân trang.
            var customers = connection.Query<Customer>("Proc_H_GetCustomers", customerFilter, commandType: CommandType.StoredProcedure);
            var paging = new Paging<Customer>()
            {
                totalRecord = totalRecord,
                totalPages = (int)totalPages,
                data = customers,
                page = customerFilter.page,
                pageSize = customerFilter.pageSize
            };
            return paging;
        }

        /// <summary>
        /// Thêm mới một khách hàng.
        /// </summary>
        /// <param name="customer">Thông tin một khách hàng.</param>
        /// <returns>Số khách hàng thêm thành công.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
        public int Insert(Customer customer)
        {
            // Thiết lập kết nối cơ sở dữ liệu.
            var connection = new MySqlConnection(_connectionString);

            var rowsAffect = connection.Execute("Proc_InsertCustomer", customer, commandType: CommandType.StoredProcedure);

            return rowsAffect;
        }

        /// <summary>
        /// Cập nhật một khách hàng.
        /// </summary>
        /// <param name="customer">Thông tin một khách hàng.</param>
        /// <returns>Số khách hàng cập nhật thành công.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
        public int Update(Customer customer)
        {
            // Thiết lập kết nối cơ sở dữ liệu.
            var connection = new MySqlConnection(_connectionString);

            var rowsAffect = connection.Execute("Proc_UpdateCustomer", customer, commandType: CommandType.StoredProcedure);

            return rowsAffect;
        }
    }
}

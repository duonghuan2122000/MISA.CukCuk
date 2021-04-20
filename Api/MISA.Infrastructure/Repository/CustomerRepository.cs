using Dapper;
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
        public bool CheckCustomerCodeExist(string customerCode, Guid? customerId = null)
        {
            // Khởi tạo kết nối.
            // Thiết lập kết nối cơ sở dữ liệu.
            var connectionString = "Server=47.241.69.179;Database=MF0_NVManh_CukCuk02;Uid=dev;Pwd=12345678;";
            var connection = new MySqlConnection(connectionString);

            // Check dữ liệu.
            var isCustomerCodeExist = connection.QueryFirstOrDefault<bool>("Proc_H_CheckCustomerCodeExists", new { customerCode, customerId }, commandType: CommandType.StoredProcedure);

            return isCustomerCodeExist;
        }

        public int Delete(Guid customerId)
        {
            // Thiết lập kết nối cơ sở dữ liệu.
            var connectionString = "Server=47.241.69.179;Database=MF0_NVManh_CukCuk02;Uid=dev;Pwd=12345678;";
            var connection = new MySqlConnection(connectionString);

            var rowsAffect = connection.Execute("Proc_DeleteCustomer", new { CustomerId = customerId }, commandType: CommandType.StoredProcedure);

            return rowsAffect;
        }

        public Customer GetCustomer(Guid customerId)
        {
            // Thiết lập kết nối cơ sở dữ liệu.
            var connectionString = "Server=47.241.69.179;Database=MF0_NVManh_CukCuk02;Uid=dev;Pwd=12345678;";
            var connection = new MySqlConnection(connectionString);

            // Lấy thông tin khách hàng.
            var customer = connection.QueryFirstOrDefault<Customer>("Proc_H_GetCustomerById", new { customerId }, commandType: CommandType.StoredProcedure);
            return customer;
        }

        public Paging<Customer> GetCustomers(int page, int pageSize, string fullName, string phoneNumber, Guid? customerGroupId)
        {
            // Thiết lập kết nối cơ sở dữ liệu.
            var connectionString = "Server=47.241.69.179;Database=MF0_NVManh_CukCuk02;Uid=dev;Pwd=12345678;";
            var connection = new MySqlConnection(connectionString);

            // Tính tổng số khách hàng có điều kiện.
            var totalRecord = connection.QueryFirstOrDefault<int>("Proc_H_GetTotalCustomers", new { fullName, phoneNumber, customerGroupId }, commandType: CommandType.StoredProcedure);

            // Tính tổng số trang.
            var totalPages = Math.Ceiling((decimal)totalRecord / pageSize);

            // Lấy danh sách khách hàng có phân trang.
            var customers = connection.Query<Customer>("Proc_H_GetCustomers", new { page, pageSize, fullName, phoneNumber, customerGroupId }, commandType: CommandType.StoredProcedure);
            var paging = new Paging<Customer>()
            {
                totalRecord = totalRecord,
                totalPages = (int)totalPages,
                data = customers,
                page = page,
                pageSize = pageSize
            };
            return paging;
        }

        public int Insert(Customer customer)
        {
            // Thiết lập kết nối cơ sở dữ liệu.
            var connectionString = "Server=47.241.69.179;Database=MF0_NVManh_CukCuk02;Uid=dev;Pwd=12345678;";
            var connection = new MySqlConnection(connectionString);

            var rowsAffect = connection.Execute("Proc_InsertCustomer", customer, commandType: CommandType.StoredProcedure);

            return rowsAffect;
        }

        public int Update(Customer customer)
        {
            // Thiết lập kết nối cơ sở dữ liệu.
            var connectionString = "Server=47.241.69.179;Database=MF0_NVManh_CukCuk02;Uid=dev;Pwd=12345678;";
            var connection = new MySqlConnection(connectionString);

            var rowsAffect = connection.Execute("Proc_UpdateCustomer", customer, commandType: CommandType.StoredProcedure);

            return rowsAffect;
        }
    }
}

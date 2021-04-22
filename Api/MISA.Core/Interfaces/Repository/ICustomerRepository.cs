using MISA.Core.Entities;
using System;

namespace MISA.Core.Interfaces.Repository
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Lấy danh sách khách hàng có lọc
        /// </summary>
        /// <param name="customerFilter">Điều kiện lọc danh sách khách hàng.</param>
        /// <returns>Danh sách khách hàng.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
        public Paging<Customer> GetCustomers(CustomerFilter customerFilter);

        /// <summary>
        /// Lấy thông tin một khách hàng theo id.
        /// </summary>
        /// <param name="customerId">Id khách hàng.</param>
        /// <returns>Thông tin một khách hàng.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
        public Customer GetCustomer(Guid customerId);

        /// <summary>
        /// Thêm mới một khách hàng.
        /// </summary>
        /// <param name="customer">Thông tin một khách hàng.</param>
        /// <returns>Số khách hàng thêm thành công.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
        public int Insert(Customer customer);

        /// <summary>
        /// Cập nhật một khách hàng.
        /// </summary>
        /// <param name="customer">Thông tin một khách hàng.</param>
        /// <returns>Số khách hàng cập nhật thành công.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
        public int Update(Customer customer);

        /// <summary>
        /// Xóa một khách hàng.
        /// </summary>
        /// <param name="customerId">Id một khách hàng.</param>
        /// <returns>Số khách hàng bị xóa.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
        public int Delete(Guid customerId);

        /// <summary>
        /// Kiểm tra sự tồn tại của mã khách hàng trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="customerCode">Mã khách hàng cần kiểm tra.</param>
        /// <param name="customerId">Id của khách hàng cần loại trừ.</param>
        /// <returns>Có hoặc không tồn tại mã khách hàng.</returns>
        /// CreatedBy: dbhuan (20/04/2021)
        public bool CheckCustomerCodeExist(string customerCode, Guid? customerId = null);

    }
}

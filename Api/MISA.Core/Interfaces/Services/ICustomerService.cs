using MISA.Core.Entities;
using System;

namespace MISA.Core.Interfaces.Services
{
    /// <summary>
    /// Service phục vụ cho đối tượng khách hàng.
    /// </summary>
    /// CreatedBy: dbhuan (20/04/2021)
    public interface ICustomerService
    {
        /// <summary>
        /// Lấy danh sách khách hàng có lọc
        /// </summary>
        /// <param name="page">Trang hiện tại.</param>
        /// <param name="pageSize">Số khách hàng trên một trang.</param>
        /// <param name="fullName">Lọc theo tên khách hàng.</param>
        /// <param name="phoneNumber">Lọc theo số điện thoại.</param>
        /// <param name="customerGroupId">Lọc theo nhóm khách hàng.</param>
        /// <returns>Danh sách khách hàng.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
        public Paging<Customer> GetCustomers(int page, int pageSize, string fullName, string phoneNumber, Guid? customerGroupId);

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
    }

}

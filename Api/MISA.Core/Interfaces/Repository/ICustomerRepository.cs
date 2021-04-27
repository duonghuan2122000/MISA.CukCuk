using MISA.Core.Entities;
using System;

namespace MISA.Core.Interfaces.Repository
{
    public interface ICustomerRepository: IBaseRepository<Customer>
    {
        /// <summary>
        /// Lấy danh sách khách hàng có lọc
        /// </summary>
        /// <param name="customerFilter">Điều kiện lọc danh sách khách hàng.</param>
        /// <returns>Danh sách khách hàng.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
        public Paging<Customer> GetCustomers(CustomerFilter customerFilter);

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

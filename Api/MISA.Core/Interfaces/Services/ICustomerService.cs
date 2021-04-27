using MISA.Core.Entities;
using System;

namespace MISA.Core.Interfaces.Services
{
    /// <summary>
    /// Service phục vụ cho đối tượng khách hàng.
    /// </summary>
    /// CreatedBy: dbhuan (20/04/2021)
    public interface ICustomerService: IBaseService<Customer>
    {
        /// <summary>
        /// Lấy danh sách khách hàng có lọc
        /// </summary>
        /// <param name="customerFilter">Điều kiện lọc danh sách khách hàng.</param>
        /// <returns>Danh sách khách hàng.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
        public Paging<Customer> GetCustomers(CustomerFilter customerFilter);
    }

}

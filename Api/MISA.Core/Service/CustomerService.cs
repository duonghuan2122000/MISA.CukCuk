using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Service
{
    /// <summary>
    /// Các dịch vụ của khách hàng.
    /// </summary>
    /// CreatedBy: dbhuan (20/04/2021)
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository): base(customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Lấy danh sách khách hàng có lọc
        /// </summary>
        /// <param name="customerFilter">Điều kiện lọc danh sách khách hàng.</param>
        /// <returns>Danh sách khách hàng.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
        public Paging<Customer> GetCustomers(CustomerFilter customerFilter)
        {
            return _customerRepository.GetCustomers(customerFilter);
        }

        /// <summary>
        /// Phương thức valid dữ liệu.
        /// </summary>
        /// <param name="t">Thông tin thực thể cần valid dữ liệu.</param>
        /// <param name="isInsert">Tham số xác định trạng thái insert hoặc update.</param>
        protected override void Validate(Customer t, bool isInsert)
        {
            if(isInsert == false)
            {
                // Nếu là update dữ liệu

                // kiểm tra sự tồn tại của trường id khách hàng.
                CustomerException.CheckCustomerIdEmpty(t.CustomerId);
            }

            // kiểm tra sự tồn tại của trường mã khách hàng.
            CustomerException.CheckCustomerCodeEmpty(t.CustomerCode);

            // Biến xác định mã khách hàng đã tồn tại trên hệ thống chưa.
            bool isExists;
            if(isInsert == true)
            {
                // nếu là insert dữ liệu.
                isExists = _customerRepository.CheckCustomerCodeExist(t.CustomerCode);
            } else
            {
                // nếu là update dữ liệu.
                isExists = _customerRepository.CheckCustomerCodeExist(t.CustomerCode, t.CustomerId);
            }

            // nếu đã tồn tại mã khách hàng thì ném ra ngoại lệ CustomerException.
            if (isExists == true)
            {
                throw new CustomerException("Mã khách hàng đã tồn tại trên hệ thống.");
            }
        }
    }
}

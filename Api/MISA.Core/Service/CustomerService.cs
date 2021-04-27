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

        protected override void Validate(Customer t, bool isInsert)
        {
            if(isInsert == false)
            {
                CustomerException.CheckCustomerIdEmpty(t.CustomerId);
            }
            CustomerException.CheckCustomerCodeEmpty(t.CustomerCode);
            bool isExists;
            if(isInsert == true)
            {
                isExists = _customerRepository.CheckCustomerCodeExist(t.CustomerCode);
            } else
            {
                isExists = _customerRepository.CheckCustomerCodeExist(t.CustomerCode, t.CustomerId);
            }
            if (isExists == true)
            {
                throw new CustomerException("Mã khách hàng đã tồn tại trên hệ thống.");
            }
        }
    }
}

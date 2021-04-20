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
    public class CustomerService : ICustomerService
    {
        ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public int Delete(Guid customerId)
        {
            var rowsAffect = _customerRepository.Delete(customerId);
            return rowsAffect;
        }

        public Customer GetCustomer(Guid customerId)
        {
            var customer = _customerRepository.GetCustomer(customerId);
            return customer;
        }

        public Paging<Customer> GetCustomers(int page, int pageSize, string fullName, string phoneNumber, Guid? customerGroupId)
        {
            var paging = _customerRepository.GetCustomers(page, pageSize, fullName, phoneNumber, customerGroupId);
            return paging;
        }

        public int Insert(Customer customer)
        {
            // valid dữ liệu đầu vào.

            // kiểm tra có tồn tại mã khách hàng.
            CustomerException.CheckCustomerCodeEmpty(customer.CustomerCode);

            // check trùng mã.
            var isCustomerCodeExist = _customerRepository.CheckCustomerCodeExist(customer.CustomerCode);
            if(isCustomerCodeExist == true)
            {
                throw new CustomerException("Mã khách hàng đã tồn tại.");
            }

            var rowsAffect = _customerRepository.Insert(customer);
            return rowsAffect;
        }

        public int Update(Customer customer)
        {
            // valid dữ liệu.

            // kiểm tra có tồn tại id khách hàng.
            CustomerException.CheckCustomerIdEmpty(customer.CustomerId);

            // kiểm tra có tồn tại mã khách hàng.
            CustomerException.CheckCustomerCodeEmpty(customer.CustomerCode);

            // check trùng mà với khách hàng khác.
            var isCustomerCodeExist = _customerRepository.CheckCustomerCodeExist(customer.CustomerCode, customer.CustomerId);
            if (isCustomerCodeExist == true)
            {
                throw new CustomerException("Mã khách hàng đã tồn tại.");
            }
            var rowsAffect = _customerRepository.Update(customer);
            return rowsAffect;
        }
    }
}

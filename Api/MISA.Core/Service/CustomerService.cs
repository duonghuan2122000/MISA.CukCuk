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

        /// <summary>
        /// Xóa một khách hàng.
        /// </summary>
        /// <param name="customerId">Id một khách hàng.</param>
        /// <returns>Số khách hàng bị xóa.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
        public int Delete(Guid customerId)
        {
            var rowsAffect = _customerRepository.Delete(customerId);
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
            var customer = _customerRepository.GetCustomer(customerId);
            return customer;
        }

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
        public Paging<Customer> GetCustomers(int page, int pageSize, string fullName, string phoneNumber, Guid? customerGroupId)
        {
            var paging = _customerRepository.GetCustomers(page, pageSize, fullName, phoneNumber, customerGroupId);
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

        /// <summary>
        /// Cập nhật một khách hàng.
        /// </summary>
        /// <param name="customer">Thông tin một khách hàng.</param>
        /// <returns>Số khách hàng cập nhật thành công.</returns>
        /// CreatedBy: dbhuan(20/04/2021)
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

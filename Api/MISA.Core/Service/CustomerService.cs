using MISA.Core.AttributeCustom;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Text;

namespace MISA.Core.Service
{
    /// <summary>
    /// Các dịch vụ của khách hàng.
    /// </summary>
    /// CreatedBy: dbhuan (20/04/2021)
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
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
        protected override void CustomValidate(Customer t, bool isInsert)
        {
            // check customerCode đúng định dạng chưa.

            if (isInsert == false)
            {
                // Nếu là update dữ liệu

                // kiểm tra sự tồn tại của trường id khách hàng.
                if (string.IsNullOrEmpty(t.CustomerId.ToString()))
                {
                    // lấy thông lỗi mặc định.
                    var msgErrorRequiredDefault = Properties.Resources.MsgErrorRequired;

                    var sb = new StringBuilder();
                    sb.AppendFormat(msgErrorRequiredDefault, "Id khách hàng");
                    throw new ClientException(sb.ToString());
                }
            }

            // Biến xác định mã khách hàng đã tồn tại trên hệ thống chưa.
            bool isExists;
            if (isInsert == true)
            {
                // nếu là insert dữ liệu.
                isExists = _customerRepository.CheckCustomerCodeExist(t.CustomerCode);
            }
            else
            {
                // nếu là update dữ liệu.
                isExists = _customerRepository.CheckCustomerCodeExist(t.CustomerCode, t.CustomerId);
            }

            // nếu đã tồn tại mã khách hàng thì ném ra ngoại lệ CustomerException.
            if (isExists == true)
            {
                throw new ClientException("Mã khách hàng đã tồn tại trên hệ thống.");
            }
        }
    }
}

using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System.Collections.Generic;

namespace MISA.Core.Service
{
    /// <summary>
    /// Service của nhóm khách hàng.
    /// </summary>
    /// CreatedBy: dbhuan (28/04/2021)
    public class CustomerGroupService : BaseService<CustomerGroup>, ICustomerGroupService
    {
        /// <summary>
        /// repo của nhóm khách hàng.
        /// </summary>
        ICustomerGroupRepository _customerGroupRepository;

        /// <summary>
        /// Phương thức khởi tạo.
        /// </summary>
        /// <param name="customerGroupRepository">repo của nhóm khách hàng.</param>
        public CustomerGroupService(ICustomerGroupRepository customerGroupRepository) : base(customerGroupRepository)
        {
            _customerGroupRepository = customerGroupRepository;
        }

        /// <summary>
        /// Lấy tất cả danh sách nhóm khách hàng.
        /// </summary>
        /// <returns>Danh sách nhóm khách hàng.</returns>
        public IEnumerable<CustomerGroup> GetCustomerGroups()
        {
            var customerGroups = _customerGroupRepository.GetCustomerGroups();
            return customerGroups;
        }
    }
}

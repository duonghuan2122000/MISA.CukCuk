using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System.Collections.Generic;

namespace MISA.Core.Service
{
    public class CustomerGroupService : BaseService<CustomerGroup>, ICustomerGroupService
    {
        ICustomerGroupRepository _customerGroupRepository;

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

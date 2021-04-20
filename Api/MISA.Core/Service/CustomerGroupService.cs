using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System.Collections.Generic;

namespace MISA.Core.Service
{
    public class CustomerGroupService : ICustomerGroupService
    {
        ICustomerGroupRepository _customerGroupRepository;

        public CustomerGroupService(ICustomerGroupRepository customerGroupRepository)
        {
            _customerGroupRepository = customerGroupRepository;
        }

        public IEnumerable<CustomerGroup> GetCustomerGroups()
        {
            var customerGroups = _customerGroupRepository.GetCustomerGroups();
            return customerGroups;
        }
    }
}

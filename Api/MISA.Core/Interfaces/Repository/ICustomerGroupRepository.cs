using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    public interface ICustomerGroupRepository: IBaseRepository<CustomerGroup>
    {
        /// <summary>
        /// Lấy tất cả danh sách nhóm khách hàng.
        /// </summary>
        /// <returns>Danh sách nhóm khách hàng.</returns>
        public IEnumerable<CustomerGroup> GetCustomerGroups();
    }
}

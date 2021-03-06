using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    /// <summary>
    /// Interface repository của nhóm khách hàng.
    /// </summary>
    /// CreatedBy: dbhuan (28/04/2021)
    public interface ICustomerGroupRepository: IBaseRepository<CustomerGroup>
    {
        /// <summary>
        /// Lấy tất cả danh sách nhóm khách hàng.
        /// </summary>
        /// <returns>Danh sách nhóm khách hàng.</returns>
        public IEnumerable<CustomerGroup> GetCustomerGroups();
    }
}

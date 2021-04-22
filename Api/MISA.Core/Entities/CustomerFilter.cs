using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Query string các tham số truyền vào của enpoint lọc danh sách khách hàng;
    /// </summary>
    /// CreatedBy: dbhuan (22/04/2021)
    public class CustomerFilter
    {
        /// <summary>
        /// Trang hiện tại.
        /// </summary>
        public int page { get; set; } = 1;

        /// <summary>
        /// Số khách hàng trên một trang.
        /// </summary>
        public int pageSize { get; set; } = 10;

        /// <summary>
        /// Lọc theo tên.
        /// </summary>
        public string fullName { get; set; } = "";

        /// <summary>
        /// Lọc theo số điện thoại.
        /// </summary>
        public string phoneNumber { get; set; } = "";

        /// <summary>
        /// Lọc theo id nhóm khách hàng.
        /// </summary>
        public Guid? customerGroupId { get; set; } = null;
    }
}

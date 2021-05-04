using System;
using System.Collections.Generic;
using System.Linq;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Thông tin phân trang
    /// </summary>
    /// CreatedBy: dbhuan (20/04/2021)
    public class Paging<T>
    {
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int totalRecord { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int totalPages
        {
            get
            {
                return (int) Math.Ceiling((decimal) totalRecord / pageSize);
            }
        }

        /// <summary>
        /// Dữ liệu
        /// </summary>
        public IEnumerable<T>? data { get; set; }

        /// <summary>
        /// Trang hiện tại.
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// Số bản ghi trên một trang.
        /// </summary>
        public int pageSize { get; set; }
    }
}

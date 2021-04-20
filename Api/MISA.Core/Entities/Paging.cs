using System.Collections.Generic;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Thông tin phân trang
    /// </summary>
    /// CreatedBy: dbhuan (20/04/2021)
    public class Paging<T>
    {
        public int totalRecord { get; set; }

        public int totalPages { get; set; }

        public IEnumerable<T> data { get; set; }

        public int page { get; set; }

        public int pageSize { get; set; }
    }
}

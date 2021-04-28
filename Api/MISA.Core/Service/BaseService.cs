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
    /// Base service.
    /// </summary>
    /// <typeparam name="T">Một thực thể.</typeparam>
    /// CreatedBy: dbhuan (28/04/2021)
    public class BaseService<T> : IBaseService<T> where T : class
    {
        /// <summary>
        /// Base repository.
        /// </summary>
        private IBaseRepository<T> _baseRepository;

        /// <summary>
        /// Phương thức khởi tạo.
        /// </summary>
        /// <param name="baseRepository">Base repository</param>
        /// CreatedBy: dbhuan (28/04/2021)
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        /// <summary>
        /// Phương thức xóa một thực thể.
        /// </summary>
        /// <param name="id">id của thực thể cần xóa.</param>
        /// <returns></returns>
        /// CreatedBy: dbhuan (28/04/2021)
        public int Delete(Guid id)
        {
            return _baseRepository.Delete(id);
        }

        /// <summary>
        /// Lấy thông tin dữ liệu của một thực thể.
        /// </summary>
        /// <param name="id">id của thực thể cần lấy.</param>
        /// <returns></returns>
        /// CreatedBy: dbhuan (28/04/2021)
        public T Get(Guid id)
        {
            return _baseRepository.Get(id);
        }

        /// <summary>
        /// Phương thức thêm mới một thực thể vào csdl.
        /// </summary>
        /// <param name="t">thông tin của thực thể.</param>
        /// <returns></returns>
        /// CreatedBy: dbhuan (28/04/2021)
        public int Insert(T t)
        {
            // Valid dữ liệu.
            Validate(t);

            // Tiến hành thêm.
            return _baseRepository.Insert(t);
        }

        /// <summary>
        /// Cập nhật một thực thể.
        /// </summary>
        /// <param name="t">thông tin của thực thể</param>
        /// <returns></returns>
        /// CreatedBy: dbhuan (28/04/2021)
        public int Update(T t)
        {
            // Valid dữ liệu.
            Validate(t, false);

            // Tiến hành cập nhật
            return _baseRepository.Update(t);
        }

        /// <summary>
        /// Phương thức valid dữ liệu
        /// </summary>
        /// <param name="t">Thông tin của thực thể.</param>
        /// <param name="isInsert">Tham số xác định insert hoặc update.</param>
        /// CreatedBy: dbhuan (28/04/2021)
        protected virtual void Validate(T t, bool isInsert = true)
        {

        }
    }
}

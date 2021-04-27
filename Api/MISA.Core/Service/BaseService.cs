using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Service
{
    public class BaseService<T> : IBaseService<T> where T : class
    {

        private IBaseRepository<T> _baseRepository;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public int Delete(Guid id)
        {
            return _baseRepository.Delete(id);
        }

        public T Get(Guid id)
        {
            return _baseRepository.Get(id);
        }

        public int Insert(T t)
        {
            Validate(t);
            return _baseRepository.Insert(t);
        }

        public int Update(T t)
        {
            Validate(t, false);
            return _baseRepository.Update(t);
        }

        protected virtual void Validate(T t, bool isInsert = true)
        {

        }
    }
}

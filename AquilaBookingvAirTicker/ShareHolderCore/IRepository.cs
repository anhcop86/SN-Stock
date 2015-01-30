using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore
{
    public interface IRepository<T>
    {
        void Save(T entity);
        void Update(T entity);
        void Delete(T entiy);
        T GetById(Guid id);
        T GetById(int id);
        IList<T> GetAll();
    }

   
}

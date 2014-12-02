using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockApiReatime.Nhibernate
{
    public interface IRepository<T>
    {
        void Save(T entity);
        void Update(T entity);
        void Delete(T entiy);
        //T GetById(Guid id);
        T GetById(object id);
        IList<T> GetAll();
    }
}
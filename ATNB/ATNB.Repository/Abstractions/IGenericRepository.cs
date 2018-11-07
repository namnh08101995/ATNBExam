using ATNB.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ATNB.Repository.Abstractions
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}

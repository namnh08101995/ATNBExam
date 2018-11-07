using ATNB.Model;
using System.Collections.Generic;

namespace ATNB.Service.Abstractions
{
    public interface IEntityService<T> : IService where T : BaseEntity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
    }
}

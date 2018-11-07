using System.ComponentModel.DataAnnotations;

namespace ATNB.Model
{
    public abstract class BaseEntity
    {

    }
    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        [StringLength(7)]
        public virtual T Id { get; set; }
    }
}

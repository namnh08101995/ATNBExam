using System.ComponentModel.DataAnnotations;

namespace ATNB.Model
{
    public abstract class BaseEntity
    {

    }
    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        [RegularExpression(@"^FW\d{5}$|^RW\d{5}$|^AP\d{5}$", 
            ErrorMessage = "ID is a string of 7 characters, started by AP, followed by 5 digits.")]
        [StringLength(7)]
        public virtual T Id { get; set; }
    }
}

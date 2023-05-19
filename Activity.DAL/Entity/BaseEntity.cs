using System;
using System.ComponentModel.DataAnnotations;

namespace Activity.DAL.Entity
{
    public abstract class BaseEntity<TKey> : BaseEntity, IBaseEntity<TKey>
    {
        [Key]
        public virtual TKey Id { get; set; }

        public override object GetIdValue()
        {
            return Id;
        }
    }

    public abstract class BaseEntity : IBaseEntity
    {
        public abstract object GetIdValue();
    }

    public interface IBaseEntity<TKey> : IBaseEntity
    {
        TKey Id { get; set; }
    }

    public interface IBaseEntity
    {
        object GetIdValue();
    }
}

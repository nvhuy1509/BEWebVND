using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Activity.DAL.Entity
{
    [Table("ga_premission")]
    public class Premission : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public int? Type { get; set; }
        public string? Description { get; set; }
       
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}

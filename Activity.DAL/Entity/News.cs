using System;
using System.ComponentModel.DataAnnotations.Schema;
using Activity.DAL.Enum;

namespace Activity.DAL.Entity
{
    [Table("News")]
    public class News : BaseEntity<Guid>
    {
        public string nameNews { get; set; }
        public string content { get; set; }
        public string thums { get; set; }
        public int status { get; set; }
    }
}

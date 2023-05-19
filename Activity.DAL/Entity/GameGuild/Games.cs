using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Activity.DAL.Entity.GameGuild
{
    [Table("game")]
    public class Games:BaseEntity<Guid>
    {
        public string Name { get; set; }
        public int SharePercentDefault { get; set; }
        public int SharePercentMax { get; set; }
        public DateTime TimeReset { get; set; }
        public string Thumb { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Status { get; set; }
    }
}

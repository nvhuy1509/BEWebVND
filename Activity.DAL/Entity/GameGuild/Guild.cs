using System;
using System.ComponentModel.DataAnnotations.Schema;
using Activity.DAL.Enum;

namespace Activity.DAL.Entity.GameGuild
{
    [Table("guild")]
    public class Guild : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thumb { get; set; }
        public GAStatus Status { get; set; }
        public Guid GameId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        [NotMapped]
        public string Gamename { get; set; }

    }
}

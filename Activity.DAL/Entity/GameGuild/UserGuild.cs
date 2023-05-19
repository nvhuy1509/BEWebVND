using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Activity.DAL.Enum;

namespace Activity.DAL.Entity.GameGuild
{
    [Table("user_guild")]
    public class UserGuild : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid GuildId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }

        [DefaultValue(0)]
        public GAPremission Status { get; set; }

        [NotMapped]
        public string UserName { get; set; }

    }
}

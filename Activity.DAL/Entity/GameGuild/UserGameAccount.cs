using System;
using System.ComponentModel.DataAnnotations.Schema;
using Activity.DAL.Enum;

namespace Activity.DAL.Entity.GameGuild
{
    [Table("user_game_account")]
    public class UserGameAccount : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid GameAccountId { get; set; }
        public Guid GameId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}

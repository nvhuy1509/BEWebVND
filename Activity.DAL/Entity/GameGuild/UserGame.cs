using System;
using System.ComponentModel.DataAnnotations.Schema;
using Activity.DAL.Enum;

namespace Activity.DAL.Entity.GameGuild
{
    [Table("user_game")]
    public class UserGame : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public GAStatus Status { get; set; }
        public Guid ConfirmBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }

        [NotMapped]
        public string UserName { get; set; }

    }
}

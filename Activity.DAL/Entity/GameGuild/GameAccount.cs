using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Activity.DAL.Entity.GameGuild
{
    [Table("game_account")]
    public class GameAccount:BaseEntity<Guid>
    {
        public Guid GameId { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Status { get; set; }
    }
}

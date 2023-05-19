using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Activity.DAL.Entity.GameGuild
{
    [Table("game_token")]
    public class GameToken : BaseEntity<Guid>
    {
        public Guid GameId { get; set; }
        public Guid TokenId { get; set; }
        public double Kpi { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}

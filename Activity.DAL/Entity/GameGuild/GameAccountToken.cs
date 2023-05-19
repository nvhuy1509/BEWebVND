using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Activity.DAL.Entity.GameGuild
{
    [Table("game_account_token")]
    public class GameAccountToken:BaseEntity<Guid>
    {
        public Guid GameAccountId { get; set; }
        public Guid TokenId { get; set; }
        public string WalletAddress { get; set; }
        public int TotalToken { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Status { get; set; }
    }
}

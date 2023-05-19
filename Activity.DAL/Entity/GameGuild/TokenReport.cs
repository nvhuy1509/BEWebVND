using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Activity.DAL.Entity.GameGuild
{
    [Table("token_report")]
    public class TokenReport:BaseEntity<Guid>
    {
        public Guid GameAccountId { get; set; }
        public Guid TokenId { get; set; }
        public float TotalToken { get; set; }
        public string ImageReport { get; set; }
        public float DailyToken { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateDateInt { get; set; }
    }
    public class ImageReport
    {
        public string Url { get; set; }
    }
}

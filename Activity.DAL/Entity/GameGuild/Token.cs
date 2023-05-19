using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Activity.DAL.Entity.GameGuild
{
    [Table("token")]
    public class Token:BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string CodeToken { get; set; }
        public string ContractToken { get; set; }
        public int OrderIndex { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        //public string Status { get; set; }
    }
}

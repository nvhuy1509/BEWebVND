using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Activity.DAL.Entity.GameGuild
{
    [Table("property_report")]
    public class PropertyReport : BaseEntity<Guid>
    {
        public Guid GameAccountId { get; set; }
        

        public Guid ComfirmBy { get; set; }
        public string Property { get; set; }
        public string PropertyFromImage { get; set; }
        public string PropertyFromAdmin { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateDateInt { get; set; }
    }
}

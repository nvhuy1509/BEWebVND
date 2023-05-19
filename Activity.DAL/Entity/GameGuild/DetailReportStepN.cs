using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Activity.DAL.Entity.GameGuild
{
    [Table("DetailReportStepN")]
    public class DetailReportStepN:BaseEntity<Guid>
    {
        public Guid GameAccountId { get; set; }
        public Guid ComfirmBy { get; set; }
        public int CreateDateInt { get; set; }
        public string idShoes { get; set; }
        public string quality { get; set; }
        public string type { get; set; }
        public float tokenTotal { get; set; }
        public float efficiency { get; set; }
        public float comfort { get; set; }
        public float resilience { get; set; }
        public float luck { get; set; }
        public float level { get; set; }
        public string date { get; set; }
        public float tokenRun { get; set; }
        public int km { get; set; }
        public string time { get; set; }
        public int steps { get; set; }
        public float energy { get; set; }
        public int durability { get; set; }
        [NotMapped]
        public int paid { get; set; }

    }
}

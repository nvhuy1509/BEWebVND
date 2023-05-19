using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Activity.DAL.Entity
{
    [Table("ga_levels")]
    public class Level : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int OrderIndex { get; set; }
        public int Thutusapxep { get; set; }
        

        public string ThumbnailUrl { get; set; }

        [NotMapped]
        public List<string> ClassOfBook { get; set; }
        [NotMapped]
        public List<string> ClassUnmapBook { get; set; }
    }
}

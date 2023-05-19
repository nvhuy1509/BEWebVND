using System;
using System.ComponentModel.DataAnnotations.Schema;
using Activity.DAL.Enum;

namespace Activity.DAL.Entity
{
    [Table("user")]
    public class UserInformation : BaseEntity<Guid>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public GAStatus Status { get; set; }

        public int? role { get; set; }

        public DateTime? CreatedDate { get; set; }        
        public DateTime? UpdateDate { get; set; }        

        
    }
}

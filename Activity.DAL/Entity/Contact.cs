using Activity.DAL.Enum;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace Activity.DAL.Entity
{
    [Table("user_contact")]
    public class Contact : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public string Discord { get; set; }

        public string Email { get; set; }

        public string Facebook { get; set; }
        public string Mobile { get; set; }
        public string Telegram { get; set; }
        public string IndentityId { get; set; }
        public string Image_back { get; set; }
        public string Image_front { get; set; }
        public GAStatus Status { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}

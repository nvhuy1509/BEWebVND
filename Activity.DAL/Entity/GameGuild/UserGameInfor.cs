using Activity.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.DAL.Entity.GameGuild
{
    public class UserGameInfor
    {
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public string GameName { get; set; }
        public string UserName { get; set; }
       
        public int countAcc { get; set; }
        public GAStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ConfirmByName { get; set; }
    }
}

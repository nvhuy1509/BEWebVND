using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Minxtu.DAL.Entity
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }

        public string Name { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
    }
    
}
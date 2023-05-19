using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myG.GameGuild.Models
{
    public class UploadModel
    {
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }

    }
}

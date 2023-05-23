using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace myG.GameGuild
{
    public class AvatarUserFileUpload
    {
        [DataType(DataType.Upload)]
        //[MaxFileSize(5  1024  1024)]
        // [AllowedExtensions(new[] { ".jpg", ".png" })]
        public IFormFile Avatar { get; set; }
    }
    public class ItemGame
    {
        [DataType(DataType.Upload)]
        //[MaxFileSize(5  1024  1024)]
        // [AllowedExtensions(new[] { ".jpg", ".png" })]
        public IFormFile Item { get; set; }
        public int IdUser { get; set; }
    }
    public class ItemGameTiny
    {
        [DataType(DataType.Upload)]
        //[MaxFileSize(5  1024  1024)]
        // [AllowedExtensions(new[] { ".jpg", ".png" })]
        public IFormFile File { get; set; }
        public int IdUser { get; set; }
    }
}

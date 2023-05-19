using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace myG.GameGuild
{
    public static class FormFileExtensions
    {
        public static byte[] GetBytes(this IFormFile formFile)
        {
            byte[] b;
            using (BinaryReader br = new BinaryReader(formFile.OpenReadStream()))
            {
                b = br.ReadBytes((int)formFile.OpenReadStream().Length);
                return b;
            }
        }
    }
}

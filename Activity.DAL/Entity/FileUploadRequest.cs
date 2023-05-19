using System;
namespace Activity.DAL.Entity
{
    public class FileUploadRequest
    {
        public Guid UserId { get; set; }

        public Guid ChildId { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }

        public byte[] File { get; set; }

        public string Path { get; set; }
    }
}

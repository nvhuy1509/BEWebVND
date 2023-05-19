using System;
namespace Activity.DAL.Entity.WebuxAff
{
    public class Tinh
    {
       public int id { get; set; }
       public int orderby { get; set; }
       public string name { get; set; }
       public string code { get; set; }
       public string status { get; set; }
       public string url { get; set; }
    }
    public class Huyen
    {
        public int id { get; set; }
        public int tinh_id { get; set; }
        public int orderby { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string status { get; set; }
        public string url { get; set; }
    }
    public class Truong
    {
        public int id { get; set; }
        public int tinh_id { get; set; }
        public int huyen_id { get; set; }
        public int orderby { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string status { get; set; }
        public string url { get; set; }
        public string type { get; set; }
    }
}

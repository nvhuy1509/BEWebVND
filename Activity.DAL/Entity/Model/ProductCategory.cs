namespace Minxtu.DAL.Entity
{
    public class ProductCategory {

        public string UrlRewrite { get; set; }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }

        private int _orderNumber;

        public int OrderNumber
        {
            get { return _orderNumber; }
            set { _orderNumber = value; }
        }

        private int _parentId;

        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        private byte _status;

        public byte Status
        {
            get { return _status; }
            set { _status = value; }
        }

    }
}
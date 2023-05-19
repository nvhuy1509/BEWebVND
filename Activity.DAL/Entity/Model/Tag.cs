namespace Minxtu.DAL.Entity
{
    public class Tag
    {

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _tagName;

        public string TagName
        {
            get { return _tagName; }
            set { _tagName = value; }
        }

        private string _productList;

        public string ProductList
        {
            get { return _productList; }
            set { _productList = value; }
        }

        private int _count;

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        private byte _status;

        public byte Status
        {
            get { return _status; }
            set { _status = value; }
        }


    }
}
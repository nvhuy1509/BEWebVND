namespace Minxtu.DAL.Entity
{
    public class WebLink
    {

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

        private string _link;

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private byte _status;

        public byte Status
        {
            get { return _status; }
            set { _status = value; }
        }


    }

    public class ShopTable
    {
        public string TABLE_NAME { get; set; }
    }
}
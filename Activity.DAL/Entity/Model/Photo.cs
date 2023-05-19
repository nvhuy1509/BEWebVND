namespace Minxtu.DAL.Entity
{
    public class Photo
    {

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _albumId;

        public int AlbumId
        {
            get { return _albumId; }
            set { _albumId = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }

        private int _pageView;

        public int PageView
        {
            get { return _pageView; }
            set { _pageView = value; }
        }

        private System.DateTime _createTime;

        public System.DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        private int _status;

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }


    }
}
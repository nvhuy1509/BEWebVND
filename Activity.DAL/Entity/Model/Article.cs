namespace Minxtu.DAL.Entity
{
    public class Article
    {
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

        private string _mainContent;

        public string MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _thumb = string.Empty;

        public string Thumb
        {
            get { return _thumb; }
            set { _thumb = value; }
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

        private System.DateTime _updateTime;

        public System.DateTime UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }

        private int _menuId;

        public int MenuId
        {
            get { return _menuId; }
            set { _menuId = value; }
        }

        private int _status;

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }



    }
}
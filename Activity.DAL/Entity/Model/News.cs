using System;

namespace Minxtu.DAL.Entity
{
    public class News {

        public string UrlRewrite { get; set; }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _categoryId;

        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        //private string _url;

        //public string Url
        //{
        //    get { return _url; }
        //    set { _url = value; }
        //}
        private string _description = string.Empty;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _mainContent = string.Empty;

        public string MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; }
        }
        private string _metaDescription = string.Empty;
        public string MetaDescription {
            get { return _metaDescription; }
            set { _metaDescription = value; }
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

        public System.String StrCreateTime
        {
            get { return string.Format("{0}",CreateTime.ToString("dd/MM/yyyy"));}
        }
        private System.DateTime _updateTime;

        public System.DateTime UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }

        private int _status;

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private int _displayNo;

        public int DisplayNo
        {
            get { return _displayNo; }
            set { _displayNo = value; }
        }
    }
}
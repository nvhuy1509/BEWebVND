namespace Minxtu.DAL.Entity
{
    public class Menu {


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

        private string _description = string.Empty;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Icon { get; set; }

        private int _orderNumber;

        public int OrderNumber
        {
            get { return _orderNumber; }
            set { _orderNumber = value; }
        }

        private string _linkMenu;

        public string LinkMenu
        {
            get { return _linkMenu; }
            set { _linkMenu = value; }
        }

        private int _articleId;

        public int ArticleId
        {
            get { return _articleId; }
            set { _articleId = value; }
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
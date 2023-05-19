namespace Minxtu.DAL.Entity
{
    public class KeywordLink
    {

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _keyword;

        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; }
        }

        private string _url;

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        private byte _status;

        public byte Status
        {
            get { return _status; }
            set { _status = value; }
        }


    }
}
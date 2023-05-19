using System;

namespace Minxtu.DAL.Entity
{
    [Serializable]
    public class Product {

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

        private string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _tags = string.Empty;

        public string Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        private string _image = string.Empty;

        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }

        private string _imageList = string.Empty;

        public string ImageList
        {
            get { return _imageList; }
            set { _imageList = value; }
        }

        private string _description = string.Empty;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }


        public string MetaDescription { get; set; }

        private string _mainContent = string.Empty;

        public string MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; }
        }

        public int ProductHot { get; set; }

        private string _shape = string.Empty;

        public string Shape
        {
            get { return _shape; }
            set { _shape = value; }
        }

        private string _color = string.Empty;

        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        private int _colorType;

        public int ColorType
        {
            get { return _colorType; }
            set { _colorType = value; }
        }

        private string _size = string.Empty;

        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }

        private string _weight = string.Empty;

        public string Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        private string _material = string.Empty;

        public string Material
        {
            get { return _material; }
            set { _material = value; }
        }

        private string _purity = string.Empty;

        public string Purity
        {
            get { return _purity; }
            set { _purity = value; }
        }

        private string _processor = string.Empty;

        public string Processor
        {
            get { return _processor; }
            set { _processor = value; }
        }

        private double _price;

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private double _priceDiscount;

        public double PriceDiscount
        {
            get { return _priceDiscount; }
            set { _priceDiscount = value; }
        }


        private string _priceString = string.Empty;

        public string PriceString
        {
            get { return _priceString; }
            set { _priceString = value; }
        }

        private int _pageView;

        public int PageView
        {
            get { return _pageView; }
            set { _pageView = value; }
        }

        private byte _productStatus;

        public byte ProductStatus
        {
            get { return _productStatus; }
            set { _productStatus = value; }
        }

        private byte _status;

        public byte Status
        {
            get { return _status; }
            set { _status = value; }
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


        //private int _id;

        //public int Id
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}

        //private int _categoryId;

        //public int CategoryId
        //{
        //    get { return _categoryId; }
        //    set { _categoryId = value; }
        //}

        //private string _code;

        //public string Code
        //{
        //    get { return _code; }
        //    set { _code = value; }
        //}

        //public string Name { get; set; }

        //private string _image = string.Empty;

        //public string Image
        //{
        //    get { return _image; }
        //    set { _image = value; }
        //}

        //private string _imageList = string.Empty;

        //public string ImageList
        //{
        //    get { return _imageList; }
        //    set { _imageList = value; }
        //}

        //private string _description;

        //public string Description
        //{
        //    get { return _description; }
        //    set { _description = value; }
        //}

        //private string _mainContent;

        //public string MainContent
        //{
        //    get { return _mainContent; }
        //    set { _mainContent = value; }
        //}

        //private string _size = string.Empty;

        //public string Size
        //{
        //    get { return _size; }
        //    set { _size = value; }
        //}

        //private double _weight;

        //public double Weight
        //{
        //    get { return _weight; }
        //    set { _weight = value; }
        //}

        //private string _material = string.Empty;

        //public string Material
        //{
        //    get { return _material; }
        //    set { _material = value; }
        //}

        //private double _price;

        //public double Price
        //{
        //    get { return _price; }
        //    set { _price = value; }
        //}

        //private string _priceString = string.Empty;

        //public string PriceString
        //{
        //    get { return _priceString; }
        //    set { _priceString = value; }
        //}
        //private int _pageView;

        //public int PageView
        //{
        //    get { return _pageView; }
        //    set { _pageView = value; }
        //}

        //private byte _status;

        //public byte Status
        //{
        //    get { return _status; }
        //    set { _status = value; }
        //}
        
    }

    [Serializable]
    public class ProductColor
    {
        public string Color { get; set; }

    }

    [Serializable]
    public class ProductMaterial
    {
        public string Material { get; set; }
    }
}
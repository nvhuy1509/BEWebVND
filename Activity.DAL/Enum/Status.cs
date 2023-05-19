using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Activity.DAL.Enum
{
    public enum StudentPremission
    {
        None = 0,
        [Display(Name = "Lớp trưởng")]
        Loptruong = 1,
        [Display(Name = "Lớp phó")]
        Loppho = 2,
        [Display(Name = "Tổ trưởng")]
        Totruong = 3,
        [Display(Name = "Quản ca")]
        Quanca = 4,
    }
    public enum GameModeEnum
    {
        [Display(Name = "Oneway")]
        Oneway = 1,
        [Display(Name = "Twoway")]
        Twoway = 2,
        [Display(Name = "Random")]
        Random = 3
    }
    public enum ContactTypeEnum
    {
        None = 0,
        Parent = 10,
        Teacher = 20,
    }

    public enum UserStatus
    {
        InActive = 0,
        Active = 1,
    }

    public enum UserType
    {
        Scholar = 20,
        Manager = 30,
        CenterAdmin = 40,
    }


    //public enum UserStatus
    //{
    //    InActive = 0,
    //    Active = 1,
    //}
    public enum AdminStatus
    {
        Active = 0,
        InActive = 1
    }
    public enum NewsStatus
    {
        Active = 0,
        InActive = 1,
        Banner = 2
    }
    public enum MenuStatus
    {
        Active = 0,
        InActive = 1
    }
    public enum NewsCategoryStatus
    {
        Active = 0,
        InActive = 1
    }
    public enum ArticleStatus
    {
        Active = 0,
        InActive = 1
    }
    public enum ProductStatus
    {
        Active = 0,
        InActive = 1,
        Hot = 2
    }
    public enum ProductCategoryStatus
    {
        Active = 0,
        InActive = 1
    }

    public enum WebAdStatus
    {
        Active = 0,
        InActive = 1
    }
    public enum WebLinkStatus
    {
        Active = 0,
        InActive = 1
    }
    public enum WebAdPosition
    {
        Top = 0,
        Bottom = 1,
        Left = 2,
        Right = 3
    }

    public enum TagStatus
    {
        Active = 0,
        InActive = 1
    }

    public enum KeywordLinkStatus
    {
        Active = 0,
        InActive = 1
    }
    public enum ProductStockStatus
    {
        InStock = 0,  //còn hàng
        SoldOut = 1 //hết hàng
    }
    public enum ProductColor
    {
        Green = 1,
        Red = 2,
        Yellow = 3,
        Pink = 4,
        Blue = 7
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
    public enum GAStatus : int
    {
        [Display(Name = "InActive")]
        InActive = 0,

        [Display(Name = "Active")]
        Active = 1,

        [Display(Name = "Delete")]
        Delete = 99,

        [Display(Name = "Mapping")]
        Mapping = 88

    }
    public enum GAPremission : int
    {
        [Display(Name = "user")]
        UserGuild = 0,

        [Display(Name = "guild manage")]
        ManagerGuild = 1,

        

    }

    public enum Menh
    {
        Kim, Mộc, Thủy, Hỏa, Thổ
    }
}

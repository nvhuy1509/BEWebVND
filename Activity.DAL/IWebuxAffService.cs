using System;
using System.Collections.Generic;
using Activity.DAL.Entity.WebuxAff;

namespace Activity.DAL
{
    public interface IWebuxAffService
    {
        Tinh SelectTinhById(long id);
        List<Tinh> SelectAllTinh();
        List<Huyen> SelectAllHuyen();
        List<Truong> SelectAllTruong();
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using Activity.DAL.Entity.WebuxAff;
using MyGo.Core;
using MySql.Data.MySqlClient;

namespace Activity.DAL
{
    public class WebuxAffService : IWebuxAffService
    {
        private readonly MySqlDBHelper db = new MySqlDBHelper(MyGo.Core.Config.PortalWebConnStr);

        public List<Tinh> SelectAllTinh()
        {
            var command = new MySqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Tinh_Get_List";
            return db.GetList<Tinh>(command);
        }
        public List<Huyen> SelectAllHuyen()
        {
            var command = new MySqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Huyen_Get_List";
            return db.GetList<Huyen>(command);
        }
        public List<Truong> SelectAllTruong()
        {
            var command = new MySqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Truong_Get_List";
            return db.GetList<Truong>(command);
        }

        public Tinh SelectTinhById(long id)
        {   
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Activity.DAL;
using Minxtu.DAL.Entity;
using MyGo.Core;
using MyGo.Core.Logs;
using MyGo.Core.Utils;
using Config = Minxtu.DAL.Entity.Config;

namespace Minxtu.DAL
{

    public class DataAccessSQLServerService : IDataAccessSQLServerService
    {
        private readonly DBHelper db = new DBHelper(MyGo.Core.Config.PortalWebConnStr);

        public DalResult InsertAdmin(Admin obj)
        {
            const string spName = "sp_Admin_Insert";
            var parameters = new SqlParameter[]
                                 {
                                     new SqlParameter("@Id", obj.Id),
                                     new SqlParameter("@UserName", obj.UserName),
                                     new SqlParameter("@Password", obj.Password),
                                     new SqlParameter("@FullName", obj.FullName),
                                     new SqlParameter("@Level", obj.Level),
                                     new SqlParameter("@Status", obj.Status),
                                 };
            var p = parameters[0];
            p.Direction = ParameterDirection.Output;
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            var id = (int) p.Value;
            if (id > 0)
            {
                dalResult.IsSuccess = true;
                dalResult.NewRowId = id;
            }
            return dalResult;
        }

        public DalResult UpdateAdmin(long id, Admin obj)
        {
            const string spName = "sp_Admin_Update";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@UserName", obj.UserName),
                                        new SqlParameter("@Password", obj.Password),
                                        new SqlParameter("@FullName", obj.FullName),
                                        new SqlParameter("@Level", obj.Level),
                                        new SqlParameter("@Status", obj.Status),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult DeleteAdmin(long id)
        {
            const string spName = "sp_Admin_Delete";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public Admin SelectAdminById(long id)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Admin_GetById";
            var pId = new SqlParameter("@Id", id);
            command.Parameters.Add(pId);
            var lists = db.GetList<Admin>(command);
            if (lists.Count == 1)
                return lists[0];
            return null;
        }

        public List<Admin> SelectAllAdmin()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Admin_Get_List";
            return db.GetList<Admin>(command);
        }

        public List<Admin> SelectAdminPaged(Admin objFrom, Admin objTo, int pageIndex, int pageSize, out int totalRows)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Admin_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<Admin>(command, out totalRows);
        }

        public List<Admin> SelectAdminPaged(string whereClause, string orderBy, int pageIndex, int pageSize,
                                            out int totalRows)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Admin_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<Admin>(command, out totalRows);
        }

        public DalResult InsertArticle(Article obj)
        {
            const string spName = "sp_Article_Insert";
            var parameters = new SqlParameter[]
                                 {
                                     new SqlParameter("@Id", obj.Id),
                                     new SqlParameter("@Title", obj.Title),
                                     new SqlParameter("@Description", obj.Description),
                                     new SqlParameter("@MainContent", obj.MainContent),
                                     new SqlParameter("@Thumb", obj.Thumb),
                                     new SqlParameter("@Name", obj.Name),
                                     new SqlParameter("@PageView", obj.PageView),
                                     new SqlParameter("@CreateTime", obj.CreateTime),
                                     new SqlParameter("@UpdateTime", obj.UpdateTime),
                                     new SqlParameter("@MenuId", obj.MenuId),
                                     new SqlParameter("@Status", obj.Status),
                                 };
            var p = parameters[0];
            p.Direction = ParameterDirection.Output;
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            var id = (int)(p.Value ?? 0);
            if (id > 0)
            {
                dalResult.IsSuccess = true;
                dalResult.NewRowId = id;
            }
            return dalResult;
        }
        public DalResult UpdateArticle(long id, Article obj)
        {
            const string spName = "sp_Article_Update";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@Title", obj.Title),
                                        new SqlParameter("@Description", obj.Description),
                                        new SqlParameter("@MainContent", obj.MainContent),
                                        new SqlParameter("@Thumb", obj.Thumb),
                                        new SqlParameter("@Name", obj.Name),
                                        new SqlParameter("@PageView", obj.PageView),
                                        new SqlParameter("@CreateTime", obj.CreateTime),
                                        new SqlParameter("@UpdateTime", obj.UpdateTime),
                                        new SqlParameter("@MenuId", obj.MenuId),
                                        new SqlParameter("@Status", obj.Status),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult UpdateArticlePageView(long id, int count)
        {
            const string spName = "sp_Article_Update_PageView";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@PageView", count),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult UpdateArticleStatus(long id, byte status)
        {
            const string spName = "sp_Article_Update_Status";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@Status", status),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult DeleteArticle(long id)
        {
            const string spName = "sp_Article_Delete";
            var objParamArray = new SqlParameter[]
{
new SqlParameter("@Id", id),
};
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }
        public Article SelectArticleById(long id)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Article_GetById";
            var pId = new SqlParameter("@Id", id);
            command.Parameters.Add(pId);
            var lists = db.GetList<Article>(command);
            if (lists.Count == 1)
                return lists[0];
            return null;
        }
        public List<Article> SelectAllArticle()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Article_Get_List";
            return db.GetList<Article>(command);
        }
        public List<Article> SelectArticlePaged(Article objFrom, Article objTo, int pageIndex, int pageSize, out int totalRows)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Article_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<Article>(command, out totalRows);
        }
        public List<Article> SelectArticlePaged(string whereClause, string orderBy, int pageIndex, int pageSize, out int totalRows)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Article_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);

            var articles = db.GetList<Article>(command, out totalRows);

            //BuildArticlesUrlRewrite(articles);

            return articles;
        }


        public DalResult InsertConfig(Config obj)
        {
            const string spName = "sp_Config_Insert";
            var parameters = new SqlParameter[]
                                 {
                                     new SqlParameter("@Key", obj.Key),
                                     new SqlParameter("@Value", obj.Value),
                                 };
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            {
                dalResult.IsSuccess = true;
                
            }
            return dalResult;
        }

        public DalResult UpdateConfig(string id, Config obj)
        {
            const string spName = "sp_Config_Update";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Key", id),
                                        new SqlParameter("@Value", obj.Value),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult DeleteConfig(string key)
        {
            const string spName = "sp_Config_Delete";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Key", key), 
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public Config SelectConfigById(string key)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Config_GetByKey";
            var pId = new SqlParameter("@Key", key);
            command.Parameters.Add(pId);
            var lists = db.GetList<Config>(command);
            if (lists.Count == 1)
                return lists[0];
            return null;
        }

        public List<Config> SelectAllConfig()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Config_Get_List";
            return db.GetList<Config>(command);
        }

        public List<Config> SelectConfigPaged(Config objFrom, Config objTo, int pageIndex, int pageSize,
                                              out int totalRows)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Config_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<Config>(command, out totalRows);
        }

        public List<Config> SelectConfigPaged(string whereClause, string orderBy, int pageIndex, int pageSize,
                                              out int totalRows)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Config_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<Config>(command, out totalRows);
        }

        public DalResult InsertFeedback(Feedback obj)
        {
            const string spName = "sp_Feedback_Insert";
            var parameters = new SqlParameter[]
                                 {
                                       new SqlParameter("@Id", obj.Id),
                                         new SqlParameter("@NameGuess", obj.NameGuess),
                                         new SqlParameter("@Email", obj.Email),
                                         new SqlParameter("@PhoneNumber", obj.PhoneNumber),
                                         new SqlParameter("@CreateTime", obj.CreateTime)
                                 };
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            {
                dalResult.IsSuccess = true;

            }
            return dalResult;
        }

        public List<Feedback> SelectAllFeedback()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Feedback_Get_List";
            return db.GetList<Feedback>(command);
        }

        public List<Feedback> SelectFeedbackPaged(Feedback objFrom, Feedback objTo, int pageIndex, int pageSize,
                                              out int totalRows)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Feedback_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<Feedback>(command, out totalRows);
        }

        public List<Feedback> SelectFeedbackPaged(string whereClause, string orderBy, int pageIndex, int pageSize,
                                              out int totalRows)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Feedback_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<Feedback>(command, out totalRows);
        }

        public DalResult InsertMenu(Menu obj)
        {
            const string spName = "sp_Menu_Insert";
            var parameters = new SqlParameter[]
                                 {
                                     new SqlParameter("@Id", obj.Id),
                                     new SqlParameter("@Title", obj.Title),
                                     new SqlParameter("@Description", obj.Description),
                                     new SqlParameter("@Icon", obj.Icon), 
                                     new SqlParameter("@OrderNumber", obj.OrderNumber),
                                     new SqlParameter("@LinkMenu", obj.LinkMenu),
                                     new SqlParameter("@ArticleId", obj.ArticleId),
                                     new SqlParameter("@Status", obj.Status),
                                     new SqlParameter("@ParentId", obj.ParentId),
                                 };
            var p = parameters[0];
            p.Direction = ParameterDirection.Output;
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            var id = (int) p.Value;
            if (id > 0)
            {
                dalResult.IsSuccess = true;
                dalResult.NewRowId = id;
            }
            return dalResult;
        }

        public DalResult UpdateMenu(long id, Menu obj)
        {
            const string spName = "sp_Menu_Update";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@Title", obj.Title),
                                        new SqlParameter("@Description", obj.Description),
                                        new SqlParameter("@Icon", obj.Icon), 
                                        new SqlParameter("@OrderNumber", obj.OrderNumber),
                                        new SqlParameter("@LinkMenu", obj.LinkMenu),
                                        new SqlParameter("@ArticleId", obj.ArticleId),
                                        new SqlParameter("@Status", obj.Status),
                                        new SqlParameter("@ParentId", obj.ParentId),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult UpdateMenuStatus(long id, byte status)
        {
            const string spName = "sp_Menu_Update_Status";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@Status", status),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult DeleteMenu(long id)
        {
            const string spName = "sp_Menu_Delete";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public Menu SelectMenuById(long id)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Menu_GetById";
            var pId = new SqlParameter("@Id", id);
            command.Parameters.Add(pId);
            var lists = db.GetList<Menu>(command);
            if (lists.Count == 1)
                return lists[0];
            return null;
        }

        public List<Menu> SelectAllMenu()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Menu_Get_List";
            return db.GetList<Menu>(command);
        }

        public List<Menu> SelectMenuPaged(Menu objFrom, Menu objTo, int pageIndex, int pageSize, out int totalRows)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Menu_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<Menu>(command, out totalRows);
        }

        public List<Menu> SelectMenuPaged(string whereClause, string orderBy, int pageIndex, int pageSize,
                                          out int totalRows)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Menu_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<Menu>(command, out totalRows);
        }

        public DalResult InsertNewsCategory(NewsCategory obj)
        {
            const string spName = "sp_NewsCategory_Insert";
            var parameters = new SqlParameter[]
                                 {
                                     new SqlParameter("@Id", obj.Id),
                                     new SqlParameter("@Title", obj.Title),
                                     new SqlParameter("@Description", obj.Description),
                                     new SqlParameter("@OrderNumber", obj.OrderNumber),
                                     new SqlParameter("@ParentId", obj.ParentId),
                                     new SqlParameter("@Status", obj.Status),
                                 };
            var p = parameters[0];
            p.Direction = ParameterDirection.Output;
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            var id = (int) p.Value;
            if (id > 0)
            {
                dalResult.IsSuccess = true;
                dalResult.NewRowId = id;
            }
            return dalResult;
        }

        public DalResult UpdateNewsCategory(long id, NewsCategory obj)
        {
            const string spName = "sp_NewsCategory_Update";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@Title", obj.Title),
                                        new SqlParameter("@Description", obj.Description),
                                        new SqlParameter("@OrderNumber", obj.OrderNumber),
                                        new SqlParameter("@ParentId", obj.ParentId),
                                        new SqlParameter("@Status", obj.Status),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult UpdateNewsCategoryStatus(long id, byte status)
        {
            const string spName = "sp_NewsCategory_Update_Status";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@Status", status),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult DeleteNewsCategory(long id)
        {
            const string spName = "sp_NewsCategory_Delete";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public NewsCategory SelectNewsCategoryById(long id)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_NewsCategory_GetById";
            var pId = new SqlParameter("@Id", id);
            command.Parameters.Add(pId);
            var lists = db.GetList<NewsCategory>(command);
            if (lists.Count == 1)
                return lists[0];
            return null;
        }

        public List<NewsCategory> SelectAllNewsCategory()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_NewsCategory_Get_List";
            return db.GetList<NewsCategory>(command);
        }

        public List<NewsCategory> SelectNewsCategoryPaged(NewsCategory objFrom, NewsCategory objTo, int pageIndex,
                                                          int pageSize, out int totalRows)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_NewsCategory_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<NewsCategory>(command, out totalRows);
        }

        public List<NewsCategory> SelectNewsCategoryPaged(string whereClause, string orderBy, int pageIndex,
                                                          int pageSize, out int totalRows)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_NewsCategory_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<NewsCategory>(command, out totalRows);
        }

        public DalResult InsertNews(News obj)
        {
            const string spName = "sp_News_Insert";
            var parameters = new SqlParameter[]
                                 {
                                     new SqlParameter("@Id", obj.Id),
                                     new SqlParameter("@CategoryId", obj.CategoryId),
                                     new SqlParameter("@Title", obj.Title),
                                     new SqlParameter("@Description", obj.Description),
                                     new SqlParameter("@MetaDescription", obj.MetaDescription),
                                     new SqlParameter("@MainContent", obj.MainContent),
                                     new SqlParameter("@Thumb", obj.Thumb),
                                     new SqlParameter("@PageView", obj.PageView),
                                     new SqlParameter("@CreateTime", obj.CreateTime),
                                     new SqlParameter("@UpdateTime", obj.UpdateTime),
                                     new SqlParameter("@DisplayNo", obj.DisplayNo),
                                     new SqlParameter("@Status", obj.Status),
                                     new SqlParameter("@Author", obj.Author),
                                     //new SqlParameter("@Url", obj.Url),
                                 };
            var p = parameters[0];
            p.Direction = ParameterDirection.Output;
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            var id = (int) p.Value;
            if (id > 0)
            {
                dalResult.IsSuccess = true;
                dalResult.NewRowId = id;
            }
            return dalResult;
        }

        public DalResult UpdateNews(long id, News obj)
        {
            const string spName = "sp_News_Update";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@CategoryId", obj.CategoryId),
                                        new SqlParameter("@Title", obj.Title),
                                        new SqlParameter("@Description", obj.Description),
                                        new SqlParameter("@MetaDescription", obj.MetaDescription),
                                        new SqlParameter("@MainContent", obj.MainContent),
                                        new SqlParameter("@Thumb", obj.Thumb),
                                        new SqlParameter("@PageView", obj.PageView),
                                        new SqlParameter("@CreateTime", obj.CreateTime),
                                        new SqlParameter("@UpdateTime", obj.UpdateTime),
                                        new SqlParameter("@DisplayNo", obj.DisplayNo),
                                        new SqlParameter("@Status", obj.Status),
                                        new SqlParameter("@Author", obj.Author),
                                        //new SqlParameter("@Url", obj.Url),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult UpdateNewsStatus(long id, byte status)
        {
            const string spName = "sp_News_Update_Status";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@Status", status),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult UpdateNewsPageView(long id, int count)
        {
            const string spName = "sp_News_Update_PageView";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@PageView",count),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult DeleteNews(long id)
        {
            const string spName = "sp_News_Delete";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        //public News SelectNewsById(long id,string cad)
        public News SelectNewsById(long id)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_News_GetById";
            var pId = new SqlParameter("@Id", id);
            command.Parameters.Add(pId);
            var lists = db.GetList<News>(command);
            if (lists.Count == 1)
            {
                var news = lists[0];
               // BuildNewsUrlRewrite(news, cad);

                return news;
            }
                
            return null;
        }
        public News SelectNewsByUrl(string url)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_News_GetByUrl";
            var pId = new SqlParameter("@Url", url);
            command.Parameters.Add(pId);
            var lists = db.GetList<News>(command);
            if (lists.Count == 1)
            {
                var news = lists[0];
                // BuildNewsUrlRewrite(news, cad);

                return news;
            }

            return null;
        }

        //public List<News> SelectAllNews(string cad)
        public List<News> SelectAllNews()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_News_Get_List";
            var news =  db.GetList<News>(command);

           // BuildNewsUrlRewrite(news, cad);

            return news;
        }

        public List<News> SelectNewsPaged(News objFrom, News objTo, int pageIndex, int pageSize, out int totalRows,string cad)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_News_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            var news = db.GetList<News>(command, out totalRows);

           // BuildNewsUrlRewrite(news, cad);

            return news;
        }

        public List<News> SelectNewsPaged(string whereClause, string orderBy, int pageIndex, int pageSize,
                                          out int totalRows,string cad)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_News_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            var news = db.GetList<News>(command, out totalRows);

           // BuildNewsUrlRewrite(news, cad);

            return news;
        }

        public DalResult InsertProductCategory(ProductCategory obj)
        {
            const string spName = "sp_ProductCategory_Insert";
            var parameters = new SqlParameter[]
                                 {
                                     new SqlParameter("@Id", obj.Id),
                                     new SqlParameter("@Title", obj.Title),
                                     new SqlParameter("@Description", obj.Description),
                                     new SqlParameter("@MetaTitle", obj.MetaTitle),
                                     new SqlParameter("@MetaDescription", obj.MetaDescription),
                                     new SqlParameter("@OrderNumber", obj.OrderNumber),
                                     new SqlParameter("@ParentId", obj.ParentId),
                                     new SqlParameter("@Status", obj.Status),
                                 };
            var p = parameters[0];
            p.Direction = ParameterDirection.Output;
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            var id = (int) p.Value;
            if (id > 0)
            {
                dalResult.IsSuccess = true;
                dalResult.NewRowId = id;
            }
            return dalResult;
        }

        public DalResult UpdateProductCategory(long id, ProductCategory obj)
        {
            const string spName = "sp_ProductCategory_Update";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@Title", obj.Title),
                                        new SqlParameter("@Description", obj.Description),
                                        new SqlParameter("@MetaTitle", obj.MetaTitle),
                                        new SqlParameter("@MetaDescription", obj.MetaDescription),
                                        new SqlParameter("@OrderNumber", obj.OrderNumber),
                                        new SqlParameter("@ParentId", obj.ParentId),
                                        new SqlParameter("@Status", obj.Status),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult UpdateProductCategoryStatus(long id, byte status)
        {
            const string spName = "sp_ProductCategory_Update_Status";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@Status", status),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult DeleteProductCategory(long id)
        {
            const string spName = "sp_ProductCategory_Delete";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public ProductCategory SelectProductCategoryById(long id)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_ProductCategory_GetById";
            var pId = new SqlParameter("@Id", id);
            command.Parameters.Add(pId);
            var lists = db.GetList<ProductCategory>(command);

           // BuildProductCategoryUrlRewrite(lists);

            if (lists.Count == 1)
                return lists[0];
            return null;
        }
        public List<ProductCategory> SelectSubProductCategoryByParentId(long parentId)
        {
            var command = new SqlCommand
                              {
                                  CommandType = CommandType.StoredProcedure,
                                  CommandText = "sp_ProductCategory_GetSubCategoryByParentId"
                              };
            var pId = new SqlParameter("@ParentId", parentId);
            command.Parameters.Add(pId);
           return db.GetList<ProductCategory>(command);
          
        }
        public List<ProductCategory> SelectAllProductCategory()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_ProductCategory_Get_List";
            var categories = db.GetList<ProductCategory>(command);

            //BuildProductCategoryUrlRewrite(categories);

            return categories;
        }

        public List<ProductCategory> SelectProductCategoryPaged(ProductCategory objFrom, ProductCategory objTo,
                                                                int pageIndex, int pageSize, out int totalRows)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_ProductCategory_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            var categories = db.GetList<ProductCategory>(command, out totalRows);

            BuildProductCategoryUrlRewrite(categories);

            return categories;
        }

        public List<ProductCategory> SelectProductCategoryPaged(string whereClause, string orderBy, int pageIndex,
                                                                int pageSize, out int totalRows)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_ProductCategory_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            var categories = db.GetList<ProductCategory>(command, out totalRows);

            BuildProductCategoryUrlRewrite(categories);

            return categories;
        }

        public DalResult InsertProduct(Product obj)
        {
            const string spName = "sp_Product_Insert";
            var parameters = new SqlParameter[]
                                 {
                                     new SqlParameter("@Id", obj.Id),
                                     new SqlParameter("@CategoryId", obj.CategoryId),
                                     new SqlParameter("@Code", obj.Code),
                                     new SqlParameter("@Name", obj.Name),
                                     new SqlParameter("@Tags", obj.Tags),
                                     new SqlParameter("@Image", obj.Image),
                                     new SqlParameter("@ImageList", obj.ImageList),
                                     new SqlParameter("@Description", obj.Description),
                                     new SqlParameter("@MainContent", obj.MainContent),
                                     new SqlParameter("@MetaDescription", obj.MetaDescription),
                                     new SqlParameter("@Shape", obj.Shape),
                                     new SqlParameter("@Color", obj.Color),
                                     new SqlParameter("@ColorType", obj.ColorType),
                                     new SqlParameter("@Size", obj.Size),
                                     new SqlParameter("@Weight", obj.Weight),
                                     new SqlParameter("@Material", obj.Material),
                                     new SqlParameter("@Purity", obj.Purity),
                                     new SqlParameter("@Processor", obj.Processor),
                                     new SqlParameter("@Price", obj.Price),
                                     new SqlParameter("@PriceDiscount", obj.PriceDiscount),
                                     new SqlParameter("@PriceString", obj.PriceString),
                                     new SqlParameter("@PageView", obj.PageView),
                                     new SqlParameter("@ProductStatus", obj.ProductStatus),
                                     new SqlParameter("@Status", obj.Status),
                                 };
            var p = parameters[0];
            p.Direction = ParameterDirection.Output;
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            var id = (int)p.Value;
            if (id > 0)
            {
                dalResult.IsSuccess = true;
                dalResult.NewRowId = id;
            }
            return dalResult;
        }
        public DalResult UpdateProduct(long id, Product obj)
        {
            const string spName = "sp_Product_Update";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@CategoryId", obj.CategoryId),
                                        new SqlParameter("@Code", obj.Code),
                                        new SqlParameter("@Name", obj.Name),
                                        new SqlParameter("@Tags", obj.Tags),
                                        new SqlParameter("@Image", obj.Image),
                                        new SqlParameter("@ImageList", obj.ImageList),
                                        new SqlParameter("@Description", obj.Description),
                                        new SqlParameter("@MainContent", obj.MainContent),
                                        new SqlParameter("@MetaDescription", obj.MetaDescription),
                                        new SqlParameter("@Shape", obj.Shape),
                                        new SqlParameter("@Color", obj.Color),
                                        new SqlParameter("@ColorType", obj.ColorType),
                                        new SqlParameter("@Size", obj.Size),
                                        new SqlParameter("@Weight", obj.Weight),
                                        new SqlParameter("@Material", obj.Material),
                                        new SqlParameter("@Purity", obj.Purity),
                                        new SqlParameter("@Processor", obj.Processor),
                                        new SqlParameter("@Price", obj.Price),
                                        new SqlParameter("@PriceDiscount", obj.PriceDiscount),
                                        new SqlParameter("@PriceString", obj.PriceString),
                                        new SqlParameter("@PageView", obj.PageView),
                                        new SqlParameter("@ProductStatus", obj.ProductStatus),
                                        new SqlParameter("@Status", obj.Status),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult UpdateProductPageView(long id, int count)
        {
            const string spName = "sp_Product_Update_PageView";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@PageView", count),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult UpdateProductStatus(long id, byte status)
        {
            const string spName = "sp_Product_Update_Status";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@Status", status),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;

        }

        public DalResult DeleteProduct(long id)
        {
            const string spName = "sp_Product_Delete";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public Product SelectProductById(long id)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Product_GetById";
            var pId = new SqlParameter("@Id", id);
            command.Parameters.Add(pId);
            var lists = db.GetList<Product>(command);
            if (lists.Count == 1)
            {
                BuildProductUrlRewrite(lists);
                return lists[0];
            }
                
            return null;
        }

        public List<Product> SelectAllProduct()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Product_Get_List";
            var products = db.GetList<Product>(command);

            BuildProductUrlRewrite(products);

            return products;
        }

        public List<ProductColor> SelectAllProductColor()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Product_Color_Get_List";
            var productColors = db.GetList<ProductColor>(command);

            return productColors;
        }

        public List<ProductMaterial> SelectAllProductMaterial()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Product_Material_Get_List";
            var productMaterial = db.GetList<ProductMaterial>(command);

            return productMaterial;
        }
        public List<Product> SelectProductTop(int top)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Product_Get_Top";
            var products = db.GetList<Product>(command);

            BuildProductUrlRewrite(products);

            return products;
        }

        public List<Product> SelectProductPaged(Product objFrom, Product objTo, int pageIndex, int pageSize,
                                                out int totalRows)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Product_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            var products = db.GetList<Product>(command, out totalRows);

            BuildProductUrlRewrite(products);

            return products;
        }

        public List<Product> SelectProductPaged(string whereClause, string orderBy, int pageIndex, int pageSize,
                                                out int totalRows)
        {
            //Khong select product status = 1 inactive
            if(string.IsNullOrEmpty(whereClause))
            {

                whereClause = "Status <> 1";
            }
            else
            {
                whereClause = string.Format("{0} AND Status <> 1", whereClause);
            }

            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Product_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            var products = db.GetList<Product>(command, out totalRows);

            BuildProductUrlRewrite(products);

            return products;
        }
        public List<Product> SelectProductPagedForAdmin(string whereClause, string orderBy, int pageIndex, int pageSize,
                                                out int totalRows)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Product_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            var products = db.GetList<Product>(command, out totalRows);

            BuildProductUrlRewrite(products);

            return products;
        }


       
        public DalResult InsertWebAd(WebAd obj)
        {
            const string spName = "sp_WebAd_Insert";
            var parameters = new SqlParameter[]
{
new SqlParameter("@Id", obj.Id),
new SqlParameter("@Image", obj.Image),
new SqlParameter("@Link", obj.Link),
new SqlParameter("@Width", obj.Width),
new SqlParameter("@Height", obj.Height),
new SqlParameter("@Position", obj.Position),
new SqlParameter("@Status", obj.Status),
};
            var p = parameters[0];
            p.Direction = ParameterDirection.Output;
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            var id = (int)p.Value;
            if (id > 0)
            {
                dalResult.IsSuccess = true;
                dalResult.NewRowId = id;
            }
            return dalResult;
        }
        public DalResult UpdateWebAd(long id, WebAd obj)
        {
            const string spName = "sp_WebAd_Update";
            var objParamArray = new SqlParameter[]
{
new SqlParameter("@Id", id),
new SqlParameter("@Image", obj.Image),
new SqlParameter("@Link", obj.Link),
new SqlParameter("@Width", obj.Width),
new SqlParameter("@Height", obj.Height),
new SqlParameter("@Position", obj.Position),
new SqlParameter("@Status", obj.Status),
};
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }
        public DalResult DeleteWebAd(long id)
        {
            const string spName = "sp_WebAd_Delete";
            var objParamArray = new SqlParameter[]
{
new SqlParameter("@Id", id),
};
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }
        public WebAd SelectWebAdById(long id)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_WebAd_GetById";
            var pId = new SqlParameter("@Id", id);
            command.Parameters.Add(pId);
            var lists = db.GetList<WebAd>(command);
            if (lists.Count == 1)
                return lists[0];
            return null;
        }
        public List<WebAd> SelectAllWebAd()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_WebAd_Get_List";
            return db.GetList<WebAd>(command);
        }
        public List<WebAd> SelectWebAdPaged(WebAd objFrom, WebAd objTo, int pageIndex, int pageSize, out int totalRows)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_WebAd_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<WebAd>(command, out totalRows);
        }
        public List<WebAd> SelectWebAdPaged(string whereClause, string orderBy, int pageIndex, int pageSize, out int totalRows)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_WebAd_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<WebAd>(command, out totalRows);
        }
        public DalResult InsertWebLink(WebLink obj)
        {
            const string spName = "sp_WebLink_Insert";
            var parameters = new SqlParameter[]
{
new SqlParameter("@Id", obj.Id),
new SqlParameter("@Title", obj.Title),
new SqlParameter("@Link", obj.Link),
new SqlParameter("@Description", obj.Description),
new SqlParameter("@Status", obj.Status),
};
            var p = parameters[0];
            p.Direction = ParameterDirection.Output;
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            var id = (int)p.Value;
            if (id > 0)
            {
                dalResult.IsSuccess = true;
                dalResult.NewRowId = id;
            }
            return dalResult;
        }
        public DalResult UpdateWebLink(long id, WebLink obj)
        {
            const string spName = "sp_WebLink_Update";
            var objParamArray = new SqlParameter[]
{
new SqlParameter("@Id", id),
new SqlParameter("@Title", obj.Title),
new SqlParameter("@Link", obj.Link),
new SqlParameter("@Description", obj.Description),
new SqlParameter("@Status", obj.Status),
};
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }
        public DalResult DeleteWebLink(long id)
        {
            const string spName = "sp_WebLink_Delete";
            var objParamArray = new SqlParameter[]
{
new SqlParameter("@Id", id),
};
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }
        public WebLink SelectWebLinkById(long id)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_WebLink_GetById";
            var pId = new SqlParameter("@Id", id);
            command.Parameters.Add(pId);
            var lists = db.GetList<WebLink>(command);
            if (lists.Count == 1)
                return lists[0];
            return null;
        }
        public List<WebLink> SelectAllWebLink()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_WebLink_Get_List";
            return db.GetList<WebLink>(command);
        }
        public List<WebLink> SelectWebLinkPaged(WebLink objFrom, WebLink objTo, int pageIndex, int pageSize, out int totalRows)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_WebLink_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<WebLink>(command, out totalRows);
        }
        public List<WebLink> SelectWebLinkPaged(string whereClause, string orderBy, int pageIndex, int pageSize, out int totalRows)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_WebLink_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<WebLink>(command, out totalRows);
        }

        public DalResult InsertTag(Tag obj)
        {
            const string spName = "sp_Tag_Insert";
            var parameters = new SqlParameter[]
                                 {
                                     new SqlParameter("@Id", obj.Id),
                                     new SqlParameter("@TagName", obj.TagName),
                                     new SqlParameter("@ProductList", obj.ProductList),
                                     new SqlParameter("@Count", obj.Count),
                                     new SqlParameter("@Status", obj.Status),
                                 };
            var p = parameters[0];
            p.Direction = ParameterDirection.Output;
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            var id = (int)p.Value;
            if (id > 0)
            {
                dalResult.IsSuccess = true;
                dalResult.NewRowId = id;
            }
            return dalResult;
        }
        public DalResult UpdateTag(long id, Tag obj)
        {
            const string spName = "sp_Tag_Update";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@TagName", obj.TagName),
                                        new SqlParameter("@ProductList", obj.ProductList),
                                        new SqlParameter("@Count", obj.Count),
                                        new SqlParameter("@Status", obj.Status),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }
        public DalResult DeleteTag(long id)
        {
            const string spName = "sp_Tag_Delete";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }
        public Tag SelectTagById(long id)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Tag_GetById";
            var pId = new SqlParameter("@Id", id);
            command.Parameters.Add(pId);
            var lists = db.GetList<Tag>(command);
            if (lists.Count == 1)
                return lists[0];
            return null;
        }

        public Tag SelectTagByTagName(string tagname)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Tag_GetByTagName";
            var pId = new SqlParameter("@TagName", tagname);
            command.Parameters.Add(pId);
            var lists = db.GetList<Tag>(command);
            if (lists.Count == 1)
                return lists[0];
            return null;
        }

        public List<Tag> SelectAllTag()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Tag_Get_List";
            return db.GetList<Tag>(command);
        }
        public List<Tag> SelectTagPaged(Tag objFrom, Tag objTo, int pageIndex, int pageSize, out int totalRows)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Tag_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<Tag>(command, out totalRows);
        }
        public List<Tag> SelectTagPaged(string whereClause, string orderBy, int pageIndex, int pageSize, out int totalRows)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Tag_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<Tag>(command, out totalRows);
        }

        public DalResult InsertKeywordLink(KeywordLink obj)
        {
            const string spName = "sp_KeywordLink_Insert";
            var parameters = new SqlParameter[]
                                 {
                                     new SqlParameter("@Id", obj.Id),
                                     new SqlParameter("@Keyword", obj.Keyword),
                                     new SqlParameter("@Url", obj.Url),
                                     new SqlParameter("@Status", obj.Status),
                                 };
            var p = parameters[0];
            p.Direction = ParameterDirection.Output;
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            var id = (int)p.Value;
            if (id > 0)
            {
                dalResult.IsSuccess = true;
                dalResult.NewRowId = id;
            }
            return dalResult;
        }
        public DalResult UpdateKeywordLink(long id, KeywordLink obj)
        {
            const string spName = "sp_KeywordLink_Update";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                        new SqlParameter("@Keyword", obj.Keyword),
                                        new SqlParameter("@Url", obj.Url),
                                        new SqlParameter("@Status", obj.Status),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }
        public DalResult DeleteKeywordLink(long id)
        {
            const string spName = "sp_KeywordLink_Delete";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }
        public KeywordLink SelectKeywordLinkById(long id)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_KeywordLink_GetById";
            var pId = new SqlParameter("@Id", id);
            command.Parameters.Add(pId);
            var lists = db.GetList<KeywordLink>(command);
            if (lists.Count == 1)
                return lists[0];
            return null;
        }
        public List<KeywordLink> SelectAllKeywordLink()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_KeywordLink_Get_List";
            return db.GetList<KeywordLink>(command);
        }
        public List<KeywordLink> SelectKeywordLinkPaged(KeywordLink objFrom, KeywordLink objTo, int pageIndex, int pageSize, out int totalRows)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_KeywordLink_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<KeywordLink>(command, out totalRows);
        }
        public List<KeywordLink> SelectKeywordLinkPaged(string whereClause, string orderBy, int pageIndex, int pageSize, out int totalRows)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_KeywordLink_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<KeywordLink>(command, out totalRows);
        }

        public DataSet BackupDatabase()
        {
            var ds = new DataSet();
       
            var tables = SelectAllTableName();

            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;

            foreach (var table in tables)
            {
                command.CommandText = string.Format("sp_{0}_Get_List",table.TABLE_NAME);
                var dtProducts = db.getDataTable(command);
                dtProducts.TableName = table.TABLE_NAME;

                ds.Tables.Add(dtProducts.Copy());
            }

            return ds;
        }

        public bool RestoreDatabase(DataSet ds)
        {
            try
            {
                foreach (DataTable dt in ds.Tables)
                {
                    SqlConnection conn = new SqlConnection(MyGo.Core.Config.AccountConnStr);
                    conn.Open();
                    SqlCommand comm = new SqlCommand("truncate table " + dt.TableName, conn);

                    Logger.Info(string.Format("Truncate table {0}", dt.TableName));

                    comm.ExecuteNonQuery();
                    conn.Close();

                    SqlBulkCopy bulkCopy = new SqlBulkCopy(MyGo.Core.Config.AccountConnStr, SqlBulkCopyOptions.KeepIdentity);
                    bulkCopy.DestinationTableName = dt.TableName;
                    bulkCopy.WriteToServer(dt);

                    Logger.Info(string.Format("Write data to table {0}", dt.TableName));

                    
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return false;
            }

        }

        private List<ShopTable> SelectAllTableName()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
            return db.GetList<ShopTable>(command);
        }
        public DalResult InsertAlbum(Album obj)
        {
            const string spName = "sp_Album_Insert";
            var parameters = new SqlParameter[]
{
new SqlParameter("@Id", obj.Id),
new SqlParameter("@Name", obj.Name),
new SqlParameter("@Description", obj.Description),
new SqlParameter("@ImageUrl", obj.ImageUrl),
new SqlParameter("@ImageList", obj.ImageList),
new SqlParameter("@CategoryId", obj.CategoryId),
new SqlParameter("@PageView", obj.PageView),
new SqlParameter("@CreateTime", obj.CreateTime),
new SqlParameter("@Status", obj.Status),
};
            var p = parameters[0];
            p.Direction = ParameterDirection.Output;
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            var id = (int)p.Value;
            if (id > 0)
            {
                dalResult.IsSuccess = true;
                dalResult.NewRowId = id;
            }
            return dalResult;
        }
        public DalResult UpdateAlbum(long id, Album obj)
        {
            const string spName = "sp_Album_Update";
            var objParamArray = new SqlParameter[]
{
new SqlParameter("@Id", id),
new SqlParameter("@Name", obj.Name),
new SqlParameter("@Description", obj.Description),
new SqlParameter("@ImageUrl", obj.ImageUrl),
new SqlParameter("@ImageList", obj.ImageList),
new SqlParameter("@CategoryId", obj.CategoryId),
new SqlParameter("@PageView", obj.PageView),
new SqlParameter("@CreateTime", obj.CreateTime),
new SqlParameter("@Status", obj.Status),
};
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }
        public DalResult DeleteAlbum(long id)
        {
            const string spName = "sp_Album_Delete";
            var objParamArray = new SqlParameter[]
{
new SqlParameter("@Id", id),
};
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }
        public Album SelectAlbumById(long id)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Album_GetById";
            var pId = new SqlParameter("@Id", id);
            command.Parameters.Add(pId);
            var lists = db.GetList<Album>(command);
            if (lists.Count == 1)
            {
                var x = lists[0];
                BuildAlbumUrlRewrite(x);

                return x;
            }
                
            return null;
        }
        public List<Album> SelectAllAlbum()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Album_Get_List";
            return db.GetList<Album>(command);
        }
        public List<Album> SelectAlbumPaged(Album objFrom, Album objTo, int pageIndex, int pageSize, out int totalRows)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Album_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<Album>(command, out totalRows);
        }
        public List<Album> SelectAlbumPaged(string whereClause, string orderBy, int pageIndex, int pageSize, out int totalRows)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Album_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            var album = db.GetList<Album>(command, out totalRows);

            BuildAlbumUrlRewrite(album);

            return album;
        }

        public DalResult InsertPhoto(Photo obj)
        {
            const string spName = "sp_Photo_Insert";
            var parameters = new SqlParameter[]
{
new SqlParameter("@Id", obj.Id),
new SqlParameter("@AlbumId", obj.AlbumId),
new SqlParameter("@Name", obj.Name),
new SqlParameter("@Description", obj.Description),
new SqlParameter("@ImageUrl", obj.ImageUrl),
new SqlParameter("@PageView", obj.PageView),
new SqlParameter("@CreateTime", obj.CreateTime),
new SqlParameter("@Status", obj.Status),
};
            var p = parameters[0];
            p.Direction = ParameterDirection.Output;
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            var id = (int)p.Value;
            if (id > 0)
            {
                dalResult.IsSuccess = true;
                dalResult.NewRowId = id;
            }
            return dalResult;
        }
        public DalResult UpdatePhoto(long id, Photo obj)
        {
            const string spName = "sp_Photo_Update";
            var objParamArray = new SqlParameter[]
{
new SqlParameter("@Id", id),
new SqlParameter("@AlbumId", obj.AlbumId),
new SqlParameter("@Name", obj.Name),
new SqlParameter("@Description", obj.Description),
new SqlParameter("@ImageUrl", obj.ImageUrl),
new SqlParameter("@PageView", obj.PageView),
new SqlParameter("@CreateTime", obj.CreateTime),
new SqlParameter("@Status", obj.Status),
};
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }
        public DalResult DeletePhoto(long id)
        {
            const string spName = "sp_Photo_Delete";
            var objParamArray = new SqlParameter[]
{
new SqlParameter("@Id", id),
};
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }
        public Photo SelectPhotoById(long id)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Photo_GetById";
            var pId = new SqlParameter("@Id", id);
            command.Parameters.Add(pId);
            var lists = db.GetList<Photo>(command);
            if (lists.Count == 1)
                return lists[0];
            return null;
        }

        public List<Photo> SelectPhotoByAlbumId(long albumId)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Photo_GetByAlbumId";
            var pId = new SqlParameter("@AlbumId", albumId);
            command.Parameters.Add(pId);
            var lists = db.GetList<Photo>(command);
            return lists;
        }
        public List<Photo> SelectAllPhoto()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Photo_Get_List";
            return db.GetList<Photo>(command);
        }
        public List<Photo> SelectPhotoPaged(Photo objFrom, Photo objTo, int pageIndex, int pageSize, out int totalRows)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Photo_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<Photo>(command, out totalRows);
        }
        public List<Photo> SelectPhotoPaged(string whereClause, string orderBy, int pageIndex, int pageSize, out int totalRows)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Photo_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<Photo>(command, out totalRows);
        }

        private void BuildAlbumUrlRewrite(Album album)
        {
            string title = FormatTitleForRewrite(album.Name);


            album.UrlRewrite = string.Format("/album/{0}-{1}", title, album.Id);
        }
        private void BuildAlbumUrlRewrite(List<Album> albums)
        {
            foreach(var album in albums)
            {
                string title = FormatTitleForRewrite(album.Name);
                album.UrlRewrite = string.Format("/album/{0}-{1}", title, album.Id);
            }
        }

        private void BuildProductUrlRewrite(Product product, ProductCategory category)
        {
            string title = FormatTitleForRewrite(product.Name);

            string categoryTitle = FormatTitleForRewrite(category.Title);

            //product.UrlRewrite = string.Format("/product/{0}-{1}", title, product.Id);
            product.UrlRewrite = string.Format("/product/{2}/{0}-{1}", title, product.Id, categoryTitle);
        }

        private  string FormatTitleForRewrite(string title)
        {
            string categoryTitle = TextHelper.NonUnicode(title).ToLower();

            //categoryTitle = categoryTitle.Replace("-", "");
            categoryTitle = categoryTitle.Replace("  ", " ");
            categoryTitle = categoryTitle.Replace(' ', '-');
            categoryTitle = categoryTitle.Replace('?', ' ');
            categoryTitle = categoryTitle.Replace('%', ' ');
            return categoryTitle;
        }

        private void BuildProductUrlRewrite(List<Product> products)
        {
            var productCategories = SelectAllProductCategory();
            foreach (var product in products)
            {
                Product product1 = product;

                var cate = new ProductCategory();

                var category = productCategories.Find(x => x.Id == product1.CategoryId);

                if(category == null)
                {
                    category = new ProductCategory()
                                   {
                                       Title = "uncategory",
                                       Id = 0
                                   };
                }

                cate.Title = category.Title;

                if(category.ParentId > 0)
                {
                    var parentCategory = productCategories.Find(x => x.Id == category.ParentId);

                    cate.Title = string.Format("{0}/{1}", parentCategory.Title, category.Title);
                }

                BuildProductUrlRewrite(product, cate);
            }
        }

        private void BuildProductCategoryUrlRewrite(ProductCategory productCategory)
        {
            string title = FormatTitleForRewrite(productCategory.Title);

            if(productCategory.ParentId > 0)
            {
                var parentCategory = SelectProductCategoryById(productCategory.ParentId);

                if(parentCategory != null)
                {
                    string parentTitle = FormatTitleForRewrite(parentCategory.Title);

                    productCategory.UrlRewrite = string.Format("/product-category/{2}/{0}-{1}", title, productCategory.Id, parentTitle);

                    return;
                }
            }
            
            productCategory.UrlRewrite = string.Format("/product-category/{0}-{1}", title, productCategory.Id);
        }

        private void BuildProductCategoryUrlRewrite(List<ProductCategory> products)
        {
            foreach (var product in products)
            {
                BuildProductCategoryUrlRewrite(product);
            }
        }

        private void BuildMenuUrlRewrite(Menu menu)
        {
            string title = TextHelper.NonUnicode(menu.Title).Replace(' ', '-').ToLower();
            title = title.Replace('?', ' ');
            menu.UrlRewrite = string.Format("/menu/{0}-{1}", title, menu.Id);
        }

        private void BuildMenuUrlRewrite(List<Menu> products)
        {
            foreach (var product in products)
            {
                BuildMenuUrlRewrite(product);
            }
        }

        public  void BuildNewUrlRewrite(News news, string cad)
        {
            string title = TextHelper.NonUnicode(news.Title).Replace("-", "").Replace("  ", " ").Replace(' ', '-').Replace("\"", "-").Replace("\\", "-").Replace("/", "-")
                .Replace("~", "").Replace("!", "").Replace("@", "").Replace("#", "").Replace("$", "").Replace("%", "").Replace("^", "")
                .Replace("&", "").Replace("&", "").Replace("*", "-").Replace("(", "").Replace(")", "").Replace("+", "").Replace("=", "")
                .Replace(";", "").Replace(":", "").Replace("'", "").Replace("|", "").Replace("<", "").Replace(",", "").Replace(".", "")
                .Replace("<", "").Replace(">", "").Replace("?", "").Replace("`", "").Replace("[", "").Replace("]", "")
                .Replace("{", "").Replace("}", "").Replace("–","").ToLower();
            title = FormatTitleForRewrite(title);
            //news.Url = string.Format("/" + cad + "/{0}-{1}", title, news.Id);
        }

        private void BuildNewsUrlRewrite(List<News> newsList, string cad)
        {
            foreach (var news in newsList)
            {
                BuildNewUrlRewrite(news, cad);
            }
        }
       
        private void BuildArticlesUrlRewrite(List<Article> articleList,string cad)
        {
            foreach (var news in articleList)
            {
                BuildArticleUrlRewrite(news,cad);
            }
        }
        public  void BuildArticleUrlRewrite(Article article,string cad)
        {
            string title = TextHelper.NonUnicode(article.Title).Replace("-", "").Replace("  ", " ").Replace(' ', '-').Replace("\"", "-").Replace("\\", "-").Replace("/", "-")
                .Replace("~", "").Replace("!", "").Replace("@", "").Replace("#", "").Replace("$", "").Replace("%", "").Replace("^", "")
                .Replace("&", "").Replace("&", "").Replace("*", "-").Replace("(", "").Replace(")", "").Replace("+", "").Replace("=", "")
                .Replace(";", "").Replace(":", "").Replace("'", "").Replace("|", "").Replace("<", "").Replace(",", "").Replace(".", "")
                .Replace("<", "").Replace(">", "").Replace("?", "").Replace("`", "").Replace("[", "").Replace("]", "")
                .Replace("{", "").Replace("}", "").ToLower();
            title = FormatTitleForRewrite(title);
            //article.Url= string.Format("/{0}/{1}-{2}",cad, title, article.Id);
        }



        public DalResult InsertFile(File obj)
        {
            const string spName = "sp_File_Insert";
            var parameters = new SqlParameter[]
                                 {
                                     new SqlParameter("@Id", obj.Id),
                                     new SqlParameter("@NameFile", obj.NameFile),
                                     new SqlParameter("@SizeFile", obj.SizeFile),
                                     new SqlParameter("@UrlFile", obj.UrlFile),
                                     new SqlParameter("@Desciption", obj.Desciption),
                                     new SqlParameter("@TimeUpload", obj.TimeUpload),
                                 };
            var p = parameters[0];
            p.Direction = ParameterDirection.Output;
            db.ExecuteScalarSP(spName, parameters);
            var dalResult = new DalResult();
            var id = (int)p.Value;
            if (id > 0)
            {
                dalResult.IsSuccess = true;
                dalResult.NewRowId = id;
            }
            return dalResult;
        }

        public DalResult UpdateFile(long id, File obj)
        {
            const string spName = "sp_File_Update";
            var objParamArray = new SqlParameter[]
                                    {
                                       new SqlParameter("@Id", obj.Id),
                                     new SqlParameter("@NameFile", obj.NameFile),
                                     new SqlParameter("@SizeFile", obj.SizeFile),
                                     new SqlParameter("@UrlFile", obj.UrlFile),
                                     new SqlParameter("@Desciption", obj.Desciption),
                                     new SqlParameter("@TimeUpload", obj.TimeUpload),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public DalResult DeleteFile(long id)
        {
            const string spName = "sp_File_Delete";
            var objParamArray = new SqlParameter[]
                                    {
                                        new SqlParameter("@Id", id),
                                    };
            db.ExecuteScalarSP(spName, objParamArray);
            DalResult dalResult = new DalResult();
            dalResult.IsSuccess = true;
            return dalResult;
        }

        public File SelectFileById(long id)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_File_GetById";
            var pId = new SqlParameter("@Id", id);
            command.Parameters.Add(pId);
            var lists = db.GetList<File>(command);
            if (lists.Count == 1)
                return lists[0];
            return null;
        }

        public List<File> SelectAllFile()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_File_Get_List";
            return db.GetList<File>(command);
        }

        public List<File> SelectFilePaged(File objFrom, File objTo, int pageIndex, int pageSize, out int totalRows)
        {
            string whereClause = Builder.BuildSQLQuery(objFrom, objTo);
            string orderBy = string.Empty;
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_File_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<File>(command, out totalRows);
        }

        public List<File> SelectFilePaged(string whereClause, string orderBy, int pageIndex, int pageSize,
                                            out int totalRows)
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_File_GetPaged";
            var pConditions = new SqlParameter("@WhereClause", whereClause);
            command.Parameters.Add(pConditions);
            var pOrderBy = new SqlParameter("@OrderBy", orderBy);
            command.Parameters.Add(pOrderBy);
            var pPageIndex = new SqlParameter("@PageIndex", pageIndex);
            command.Parameters.Add(pPageIndex);
            var pPageSize = new SqlParameter("@PageSize", pageSize);
            command.Parameters.Add(pPageSize);
            return db.GetList<File>(command, out totalRows);
        }


       
    }
}
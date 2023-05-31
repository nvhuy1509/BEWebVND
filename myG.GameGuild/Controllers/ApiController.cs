using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Activity.Biz;
using Activity.DAL;
using Microsoft.AspNetCore.Mvc;
using Minxtu.DAL.Entity;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace myG.GameGuild.Controllers
{
    public class ApiController : Controller
    {
        private readonly ISessionManager _sessionManager;
        public ApiController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
        // GET: /<controller>/
        

        #region http
        [HttpPost]
        public async Task<JsonResult> searchPostTrend( string TagName, string txtSearch)
        {
            DalResult result = new DalResult();
            try
            {
                List<News> lstBLogs = Provider.DataAccessSQLServerService.SelectAllNews().Where(t => t.Status == 1).OrderByDescending(t => t.PageView).OrderByDescending(t => t.CreateTime).ToList();
                if(!string.IsNullOrEmpty(TagName))
                {
                    lstBLogs = lstBLogs.Where(t => t.MetaDescription.Contains(TagName)).ToList();
                }
                if(!string.IsNullOrEmpty(txtSearch))
                {
                    lstBLogs = lstBLogs.Where(t => t.Title.ToLower().Contains(txtSearch.ToLower())).ToList();
                }
                lstBLogs = lstBLogs.Take(3).ToList();
                result.Data = lstBLogs;
                result.IsSuccess = true;
                return Json(result);

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
                return Json(result);
            }

        }
        
        [HttpPost]
        public async Task<JsonResult> getMoreBlogTrend(List<int> listBlog, string TagName, string txtSearch)
        {
            DalResult result = new DalResult();
            try
            {
                List<News> lstBLogs = Provider.DataAccessSQLServerService.SelectAllNews().Where(t => !listBlog.Contains(t.Id) && t.Status == 1).OrderByDescending(t => t.PageView).OrderByDescending(t => t.CreateTime).ToList();
                if(!string.IsNullOrEmpty(TagName))
                {
                    lstBLogs = lstBLogs.Where(t => t.MetaDescription.Contains(TagName)).ToList();
                }
                if(!string.IsNullOrEmpty(txtSearch))
                {
                    lstBLogs = lstBLogs.Where(t => t.Title.Contains(txtSearch)).ToList();
                }
                if(lstBLogs.Count <=3)
                {
                    result.EffectRow = 999;
                }
                lstBLogs = lstBLogs.Take(3).ToList();


                result.Data = lstBLogs;
                result.IsSuccess = true;
                return Json(result);

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
                return Json(result);
            }

        }
        [HttpPost]
        public async Task<JsonResult> getMoreBlogAll(List<int> listBlog, string TagName)
        {
            DalResult result = new DalResult();
            try
            {
                List<News> lstBLogs = Provider.DataAccessSQLServerService.SelectAllNews().Where(t => !listBlog.Contains(t.Id) && t.Status == 1).OrderByDescending(t => t.CreateTime).ToList();
                if (lstBLogs.Count <= 3)
                {
                    result.EffectRow = 999;
                }
                if (string.IsNullOrEmpty(TagName))
                {
                    lstBLogs = lstBLogs.Take(3).ToList();
                }
                else
                {
                    lstBLogs = lstBLogs.Where(t =>  t.MetaDescription.Contains(TagName)).Take(3).ToList();
                }
                result.Data = lstBLogs;
                result.IsSuccess = true;
                return Json(result);

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
                return Json(result);
            }

        }
        
        [HttpPost]
        public async Task<JsonResult> getPostFTagTrend(string TagName)
        {
            DalResult result = new DalResult();
            try
            {
                List<News> lstBLogs = (Provider.DataAccessSQLServerService.SelectAllNews().Where(t => t.Status == 1 && t.MetaDescription.Contains(TagName)).OrderByDescending(t=> t.PageView).OrderByDescending(t => t.CreateTime)).ToList();
                result.Data = lstBLogs;
                result.IsSuccess = true;
                return Json(result);

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
                return Json(result);
            }

        }
        [HttpPost]
        public async Task<JsonResult> getPostFTagAll(string TagName)
        {
            DalResult result = new DalResult();
            try
            {
                List<News> lstBLogs = (Provider.DataAccessSQLServerService.SelectAllNews().Where(t => t.Status == 1 && t.MetaDescription.Contains(TagName)).OrderByDescending(t => t.CreateTime)).ToList();
                result.Data = lstBLogs;
                result.IsSuccess = true;
                return Json(result);

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
                return Json(result);
            }

        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Activity.Biz;
using Activity.Biz.Enum;
using Activity.DAL;
using Microsoft.AspNetCore.Mvc;
using Minxtu.DAL.Entity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace myG.GameGuild.Controllers
{
    public class ConfigController : Controller
    {
        private readonly ISessionManager _sessionManager;
        public ConfigController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            List<Config> lstData = Provider.DataAccessSQLServerService.SelectAllConfig();
            return View(lstData);
        }
        public IActionResult AddConfig()
        {
            ViewData["UserID"] = _sessionManager.AccountId;
            return View();
        }
        
        public IActionResult EditConfig(string Key)
        {
            ViewData["UserID"] = _sessionManager.AccountId;
            Config item = Provider.DataAccessSQLServerService.SelectConfigById(Key);
            return View(item);
        }

        #region http
        [HttpPost]
        public async Task<JsonResult> Add(string Key, string Value )
        {
            DalResult result = new DalResult();
            try
            {

                var newConfig = new Config
                {
                    Key = Key, 
                    Value = Value
                };


                Provider.DataAccessSQLServerService.InsertConfig(newConfig);
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
        public async Task<JsonResult> Update(string Key, string Value)
        {
            DalResult result = new DalResult();
            try
            {
                var item = Provider.DataAccessSQLServerService.SelectConfigById(Key);
                item.Value = Value;

                Provider.DataAccessSQLServerService.UpdateConfig(Key, item);

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
        //[HttpPost]
        //public async Task<JsonResult> Del(int Id)
        //{
        //    DalResult result = new DalResult();
        //    try
        //    {

        //        var item = Provider.DataAccessSQLServerService.SelectNewsById(Id);
        //        if (item == null)
        //        {
        //            result.IsSuccess = true;
        //            result.ErrorMessage = "Id không tồn tại.";
        //            return Json(result);
        //        }
        //        else
        //        {
        //            Provider.DataAccessSQLServerService.UpdateNewsStatus(Id, 0); // status = 0 - xóa
        //        }
        //        result.IsSuccess = true;
        //        return Json(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.IsSuccess = true;
        //        result.ErrorMessage = ex.Message;
        //        return Json(result);
        //    }

        //}
        #endregion
    }
}

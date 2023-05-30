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
    public class TagsController : Controller
    {
        private readonly ISessionManager _sessionManager;
        public TagsController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            List<Tag> lstData = (Provider.DataAccessSQLServerService.SelectAllTag().Where(x => x.Status != 0)).ToList();
            return View(lstData);
        }
        public IActionResult AddTag()
        {
            ViewData["UserID"] = _sessionManager.AccountId;
            return View();
        }
        
        public IActionResult EditTag(int Id)
        {
            ViewData["UserID"] = _sessionManager.AccountId;
            Tag item = Provider.DataAccessSQLServerService.SelectTagById(Id);
            return View(item);
        }

        #region http
        [HttpPost]
        public async Task<JsonResult> Add(string TagName, int Status )
        {

            DalResult result = new DalResult();
            try
            {

                var newTag = new Tag
                {
                    TagName = TagName, 
                    ProductList = "",
                    Count = 1,
                    Status = Status
                };


                Provider.DataAccessSQLServerService.InsertTag(newTag);
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
        public async Task<JsonResult> Update(int Id, string TagName, int Status)
        {
            DalResult result = new DalResult();
            try
            {
                var item = Provider.DataAccessSQLServerService.SelectTagById(Id);
                item.TagName = TagName;
                item.Status = Status;

                Provider.DataAccessSQLServerService.UpdateTag(Id, item);

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
        public async Task<JsonResult> Del(int Id)
        {
            DalResult result = new DalResult();
            try
            {

                var item = Provider.DataAccessSQLServerService.SelectTagById(Id);
                if (item == null)
                {
                    result.IsSuccess = true;
                    result.ErrorMessage = "Id không tồn tại.";
                    return Json(result);
                }
                else
                {
                    Provider.DataAccessSQLServerService.UpdateNewsStatus(Id, 0); // status = 0 - xóa
                }
                result.IsSuccess = true;
                return Json(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = true;
                result.ErrorMessage = ex.Message;
                return Json(result);
            }

        }
        #endregion
    }
}

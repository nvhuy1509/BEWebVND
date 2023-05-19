using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Activity.Biz;
using Activity.DAL;
using Microsoft.AspNetCore.Mvc;
using Minxtu.DAL.Entity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace myG.GameGuild.Controllers
{
    public class NewsController : Controller
    {
        private readonly ISessionManager _sessionManager;
        public NewsController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            List<News> lstBLogs = Provider.DataAccessSQLServerService.SelectAllNews().Where(t => t.Status == 1).ToList();
            return View(lstBLogs);
        }
        public IActionResult AddNew()
        {
            ViewData["UserID"] = _sessionManager.AccountId;
            return View();
        }
        
        public IActionResult EditNew(int id)
        {
            ViewData["UserID"] = _sessionManager.AccountId;
            News NewItem = Provider.DataAccessSQLServerService.SelectNewsById(id);
            return View(NewItem);
        }
         public IActionResult Detail()
        {
            List<News> lstBLogs = Provider.DataAccessSQLServerService.SelectAllNews().Where(t => t.Status != 0).ToList(); 
            return View(lstBLogs);
        }

        #region http
        [HttpPost]
        public async Task<JsonResult> Add(string Featured, string Title , string Description, string Content, string Url, int Status)
        {
            DalResult result = new DalResult();
            try
            {

                var newBlog = new News
                {
                    CategoryId = 1, // đặt mặc định = 1 là blog
                    Title = Title,
                    Description = Description,
                    MetaDescription = Featured,
                    MainContent = Content,
                    Thumb = Url,
                    PageView = 1, // đặt mặc định vì chưa có page quản lý
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    DisplayNo = 0,
                    Status = Status, // status == 1 : active
                };

                Provider.DataAccessSQLServerService.InsertNews(newBlog);
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
        public async Task<JsonResult> Update(int Id, string Featured, string Title, string Description, string Content, string Url, int Status)
        {
            DalResult result = new DalResult();
            try
            {
                var item = Provider.DataAccessSQLServerService.SelectNewsById(Id);
                item.Title = Title;
                item.Description = Description;
                item.Thumb = Url;
                item.MainContent = Content;
                item.MetaDescription = Featured;
                item.Status = Status;

                Provider.DataAccessSQLServerService.UpdateNews(Id, item);

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

                var item = Provider.DataAccessSQLServerService.SelectNewsById(Id);
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

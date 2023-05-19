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
    public class SolutionController : Controller
    {
        private readonly ISessionManager _sessionManager;
        public SolutionController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            List<Article> lstSoltuion = Provider.DataAccessSQLServerService.SelectAllArticle().Where(t => t.Status == (int)StatusAll.Active).ToList();
            return View(lstSoltuion);
        }
        public IActionResult AddSolution()
        {
            ViewData["UserID"] = _sessionManager.AccountId;
            return View();
        }

        public IActionResult EditSolution(int id)
        {
            ViewData["UserID"] = _sessionManager.AccountId;
            Article item = Provider.DataAccessSQLServerService.SelectArticleById(id);
            return View(item);
        }
        public IActionResult Detail()
        {
            List<Article> lstSoltuion = Provider.DataAccessSQLServerService.SelectAllArticle().Where(t => t.Status != (int)StatusAll.Delete).ToList();
            return View(lstSoltuion);
        }

        #region http
        [HttpPost]
        public async Task<JsonResult> Add(string Name, string Title, string Description, string Content, string Url, int Status)
        {
            DalResult result = new DalResult();
            try
            {

                var newSolution = new Article
                {
                    Name = Name,
                    Title = Title,
                    Description = Description,
                    MainContent = Content,
                    Thumb = Url,
                    PageView = 1, // đặt mặc định vì chưa có page quản lý
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    MenuId = 0,
                    Status = Status // status == 1 : active
                };

                Provider.DataAccessSQLServerService.InsertArticle(newSolution);
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
        public async Task<JsonResult> Update(int Id, string Name, string Title, string Description, string Content, string Url, int Status)
        {
            DalResult result = new DalResult();
            try
            {
                var item = Provider.DataAccessSQLServerService.SelectArticleById(Id);
                item.Title = Title;
                item.Description = Description;
                item.Thumb = Url;
                item.MainContent = Content;
                item.Name = Name;
                item.Status = Status;

                Provider.DataAccessSQLServerService.UpdateArticle(Id, item);

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

                var item = Provider.DataAccessSQLServerService.SelectArticleById(Id);
                if (item == null)
                {
                    result.IsSuccess = true;
                    result.ErrorMessage = "Id không tồn tại.";
                    return Json(result);
                }
                else
                {
                    Provider.DataAccessSQLServerService.UpdateArticleStatus(Id, 0);// status = 0 - xóa
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

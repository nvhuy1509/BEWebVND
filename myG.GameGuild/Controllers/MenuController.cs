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
    public class MenuController : Controller
    {
        private readonly ISessionManager _sessionManager;
        public MenuController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            List<Menu> lstData = Provider.DataAccessSQLServerService.SelectAllMenu();
            return View(lstData);
        }
        public IActionResult AddMenu()
        {
            ViewData["UserID"] = _sessionManager.AccountId;
            return View();
        }
        
        public IActionResult EditMenu(int id)
        {
            ViewData["UserID"] = _sessionManager.AccountId;
            Menu item = Provider.DataAccessSQLServerService.SelectMenuById(id);
            return View(item);
        }

        #region http
        [HttpPost]
        public async Task<JsonResult> Add(string Title, string Description, int Number, string Url, int ArticleId, int Status )
        {
            DalResult result = new DalResult();
            try
            {
                if (string.IsNullOrEmpty(Description))
                {
                    Description = "";
                }
                var newMenu = new Menu
                {
                    Title = Title,
                    Description = Description,
                    Icon = "",
                    ArticleId = ArticleId, // == 1 mặc định hiển thị ở cả header and footer, == 2 nếu chỉ hiện thị ở header, == 3 nếu chỉ hiển thị ở footer
                    LinkMenu = Url,
                    OrderNumber =  Number,
                    ParentId = 1, // để tạm
                    Status = Convert.ToByte(Status)
                };


                Provider.DataAccessSQLServerService.InsertMenu(newMenu);
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
        public async Task<JsonResult> Update(int Id,string Title, string Description, int Number, string Url, int ArticleId, int Status)
        {
            DalResult result = new DalResult();
            if (string.IsNullOrEmpty(Description))
            {
                Description = "";
            }
            try
            {
                var item = Provider.DataAccessSQLServerService.SelectMenuById(Id);
                item.Title = Title;
                item.Description = Description;
                item.Icon = "";
                item.ArticleId = ArticleId; // == 1 mặc định hiển thị ở cả header and footer, == 2 nếu chỉ hiện thị ở header, == 3 nếu chỉ hiển thị ở footer
                item.LinkMenu = Url;
                item.OrderNumber = Number;
                item.ParentId = 1; // để tạm
                item.Status = Convert.ToByte(Status);

                Provider.DataAccessSQLServerService.UpdateMenu(Id, item);

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

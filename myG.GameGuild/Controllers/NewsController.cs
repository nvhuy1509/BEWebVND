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
            List<Config> lstConfig = Provider.DataAccessSQLServerService.SelectAllConfig();
            var urlFb = lstConfig.Where(t => t.Key == "facebook").Select(t => t.Value).ToList();
            var urlTele = lstConfig.Where(t => t.Key == "telegram").Select(t => t.Value).ToList();
            var urlTt = lstConfig.Where(t => t.Key == "twitter").Select(t => t.Value).ToList();
            List<Menu> lstMenu = Provider.DataAccessSQLServerService.SelectAllMenu().Where(x => x.Status == 1).OrderBy(x => x.OrderNumber).ToList();
            List<Menu> headerMenu = lstMenu.Where(t => t.ArticleId == 1 || t.ArticleId == 2).ToList();
            List<Menu> footerMenu = lstMenu.Where(t => t.ArticleId == 1 || t.ArticleId == 3).ToList();
            List<News> tredingBLogs = (Provider.DataAccessSQLServerService.SelectAllNews().Where(t => t.Status == 1).OrderByDescending(t => t.PageView).OrderByDescending(t => t.CreateTime)).Take(3).ToList();
            List<News> lstBLogs = (Provider.DataAccessSQLServerService.SelectAllNews().Where(t => t.Status == 1)).OrderByDescending(t => t.CreateTime).Take(5).ToList();
            List<Tag> lstTag = (Provider.DataAccessSQLServerService.SelectAllTag().OrderByDescending(t => t.Count).Take(3)).ToList();
            foreach (News blog in lstBLogs)
            {
                var username = Provider.DataAccessSQLServerService.SelectAdminById(blog.Id);
            }
            dynamic mymodel = new ExpandoObject();
            mymodel.lstBLogs = lstBLogs;
            mymodel.tredingBLogs = tredingBLogs;
            mymodel.urlFb = urlFb[0];
            mymodel.urlTele = urlTele[0];
            mymodel.urlTt = urlTt[0];
            mymodel.lstTag = lstTag;
            mymodel.headerMenu = headerMenu;
            mymodel.footerMenu = footerMenu;
            return View(mymodel);
        }
        public IActionResult AddNew()
        {
            ViewData["UserID"] = _sessionManager.AccountId;
            List<Tag> lstTag = (Provider.DataAccessSQLServerService.SelectAllTag().Where(t => t.Status == 1)).ToList();
            return View(lstTag);
        }
        public IActionResult EditNew(int id)
        {
            ViewData["UserID"] = _sessionManager.AccountId;
            News NewItem = Provider.DataAccessSQLServerService.SelectNewsById(id);
            List<Tag> lstTag = (Provider.DataAccessSQLServerService.SelectAllTag().Where(t => t.Status == 1)).ToList();
            List<string> tags = JsonConvert.DeserializeObject<List<string>>(NewItem.MetaDescription);

            dynamic mymodel = new ExpandoObject();
            mymodel.dataItem = NewItem;
            mymodel.lstTag = tags;
            mymodel.lstTagAll = lstTag;

            return View(mymodel);
        }
        [Route("News/Post/{Id}")]
        public IActionResult DetailBlog(int id)
        {
            ViewData["UserID"] = _sessionManager.AccountId;
            News NewItem = Provider.DataAccessSQLServerService.SelectNewsById(id);
            List<News> lstBLogs = (Provider.DataAccessSQLServerService.SelectAllNews().Where(t => t.Status == 1).OrderByDescending(t => t.PageView).OrderByDescending(t => t.CreateTime)).Take(5).ToList();
            List<Config> lstConfig = Provider.DataAccessSQLServerService.SelectAllConfig();
            var urlFb = lstConfig.Where(t => t.Key == "facebook").Select(t => t.Value).ToList();
            var urlTele = lstConfig.Where(t => t.Key == "telegram").Select(t => t.Value).ToList();
            var urlTt = lstConfig.Where(t => t.Key == "twitter").Select(t => t.Value).ToList();
            List<Menu> lstMenu = Provider.DataAccessSQLServerService.SelectAllMenu().Where(x => x.Status == 1).OrderBy(x => x.OrderNumber).ToList();
            List<Menu> headerMenu = lstMenu.Where(t => t.ArticleId == 1 || t.ArticleId == 2).ToList();
            List<Menu> footerMenu = lstMenu.Where(t => t.ArticleId == 1 || t.ArticleId == 3).ToList();

            dynamic mymodel = new ExpandoObject();
            mymodel.NewItem = NewItem;
            mymodel.lstBLogs = lstBLogs;
            mymodel.urlFb = urlFb[0];
            mymodel.urlTele = urlTele[0];
            mymodel.urlTt = urlTt[0];
            mymodel.headerMenu = headerMenu;
            mymodel.footerMenu = footerMenu;


            return View(mymodel);
        }
        public IActionResult Detail()
        {
            List<News> lstBLogs = Provider.DataAccessSQLServerService.SelectAllNews().Where(t => t.Status != 0).OrderByDescending(t => t.CreateTime).ToList(); 
            return View(lstBLogs);
        }

        #region http
        [HttpPost]
        public async Task<JsonResult> Add(string Title, string Thumb, string Description, string Content, string MetaDescription, int Status, string Author)
        {
            DalResult result = new DalResult();
            try
            {

                var newBlog = new News
                {
                    CategoryId = 1, // đặt mặc định = 1 là blog
                    Title = Title,
                    Description = Description,
                    MetaDescription = MetaDescription,
                    MainContent = Content.Replace("<img src=\"..", "<img src=\""),
                    Thumb = Thumb,
                    PageView = 1, // đặt mặc định vì chưa có page quản lý
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    DisplayNo = int.Parse(_sessionManager.AccountId), //Id người tạo để lấy author
                    Status = Status, // status == 1 : active
                    Author = Author 
                };

                Provider.DataAccessSQLServerService.InsertNews(newBlog);

                List<string> listTag = JsonConvert.DeserializeObject<List<string>>(MetaDescription);
                for (int i = 0; i < listTag.Count; i++)
                {
                    var itemTag = Provider.DataAccessSQLServerService.SelectTagByTagName(listTag[i]);
                    itemTag.Count += 1;
                    Provider.DataAccessSQLServerService.UpdateTag(itemTag.Id, itemTag);
                }


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
        public async Task<JsonResult> Update(string Id, string Title, string Thumb, string Description, string Content, string MetaDescription, int Status, string Author)
        {
            DalResult result = new DalResult();
            try
            {
                var item = Provider.DataAccessSQLServerService.SelectNewsById( Convert.ToInt64(Id) );
                item.Title = Title;
                item.Description = Description;
                item.Thumb = Thumb;
                item.MainContent = Content.Replace("<img src=\"..", "<img src=\"");
                item.MetaDescription = MetaDescription;
                item.Status = Status;
                item.Author = Author;


                Provider.DataAccessSQLServerService.UpdateNews(Convert.ToInt64(Id), item);

                
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

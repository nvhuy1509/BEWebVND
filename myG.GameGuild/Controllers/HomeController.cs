using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Activity.Biz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myG.GameGuild.Models;
using SmartBreadcrumbs.Attributes;
using Minxtu.DAL.Entity;
using System.Dynamic;

namespace myG.GameGuild.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISessionManager _sessionManager;
        private readonly Guid accountId = Guid.Empty;
        public HomeController(ILogger<HomeController> logger, ISessionManager sessionManager)
        {

            //var news =   Provider.DataAccessSQLServerService.Inss("");
              //Provider.DataAccessSQLServerService.New("");


            Guid.TryParse(sessionManager.AccountId, out accountId);
            _logger = logger;
            _sessionManager = sessionManager;
        }
        [DefaultBreadcrumb("Trang chủ")]
        public async Task<IActionResult> Index()
        {
            List<Config> lstConfig = Provider.DataAccessSQLServerService.SelectAllConfig();
            var urlFb = lstConfig.Where(t => t.Key == "facebook").Select(t => t.Value).ToList();
            var urlTele = lstConfig.Where(t => t.Key == "telegram").Select(t => t.Value).ToList();
            var urlMail = lstConfig.Where(t => t.Key == "email").Select(t => t.Value).ToList();
            var urlTt = lstConfig.Where(t => t.Key == "twitter").Select(t => t.Value).ToList();
            var urlAp = lstConfig.Where(t => t.Key == "downloadAppStore").Select(t => t.Value).ToList();
            var urlAndr = lstConfig.Where(t => t.Key == "downloadAndroid").Select(t => t.Value).ToList();

            List<News> lstBLogs = (Provider.DataAccessSQLServerService.SelectAllNews().Where(t => t.Status == 1).OrderByDescending(t => t.PageView).OrderByDescending(t => t.CreateTime)).Take(5).ToList();
            List<Menu> lstMenu = Provider.DataAccessSQLServerService.SelectAllMenu().Where(x => x.Status == 1).OrderBy(x => x.OrderNumber).ToList();
            List<Menu> headerMenu = lstMenu.Where(t => t.ArticleId == 1 || t.ArticleId == 2).ToList();
            List<Menu> footerMenu = lstMenu.Where(t => t.ArticleId == 1 || t.ArticleId == 3).ToList();
            dynamic mymodel = new ExpandoObject();
            mymodel.urlFb = urlFb[0];
            mymodel.urlTele = urlTele[0];
            mymodel.urlMail = urlMail[0];
            mymodel.urlTt = urlTt[0];
            mymodel.urlAp = urlAp[0];
            mymodel.urlAndr = urlAndr[0];
            mymodel.lstBLogs = lstBLogs;
            mymodel.headerMenu = headerMenu;
            mymodel.footerMenu = footerMenu;

            return View(mymodel);
        }

        public async Task<IActionResult> IndexAdmin()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}

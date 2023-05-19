using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Activity.Biz;
using Activity.DAL.Entity.GameGuild;
using Activity.DAL.Entity;
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

            dynamic mymodel = new ExpandoObject();
            mymodel.urlFb = urlFb[0];
            mymodel.urlTele = urlTele[0];
            mymodel.urlMail = urlMail[0];
            mymodel.urlTt = urlTt[0];
            mymodel.urlAp = urlAp[0];
            mymodel.urlAndr = urlAndr[0];

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

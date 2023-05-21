using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Activity.Biz;
using Activity.DAL;
using Microsoft.AspNetCore.Mvc;
using Minxtu.DAL.Entity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace myG.GameGuild.Controllers
{
    public class WorkController : Controller
    {
        private readonly ISessionManager _sessionManager;
        private readonly IMailService mailService;
        public WorkController(ISessionManager sessionManager, IMailService mailService)
        {
            _sessionManager = sessionManager;
            this.mailService = mailService;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            List<Config> lstConfig = Provider.DataAccessSQLServerService.SelectAllConfig();
            var urlFb = lstConfig.Where(t => t.Key == "facebook").Select(t => t.Value).ToList();
            var urlTele = lstConfig.Where(t => t.Key == "telegram").Select(t => t.Value).ToList();
            var urlMail = lstConfig.Where(t => t.Key == "email").Select(t => t.Value).ToList();
            var urlTt = lstConfig.Where(t => t.Key == "twitter").Select(t => t.Value).ToList();

            dynamic mymodel = new ExpandoObject();
            mymodel.urlFb = urlFb[0];
            mymodel.urlTele = urlTele[0];
            mymodel.urlMail = urlMail[0];
            mymodel.urlTt = urlTt[0];
            return View(mymodel);
        }

    }
}

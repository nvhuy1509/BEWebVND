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
    public class ContactController : Controller
    {
        private readonly ISessionManager _sessionManager;
        private readonly IMailService mailService;
        public ContactController(ISessionManager sessionManager, IMailService mailService)
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
            List<Menu> lstMenu = Provider.DataAccessSQLServerService.SelectAllMenu().Where(x => x.Status == 1).OrderBy(x => x.OrderNumber).ToList();
            List<Menu> headerMenu = lstMenu.Where(t => t.ArticleId == 1 || t.ArticleId == 2).ToList();
            List<Menu> footerMenu = lstMenu.Where(t => t.ArticleId == 1 || t.ArticleId == 3).ToList();

            dynamic mymodel = new ExpandoObject();
            mymodel.urlFb = urlFb[0];
            mymodel.urlTele = urlTele[0];
            mymodel.urlMail = urlMail[0];
            mymodel.urlTt = urlTt[0];
            mymodel.headerMenu = headerMenu;
            mymodel.footerMenu = footerMenu;
            return View(mymodel);
        }

        public IActionResult Feedback()
        {
            List<Feedback> lstFeedback = Provider.DataAccessSQLServerService.SelectAllFeedback();
            return View(lstFeedback);
        }

        #region http

        [HttpPost]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            DalResult result = new DalResult();
            try
            {
                await mailService.SendEmailAsync(request);

                var newFeedback = new Feedback
                {
                    NameGuess = request.Name,
                    Email = request.Mail,
                    PhoneNumber = request.Phone,
                    CreateTime = DateTime.Now
                };

                Provider.DataAccessSQLServerService.InsertFeedback(newFeedback);

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

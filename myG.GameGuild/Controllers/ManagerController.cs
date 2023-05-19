using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace myG.GameGuild.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ISessionManager _sessionManager;
        public ManagerController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {

            if(_sessionManager.UserType1 == Activity.DAL.Enum.UserType.CenterAdmin)
            {
                
            }
            if(_sessionManager.UserType1 == Activity.DAL.Enum.UserType.Manager)
            {

            }

            return View();
        }
        public IActionResult AddManager()
        {
            return View();
        }
    }
}

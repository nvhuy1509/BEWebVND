using Activity.Biz;
using Activity.DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using SmartBreadcrumbs.Attributes;
using Activity.Biz.Enum;

namespace myG.GameGuild.Controllers
{
    public class UserController : Controller
    {
        [Breadcrumb("Danh sách tài khoản")]
        public async  Task<IActionResult> Index()
        {
            var listUser =  Provider.DataAccessSQLServerService.SelectAllAdmin().Where(t => t.Status != (int)AdminStatus.Delete).ToList();
            return View(listUser);
        }

        [HttpPost]
        public async Task<IActionResult> DelUser(Guid Id)
        {
            try
            {
                var user = await Provider.DataAccessService.GetByIdAsync<Activity.DAL.Entity.Contact, Guid>(Id);
                user.Status = (int)AdminStatus.Delete;
                await Provider.DataAccessService.UpdateRecord(user);
                return Ok(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Ok(false);
            }
        }

    }
}

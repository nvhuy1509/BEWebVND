using Activity.Biz;
using Activity.DAL.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myG.GameGuild.Models;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace myG.GameGuild.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHostingEnvironment Environment;
        private readonly ISessionManager _sessionManager;

        public ProfileController(IHostingEnvironment hostingEnvironment, ISessionManager sessionManager)
        {
            Environment = hostingEnvironment;
            _sessionManager = sessionManager;
        }
        [Breadcrumb("Thông tin tài khoản")]
        public async Task<IActionResult> Index()
        {
           
            var userinformation = await Provider.DataAccessService.GetByIdAsync<UserInformation, Guid>(Guid.Parse(_sessionManager.AccountId));
            var usercontact = await Provider.DataAccessService.FindOneAsync<Contact>(x => x.UserId == userinformation.Id);
           
         
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(UploadModel files)
        {
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();

            string fileName = Path.GetFileName(files.File.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                files.File.CopyTo(stream);
                uploadedFiles.Add(fileName);
            }

            return Ok(files.File.FileName);
        }

        [HttpPost]
        public async Task<IActionResult> Capnhatthongtin(string Name, string Address, string Email, string Facebook, string Phonenumber, string Telegram, string Discord, string urlCMND_mattruoc, string urlCMND_matsau,string soCMND)
        {

            var userinformation = await Provider.DataAccessService.GetByIdAsync<UserInformation, Guid>(Guid.Parse(_sessionManager.AccountId));

            var usercontact = await Provider.DataAccessService.FindOneAsync<Contact>(x => x.UserId == userinformation.Id);
            
          
                return Ok(true);
        }
    
        [HttpPost]
        public async Task<IActionResult> Thongtinck(string Type,string Addresswallet,string name)
        {
          
            return Ok(true);
        }

        [HttpPost]
        public async Task<IActionResult> Xacminh(Guid UserId)
        {
            var usercontact = await Provider.DataAccessService.FindOneAsync<Contact>(x=> x.UserId == UserId);
           
            return Ok(true);
        }
        [Breadcrumb("Thông tin tài khoản",FromController = typeof(UserController),FromAction ="Index")]
        public async Task<IActionResult> ViewProfile(Guid UserId)
        {
            var Profile = await Provider.DataAccessService.FindOneAsync<Contact>(x=> x.UserId == UserId);
            
            return View(Profile);
        }

    }


}

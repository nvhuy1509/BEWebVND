using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Activity.Biz;
using Activity.DAL.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Minxtu.DAL.Entity;
using myG.GameGuild.Models;
using MyGo.Core.Utils;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace myG.GameGuild.Controllers
{
    //[Route("uploads")]
    public class UploadFileController : Controller
    {
        private readonly IHostingEnvironment Environment;
        private readonly ISessionManager _sessionManager;
        private readonly AppSetting _appSetting;
        private readonly IUploadImageService _imageService;

        public UploadFileController(IHostingEnvironment hostingEnvironment, IUploadImageService context, ISessionManager sessionManager, IOptions<AppSetting> options)
        {
            _imageService = context;
            Environment = hostingEnvironment;
            _appSetting = options.Value;
            _sessionManager = sessionManager;
        }
        [HttpPost]
        // GET: /<controller>/
        public IActionResult Index(UploadModel files)
        {
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;
            string pathRoot = "uploads";
            string path = Path.Combine(wwwPath, pathRoot);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            string extension = System.IO.Path.GetExtension(files.File.FileName);
            if (extension == ".jpg" || extension == ".png" || extension == ".jpeg" )
            {
                string fileName = Path.GetFileName(files.File.FileName);
                fileName = TextHelper.NonUnicode(fileName);
                fileName = fileName.Replace(" ", "_");
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    files.File.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                }

                return Ok("/" + pathRoot + "/" + fileName);
            }
            else
            {
                return Ok(false);
            }
            
           
        }

        public IActionResult UploadCc(UploadModel files)
        {
           
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;
            string pathRoot = "uploads";
            string path = Path.Combine(wwwPath, pathRoot);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            string extension = System.IO.Path.GetExtension(files.File.FileName);

            if (extension == ".jpg" || extension == ".png")
            {
                string fileName = Path.GetFileName(files.File.FileName);
                fileName = TextHelper.NonUnicode(fileName);
                fileName = fileName.Replace(" ", "_");
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    files.File.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                }

                return Ok("/" + pathRoot + "/" + fileName);
            }
            else
            {
                return Ok(false);
            }
            


        }

        [HttpPost]

        public async Task<ContentResult> UploadFile(ItemGame request)
        {

            try
            {
                Random rnd = new Random();
                var a = request;
                var b = request.Item.FileName;

                var uploadReqeust = new FileUploadRequest
                {
                    UserId = new Guid(),
                    FileType = request.Item.ContentType,
                    FileName = rnd.Next(1, 999999) + request.Item.FileName,
                    File = request.Item.GetBytes(),
                    Path = $"{Constants.FileUpload.MMC_User}/{request.IdUser}"
                };

                if (request.IdUser == null)
                {
                    uploadReqeust.Path = $"{Constants.FileUpload.MMC_UserNull}/Other";
                }
                if (request.IdUser != null)
                {
                    uploadReqeust.Path = $"{Constants.FileUpload.MMC_User}/{request.IdUser}";
                }
                if (request.Item.ContentType == "application/x-zip-compressed")
                {
                    uploadReqeust.Path = $"{Constants.FileUpload.Thumbnail}/{Guid.NewGuid().ToString()}";
                }
                var result = await _imageService.UploadFile(request.Item, uploadReqeust.Path, uploadReqeust.FileName);


                if (result && request.Item.ContentType == "application/x-zip-compressed")
                {
                    string sourceFile = $"{_appSetting.FileFolder}/{uploadReqeust.Path}/{uploadReqeust.FileName}";
                    string pathDir = $"{_appSetting.FileFolder}/{uploadReqeust.Path}";
                    ZipFile.ExtractToDirectory(sourceFile, pathDir);

                    string responseUrlspine = string.Format("{0}/images/{1}/{2}", _appSetting.FileUrl, uploadReqeust.Path, uploadReqeust.FileName);
                    string reponse1 = string.Format("{{\"uploaded\": 1,\"fileName\": \"{0}\",\"url\": \"{1}\"}}", uploadReqeust.FileName, responseUrlspine);
                    return Content(reponse1);
                }

                string responseUrl = string.Format("{0}/images/{1}/{2}", _appSetting.FileUrl, uploadReqeust.Path, uploadReqeust.FileName);
                string reponse = string.Format("{{\"uploaded\": 1,\"fileName\": \"{0}\",\"url\": \"{1}\"}}", uploadReqeust.FileName, responseUrl);


                try
                {
                    var mediaUser = new Photo
                    {
                        AlbumId = 1,  // đang đặt tạm = 1 vì chưa có phần quản lý album
                        Name = uploadReqeust.FileName,
                        Description = "Upload_By" + request.IdUser,
                        ImageUrl = "/images/" + uploadReqeust.Path + "/" + uploadReqeust.FileName,
                        CreateTime = DateTime.Now,
                        Status = 1,
                    };
                    //await Provider.DataAccessService.AddRecord(mediaUser);
                    Provider.DataAccessSQLServerService.InsertPhoto(mediaUser);

                    return Content(reponse.Replace(_appSetting.FileUrl, ""));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception => :" + ex);
                    string reponse1 = string.Format("{{\"uploaded\": 0,\"fileName\": \"{0}\",\"url\": \"{1}\",\"classItem\":\"{2}\",\"error\":\"{3}\" }}", "", "", "", ex.Message);
                    return Content(reponse1);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception => :" + ex);

                string reponse = string.Format("{{\"uploaded\": 0,\"fileName\": \"{0}\",\"url\": \"{1}\",\"classItem\":\"{2}\",\"error\":\"{3}\" }}", "", "", "", ex.Message);
                return Content(reponse);
            }
        }

        public async Task<ContentResult> UploadFileTiny(ItemGameTiny request)
        {

            try
            {
                Random rnd = new Random();
                var a = request;
                var b = request.File.FileName;

                var uploadReqeust = new FileUploadRequest
                {
                    UserId = new Guid(),
                    FileType = request.File.ContentType,
                    FileName = rnd.Next(1, 999999) + request.File.FileName,
                    File = request.File.GetBytes(),
                    Path = $"{Constants.FileUpload.MMC_User}/{request.IdUser}"
                };

                if (request.IdUser == null)
                {
                    uploadReqeust.Path = $"{Constants.FileUpload.MMC_UserNull}/Other";
                }
                if (request.IdUser != null)
                {
                    uploadReqeust.Path = $"{Constants.FileUpload.MMC_User}/{request.IdUser}";
                }
                if (request.File.ContentType == "application/x-zip-compressed")
                {
                    uploadReqeust.Path = $"{Constants.FileUpload.Thumbnail}/{Guid.NewGuid().ToString()}";
                }
                var result = await _imageService.UploadFile(request.File, uploadReqeust.Path, uploadReqeust.FileName);


                if (result && request.File.ContentType == "application/x-zip-compressed")
                {
                    string sourceFile = $"{_appSetting.FileFolder}/{uploadReqeust.Path}/{uploadReqeust.FileName}";
                    string pathDir = $"{_appSetting.FileFolder}/{uploadReqeust.Path}";
                    ZipFile.ExtractToDirectory(sourceFile, pathDir);

                    string responseUrlspine = string.Format("{0}/images/{1}/{2}", _appSetting.FileUrl, uploadReqeust.Path, uploadReqeust.FileName);
                    string reponse1 = string.Format("{{\"uploaded\": 1,\"fileName\": \"{0}\",\"url\": \"{1}\"}}", uploadReqeust.FileName, responseUrlspine);
                    return Content(reponse1);
                }

                string responseUrl = string.Format("{0}/images/{1}/{2}", _appSetting.FileUrl, uploadReqeust.Path, uploadReqeust.FileName);
                string reponse = string.Format("{{\"uploaded\": 1,\"fileName\": \"{0}\",\"location\": \"{1}\"}}", uploadReqeust.FileName, responseUrl);


                try
                {
                    var mediaUser = new Photo
                    {
                        AlbumId = 1,  // đang đặt tạm = 1 vì chưa có phần quản lý album
                        Name = uploadReqeust.FileName,
                        Description = "Upload_By" + request.IdUser,
                        ImageUrl = "/images/" + uploadReqeust.Path + "/" + uploadReqeust.FileName,
                        CreateTime = DateTime.Now,
                        Status = 1,
                    };
                    //await Provider.DataAccessService.AddRecord(mediaUser);
                    Provider.DataAccessSQLServerService.InsertPhoto(mediaUser);

                    return Content(reponse.Replace(_appSetting.FileUrl, ""));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception => :" + ex);
                    string reponse1 = string.Format("{{\"uploaded\": 0,\"fileName\": \"{0}\",\"url\": \"{1}\",\"classItem\":\"{2}\",\"error\":\"{3}\" }}", "", "", "", ex.Message);
                    return Content(reponse1);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception => :" + ex);

                string reponse = string.Format("{{\"uploaded\": 0,\"fileName\": \"{0}\",\"url\": \"{1}\",\"classItem\":\"{2}\",\"error\":\"{3}\" }}", "", "", "", ex.Message);
                return Content(reponse);
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Activity.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace myG.GameGuild
{
    public interface IUploadImageService
    {
        // Task<bool> UploadImageResponse(FileUploadRequest request);
        Task<bool> UploadFile(IFormFile fileUpload, string path, string fileName);
    }

    public class UploadImageService : IUploadImageService
    {
        private readonly AppSetting _appSetting;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UploadImageService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IOptions<AppSetting> options)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _appSetting = options.Value;
        }

        public async Task<bool> UploadImageResponse(FileUploadRequest request)
        {
            Debug.Write("UploadFile");
            var uri = $"{_appSetting.FileUrl}/api/upload-file/image";
            var content = GetStringContent(request);

            BaseHttpFactory baseHttpFactory = new BaseHttpFactory(_httpClientFactory, _httpContextAccessor);
            var response = (await baseHttpFactory.PostAsync<FileUploadResponse>(Constants.FileUpload.FileUploadHttpClient, uri, content)).FirstOrDefault();
            return response.Value != null && response.Value.Success;

        }




        private MultipartFormDataContent GetStringContent(FileUploadRequest request)
        {
            Debug.Write("UploadFile");
            var requestContent = new MultipartFormDataContent();

            var imageContent = new ByteArrayContent(request.File);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(request.FileType);

            requestContent.Add(imageContent, "File", request.FileName);

            requestContent.Add(new StringContent(request.Path), "Path");

            return requestContent;
        }

        public async Task<bool> UploadFile(IFormFile fileUpload, string path, string fileName)
        {

            if (fileUpload != null)
            {

                //Set Key Name
                // string ImageName = Guid.NewGuid().ToString() + fileUpload.FileName.ToString().ConvertToUnSign3().Replace(" ", "");
                //+ Path.GetExtension(fileUpload.FileName.ToString().ConvertToUnSign3().Replace(" ", ""));

                string pathFull = string.Format("{0}/{1}", _appSetting.FileFolder, path);
                if (!Directory.Exists(path)) Directory.CreateDirectory(pathFull);

                //Get url To Save
                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), pathFull, fileName);

                using (var stream = new FileStream(SavePath, FileMode.Create))
                {
                    fileUpload.CopyTo(stream);
                    return true;
                }
            }
            return false;
        }


    }
}

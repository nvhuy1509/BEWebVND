using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.OleDb;
using OfficeOpenXml;
using System.Diagnostics;
using Activity.DAL.Entity.GameGuild;
using Activity.Biz;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace myG.GameGuild.Controllers
{
    public class ExcelController : Controller
    {
        public ExcelController()
        {
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> upload(AvatarUserFileUpload request)
        {
            Debug.WriteLine("Upload excel file");


            List<StepnReport> userList = new List<StepnReport>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            try
            {
                var file = request.Avatar.OpenReadStream();
                // mở file excel
                var package = new ExcelPackage(file);

                // lấy ra sheet đầu tiên để thao tác
                ExcelWorksheet workSheet = package.Workbook.Worksheets[0];

                // duyệt tuần tự từ dòng thứ 2 đến dòng cuối cùng của file. lưu ý file excel bắt đầu từ số 1 không phải số 0
                int startRow = 2;
                for (int i = startRow; i <= workSheet.Dimension.End.Row; i++)
                {
                    try
                    {
                        // biến j biểu thị cho một column trong file
                        int j = 1;

                        // lấy ra cột họ tên tương ứng giá trị tại vị trí [i, 1]. i lần đầu là 2
                        // tăng j lên 1 đơn vị sau khi thực hiện xong câu lệnh
                        var t1 = workSheet.Cells[i, 1].Value == null ? "0" : workSheet.Cells[i, 1].Value.ToString();
                        var t2 = workSheet.Cells[i, 2].Value == null ? "0" : workSheet.Cells[i, 2].Value.ToString() ;
                        var t3 = workSheet.Cells[i, 3].Value == null ? "0" : workSheet.Cells[i, 3].Value.ToString();
                        var t4 = workSheet.Cells[i, 4].Value == null ? "0" : workSheet.Cells[i, 4].Value.ToString();
                        var t5 = workSheet.Cells[i, 5].Value == null ? "0" : workSheet.Cells[i, 5].Value.ToString();
                        var t6 = workSheet.Cells[i, 6].Value == null ? "0" : workSheet.Cells[i, 6].Value.ToString();
                        var t7 = workSheet.Cells[i, 7].Value == null ? "0" : workSheet.Cells[i, 7].Value.ToString();
                        var t8 = workSheet.Cells[i, 8].Value == null ? "0" : workSheet.Cells[i, 8].Value.ToString();
                        var t9 = workSheet.Cells[i, 9].Value == null ? "0" : workSheet.Cells[i, 9].Value.ToString();
                        var t10 = workSheet.Cells[i, 10].Value == null ? "0" : workSheet.Cells[i, 10].Value.ToString();
                        var t11 = workSheet.Cells[i, 11].Value == null ? "0" : workSheet.Cells[i, 11].Value.ToString();
                        var t12 = workSheet.Cells[i, 12].Value == null ? "0" : workSheet.Cells[i, 12].Value.ToString();
                        var t13 = workSheet.Cells[i, 13].Value == null ? "0" : workSheet.Cells[i, 13].Value.ToString();
                        var t14 = workSheet.Cells[i, 14].Value == null ? "0" : workSheet.Cells[i, 14].Value.ToString();
                        var t15 = workSheet.Cells[i, 15].Value == null ? "0" : workSheet.Cells[i, 15].Value.ToString();
                        var t16 = workSheet.Cells[i, 16].Value == null ? "0" : workSheet.Cells[i, 16].Value.ToString();
                        var t17 = workSheet.Cells[i, 17].Value == null ? "0" : workSheet.Cells[i, 17].Value.ToString();
                        var t18 = workSheet.Cells[i, 18].Value == null ? "0" : workSheet.Cells[i, 18].Value.ToString();
                        var t19 = workSheet.Cells[i, 19].Value == null ? "0" : workSheet.Cells[i, 19].Value.ToString();
                        var a =  workSheet.Cells[i, 20].Value == null ? "0" : workSheet.Cells[i, 20].Value.ToString();
                        var t29 = workSheet.Cells[i, 29].Value == null ? "0" : workSheet.Cells[i, 29].Value.ToString();
                      //  string t30 = workSheet.Cells[i, 30].Value.ToString();

                        //DateTime td1 = DateTime.ParseExact(t30 , "d/M/yyyy HH:mm:ss",
                        //               System.Globalization.CultureInfo.InvariantCulture);
                       
                        //// tạo UserInfo từ dữ liệu đã lấy được
                        var level = string.IsNullOrEmpty(t7) ? 0 : float.Parse(t7);
                        var energy = string.IsNullOrEmpty(t8) ? 0 : float.Parse(t8);
                        var km = string.IsNullOrEmpty(t9) ? 0 : float.Parse(t9);
                        var time = (string.IsNullOrEmpty(t10) ? 0 : float.Parse(t10)) + " : " + (string.IsNullOrEmpty(t11) ? 0 : float.Parse(t11)) + " : " + (string.IsNullOrEmpty(t12) ? 0 : float.Parse(t12));
                        var steps = string.IsNullOrEmpty(t13) ? 0 : float.Parse(t13);
                        var tokenRun = string.IsNullOrEmpty(t14) ? 0 : float.Parse(t14);
                        var durability = string.IsNullOrEmpty(t15) ? 0 : float.Parse(t15);
                        var efficiency = string.IsNullOrEmpty(t17) ? 0 : float.Parse(t17);
                        var luck = string.IsNullOrEmpty(t18) ? 0 : float.Parse(t18);
                        var comfort = string.IsNullOrEmpty(t19) ? 0 : float.Parse(t19);
                        var resilience = string.IsNullOrEmpty(t19) ? 0 : float.Parse(t19);
                        StepnReport user = new StepnReport()
                        {
                            idShoes = t2,
                            date = DateTime.ParseExact(t29, "d/M/yyyy HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture),
                            tokenTotal = 0,
                            quality = t5,
                            type = t6,
                            level = string.IsNullOrEmpty(t7) ? 0: float.Parse(t7),
                            energy = string.IsNullOrEmpty(t8) ? 0 : float.Parse(t8),
                            km = string.IsNullOrEmpty(t9) ? 0 : float.Parse(t9),
                            time = (string.IsNullOrEmpty(t10) ? 0 : float.Parse(t10)) + " : "+ (string.IsNullOrEmpty(t11) ? 0 : float.Parse(t11)) + " : " + (string.IsNullOrEmpty(t12) ? 0 : float.Parse(t12)),
                            steps = string.IsNullOrEmpty(t13) ? 0 : float.Parse(t13),
                            tokenRun = string.IsNullOrEmpty(t14) ? 0 : float.Parse(t14),
                            durability = string.IsNullOrEmpty(t15) ? 0 : float.Parse(t15),
                            efficiency = string.IsNullOrEmpty(t17) ? 0 : float.Parse(t17),
                            luck = string.IsNullOrEmpty(t18) ? 0 : float.Parse(t18),
                            comfort = string.IsNullOrEmpty(t19) ? 0 : float.Parse(t19),
                            resilience = string.IsNullOrEmpty(t19) ? 0 : float.Parse(t19),
                            
                        };

                        //// add UserInfo vào danh sách userList
                        userList.Add(user);
                        Debug.WriteLine(1);
                    }
                    catch (Exception exe)
                    {
                        Debug.WriteLine(exe);
                    }
                }
            }
            catch (Exception ee)
            {
                Debug.WriteLine(ee);
            }


            foreach (var report in userList)
            {

                var idShoes = report.idShoes;
                if (!idShoes.StartsWith("#")) idShoes = "#"+idShoes;
                List<string> imageReports = new List<string>();
                var gameAccount = await Provider.DataAccessService.FindOneAsync<GameAccount>(t => t.DisplayName == idShoes);
                if(gameAccount != null)
                {
                    PropertyReport propertyReport = new PropertyReport();
                    propertyReport.GameAccountId = gameAccount.Id;
                    propertyReport.Property = report.JsonSerialize();

                    propertyReport.Image = imageReports.JsonSerialize();
                    propertyReport.CreatedDate = DateTime.Now;
                    propertyReport.UpdateDate = DateTime.Now;
                    propertyReport.PropertyFromImage = imageReports.JsonSerialize();
                    propertyReport.CreateDateInt = int.Parse(report.date.ToString("yyyyMMdd"));
                    await Provider.DataAccessService.AddRecord(propertyReport);
                }
                
            }


            //var a = request;
            //var uploadReqeust = new FileUploadRequest
            //{
            //    UserId = Guid.Parse(_sessionManager.AccountId),
            //    FileType = request.Avatar.ContentType,
            //    FileName = request.Avatar.FileName,
            //    File = request.Avatar.GetBytes(),
            //    Path = $"{Constants.FileUpload.UserPath}/{_sessionManager.AccountId}"
            //};
            //var result = await _imageService.UploadImageResponse(uploadReqeust);
            //string responseUrl = string.Format("{0}/images/{1}/{2}", _appSetting.FileUrl, uploadReqeust.Path, uploadReqeust.FileName);
            //string reponse = string.Format("{{\"uploaded\": 1,\"fileName\": \"{0}\",\"url\": \"{1}\"}}", uploadReqeust.FileName, responseUrl);
            return Json(/*reponse*/"OK");
        }
    }
}

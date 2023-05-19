using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Activity.Biz;
using Activity.DAL;
using Activity.DAL.Entity;
using Activity.DAL.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Minxtu.DAL.Entity;
using MyGo.Core;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace myG.GameGuild.Controllers
{
    public class AuthController : Controller
    {
        private readonly ISessionManager _sessionManager;
        private readonly AppSetting _appSetting;
        public AuthController(ISessionManager sessionManager, IOptions<AppSetting> options)
        {
            _sessionManager = sessionManager;
            _appSetting = options.Value;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }
        #region http
        public async Task<JsonResult> Login(string username, string password)
        {
            DalResult result = new DalResult();
            try
            {
                if (string.IsNullOrEmpty(username.Trim()) || string.IsNullOrEmpty(password.Trim()))
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Không được để trống tên đăng nhập và mật khẩu";
                    return Json(result);
                }


                string passHash = Encrypt.MD5(password);

                //var user = await Provider.DataAccessService.FindOneAsync<UserInformation>(t => t.UserName.ToLower() == username.ToLower() && t.Status == GAStatus.Active);



                var user =  Provider.DataAccessSQLServerService.SelectAllAdmin().Where(t => t.UserName == username && t.Password == passHash).ToList();

                if (user.Count <= 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Sai tên tài khoản hoặc mật khẩu. Vui lòng kiểm tra lại";
                    return Json(result);
                }

                _sessionManager.AccountId = (user[0].Id).ToString();
                _sessionManager.AccountName = user[0].UserName;
                _sessionManager.LevelId = (user[0].Level).ToString();

                #region Jwt Token
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(_appSetting.JwtToken);
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, username)}),
                    // Duration of the Token
                    // Now the the Duration to 1 Hour
                    Expires = DateTime.UtcNow.AddHours(6),

                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature) //setting sha256 algorithm
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var Token = tokenHandler.WriteToken(token);
                Debug.WriteLine("Token => " + Token);
                #endregion

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

        public async Task<JsonResult> Reg(string username, string password)
        {
            DalResult result = new DalResult();
            try
            {
                if (string.IsNullOrEmpty(username.Trim()) || string.IsNullOrEmpty(password.Trim()))
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Không được để trống tên đăng nhập và mật khẩu";
                    return Json(result);
                }


                string passHash = Encrypt.MD5(password);

                var user = await Provider.DataAccessService.FindOneAsync<UserInformation>(t => t.UserName == username && t.Status == GAStatus.Active);

                if (user == null) // chua co tai khoan nao
                {
                    var newuser = new UserInformation
                    {
                        CreatedDate = DateTime.Now,
                        Password = Encrypt.MD5(password),
                        role = (int)UserType.Scholar,
                        UpdateDate = DateTime.Now,
                        Status = GAStatus.Active,
                        UserName = username
                    };

                    await Provider.DataAccessService.AddRecord(newuser);
                    result.IsSuccess = true;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Tên tài khoản đã tồn tại";
                }
                


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

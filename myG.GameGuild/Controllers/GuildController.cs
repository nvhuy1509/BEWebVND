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
using System.Dynamic;
using Activity.DAL.Enum;
using SmartBreadcrumbs.Attributes;

namespace myG.GameGuild.Controllers
{
    public class GuildController : Controller
    {
        private readonly ISessionManager _sessionManager;

        public GuildController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
        [Breadcrumb("Danh sách Guild")]
        public async Task<IActionResult> Index() // danh sach guid
        {
            var listguild = new List<Guild>();
            ViewData["_sessionManager"] = _sessionManager;
            var userinformation = await Provider.DataAccessService.GetByIdAsync<UserInformation, Guid>(Guid.Parse(_sessionManager.AccountId));
            if (userinformation.role == (int)UserType.CenterAdmin)
            {
                listguild = (await Provider.DataAccessService.FindAsync<Guild>(x => x.Status == Activity.DAL.Enum.GAStatus.Active)).ToList();
                foreach (var item in listguild)
                {
                    item.Gamename = (await Provider.DataAccessService.GetByIdAsync<Games, Guid>(item.GameId)).Name;
                }
            }
            if (userinformation.role == (int)UserType.Manager)
            {
                var listguidOfuser = await Provider.DataAccessService.FindAsync<UserGuild>(x => x.UserId == Guid.Parse(_sessionManager.AccountId));
                var allguid = listguidOfuser.Select(x => x.GuildId).Distinct();

                listguild = (await Provider.DataAccessService.FindAsync<Guild>(x => x.Status == Activity.DAL.Enum.GAStatus.Active && allguid.Contains(x.Id))).ToList();
                foreach (var item in listguild)
                {
                    item.Gamename = (await Provider.DataAccessService.GetByIdAsync<Games, Guid>(item.GameId)).Name;
                }
            }

            return View(listguild);
        }
        [Breadcrumb("Guild", FromAction = "Index")]
        public async Task<IActionResult> ViewGuild(Guid GuildId) // danh sach user trong guild
        {
            var listuseringuild = await Provider.DataAccessService.FindAsync<UserGuild>(x => x.GuildId == GuildId);

            foreach (var item in listuseringuild)
            {
                item.UserName = (await Provider.DataAccessService.FindOneAsync<Contact>(x => x.UserId == item.UserId)).Name;
            }
            var inforguild = await Provider.DataAccessService.GetByIdAsync<Guild, Guid>(GuildId);
            inforguild.Gamename = (await Provider.DataAccessService.GetByIdAsync<Games, Guid>(inforguild.GameId)).Name;
            dynamic mymodel = new ExpandoObject();
            mymodel.user = listuseringuild;
            mymodel.guild = inforguild;
            ViewData["GameId"] = inforguild.GameId;
            ViewData["ROLE"] = _sessionManager.UserType;
            ViewData["_sessionManager"] = _sessionManager;
            return View(mymodel);
        }
        [Breadcrumb("Thêm guild", FromAction = "Index")]
        public async Task<IActionResult> CreateGuild()
        {
            var listgame = await Provider.DataAccessService.GetALL<Games>();
            return View(listgame);
        }
        [Breadcrumb("Thêm người chơi", FromAction = "ViewGuild")]
        public async Task<IActionResult> AddUser(Guid GuildId)
        {
            ViewData["AccountId"] = _sessionManager.AccountId;
            var guild = await Provider.DataAccessService.GetByIdAsync<Guild, Guid>(GuildId);//lấy thông tin guild

            var allGuildInGame = await Provider.DataAccessService.FindAsync<Guild>(x => x.GameId == guild.GameId);//lấy tẩt cả guild của game
            var allGuildIdInGame = allGuildInGame.Select(t => t.Id).Distinct();//lấy các guildId theo game ra

            var userAllGuild = await Provider.DataAccessService.FindAsync<UserGuild>(x => allGuildIdInGame.Contains(x.GuildId)); // danh sach user trong các  guild

            var usergame = await Provider.DataAccessService.FindAsync<UserGame>(x => x.GameId == guild.GameId); // danh sach user dc mapping voi game id == gameid cua guild

            var gameAccountDontExits = usergame.Where(p => userAllGuild.All(p2 => p2.UserId != p.UserId));//xem ở usergame có thằng nào không tồn tại trong userGuild khồn 


            var allUerIdGame = gameAccountDontExits.Select(t => t.UserId).Distinct();
            var allUsrInGame = await Provider.DataAccessService.FindAsync<UserInformation>(x => allUerIdGame.Contains(x.Id)); // danh sach user trong game không có trong guild nào



            var userGuild = await Provider.DataAccessService.FindAsync<UserGuild>(x => x.GuildId == GuildId); // danh sach user trong guild
            var allUerId = userGuild.Select(t => t.UserId).Distinct();
            var allUsrInGuild = await Provider.DataAccessService.FindAsync<UserInformation>(x => allUerId.Contains(x.Id)); // danh sach user trong guild

            //foreach (var item in usergame)
            //{
            //    item.UserName = (await Provider.DataAccessService.FindOneAsync<Contact>(x=> x.UserId == item.UserId)).Name;
            //}
            dynamic mymodel = new ExpandoObject();
            mymodel.user = allUsrInGame;
            mymodel.guild = guild;
            mymodel.userGuild = allUsrInGuild;
            return View("addusertoguild", mymodel);
        }

        public async Task<IActionResult> EditGuild()
        {
            return View();
        }



        #region http





        [HttpPost]
        public async Task<IActionResult> AddGuild(string Name, string Description, Guid Game, string thumb) // them guild
        {
            if (String.IsNullOrEmpty(thumb))
            {
                thumb = "/assets/images/media/26.jpg";
            }
            var guild = new Guild
            {
                Name = Name,
                CreatedDate = DateTime.Now,
                Description = Description,
                GameId = Game,
                Thumb = thumb,
                Status = Activity.DAL.Enum.GAStatus.Active,
            };
            await Provider.DataAccessService.AddRecord(guild);
            return Ok(true);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserGuild(List<Guid> UserId, Guid GuildId) // them user vao guild
        {

            if(UserId.Count == 0|| UserId == null)
            {
                await Provider.DataAccessService.DeleteManyAsync<UserGuild>(x=> x.GuildId == GuildId);
                return Ok(true);
            }

            var alluseringuild = await Provider.DataAccessService.FindAsync<UserGuild>(x => x.GuildId == GuildId); // tat ca user trong guild
                                                                                                                   //var listIdUserInGuild = alluseringuild.Select(x => x.UserId); // danh sach userId trong guild
            var listAlluserIdGuid = alluseringuild.Select(x => x.UserId).ToList();
            foreach (var item in alluseringuild) //  delete cac userId khong co trong list userId
            {
                if (UserId.Any(x => x == item.UserId) == false)
                {
                    await Provider.DataAccessService.DeleteAsync(item);
                    //listAlluserIdGuid.RemoveAll(x => x == item.UserId);
                }
            }
            //var alluseringuild_afterdelete = (await Provider.DataAccessService.FindAsync<UserGuild>(x => x.GuildId == GuildId)).ToList();
            //foreach (var item1 in listAlluserIdGuid)
            //{
            //    if (UserId.Any(x => x == item1) != false)
            //    {
            //        listAlluserIdGuid.RemoveAll(x => x == item1);
            //    }
            //}

            if (listAlluserIdGuid.Count > 0)
            {
                foreach (var item in UserId)
                {
                    if (listAlluserIdGuid.Any(x => x == item) == false)
                    {
                        var newUser = new UserGuild
                        {
                            UserId = item,
                            GuildId = GuildId,
                            CreatedDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            Status = GAPremission.UserGuild
                        };
                        await Provider.DataAccessService.AddRecord(newUser);
                    }
                }
            }
            else
            {
                foreach (var item in UserId)
                {
                    var newUser = new UserGuild
                    {
                        UserId = item,
                        GuildId = GuildId,
                        CreatedDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Status = GAPremission.UserGuild
                    };
                    await Provider.DataAccessService.AddRecord(newUser);
                }
            }





            return Ok(true);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGuild(Guid GuildId) // xoa guild
        {
            if (int.Parse(_sessionManager.UserType) == (int)UserType.CenterAdmin)
            {
                var guild = await Provider.DataAccessService.FindOneAsync<Guild>(x => x.Id == GuildId);
                guild.Status = Activity.DAL.Enum.GAStatus.InActive;
                await Provider.DataAccessService.UpdateRecord(guild);
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGuild(Guid GuildId, string Name, string Description) // update guild
        {
            if (int.Parse(_sessionManager.UserType) == (int)UserType.CenterAdmin)
            {
                var guild = await Provider.DataAccessService.FindOneAsync<Guild>(x => x.Id == GuildId);
                guild.Name = Name;
                guild.Description = Description;
                await Provider.DataAccessService.UpdateRecord(guild);
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid UserId, Guid GuildId) // xoa user guild
        {
            if (int.Parse(_sessionManager.UserType) == (int)UserType.CenterAdmin || int.Parse(_sessionManager.UserType) == (int)UserType.Manager)
            {
                await Provider.DataAccessService.DeleteManyAsync<UserGuild>(x => x.UserId == UserId && x.GuildId == GuildId);
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }

        }

        [HttpPost]
        public async Task<IActionResult> SetManagerGuild(Guid UserId, Guid GuildId) // xoa user guild
        {
            if (int.Parse(_sessionManager.UserType) == (int)UserType.CenterAdmin || int.Parse(_sessionManager.UserType) == (int)UserType.Manager)
            {
                var a = await Provider.DataAccessService.FindOneAsync<UserGuild>(x => x.UserId == UserId && x.GuildId == GuildId);
                var userinformation = await Provider.DataAccessService.GetByIdAsync<UserInformation, Guid>(a.UserId);
                if (a.Status == Activity.DAL.Enum.GAPremission.UserGuild)
                {
                    a.Status = Activity.DAL.Enum.GAPremission.ManagerGuild;
                    userinformation.role = (int)UserType.Manager;
                }
                else
                {
                    a.Status = Activity.DAL.Enum.GAPremission.UserGuild;
                    userinformation.role = (int)UserType.Manager;
                }
                await Provider.DataAccessService.UpdateRecord(a);
                await Provider.DataAccessService.UpdateRecord(userinformation);

                return Ok(true);
            }
            else
            {
                return Ok(false);
            }


        }
        #endregion
    }
}

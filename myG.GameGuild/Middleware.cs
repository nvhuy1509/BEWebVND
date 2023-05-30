using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Activity.Biz;
using Minxtu.DAL.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Activity.DAL.Enum;
using myG.GameGuild;

namespace myG.GameGuild
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private ISessionManager _sessionManager;
        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ISessionManager sessionManager)
        {

            _sessionManager = sessionManager;
            _sessionManager.IsPremission = true;
            if (httpContext.Request.Path.ToString().EndsWith("/") || httpContext.Request.Path.ToString().EndsWith("/Work")  || httpContext.Request.Path.ToString().EndsWith("/News/getPostFTagTrend")  || httpContext.Request.Path.ToString().EndsWith("/News/getPostFTagAll")  || httpContext.Request.Path.ToString().EndsWith("/News/searchPostTrend")  || httpContext.Request.Path.ToString().EndsWith("/News/getMoreBlogTrend") || httpContext.Request.Path.ToString().EndsWith("/News/getMoreBlogAll") || httpContext.Request.Path.ToString().EndsWith("/News") || httpContext.Request.Path.ToString().EndsWith("/Transparency") || httpContext.Request.Path.ToString().EndsWith("/Contact") || httpContext.Request.Path.ToString().Contains("/News/Post"))
            {
                if (httpContext.Request.Path.ToString().Contains("/News/Post/"))
                {
                    var url = httpContext.Request.Path.ToString();
                    var Id = int.Parse(url.Substring(url.LastIndexOf("/Post/") + 6));

                    News ItemNew = Provider.DataAccessSQLServerService.SelectNewsById(Id);
                    ItemNew.PageView += 1;

                    Provider.DataAccessSQLServerService.UpdateNews(Id, ItemNew);
                }
                await _next(httpContext);
            }
            else if (httpContext.Request.Path.ToString().Contains("/Auth"))
            {
                await _next(httpContext);
            }
            else
            {
                var Accounts = _sessionManager.AccountName;
                var AccountId = _sessionManager.AccountId;

                if (string.IsNullOrEmpty(_sessionManager.AccountId))
                {
                    await Task.Run(
                           () =>
                           {
                               httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                               httpContext.Response.Redirect("/Auth?t=" + httpContext.Request.Path);

                               var a = httpContext.Request.Path.ToString();
                           }
                        );

                    return;
                }


                var id = int.Parse(AccountId);
                var admin =  Provider.DataAccessSQLServerService.SelectAdminById(id);
                await RenderMenu(httpContext);
                await _next(httpContext);
            }
        }

        public async Task RenderMenu(HttpContext httpContext)
        {
            PremissionFliter premission = new PremissionFliter();
            if (premission.IsAllow(httpContext, new UserType[] { UserType.Scholar }, true))
            {
                string htmlMenu = "";
                string htmlMenuRp = "";
            
                    //list_class.Add(new TeacherClassInfor { IdClass = @class.Id, ClassName = @class.Name, StudentCount = @soluonghocsinhclass.Count(), Status = @class.Status });
                    string menu = string.Format("<li><a href =\"/StudentClass-{0}\" class=\"slide-item\"> {1} </a></li>", 1, 1);
                    htmlMenu += menu;
                
         

                httpContext.Items.Add("MENU", htmlMenu);
                httpContext.Items.Add("MENURP", htmlMenuRp);
            }
        }

    }


    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareClassTemplate(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
}
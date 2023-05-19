using System;
using System.Collections.Generic;
using Activity.DAL.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace myG.GameGuild
{
    public class PremissionFliter : ActionFilterAttribute
    {
        private readonly List<UserType> _userTypes = new List<UserType>() { UserType.CenterAdmin };
        public PremissionFliter(UserType[] userTypes = null)
        {

            if (userTypes != null)
                foreach (var userType in userTypes)
                {
                    _userTypes.Add(userType);
                }
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var SessionUserType = context.HttpContext.Session.GetString(SessionManager.KEY_USERTYPE) ?? "100";
            var UserType = (UserType)int.Parse(SessionUserType);
            if (_userTypes.Find(t => t == UserType) != 0)
            {
                base.OnResultExecuting(context);
            }
            else
            {

                context.HttpContext.Response.Redirect("Auth?=/");
            }

        }
        public bool IsAllow(HttpContext context)
        {
            var SessionUserType = context.Session.GetString(SessionManager.KEY_USERTYPE) ?? "100";
            var UserType = (UserType)int.Parse(SessionUserType);
            if (_userTypes.Find(t => t == UserType) != 0)
                return true;
            else
                return false;
        }
        public bool IsAllow(HttpContext context, UserType[] userTypes = null)
        {
            if (userTypes != null)
                foreach (var userType in userTypes)
                {
                    _userTypes.Add(userType);
                }
            var SessionUserType = context.Session.GetString(SessionManager.KEY_USERTYPE) ?? "100";
            var UserType = (UserType)int.Parse(SessionUserType);
            if (_userTypes.Find(t => t == UserType) != 0)
                return true;
            else
                return false;
        }
        public bool IsAllow(HttpContext context, UserType[] userTypes = null, bool isOnly = true)
        {
            if (isOnly)
            {
                _userTypes.Clear();
            }

            if (userTypes != null)
                foreach (var userType in userTypes)
                {
                    _userTypes.Add(userType);
                }
            var SessionUserType = context.Session.GetString(SessionManager.KEY_USERTYPE) ?? "100";
            var UserType = (UserType)int.Parse(SessionUserType);
            if (_userTypes.Find(t => t == UserType) != 0)
                return true;
            else
                return false;
        }
    }
}


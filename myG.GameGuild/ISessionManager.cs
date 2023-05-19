using System;
using Activity.DAL.Enum;
using Microsoft.AspNetCore.Http;

namespace myG.GameGuild
{
    public interface ISessionManager
    {
        string AccountName { get; set; }
        string AccountId { get; set; }
        string UserType { get; set; }
        UserType UserType1 { get; }
        string PremissionId { get; set; }
        string LevelId { get; set; }
        bool IsPremission { get; set; }

    }
    public class SessionManager : ISessionManager
    {
        private ISession _session;
        public static string KEY_ACCOUNT_ID = "AccountId";
        public static string KEY_ACCOUNT_NAME = "Accounts";
        public static string KEY_PREMISSION_ID = "PremissionId";
        public static string KEY_USERTYPE = "UserType";
        public static string KEY_LEVEL_ID = "LevelId";
        public static string KEY_PREMISSION = "IS_PREMISSION";
        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }
        public string AccountId
        {
            get
            {
                return _session.GetString(KEY_ACCOUNT_ID);
            }
            set
            {
                _session.SetString(KEY_ACCOUNT_ID, value ?? "");
            }
        }
        public string AccountName
        {
            get
            {
                return _session.GetString(KEY_ACCOUNT_NAME);
            }
            set
            {
                _session.SetString(KEY_ACCOUNT_NAME, value ?? "");
            }
        }
        public string PremissionId
        {
            get
            {
                return _session.GetString(KEY_PREMISSION_ID);
            }
            set
            {
                _session.SetString(KEY_PREMISSION_ID, value ?? "");
            }
        }
        public string UserType
        {
            get
            {
                return _session.GetString(KEY_USERTYPE);
            }
            set
            {
                _session.SetString(KEY_USERTYPE, value ?? "100");
            }
        }
        public UserType UserType1
        {
            get
            {
                return (UserType)(int.Parse(_session.GetString(KEY_USERTYPE)));
            }
        }

        public string LevelId
        {
            get
            {
                return _session.GetString(KEY_LEVEL_ID);
            }
            set
            {
                _session.SetString(KEY_LEVEL_ID, value ?? "");
            }
        }
        public bool IsPremission
        {
            get
            {
                string permission = _session.GetString(KEY_PREMISSION);
                bool isPremission = false;
                if (string.IsNullOrEmpty(permission))
                {
                    return isPremission;
                }
                bool.TryParse(permission, out isPremission);
                return isPremission;

            }
            set
            {
                _session.SetString(KEY_PREMISSION, value.ToString() ?? "0");
            }
        }

    }
}

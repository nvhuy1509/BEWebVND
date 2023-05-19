using System;

namespace MyGo.Core
{
    public enum CacheTypeEnum
    {
        NCache = 0,
        Memcached = 1,
        SharedCache = 2
    }

    public class Config
    {
        private static readonly Config instance = new Config();
        private readonly string _accountConnStr;
        private readonly string _billingLogConnStr;
        private readonly string _lotteryConnStr;
        private readonly string _teenTvConnStr;

        public static string TeenTvConnStr
        {
            get { return instance._teenTvConnStr; }
        }

        private readonly string _cacheCommonServer;
        private readonly string _cachePersistentServer;
        private readonly string _cacheType;
        private readonly string _cacheUserInfoServer;
        private readonly string _commentConnStr;
        private readonly string _friendConnstr;
        private readonly string _getCacheProvider;
        private readonly string _isEnableCache;
        private readonly string _linkConnStr;
        private readonly string _mediaUrl;
        private readonly string _memcachedPoolname;

        private readonly string _billingConnStr;
        private readonly string _consumerConnStr;

        private Config()
        {
            var configManager = ConfigManager.getIntanse();
            _portalWebConnStr = configManager.GetConnStr("DefaultConnection");
          
        }

        public static string mediaUrl
        {
            get { return instance._mediaUrl; }
        }

        public static string MemcachedPoolName
        {
            get { return instance._memcachedPoolname; }
        }

        public static string CachingUserInfoServer
        {
            get { return instance._cacheUserInfoServer; }
        }

        public static string CachingCommonServer
        {
            get { return instance._cacheCommonServer; }
        }

        public static string CachingPersistentServer
        {
            get { return instance._cachePersistentServer; }
        }

        public static CacheTypeEnum CacheType
        {
            get { return (CacheTypeEnum) Enum.Parse(typeof (CacheTypeEnum), instance._cacheType); }
        }

        public static string AccountConnStr
        {
            get { return instance._accountConnStr; }
        }

        public static string BillingLogConnStr
        {
            get { return instance._billingLogConnStr; }
        }
        public static string LotteryConnStr
        {
            get { return instance._lotteryConnStr; }
        }
        public static string LinkConnStr
        {
            get { return instance._linkConnStr; }
        }

        public static string FriendConnstr
        {
            get { return instance._friendConnstr; }
        }

        public static string GetCacheProvider
        {
            get { return instance._getCacheProvider; }
        }

        public static string IsEnableCache
        {
            get { return instance._isEnableCache; }
        }

        public static string CommentConnStr
        {
            get { return instance._commentConnStr; }
        }

        public static string ConsumerConnStr
        {
            get { return instance._consumerConnStr; }
        }

        public static string BillingConnStr
        {
            get
            {
                return instance._billingConnStr;
            }
        }
        private string _mobilePlatformConnStr;
        public static string MobilePlatformConnStr
        {
            get { return instance._mobilePlatformConnStr; }

        }
        private string _portalWebConnStr;
        public static string PortalWebConnStr
        {
            get { return instance._portalWebConnStr; }

        }

        private string _giftCodeConnStr;

        public static string GiftCodeConnStr
        {
            get { return instance._giftCodeConnStr; }
        }
        

        public static Config Instance
        {
            get { return instance; }
        }
        private string _goProfileApiConnStr;
        public static string GoProfileApiConnStr
        {
            get
            {
                return instance._goProfileApiConnStr;
            }
        }
    }
}
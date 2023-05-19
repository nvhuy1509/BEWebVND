//using System.Configuration;

using Microsoft.Extensions.Configuration;

namespace MyGo.Core
{
    public interface IConfigManager
    {
        /// <summary>
        /// Gets the conn STR.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        string GetConnStr(string name);

        /// <summary>
        /// Gets the app STR.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        string GetAppStr(string name);
    }

    /// <summary>
    /// 
    /// </summary>
    public class ConfigManager : IConfigManager
    {
        #region IConfigManager Members
        public IConfiguration Configuration { get; set; }
        public static ConfigManager instanse;
        public static ConfigManager getIntanse()
        {
            if (instanse != null) return instanse;
            else
            {
                instanse = new ConfigManager();
                return instanse;
            }
        }
        
        public void SetConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the conn STR.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual string GetConnStr(string name)
        {
            try
            {
                var rijndaelKey = new RijndaelEnhanced("OhayVN", "@1B2c3D4e5F6g7H8");
                //return Configuration.GetConnectionString(name);
               return rijndaelKey.Decrypt(Configuration.GetConnectionString(name));
                //return ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the app STR.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual string GetAppStr(string name)
        {
            return "";
            //return Configuration.Get[name] ?? string.Empty;
        }

        #endregion
    }
}
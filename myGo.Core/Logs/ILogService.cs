using System;
using System.Configuration;
//using System.Configuration;
using System.Threading;
//usinng System.Web.Configuration;
using log4net;
using log4net.Config;


namespace MyGo.Core.Logs
{
    internal interface ILogService
    {
        void Debug(string description);
        void Error(Exception error);
        void Error(Exception error, string detail);
        void Information(string description);
    }

    public class Log4NetService : ILogService
    {
        private static ILog logger;
        private static readonly LogConfigurationSection section = LogConfigurationSection.Default;

        public Log4NetService()
        {
            string instanceName = section.InstanceName;

            logger = LogManager.GetLogger(instanceName);

            XmlConfigurator.Configure();
        }

        #region ILogService Members

        public void Debug(string description)
        {
            if (section.IsLog)
                logger.Debug(description);
        }

        public void Error(Exception error)
        {
            if (section.IsLog)
                logger.Error(error.Message, error);
        }

        public void Error(Exception error, string detail)
        {
            if (section.IsLog)
                logger.Error(detail, error);
        }

        public void Information(string description)
        {
            if (section.IsLog)
                logger.Info(description);
        }

        #endregion
    }

    public class LogConfigurationSection 
    {
        private const string DEFAULT_SECTION_NAME = "mygo.core.log";
        private static LogConfigurationSection currentConfig;

        //[ConfigurationProperty("log", IsRequired = false)]
        public LogElement Common
        {
            get { return new LogElement(); }
            //get { return (LogElement) base["log"]; }
        }

        public bool IsLog
        {
            get { return Common.Log; }
        }

        public string LogPath
        {
            get { return Common.LogPath; }
        }

        public string InstanceName
        {
            get { return Common.InstanceName; }
        }

       static System.Configuration.Configuration config =
                 ConfigurationManager.OpenExeConfiguration(
                 ConfigurationUserLevel.None) as Configuration;
        public static LogConfigurationSection Default
        {
            get
            {
                

                if (currentConfig == null)
                {

                 //   LogConfigurationSection logconfig = config.GetSection("CustomSection") as LogConfigurationSection;
                    //(LogConfigurationSection) WebConfigurationManager.GetSection(DEFAULT_SECTION_NAME);
                    //if (logconfig == null)
                    //{
                    //    logconfig = new LogConfigurationSection();
                    //}

                    //Interlocked.CompareExchange(ref currentConfig, logconfig, null);
                    //Interlocked.CompareExchange(ref currentConfig, new LogConfigurationSection(), null);
                }
                return currentConfig;
            }
        }
    }

    public sealed class LogElement
    {
        //[ConfigurationProperty("enable", IsRequired = false, DefaultValue = true)]
        public bool Log
        {
            get { return true; }
            //get { return (bool) base["enable"]; }
        }

        //[ConfigurationProperty("log-type", IsRequired = false,
        //    DefaultValue = "Activity.Biz.Logs.FileLogListener, Activity.Biz")]
        public string LogType
        {
            get { return "log-type"; }
            //get { return (string) base["log-type"]; }
        }


        //[ConfigurationProperty("log-path", IsRequired = false, DefaultValue = "C:\\Logs")]
        public string LogPath
        {
            get { return "log-path"; }
            //get { return (string) base["log-path"]; }
        }

        //[ConfigurationProperty("instance-name", IsRequired = false, DefaultValue = "Default")]
        public string InstanceName
        {
            get { return "instance-name"; }
            //get { return (string) base["instance-name"]; }
        }
    }
}
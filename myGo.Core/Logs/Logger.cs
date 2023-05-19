using System;

namespace MyGo.Core.Logs
{
    public static class Logger
    {

       
        private static readonly ILogService logService = new Log4NetService();

        public static void Error(Exception ex)
        {

            //logService.Error(ex);
        }
        
        public static void Error(Exception ex, string detail)
        {
          //  logService.Error(ex, detail);
        }

        public static void Info(string message)
        {
           // logService.Information(message);
        }

        public static void Debug(string message)
        {
           // logService.Debug(message);
        }
    }
}
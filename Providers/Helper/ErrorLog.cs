using System;
using System.IO;
using System.Web;

namespace Providers.Helper
{
    public class ErrorLog
    {
        public static void LogError(Exception ex)
        {
            try
            {
                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                message += string.Format("Message: {0}", ex.Message);
                message += Environment.NewLine;
                message += string.Format("StackTrace: {0}", ex.StackTrace);
                message += Environment.NewLine;
                message += string.Format("Source: {0}", ex.Source);
                message += Environment.NewLine;
                message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
                message += Environment.NewLine;
                if (ex.InnerException != null)
                {
                    message += string.Format("InnerException: {0}", ex.InnerException.ToString());
                    message += Environment.NewLine;
                }
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                string path = HttpContext.Current.Server.MapPath("~/ErrorLogs.txt");
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
            catch (Exception exception)
            {
                LogError(exception);
            }
        }
    }
}
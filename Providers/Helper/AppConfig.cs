using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.Helper
{
    public class AppConfig
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["dbConnection"];
            }
        }
    }
}

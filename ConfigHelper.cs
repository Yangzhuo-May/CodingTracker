using System.Linq;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Coding_Tracker
{
    public class ConfigHelper
    {
        private static IConfiguration config;
        static ConfigHelper()
        {
            config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false).Build();
        }

        public static string GetConnectionString()
        {
            return config.GetConnectionString("MyDatabase");
        }
    }
}

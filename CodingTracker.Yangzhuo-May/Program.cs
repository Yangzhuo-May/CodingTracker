using System.Diagnostics.Metrics;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker
{
    class Program
    {
        static string? connectionString = ConfigHelper.GetConnectionString();
        static void Main(string[] args)
        {
            DatabaseManager databaseManager = new DatabaseManager();   
            databaseManager.CreateDatabase(connectionString);
            UserInput.GetUserInput();
        }
    }
}

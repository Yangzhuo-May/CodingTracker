using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;
using Coding_Tracker;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;
using System.Runtime.CompilerServices;

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

using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker
{
    internal class DatabaseManager
    {
        internal void CreateDatabase(string? connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS coding_time (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Date TEXT,
                    StartTime TEXT,
                    EndTime TEXT,
                    Duration TEXT 
                    )";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}

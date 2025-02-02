using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Spectre.Console;
using System.Numerics;

namespace Coding_Tracker
{
    internal class CodingController
    {
        private static string connectionString = ConfigHelper.GetConnectionString();
        TableVisualisationEngine tableVisualisationEngine = new TableVisualisationEngine();

        public void Insert()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                string date, startTime, endTime, end = null, start = null;
                bool TimeSafe = true;
                
                connection.Open();
                var tableCmd = connection.CreateCommand();

                date = Validation.GetDateInput();

                do
                {
                    if (TimeSafe == false)
                    {
                        AnsiConsole.Markup($"[red]!!INCORRECT TIME!![/] Endtime:{end} is samller then Starttime:{start}\n");
                    }

                    startTime = Validation.GetTimeInput();
                    endTime = Validation.GetTimeInput();
                    end = endTime;
                    start = startTime;

                    TimeSafe = Validation.TimeSafe(startTime, endTime);
                } while (!TimeSafe);

                tableCmd.CommandText = $"INSERT INTO coding_time(date, starttime, endtime, duration) VALUES('{date}', '{startTime}', '{endTime}', '{UserInput.CalculateDuration(startTime, endTime)}')";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

            UserInput.AskForNextAction("1", "Insert");
        }

        public void Read()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"SELECT * from coding_time";
            
                List<CodingSession> tableData = new();

                SqliteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(new CodingSession
                        {
                            Id = reader.GetInt32(0),
                            Date = reader.GetString(1),
                            StartTime = reader.GetString(2),
                            EndTime = reader.GetString(3),
                            Duration = reader.GetString(4),
                        });
                    }
                }

                connection.Close();
                tableVisualisationEngine.ShowTable(tableData);
            }
        }

        public void Update(string Id)
        {
            int checkQuery;

            using (var connection = new SqliteConnection(connectionString))
            {
                do
                {
                    connection.Open();
                    var checkCmd = connection.CreateCommand();
                    checkCmd.CommandText =
                        $"SELECT EXISTS(SELECT 1 FROM coding_time WHERE Id = {Id})";
                    checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (checkQuery == 0)
                    {
                        Console.Clear();
                        Read();
                        AnsiConsole.Markup($"[red]Record with Id {Id} doesn't exist.[/]\n");
                        Id = Validation.GetNumberInput("Please type another Id of the record you want to update. Type 0 to return to main manu.");
                        connection.Close();
                    }
                } while (checkQuery == 0);

                var tableCmd = connection.CreateCommand();

                string date, startTime, endTime, end = null, start = null;
                bool TimeSafe = true ;

                date = Validation.GetDateInput();

                do
                {
                    if (TimeSafe == false)
                    {
                        AnsiConsole.Markup($"[red]!!INCORRECT TIME!![/] Endtime:{end} is samller then Starttime:{start}\n");
                    }

                    startTime = Validation.GetTimeInput();
                    endTime = Validation.GetTimeInput();
                    end = endTime;
                    start = startTime;

                    TimeSafe = Validation.TimeSafe(startTime, endTime);
                } while (!TimeSafe);

                tableCmd.CommandText = $"UPDATE coding_time SET date = '{date}', starttime = '{startTime}', endtime = '{endTime}', duration = '{UserInput.CalculateDuration(startTime, endTime)}' WHERE Id = {Id}";

                tableCmd.ExecuteNonQuery();
                connection.Close();
            }

            UserInput.AskForNextAction(Id, "Update");
        }

        public void Delete(string Id)
        {
            int checkQuery;

            using (var connection = new SqliteConnection(connectionString))
            {
                do
                {
                    connection.Open();
                    var checkCmd = connection.CreateCommand();
                    checkCmd.CommandText =
                        $"SELECT EXISTS(SELECT 1 FROM coding_time WHERE Id = {Id})";
                    checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (checkQuery == 0)
                    {
                        Console.Clear();
                        Read();
                        AnsiConsole.Markup($"[red]Record with Id {Id} doesn't exist.[/]\n");
                        Id = Validation.GetNumberInput("Please type another Id of the record you want to delete. Type 0 to return to main manu.");
                        connection.Close();
                    }
                } while (checkQuery == 0);

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"DELETE from coding_time WHERE Id = '{Id}'";

                tableCmd.ExecuteNonQuery();
                connection.Close();
            }

            UserInput.AskForNextAction(Id, "Delete");
        }
    }
}

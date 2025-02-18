using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Spectre.Console;
using Dapper;

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
                string date, startTime, endTime, end = null, start = null, duration;
                bool TimeSafe = true;
                
                connection.Open();
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
                    duration = UserInput.CalculateDuration(startTime, endTime);

                    TimeSafe = Validation.TimeSafe(startTime, endTime);
                } while (!TimeSafe);

                 var tableCmd = "INSERT INTO coding_time(date, starttime, endtime, duration) VALUES(@date, @startTime, @endTime, @duration)";

                var parameters =  new { date = date, startTime = startTime, endTime = endTime, duration = duration } ;

                connection.Execute(tableCmd, parameters);
            }
            UserInput.AskForNextAction("1", "Insert");
        }

        public void Read()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                List<CodingSession> tableData = new List<CodingSession>();
                
                var tableCmd = $"SELECT * from coding_time";
                tableData = connection.Query<CodingSession>(tableCmd).ToList();

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
                    var checkCmd =
                        "SELECT EXISTS(SELECT 1 FROM coding_time WHERE Id = @id)";
                    var checkParameters = new { id =  Id };

                    checkQuery = Convert.ToInt32(connection.ExecuteScalar(checkCmd, checkParameters));

                    if (checkQuery == 0)
                    {
                        Console.Clear();
                        Read();
                        AnsiConsole.Markup($"[red]Record with Id {Id} doesn't exist.[/]\n");
                        Id = Validation.GetNumberInput("Please type another Id of the record you want to update. Type 0 to return to main manu.");
                        connection.Close();
                    }
                } while (checkQuery == 0);

                string date, startTime, endTime, end = null, start = null, duration;
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
                    duration = UserInput.CalculateDuration(startTime, endTime);

                    TimeSafe = Validation.TimeSafe(startTime, endTime);
                } while (!TimeSafe);

                var tableCmd = "UPDATE coding_time SET date = @date, starttime = @startTime, endtime = @endTime, duration = @duration WHERE Id = @id";

                var parameters = new { date = date, startTime = startTime, endTime = endTime, duration = duration, id = Id };

                connection.Execute(tableCmd, parameters);
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

                    var checkCmd =
                        "SELECT EXISTS(SELECT 1 FROM coding_time WHERE Id = @id)";
                    var checkParameters = new { id = Id };

                    checkQuery = Convert.ToInt32(connection.ExecuteScalar(checkCmd, checkParameters));

                    if (checkQuery == 0)
                    {
                        Console.Clear();
                        Read();
                        AnsiConsole.Markup($"[red]Record with Id {Id} doesn't exist.[/]\n");
                        Id = Validation.GetNumberInput("Please type another Id of the record you want to delete. Type 0 to return to main manu.");
                        connection.Close();
                    }
                } while (checkQuery == 0);

                var tableCmd = $"DELETE from coding_time WHERE Id = @id";
                var parameters = new { id = Id };

                connection.Execute(tableCmd, parameters);
            }

            UserInput.AskForNextAction(Id, "Delete");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Coding_Tracker
{
    internal class UserInput
    {
        public static CodingController controller = new CodingController();

        public static bool isMain;
        public static Dictionary<string, int> operation = new Dictionary<string, int>()
        {
            { "View", 1 },
            { "Insert", 2},
            { "Delete", 3},
            { "Update", 4}
        };

        public static void GetUserInput()
        {
            Console.Clear();
            bool closeApp = false;

            string Id;

            while (closeApp == false)
            {
                Console.WriteLine("--------CODING TRACKER--------");
                Console.WriteLine("         MAIN MENU         ");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("Type 0 to Close Application.");
                Console.WriteLine("Type 1 to View All Records.");
                Console.WriteLine("Type 2 to Insert Records.");
                Console.WriteLine("Type 3 to Delete Records.");
                Console.WriteLine("Type 4 to Update Records.");
                Console.WriteLine("----------------------------");

                string command = Console.ReadLine();

                switch (command)
                {
                    case "0":
                        Console.WriteLine("Goodbye!");
                        closeApp = true;
                        Environment.Exit(0);
                        break;

                    case "1":
                        isMain = true;
                        Console.Clear();
                        controller.Read();
                        if (isMain) AskForNextAction("0", "View");
                        break;

                    case "2":
                        Console.Clear();
                        controller.Read();
                        controller.Insert();
                        controller.Read();
                        break;

                    case "3":
                        isMain = false;
                        Console.Clear();
                        controller.Read();
                        Id = Validation.GetNumberInput("Please type the Id of the record you want to delete or type 0 to go back to Main Menu.");
                        controller.Delete(Id);
                        controller.Read();
                        break;

                    case "4":
                        isMain = false;
                        Console.Clear();
                        controller.Read();
                        Id = Validation.GetNumberInput("Please type the Id of the record you want to update or type 0 to go back to Main Menu.");
                        controller.Update(Id);
                        controller.Read();
                        break;

                    default:
                        Console.Clear();
                        AnsiConsole.Markup($"[red]\nInvalid Command.[/] Please type a number from 0 to 4.\n");
                        break;
                }
            }
        }

        public static void AskForNextAction(string recordId, string userInput)
        {
            string user,Id;

            Console.Clear();
            controller.Read();

            if (userInput == "Insert")
            {
                user = Validation.GetNumberInput($"The record has been inserted, please enter {operation[userInput]} to {userInput} another one or 0 to go back to Main Menu");
            }
            else if (userInput == "View")
            {
                user = Validation.GetNumberInput("Enter 0 to go back to Main Menu.");
            }
            else
            {
                user = Validation.GetNumberInput($"The {recordId} has been {userInput}d, please enter {operation[userInput]} to {userInput} another one or 0 to go back to Main Menu");
            }

            switch (user)
            {
                case "0":
                    GetUserInput();
                    break;

                case "2":
                    controller.Insert();
                    break;

                case "3":
                    Id = Validation.GetNumberInput("Please type the Id of the record you want to delete or type 0 to go back to Main Menu.");
                    controller.Delete(Id);
                    break;

                case "4":
                    Id = Validation.GetNumberInput("Please type the Id of the record you want to Update or type 0 to go back to Main Menu.");
                    controller.Update(Id);
                    break;
            }
        }

        public static string CalculateDuration(string startTime, string endTime)
        {
            int StartHour = Convert.ToInt32($"{startTime[0]}{startTime[1]}");
            int EndHour = Convert.ToInt32($"{endTime[0]}{endTime[1]}");
            int StartMinute = Convert.ToInt32($"{startTime[3]}{startTime[4]}");
            int EndMinute = Convert.ToInt32($"{endTime[3]}{endTime[4]}");
            int DurationHour, DurationMinute;
            string DurationString;

            if (StartMinute > EndMinute)
            {
                StartMinute += 60;
                StartHour -= 1;

                DurationHour = EndHour - StartHour;
                DurationMinute = EndMinute - StartMinute;
            } else
            {
                DurationHour = EndHour - StartHour;
                DurationMinute = EndMinute - StartMinute;
            }

            return DurationString = $"{DurationHour}:{DurationMinute}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Spectre.Console;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Coding_Tracker
{
    internal class Validation
    {
        public static CodingController controller = new CodingController();
        public static string GetDateInput()
        {
            DateTime date;
            bool success;

            do
            {
                Console.WriteLine("Please insert the date: (Format: dd-mm-yy). Type 0 to return to main manu.");
                string dateInput = Console.ReadLine();
                success = DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out date);

                if (dateInput == "0") UserInput.GetUserInput();

                if (!success)
                {
                    Console.Clear();
                    controller.Read();
                AnsiConsole.Markup($"[red]Invalid date format.[/] Please enter the date in the format dd-MM-yy.\n");
                }
            } while (!success);

            var dateString = date.ToString("dd-MM-yy");
            return dateString;
        }

        public static string GetTimeInput()
        {
            DateTime time;
            bool success;

            do
            {
                Console.WriteLine("Please insert the time: (Format: HH:mm). Type 0 to return to main manu.");
                string timeInput = Console.ReadLine();

                success = DateTime.TryParseExact(timeInput, "HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out time);

                if (timeInput == "0") UserInput.GetUserInput();

                if (!success)
                {
                    Console.Clear();
                    controller.Read();
                AnsiConsole.Markup($"[red]Invalid date format.[/] Please enter the date in the format HH:mm.\n");
                }
            } while (!success);

            var timeString = time.ToString("HH:mm");
            return timeString;
        }

        public static string GetNumberInput(string message)
        {
            bool success = true;
            int finalInput;

            do
            {
                Console.WriteLine(message);
                string numberInput = Console.ReadLine();
                if (numberInput == "0") UserInput.GetUserInput();
                success = int.TryParse(numberInput, out finalInput);

                if (!success)
                {
                    Console.Clear();
                    controller.Read();
                    Console.WriteLine("\nInvalid Command. Please type a id of records.\n");
                }
            }
            while (success == false && finalInput >= 0 && finalInput <= 4);

            return finalInput.ToString();
        }

        public static bool TimeSafe(string startTime, string endTime)
        {
            int StartHour = Convert.ToInt32($"{startTime[0]}{startTime[1]}");
            int EndHour = Convert.ToInt32($"{endTime[0]}{endTime[1]}");
            int StartMinute = Convert.ToInt32($"{startTime[3]}{startTime[4]}");
            int EndMinute = Convert.ToInt32($"{endTime[3]}{endTime[4]}");

            if (StartHour > EndHour )
            {
                return false;
            }
            return true;
        }
    }
}

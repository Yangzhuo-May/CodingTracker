using System;
using System.Globalization;
using System.Linq;
using Spectre.Console;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Coding_Tracker
{
    public static class Validation
    {
        public static CodingController controller = new CodingController();

        static DateTime dateTimeInput;
        static int finalInput;

        public static string GetDateInput()
        {
            bool success;
            string dateInput;

            do
            {
                Console.WriteLine("Please insert the date: (Format: dd-mm-yy). Type 0 to return to main manu.");
                dateInput = Console.ReadLine();

                if (dateInput == "0") UserInput.GetUserInput();

                if (!isVailableDate(dateInput))
                {
                    Console.Clear();
                    controller.Read();
                AnsiConsole.Markup($"[red]Invalid date format.[/] Please enter the date in the format dd-MM-yy.\n");
                }
            } while (!isVailableDate(dateInput));

            var dateString = dateTimeInput.ToString("dd-MM-yy");
            return dateString;
        }

        public static bool isVailableDate(string dateInput)
        {
            return DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out dateTimeInput);
        }

        public static string GetTimeInput()
        {
            bool success;
            string timeInput;

            do
            {
                Console.WriteLine("Please insert the time: (Format: HH:mm). Type 0 to return to main manu.");
                timeInput = Console.ReadLine();

                if (timeInput == "0") UserInput.GetUserInput();

                if (!isVailableTime(timeInput))
                {
                    Console.Clear();
                    controller.Read();
                AnsiConsole.Markup($"[red]Invalid date format.[/] Please enter the date in the format HH:mm.\n");
                }
            } while (!isVailableTime(timeInput));

            var timeString = dateTimeInput.ToString("HH:mm");
            return timeString;
        }

        public static bool isVailableTime(string timeInput)
        {
            return DateTime.TryParseExact(timeInput, "HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out dateTimeInput);
        }


        public static string GetNumberInput(string message)
        {
            string numberInput;

            do
            {
                Console.WriteLine(message);
                numberInput = Console.ReadLine();
                if (numberInput == "0") UserInput.GetUserInput();

                if (!isVailableCom(numberInput))

                {
                    Console.Clear();
                    controller.Read();
                    Console.WriteLine("\nInvalid Command. Please type a id of records.\n");
                }
            }
            while (!isVailableCom(numberInput));

            return finalInput.ToString();
        }

        public static bool isVailableCom(string numberInput)
        {
            bool isInt = int.TryParse(numberInput, out finalInput);
            bool valiableCom = (finalInput >= 0 && finalInput <= 4);
            return (isInt && valiableCom);
        }


        public static bool TimeSafe(string startTime, string endTime)
        {
            int StartHour = Convert.ToInt32($"{startTime[0]}{startTime[1]}");
            int EndHour = Convert.ToInt32($"{endTime[0]}{endTime[1]}");

            if (StartHour > EndHour )
            {
                return false;
            }
            return true;
        }
    }
}

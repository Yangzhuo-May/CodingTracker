﻿using System.Globalization;

namespace ThePortugueseMan.CodingTracker;

public class AskInput
{
    public void ClearPreviousLines(int numberOfLines)
    {
        for (int i = 1; i <= numberOfLines; i++)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
    
    public int PositiveNumber(string message)
    {
        string? input;
        bool showError = false;
        int number;
        do
        {

            if (showError)
            {
                ClearPreviousLines(2);
                Console.Write("Invalid Input. ");
            }

            Console.WriteLine(message);
            input = Console.ReadLine();
            showError = true;
        }
        while (!(Int32.TryParse(input, out number) && number >= 0));
        return number;
    }
    
    public DateTime AskForSimpleDate(string message)
    {
        string? input;
        DateTime returnDate = new();
        bool showError = false;
        bool validDate = false;
        do
        {
            if (!showError) Console.WriteLine(message + " Use dd-mm-yy as format");
            else
            {
                ClearPreviousLines(2);
                Console.WriteLine("Please write a valid date using dd-mm-yy. Or 0 to return");
            }
            input = Console.ReadLine();
            if (input is "0")
            {
                returnDate = DateTime.MinValue;
                break; 
            }

            try
            {
                returnDate = DateTime.ParseExact(input, "dd-MM-yy", new CultureInfo("en-US"));
                validDate = true;
            }
            catch (FormatException)
            {
                showError = true;
            }
        }
        while (!validDate);
        return returnDate;
    }

    public DateTime[] DateInterval(string askStartDateMessage, string askEndDateMessage)
    {
        DateTime[] result = new DateTime[2];
        bool validInterval;

        do
        {
            result[0] = AskForSimpleDate(askStartDateMessage);

            if (result[0] == DateTime.MinValue) return null;
            else
            {
                result[1] = AskForSimpleDate(askEndDateMessage);
                if (result[1] == DateTime.MinValue) return null;
                else
                {
                    if (result[1].Subtract(result[0]) >= TimeSpan.Zero)
                    {
                        validInterval = true;
                    }
                    else
                    {
                        Console.WriteLine("End date is earlier than the start date.");
                        validInterval = false;
                        ClearPreviousLines(2);
                    }
                }
            }
        } while (!validInterval);
        return result;
    }

    public DateTime[] DateIntervalWithHours(string askStartDateMessage, string askEndDateMessage)
    {
        DateTime[] result = new DateTime[2];
        bool validInterval;

        do
        {
            result[0] = AskForDateWithHours(askStartDateMessage);

            if (result[0] == DateTime.MinValue) return null;
            else
            {
                result[1] = AskForDateWithHours(askEndDateMessage);
                if (result[1] == DateTime.MinValue) return null;
                else
                {
                    if (result[1].Subtract(result[0]) >= TimeSpan.Zero)
                    {
                        validInterval = true;
                    }
                    else
                    {
                        AnyKeyToContinue("End date is earlier than the start date. Press any key to continue");
                        validInterval = false;
                    }
                }
            }
        } while (!validInterval);
        return result;
    }
    
    private DateTime AskForHoursAndMinutes(string message)
    {
        DateTime returnTime = new();
        string? input;
        bool showError = false;
        bool validInput = false;
        
        do
        {
            if (!showError) Console.WriteLine(message + " Use hh:mm as with a 24h format");
            else
            {
                ClearPreviousLines(2);
                Console.WriteLine("Please write a valid time using hh:mm as with a 24h format. Or 0 to return");
            }
            input = Console.ReadLine();
            if (input is "0") break;

            try
            {
                returnTime = DateTime.ParseExact(input, "HH:mm", new CultureInfo("en-US"));
                validInput = true;
            }
            catch (FormatException)
            {
                showError = true;
            }
        }
        while (!validInput);

        if (!validInput) return DateTime.MinValue;
        else return returnTime;
    }

    public DateTime AskForDateWithHours(string message)
    {
        DateTime auxDate, auxTime, returnDate;

        try
        {
            auxDate = AskForSimpleDate(message);
            if (auxDate == DateTime.MinValue) return DateTime.MinValue;
            auxTime = AskForHoursAndMinutes("Insert the time.");
            if (auxTime == DateTime.MinValue) return DateTime.MinValue;

            returnDate = new(auxDate.Year, auxDate.Month, auxDate.Day, auxTime.Hour, auxTime.Minute, auxTime.Second);

            return returnDate;
        }
        catch (FormatException)
        {
            return DateTime.MinValue;
        }
    }
    
    public void AnyKeyToContinue()
    {
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public void AnyKeyToContinue(string? message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }

    public bool ZeroOrOtherAnyKeyToContinue(string? message)
    {
        Console.WriteLine(message);
        if (Console.ReadKey().ToString() == "0") return true;
        else return false;
    }
}
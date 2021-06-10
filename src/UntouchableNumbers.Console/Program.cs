using System;

namespace UntouchableNumbers.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("The app requires either 1 or 2 int arguments.");
            }

            LogToConsole("Starting...");
            if (args.Length == 1) 
            { // single number
                var theNumber = int.Parse(args[0]);
                LogToConsole("Preparing data...");
                var untouchableNumbers = new Core.UntouchableNumbers(theNumber);
                LogToConsole("Data ready for processing");
                var isUntouchable = untouchableNumbers.IsUntouchableNumber(theNumber);
                if (isUntouchable)
                {
                    LogToConsole("Yes");
                }
                else
                {
                    LogToConsole("No");
                }
            }
            else if (args.Length == 2) 
            { // range
                var minNumber = int.Parse(args[0]);
                var maxNumber = int.Parse(args[1]);
                LogToConsole("Preparing data...");
                var untouchableNumbers = new Core.UntouchableNumbers(minNumber, maxNumber);
                LogToConsole("Data ready for processing");
                var count = 1;
                for (var number = minNumber; number <= maxNumber; number++)
                {
                    var isUntouchable = untouchableNumbers.IsUntouchableNumber(number);
                    if (isUntouchable)
                    {
                        System.Console.WriteLine($"{count}: {number}");
                        count++;
                    }
                }
            }

            LogToConsole("Done");
        }

        private static void LogToConsole(string message) => System.Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}: {message}");
    }
}

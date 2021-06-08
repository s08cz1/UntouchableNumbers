using System;
using System.Collections.Generic;
using UntouchableNumbers.Core;

namespace UntouchableNumbers.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var minNumber = 0;
            var maxNumber = 1000;
            if (args.Length == 1) 
            { // single number
                minNumber = int.Parse(args[0]);
                maxNumber = int.Parse(args[0]);
            }
            if (args.Length == 2) 
            { // range
                minNumber = int.Parse(args[0]);
                maxNumber = int.Parse(args[1]);
            }


            var untouchableNumbers = new UntouchableNumbers.Core.UntouchableNumbers(minNumber, maxNumber);

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
            System.Console.WriteLine("Done");
        }
    }
}

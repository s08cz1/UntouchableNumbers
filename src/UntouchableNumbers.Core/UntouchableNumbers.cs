using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System;

namespace UntouchableNumbers.Core
{
    public class UntouchableNumbers
    {
        private readonly int _multiplier = 10; // ten times the number should be enough to get all the sums matching the range
        private readonly List<int> _sums;

        public UntouchableNumbers(int theNumber)
        {
            _sums = GetListOfSumOfProperDivisor(theNumber);
        }
        public UntouchableNumbers(int minNumber, int maxNumber)
        {
            _sums = GetListOfSumOfProperDivisor(minNumber, maxNumber);
        }
        public bool IsUntouchableNumber(int number) {
            if (number <= 0) // must be a positive integer
            {
                return false; 
            }
            if (number == 5) // exception from the rule: 5 is the only odd untouchable number
            { 
                return true;
            }
            if (_sums.Any(sum => sum == number) || // untouchable number cannot be expressed as the sum of all the proper divisors of any positive integer
                number % 2 == 1 || // no odd number is an untouchable number, apart from 5 which is the only odd untouchable number
                IsPrimeNumber(number - 1) // No untouchable number is one more than a prime number
            ) 
            {
                return false;
            }
            return true;
        }
        private List<int> GetListOfSumOfProperDivisor(int minNumber, int maxNumber)
        {
            var sums = new ConcurrentDictionary<int, int>();
            Parallel.For(minNumber, maxNumber * _multiplier, (int number, ParallelLoopState state) =>
            {
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.75) * 2.0))
                };
                var sum = GetSum(number);
                if (sum != 0)
                {
                    sums.TryAdd(sum, 0);
                }

            });
            return sums.Select(x => x.Key).ToList();
        }
        private List<int> GetListOfSumOfProperDivisor(int theNumber)
        {
            var sums = new ConcurrentDictionary<int, int>();
            Parallel.For(theNumber, theNumber * _multiplier, (int number, ParallelLoopState state) =>
            {
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.75) * 2.0))
                };
                var sum = GetSum(number);
                if (sum == theNumber)
                {
                    sums.TryAdd(sum, 0);
                }

            });
            return sums.Where(x => x.Key == theNumber).Select(x => x.Key).ToList();
        }
        private static int GetSum(int number)
        {
            var sum = 0;
            for (var i = 1; i < number; i++)
            {
                if (number % i == 0)
                {
                    sum += i;
                }
            }
            return sum;
        }
        private static bool IsPrimeNumber(int number) {
            if (number <= 1) 
            {
                return false;
            }
            for (var i = 2; i < number; i++) 
            {
                if (number % i == 0) 
                {
                    return false;
                }
            }
            return true;
        }
    }
}

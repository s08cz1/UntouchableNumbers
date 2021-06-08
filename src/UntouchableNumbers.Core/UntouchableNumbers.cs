using System.Linq;
using System.Collections.Generic;

namespace UntouchableNumbers.Core
{
    public class UntouchableNumbers
    {
        private readonly int _multiplier = 10; // ten times the number should be enough to get all the sums matching the range
        private readonly List<int> _sums;

        public UntouchableNumbers(int minNumber, int maxNumber)
        {
            _sums = GetListOfSumOfProperDivisor(minNumber, maxNumber);
        }
        public bool IsUntouchableNumber(int number) {
            if (number < 0) // must be a positive integer
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

        private List<int> GetListOfSumOfProperDivisor(int minNumber, int maxNumber) {   
            var listOfSums = new List<int>();
            for (var n = minNumber; n <= maxNumber * _multiplier; n++) { 
                var sum = 0;
                for (var i = 1; i < n; i++) {
                    if (n % i == 0) {
                        sum += i;
                    }
                }
                if (sum != 0){
                    listOfSums.Add(sum);
                }
            }
            return listOfSums;
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

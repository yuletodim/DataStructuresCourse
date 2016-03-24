namespace SumAndAverage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class SumAvgOfInts
    {
        static void Main(string[] args)
        {
            try
            {
                List<int> numbers = ReadInput();
                int sum = numbers.Sum();
                double average = numbers.Average();
                Console.WriteLine("Sum={0}; Average={1:f2}", sum, average);
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static List<int> ReadInput()
        {
            Console.WriteLine("Enter some numbers separated by space:");
            string input = Console.ReadLine();
            List<string> stringsNums = input.Split(' ').ToList();
            List<int> numbers = new List<int>();
            foreach (var strNum in stringsNums)
            {
                numbers.Add(Convert.ToInt32(strNum));
            }

            return numbers;
        }
    }
}

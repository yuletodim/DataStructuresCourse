namespace RemoveOddOccurences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class RemoveOddOccurences
    {
        static void Main(string[] args)
        {
            try
            {
                List<int> numbers = ReadInput();
                RemoveOddOccurence(numbers);
                Console.WriteLine(string.Join(" ", numbers));
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void RemoveOddOccurence(List<int> numbers)
        {
            List<int> numbersOddOcurences = new List<int>();

            foreach(int number in numbers)
            {
                int count = numbers.Count(n => n == number);
                if(IsOdd(count))
                {
                    numbersOddOcurences.Add(number);
                }
            }

            foreach(int number in numbersOddOcurences)
            {
                numbers.RemoveAll(n => n == number);
            }
    
        }

        static bool IsOdd(int num)
        {
            bool isOdd = true;
            if (num % 2 == 0)
            {
                isOdd = false;
            }

            return isOdd;
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

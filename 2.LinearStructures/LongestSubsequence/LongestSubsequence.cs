namespace LongestSubsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class LongestSubsequence
    {
        static void Main(string[] args)
        {
            try
            {
                List<int> numbers = ReadInput();
                List<int> longestSequence = FindLongestSubsequence(numbers);
                Console.WriteLine(string.Join(" ", longestSequence));
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

        static List<int> FindLongestSubsequence(List<int> numbers)
        {
            List<int> longestSubsequence = new List<int>();
            for (int i = 0; i < numbers.Count; i++)
            {
                List<int> currentSubsequence = new List<int>() { numbers[i] };
                for (int j = i+1; j < numbers.Count; j++)
                {
                    if (numbers[i] == numbers[j])
                    {
                        currentSubsequence.Add(numbers[j]);
                    }
                    else
                    {
                        break;
                    }
                }

                if (currentSubsequence.Count > longestSubsequence.Count)
                {
                    longestSubsequence = currentSubsequence;
                }
            }
            return longestSubsequence;
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

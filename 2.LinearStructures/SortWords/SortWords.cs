namespace SortWords
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class SortWords
    {
        static void Main(string[] args)
        {
            try
            {
                List<string> words = ReadInput();

                var sortedWords = words.OrderBy(w => w.Substring(0));
                Console.WriteLine("\nWords sorted with OrderBy():");
                Console.WriteLine(string.Join(" ", sortedWords));

                words.Sort();
                Console.WriteLine("\nWords sorted with Sort():");
                Console.WriteLine(string.Join(" ", words));

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

        static List<string> ReadInput()
        {
            Console.WriteLine("Enter some words separated by space:");
            string input = Console.ReadLine();
            List<string> words = input.Split(' ').ToList();
            return words;
        }
    }
}

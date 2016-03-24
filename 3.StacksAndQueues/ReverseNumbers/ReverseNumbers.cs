namespace ReverseNumbers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class ReverseNumbers
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter some int numbers separated by space:");
            var numbersInput = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(Int32.Parse)
                .ToList();

            var numbersStack = new Stack<int>();
            foreach (var number in numbersInput)
            {
                numbersStack.Push(number);
            }

            while(numbersStack.Count > 0)
            {
                Console.Write("{0} ", numbersStack.Pop());
            }

            Console.WriteLine();
        }
    }
}

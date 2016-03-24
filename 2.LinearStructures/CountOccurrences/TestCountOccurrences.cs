namespace CountOccurrences
{
    using System;
    using System.Linq;

    class TestCountOccurrences
    {
        static void Main(string[] args)
        {
            try
            {
                int[] numbers = ReadInput();
                PrintCountOccurrences(numbers);
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void PrintCountOccurrences(int[] numbers)
        {
            var uniqueValues = numbers.Distinct().ToArray();
            Array.Sort(uniqueValues);
            foreach(var value in uniqueValues)
            {
                int count = numbers.Count(n => n == value);
                Console.WriteLine("{0} -> {1} times", value, count);
            }
        }

        static int[] ReadInput()
        {
            Console.WriteLine("Enter some numbers in range [0..1000] separated by space:");
            string input = Console.ReadLine();
            string[] stringsNums = input.Split(' ');
            int[] numbers = new int[stringsNums.Length];
            for (int i = 0; i < stringsNums.Length; i++)
            {
                int number = Convert.ToInt32(stringsNums[i]);
                if(number < 0 || number > 1000)
                {
                    throw new ArgumentOutOfRangeException(
                        "value",
                        string.Format("The number {0} is out of the range [0..1000].", number));
                }

                numbers[i] = number;
            }

            return numbers;
        }
    }
}

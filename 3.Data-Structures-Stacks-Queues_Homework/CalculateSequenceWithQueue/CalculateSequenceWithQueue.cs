namespace CalculateSequenceWithQueue
{
    using System;
    using System.Collections.Generic;

    class CalculateSequenceWithQueue
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter an integer number: ");
            int number = 0;
            try
            {
                number = int.Parse(Console.ReadLine());
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var sequenceQueue = new Queue<int>();
            sequenceQueue.Enqueue(number);
            int count = 0;

            while(count < 49)
            {
                var currentNumber = sequenceQueue.Dequeue();
                count++;
                Console.Write("{0}, ", currentNumber);

                sequenceQueue.Enqueue(currentNumber + 1);
                sequenceQueue.Enqueue(2 * currentNumber + 1);
                sequenceQueue.Enqueue(currentNumber + 2);
            }

            Console.WriteLine("{0}\n", sequenceQueue.Dequeue());
        }
    }
}

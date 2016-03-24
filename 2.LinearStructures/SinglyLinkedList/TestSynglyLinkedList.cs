namespace SinglyLinkedList
{
    using System;

    class TestSynglyLinkedList
    {
        static void Main(string[] args)
        {
            try
            {
                SinglyLinkedList<int> numbers = new SinglyLinkedList<int>();
                // Try to remove element from empty list
                // numbers.Remove(1);
                Console.WriteLine("Check LinkedList.Add()");
                for (int i = 0; i < 10; i++)
                {
                    numbers.Add(i * 5);
                }

                Console.WriteLine("\nPrint elements with foreach using IEnumerator: ");
                foreach (var number in numbers)
                {
                    Console.Write("{0} ", number);
                }

                //numbers.Add(5);
                Console.WriteLine("\n\nRemove element by index:");
                var element1 = numbers.Remove(2);
                var element2 = numbers.Remove(5);
                var element3 = numbers.Remove(7);
                // Try to remove element with index out of range
                // numbers.Remove(20);
                Console.WriteLine("{0} removed from list.", element1.Value);
                Console.WriteLine("{0} removed from list.", element2.Value);
                Console.WriteLine("{0} removed from list.", element3.Value);
                Console.Write("Rest list: ");
                foreach (var number in numbers)
                {
                    Console.Write("{0} ", number);
                }

                int num = 3;
                numbers.Add(num);
                numbers.Add(num);
                numbers.Add(num);

                Console.Write("\n\nIncreased list: ");
                foreach (var number in numbers)
                {
                    Console.Write("{0} ", number);
                }

                Console.WriteLine("\n\nFirst ocurrence of {0} in list: {1}", num, numbers.FirstIndexOf(num));
                Console.WriteLine("\nLast ocurrence of {0} in list: {1}\n", num, numbers.LastIndexOf(num));
            }
            catch(IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

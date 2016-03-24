namespace DataStructureReversedList
{
    using System;

    class TestReversedList
    {
        static void Main(string[] args)
        {
            ReversedList<int> reversedList = new ReversedList<int>();

            for (int i = 0; i < 5; i++)
            {
                reversedList.Add(i);
                Console.WriteLine("{0} added to list.", i);
            }

            Console.WriteLine("\nPrint reversed list with foreach using IEnumerator:");
            foreach(var item in reversedList)
            {
                Console.Write("{0} ", item);
            }

            Console.WriteLine("\n\nAccess elements by index:");
            Console.WriteLine("First element -> {0}", reversedList[0]);
            Console.WriteLine("Last element -> {0}", reversedList[reversedList.Count - 1]);

            Console.WriteLine("\nRemove elements by index:");
            var element1 = reversedList.Remove(4);
            var element2 = reversedList.Remove(2);
            Console.WriteLine("{0} removed from list.", element1);
            Console.WriteLine("{0} removed from list.", element2);
            Console.WriteLine("\nRest of list: {0}\n", string.Join(" ", reversedList));
        }
    }
}

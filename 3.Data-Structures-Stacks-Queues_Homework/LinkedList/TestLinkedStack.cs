namespace LinkedStack
{
    using System;

    class TestLinkedStack
    {
        static void Main(string[] args)
        {
            var linkedStackStrings = new LinkedStack<string>();
            Console.WriteLine("---Fill stack: ");
            for (int i = 1; i <= 5; i++)
            {
                var str = "str-" + i;
                linkedStackStrings.Push(str);
                Console.Write("{0} ", str);
            }

            Console.WriteLine("\nStack count = {0}\n", linkedStackStrings.Count);

            Console.WriteLine("---Fill more elements in the stack:");
            for (int i = 6; i <= 20; i++)
            {
                var str = "str-" + i;
                linkedStackStrings.Push(str);
                Console.Write("{0} ", str);
            }

            Console.WriteLine("\nStack count = {0}\n", linkedStackStrings.Count);

            Console.WriteLine("---Remove 3 elements:");
            for (int i = 1; i <= 3; i++)
            {
                var element = linkedStackStrings.Pop();
                Console.WriteLine("{0}.Remove {1}", i, element.Value);
            }

            Console.WriteLine("Stack count = {0}\n", linkedStackStrings.Count);

            Console.WriteLine("---Convert to array:");
            var arr = linkedStackStrings.ToArray();
            Console.WriteLine("Array length = {0}", arr.Length);

            // this is only for me -> training indexer
            Console.WriteLine("\n---Test indexer:");
            Console.WriteLine("First is the last entered: {0}", linkedStackStrings[0].Value);
            Console.WriteLine("5th element: {0}", linkedStackStrings[5].Value);
            Console.WriteLine("6th element: {0}", linkedStackStrings[6].Value);
        }
    }
}

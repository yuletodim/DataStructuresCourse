namespace ArrayBasedStack
{
    using System;

    class TestArrayStack
    {
        static void Main(string[] args)
        {
            var stackArrayInts = new ArrayStack<int>();
            Console.WriteLine("---Fill stack: ");
            for (int i = 1; i <= 5; i++)
            {
                stackArrayInts.Push(i);
                Console.Write("{0} ", i);
            }

            Console.WriteLine("\nStack count = {0}\n", stackArrayInts.Count);

            Console.WriteLine("---Fill more elements in the stack:");
            for (int i = 6; i <= 20; i++)
            {
                stackArrayInts.Push(i);
                Console.Write("{0} ", i);
            }

            Console.WriteLine("\nStack count = {0}\n", stackArrayInts.Count);

            Console.WriteLine("---Remove 3 elements:");
            for (int i = 1; i <= 3; i++)
            {
                var element = stackArrayInts.Pop();
                Console.WriteLine("{0}.Remove {1}", i, element);
            }

            Console.WriteLine("Stack count = {0}\n", stackArrayInts.Count);

            Console.WriteLine("---Convert to array:");
            var arr = stackArrayInts.ToArray();
            Console.WriteLine("Array length = {0}", arr.Length);
        }
    }
}

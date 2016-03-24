namespace LinkedQueue
{
    using System;

    class TestLinkedQueue
    {
        static void Main(string[] args)
        {
            var linkedQueueStrings = new LinkedQueue<string>();

            Console.WriteLine("---Enqueque 10 elements in the queue:");
            for (int i = 1; i <=10; i++)
            {
                string str = "str-" + i;
                linkedQueueStrings.Enqueque(str);
                Console.Write("{0} ", str);
            }

            Console.WriteLine("\nLinked queue count: {0}\n", linkedQueueStrings.Count);

            Console.WriteLine("---Dequeue 3 elements:");
            Console.WriteLine("{0} dequeued.", linkedQueueStrings.Dequeue());
            Console.WriteLine("{0} dequeued.", linkedQueueStrings.Dequeue());
            Console.WriteLine("{0} dequeued.", linkedQueueStrings.Dequeue());
            Console.WriteLine("Linked queue count: {0}\n", linkedQueueStrings.Count);

            Console.WriteLine("---Convert queue to array:");
            var queueAsArray = linkedQueueStrings.ToArray();
            Console.WriteLine("Array length: {0}", queueAsArray.Length);
            foreach (var item in queueAsArray)
            {
                Console.Write("{0} ", item);
            }

            // this is only for me -> treaning indexer
            Console.WriteLine("\n\n---Test indexer:");
            Console.WriteLine("1st element: {0} ", linkedQueueStrings[0]);
            Console.WriteLine("Last element: {0}", linkedQueueStrings[linkedQueueStrings.Count - 1]);
        }
    }
}

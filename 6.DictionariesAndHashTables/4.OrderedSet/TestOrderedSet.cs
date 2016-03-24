namespace OrderedSetImplementation
{
    class TestOrderedSet
    {
        static void Main(string[] args)
        {
            // Create new OrderedSet
            OrderedSet<int> numericOrderedSet = new OrderedSet<int>();

            // Add elements
            numericOrderedSet.Add(17);
            numericOrderedSet.Add(9);
            numericOrderedSet.Add(12);
            numericOrderedSet.Add(19);
            numericOrderedSet.Add(6);
            numericOrderedSet.Add(25);

            // Print OredredSet using foreach
            System.Console.WriteLine("Initialized new ordered set with {0} elements:", numericOrderedSet.Count);
            System.Console.WriteLine("Print collection with foreach:");
            foreach (var node in numericOrderedSet)
            {
                System.Console.WriteLine(node);
            }

            System.Console.WriteLine();

            // Remove element from OrderedSet
            numericOrderedSet.Remove(9);
            // Remove Root
            numericOrderedSet.Remove(17);

            // Print OrderedSet with PrintInOrder method
            System.Console.WriteLine("Print collection with PrintInOreder method:");
            numericOrderedSet.PrintInOrder();
        }
    }
}

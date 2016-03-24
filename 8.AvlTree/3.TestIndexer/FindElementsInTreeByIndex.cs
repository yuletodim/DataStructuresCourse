namespace TestIndexer
{
    using System;
    using System.Linq;
    using AvlTreeLab;

    class FindElementsInTreeByIndex
    {
        static void Main(string[] args)
        {
            var tree = ReadInput();
            var indexAsString = Console.ReadLine();
            while (indexAsString != string.Empty)
            {
                try
                {
                    int index = int.Parse(indexAsString);
                    var element = tree[index];
                    Console.WriteLine("tree[{0}] = {1}", index, element);
                }
                catch (IndexOutOfRangeException )
                {
                    Console.WriteLine("Invalid index.");
                }

                indexAsString = Console.ReadLine();
            }
        }

        private static AvlTree<int> ReadInput()
        {
            var tree = new AvlTree<int>();
            int[] numsInput = Console.ReadLine().Trim().Split(' ').Select(int.Parse).ToArray();

            foreach (var num in numsInput)
            {
                tree.Add(num);
            }
            return tree;
        }
    }
}

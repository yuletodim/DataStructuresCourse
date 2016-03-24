namespace FindElementsInRange
{
    using System;
    using System.Linq;
    using AvlTreeLab;

    class FindElementsInRange
    {
        static void Main(string[] args)
        {
            int[] numsInput = Console.ReadLine().Trim().Split(' ').Select(int.Parse).ToArray();
            int[] rangeInput = Console.ReadLine().Trim().Split(' ').Select(int.Parse).ToArray();

            var tree = new AvlTree<int>();

            foreach (var num in numsInput)
            {
                tree.Add(num);
            }

            var numsInRange = tree.FindElementsInRange(rangeInput[0], rangeInput[1]);

            if (numsInRange.Count() > 0)
            {
                Console.WriteLine(string.Join(" ", numsInRange));
            }
            else
            {
                Console.WriteLine("There is no elements in range [{0}..{1}].",
                    rangeInput[0], rangeInput[1]);
            }
        }
    }
}

namespace SequenceNtoM
{
    using System;
    using System.Collections.Generic;

    class SequenceNtoM
    {
        static void Main(string[] args)
        {
            Console.Write("Write an integer number n = ");
            int n = int.Parse(Console.ReadLine());
            Console.Write("Write second integer number bigger than {0} m = ", n);
            int m = int.Parse(Console.ReadLine());

            FindSequence(n, m);
        }

        static void FindSequence(int n, int m)
        {
            var newItem = new Item(n);
            var queue = new Queue<Item>();
            queue.Enqueue(newItem);

            Item solution = null;

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                if (item.Value < m)
                {
                    queue.Enqueue(new Item(item.Value + 1, item));
                    queue.Enqueue(new Item(item.Value + 2, item));
                    queue.Enqueue(new Item(item.Value * 2, item));
                }

                if (item.Value == m)
                {
                    solution = item;
                    break;
                }
            }

            if (solution != null)
            {
                PrintSolution(solution);
            }
            else
            {
                Console.WriteLine("No solution.");
            }
        }

        static void PrintSolution(Item item)
        {
            string solution = item.Value.ToString();
            var currentItem = item.PrevItem;
            while (currentItem != null)
            {
                solution = currentItem.Value + " -> " + solution;
                currentItem = currentItem.PrevItem;
            }

            Console.WriteLine(solution);
        }

        private class Item
        {
            public Item(int value, Item prevItem = null)
            {
                this.Value = value;
                this.PrevItem = prevItem;
            }

            public int Value { get; private set; }

            public Item PrevItem { get; set; }
        }
    }
}

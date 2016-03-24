namespace FindTheRoot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class FindRoot
    {
        private static int nodes;
        private static int edges;
        
        public static void Main(string[] args)
        {
            var treeStructure = ReadInput();
            var hasParent = FindChildrenWithParents(nodes, treeStructure);
            var parents = FindParents(hasParent);
            if(parents.Count == 1)
            {
                Console.WriteLine(
                    "The graph is a tree holding {0} nodes (0...{1}) and {2} edges. The root node is {3}.",
                    nodes, nodes - 1, edges, parents[0]);
            }
            else if(parents.Count == 0)
            {
                Console.WriteLine(
                    "The graph is not a tree (all nodes have parents). The graph has {0} nodes (0...{1}) and {2} edges.",
                    nodes, nodes - 1, edges);
            }
            else
            {
                Console.WriteLine(
                    "The graph is not a tree (it is a forest). The graph has {0} nodes (0...{1}) and {2} edges. " +
                    "There are several trees in the forest and their roots are: {3}",
                    nodes, nodes - 1, edges, string.Join(", ", parents));
            }
        }

        private static List<int> FindParents(bool[] hasParent)
        {
            var parents = new List<int>();
            for (int i = 0; i < nodes; i++)
            {
                if (hasParent[i] != true)
                {
                    parents.Add(i);
                }
            }

            return parents;
        }

        private static bool[] FindChildrenWithParents(int nodes, List<int>[] treeStructure)
        {
            var visited = new bool[nodes];
            var hasParent = new bool[nodes];

            for (int i = 0; i < nodes; i++)
            {
                if (!visited[i])
                {
                    visited[i] = true;
                    if (treeStructure[i] != null)
                    {
                        foreach (var childNode in treeStructure[i])
                        {
                            hasParent[childNode] = true;
                        }
                    }
                }
            }

            return hasParent;
        }

        private static List<int>[] ReadInput()
        {
            nodes = int.Parse(Console.ReadLine());
            edges = int.Parse(Console.ReadLine());
            var inputTreeLike = new List<int>[nodes];


            for (int i = 0; i < edges; i++)
            {
                int[] line = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                if (inputTreeLike[line[0]] == null)
                {
                    inputTreeLike[line[0]] = new List<int>();
                    inputTreeLike[line[0]].Add(line[1]);
                }
                else
                {
                    inputTreeLike[line[0]].Add(line[1]);
                }
            }

            return inputTreeLike;
        }
    }
}

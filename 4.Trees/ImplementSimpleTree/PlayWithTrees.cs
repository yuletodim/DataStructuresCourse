namespace PlayWithTrees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PlayWithTrees
    {
        private static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();
        private static int longestPath = 0;
        private static Tree<int> mostLeftDeeperNode;
        private static List<Tree<int>> pathsGivenSum = new List<Tree<int>>();
        private static List<Tree<int>> subtreesGivenSum = new List<Tree<int>>();

        public static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            for (int i = 1; i < nodesCount; i++)
            {
                string[] edge = Console.ReadLine().Split(' ');
                int parentValue = int.Parse(edge[0]);
                Tree<int> parentNode = GetTreeNodeByValue(parentValue);
                int childValue = int.Parse(edge[1]);
                Tree<int> childNode = GetTreeNodeByValue(childValue);
                parentNode.Children.Add(childNode);
                childNode.Parent = parentNode;
            }

            int sumPath = int.Parse(Console.ReadLine());
            int subtreeSum = int.Parse(Console.ReadLine());

            // Find root node
            var rootNode = FindRootNode();
            Console.WriteLine("Root node: {0}", rootNode.Value);

            // Find leaf nodes
            var leafNodes = FindLeafNodes();
            var leafValues = leafNodes
                .Select(leaf => leaf.Value)
                .OrderBy(value => value);
            Console.WriteLine("Leaf nodes: {0}", string.Join(", ", leafValues));

            // Find middle nodes
            var middleNodes = FindMiddleNodes();
            var middleValues = middleNodes
                .Select(node => node.Value)
                .OrderBy(value => value);
            Console.WriteLine("Middle nodes: {0}", string.Join(", ", middleValues));

            // Find longest path
            FindLongestPath(rootNode);
            var path = GetNodePath(mostLeftDeeperNode);
            Console.WriteLine("Longest path: {0} (length = {1})", path, longestPath);

            // Find paths with given sum
            FindPathsGivenSum(rootNode, sumPath);
            if(pathsGivenSum.Count == 0)
            {
                Console.WriteLine("No path with this sum.", sumPath);
            }
            else
            {
                Console.WriteLine("Paths of sum {0}:", sumPath);
                foreach (var node in pathsGivenSum)
                {
                    Console.WriteLine(GetNodePath(node));
                }
            }

            // Find root tree sum
            int sum = GetTreeSum(rootNode);
            Console.WriteLine(sum);

            // Find subtrees given sum
            FindSubtreesGivenSum(rootNode, subtreeSum);
            foreach (var subtree in subtreesGivenSum)
            {
                PrintSubtreeSumExpression(subtree);
            }
        }

        private static Tree<int> GetTreeNodeByValue(int value)
        {
            if(!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree<int>(value);
            }

            return nodeByValue[value];
        }

        private static Tree<int> FindRootNode()
        {
            var rootNode = nodeByValue.Values.FirstOrDefault(node => node.Parent == null);
            return rootNode;
        }

        private static IEnumerable<Tree<int>> FindMiddleNodes()
        {
            var middleNodes = nodeByValue.Values
                .Where(node => node.Children.Count > 0 && node.Parent != null)
                .ToList();
            return middleNodes;
        }

        private static IEnumerable<Tree<int>> FindLeafNodes()
        {
            var leafNodes = nodeByValue.Values
                .Where(node => node.Children.Count == 0)
                .ToList();
            return leafNodes;
        }

        private static void FindLongestPath(Tree<int> tree, int depth = 1) 
        {
            if(depth > longestPath)
            {
                longestPath = depth;
                mostLeftDeeperNode = tree;
            }

            foreach (var child in tree.Children)
            {
                FindLongestPath(child, depth + 1);
            }
        }

        private static string GetNodePath(Tree<int> node)
        {
            string path = node.Value.ToString();
            var currentNode = node.Parent;
            while(currentNode != null)
            {
                path = currentNode.Value + " -> " + path;
                currentNode = currentNode.Parent;
            }

            return path;
        }

        private static void FindPathsGivenSum(Tree<int> tree, int sum)
        {
            if(tree.Value == sum)
            {
                pathsGivenSum.Add(tree);
            }
            else
            {
                foreach (var child in tree.Children)
                {
                    FindPathsGivenSum(child, sum - tree.Value);
                }
            }
        }

        private static void FindSubtreesGivenSum(Tree<int> tree, int sum)
        {
            if(GetTreeSum(tree) == sum)
            {
                subtreesGivenSum.Add(tree);
            }
            else
            {
                foreach (var child in tree.Children)
                {
                    FindSubtreesGivenSum(child, sum);
                }
            }
        }

        private static int GetTreeSum(Tree<int> tree) 
        {
            int sum = tree.Value;

            foreach (var child in tree.Children)
            {
                sum += GetTreeSum(child);
            }

            return sum;
        }

        private static void PrintSubtreeSumExpression(Tree<int> node)
        {
            var subtreeNodes = new List<int>();
            TraverseTree(node, subtreeNodes);

            Console.WriteLine(string.Join(" + ", subtreeNodes));
        }

        private static void TraverseTree(Tree<int> tree, List<int> values)
        {
            values.Add(tree.Value);
            foreach (var child in tree.Children)
            {
                TraverseTree(child, values);
            }
        }
    }
}

namespace QuadTreeImplementation.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class QuadTree<T> where T : IBoundable
    {
        public const int DefaultMaxDepth = 5;

        public readonly int MaxDepth;

        private Node<T> root;

        public QuadTree(int width, int height, int maxDepth = DefaultMaxDepth)
        {
            this.root = new Node<T>(0, 0, width, height);
            this.Bounds = this.root.Bounds;
            this.MaxDepth = maxDepth;
        }

        public int Count { get; private set; }

        public Rectangle Bounds { get; private set; }

        public bool Insert(T item)
        {
            // I. If new object is outside the bounds of the Quadtree -> cannot add
            if (!item.Bounds.IsInside(this.Bounds))
            {
                return false;
            }

            // II. Insert item at the most lower possible node which can contain the entire item.
            int depth = 1;
            var currentNode = this.root;
            // Traverse the tree and check where to inset item
            while(currentNode.Children != null)
            {
                var quadrant = GetQuadrant(currentNode, item.Bounds);
                if(quadrant == -1)
                {
                    break;
                }

                currentNode = currentNode.Children[quadrant];
            }

            currentNode.Items.Add(item);
            this.Split(currentNode, depth);
            this.Count++;

            return true;
        }

        private void Split(Node<T> node, int nodeDepth)
        {
            // If node does not need to split or it has reached its max depth -> stop
            if(!(node.ShouldSplit && nodeDepth < MaxDepth))
            {
                return;
            }

            var leftWidth = node.Bounds.Width / 2;
            var rightWidth = node.Bounds.Width - leftWidth;
            var topHeight = node.Bounds.Height / 2;
            var bottomHeight = node.Bounds.Height - topHeight;

            node.Children = new Node<T>[4];
            node.Children[0] = new Node<T>(node.Bounds.MidX, node.Bounds.Y1, rightWidth, topHeight);
            node.Children[1] = new Node<T>(node.Bounds.X1, node.Bounds.Y1, leftWidth, topHeight);
            node.Children[2] = new Node<T>(node.Bounds.X1, node.Bounds.MidY, leftWidth, bottomHeight);
            node.Children[3] = new Node<T>(node.Bounds.MidX, node.Bounds.MidY, rightWidth, bottomHeight);

            // Transfer the items from parent to new nodes
            for (int i = node.Items.Count- 1; i >= 0; i--)
            {
                var item = node.Items[i];
                var quadrant = GetQuadrant(node, item.Bounds);
                // Remove item from parent and add to child node
                if (quadrant != -1)
                {
                    node.Children[quadrant].Items.Add(item);
                    node.Items.Remove(item);
                }
            }

            // If all items had gone to one node -> attepmt split recursively
            foreach (var child in node.Children)
            {
                this.Split(child, nodeDepth + 1);
            }
        }

        /// <summary>
        /// Reports collisions
        /// </summary>
        /// <param name="bounds">Bounds of the  current object</param>
        /// <returns>List with all other objects that may intersect the current object</returns>
        public List<T> Report(Rectangle bounds)
        {
            var collisionCandidates = new List<T>();
            GetCollisionCandidates(this.root, bounds, collisionCandidates);
            return collisionCandidates;
        }

        private static void GetCollisionCandidates(Node<T> node, Rectangle bounds, List<T> results)
        {
            var quadrant = GetQuadrant(node, bounds);
            if(quadrant == -1)
            {
                // object does not fit in any sub-quadrant
                GetSubtreeContents(node, bounds, results);
            }
            else
            {
                if (node.Children != null)
                {
                    // Call recursion for the quadrant that contains bounds
                    GetCollisionCandidates(node.Children[quadrant], bounds, results);
                }

                results.AddRange(node.Items);
            }
        }

        // Post-Order DFS to retrieve all items from a given subtree
        private static void GetSubtreeContents(Node<T> node, Rectangle bounds, List<T> results)
        {
            if(node.Children != null)
            {
                foreach (var childNode in node.Children)
                {
                    if (childNode.Bounds.Intersects(bounds))
                    {
                        GetSubtreeContents(childNode, bounds, results);
                    }
                }
            }

            results.AddRange(node.Items);
        }

        private static int GetQuadrant(Node<T> node, Rectangle bounds)
        {
            // X prez koito minava vertikalata
            var verticalMidPoint = node.Bounds.MidX;
            // Y prez koito minava horizontalata
            var horizontalMidPoint = node.Bounds.MidY;

            // Check if item(bounds) is up or down
            var inTopQuadrant = node.Bounds.Y1 <= bounds.Y1 && bounds.Y2 <= horizontalMidPoint;
            var inBottomQuadrant = horizontalMidPoint <= bounds.Y1 && bounds.Y2 <= node.Bounds.Y2;

            // Check if item(bounds) is left or right
            var inLeftQuadrant = node.Bounds.X1 <= bounds.X1 && bounds.X2 <= verticalMidPoint;
            var inRightQuadrant = verticalMidPoint <= bounds.X1 && bounds.X2 <= node.Bounds.X2;

            // Determinate the quadrant -> 0, 1, 2, 3 if no one contains entire item -> -1
            if (inTopQuadrant)
            {
                if (inRightQuadrant)
                {
                    return 0;
                }
                else if (inLeftQuadrant)
                {
                    return 1;
                }
            }

            if (inBottomQuadrant)
            {
                if (inLeftQuadrant)
                {
                    return 2;
                }
                else if (inRightQuadrant)
                {
                    return 3;
                }
            }

            return -1;
        }

        public void ForEachDfs(Action<List<T>, int, int> action)
        {
            this.ForEachDfs(this.root, action);
        }

        private void ForEachDfs(Node<T> node, Action<List<T>, int, int> action, int depth = 1, int quadrant = 0)
        {
            if (node == null)
            {
                return;
            }

            if (node.Items.Any())
            {
                // Call action with arguments -> currentNode, nodeDepth, quadrant[0...3]
                action(node.Items, depth, quadrant);
            }

            if(node.Children != null)
            {
                foreach (var childNode in node.Children)
                {
                    ForEachDfs(childNode, action, depth+1, quadrant);
                }
            }
        }
    }
}

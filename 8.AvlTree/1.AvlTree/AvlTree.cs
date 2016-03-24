namespace AvlTreeLab
{
    using System;
    using System.Collections.Generic;

    public class AvlTree<T> where T : IComparable<T>
    {
        private Node<T> root;

        public Node<T> Root { get { return this.root; } }

        public int Count { get; private set; }

        public T this[int index]
        {
            get
            {
                if(index < 0 || index > this.Count - 1)
                {
                    throw new IndexOutOfRangeException(
                        string.Format("Index chould be in range [0..{0}]", this.Count - 1));
                }

                return this.FindElementValue(index);
            }
        }

        private T FindElementValue(int index)
        {
            T elementValue = default(T);
            var currentNode = this.root;

            while (currentNode != null)
            {
                if (currentNode.Index == index)
                {
                    elementValue = currentNode.Value;
                    break;
                }
                else if (currentNode.Index > index)
                {
                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    currentNode = currentNode.RightChild;
                }
            }

            return elementValue;
        }

        public void Add(T item)
        {
            var inserted = true;
            // If the tree is empty set the root
            if (this.root == null)
            {
                this.root = new Node<T>(item);
            }
            // Else we call method to  insert the new node -> leaf
            else
            {
                inserted = this.InsertInternal(this.root, item);
            }

            // If insertion successful -> increase count
            if (inserted)
            {
                this.Count++;
            }
        }

        private bool InsertInternal(Node<T> node, T item)
        {
            // If item exist -> break
            if (this.Contains(item))
            {
                return false;
            }

            var currentNode = node;
            var newNode = new Node<T>(item);
            bool shouldRetrace = false;

            while (true)
            {
                // Lookup and insert node
                // If newNode < currentNode -> go right
                if(currentNode.Value.CompareTo(item) < 0)
                {
                    if (currentNode.RightChild == null)
                    {
                        // Set newNode as right child
                        currentNode.RightChild = newNode;
                        // CurrentNode -. heavy in right => decrease BF
                        currentNode.BalanceFactor--;
                        // Check if BF != 0
                        shouldRetrace = currentNode.BalanceFactor != 0;
                        break;
                    }

                    currentNode = currentNode.RightChild;
                }
                // Else -> we go in left do the same
                else
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = newNode;
                        // Increase BF - heavy in left
                        currentNode.BalanceFactor++;
                        shouldRetrace = currentNode.BalanceFactor != 0;
                        break;
                    }

                    currentNode = currentNode.LeftChild;
                }
            }

            this.SetElementsIndex(newNode);

            if (shouldRetrace)
            {
                // Retarce and rebalnce the tree
                this.RetraceInsert(currentNode);
            }

            return true;
        }

        private void RetraceInsert(Node<T> node)
        {
            // Retrace from the newNode parent to the root
            var parent = node.Parent;
            while (parent != null)
            {
                if (node.IsLeftChild)
                {
                    // Node is left -> left subtree has grown -> increase BF + 1
                    // Check Parent's BF, if necessary perform rotation (rotation when BF == 2)
                    // There are 3 cases before insertion -> 0, 1, -1
                    if(parent.BalanceFactor == 1)
                    {
                        // If it was 1 -> 2
                        parent.BalanceFactor++;
                        // Check if is straight
                        if(node.BalanceFactor == -1)
                        {
                            // Left rotation of node
                            this.RotateLeft(node);
                        }

                        // Right rotation of parent
                        this.RotateRight(parent);
                        break;

                        // Check if right branch is heavyer
                        if(node.BalanceFactor == 2)
                        {

                        }
                    }
                    else if(parent.BalanceFactor == -1)
                    {
                        // If it was -1 -> 0
                        parent.BalanceFactor = 0;
                        break;
                    }
                    else
                    {
                        // If it was 0 -> 1
                        parent.BalanceFactor = 1;
                    }

                }
                else
                {
                    // Node is right -> right subtree has grown -> decrease BF - 1
                    // Check Parent's BF, if necessary perform rotation (rotation when BF == -2)
                    if (parent.BalanceFactor == -1)
                    {
                        // Parent BF -> -2
                        parent.BalanceFactor--;
                        // Check if branch is straight
                        if(node.BalanceFactor == 1)
                        {
                            this.RotateRight(node);
                        }

                        this.RotateLeft(parent);
                        break;
                    }
                    else if (parent.BalanceFactor == 1)
                    {
                        parent.BalanceFactor = 0;
                        break;
                    }
                    else
                    {
                        parent.BalanceFactor = -1;
                    }
                }

                node = parent;
                parent = node.Parent;
            }
        }

        private void RotateRight(Node<T> node)
        {
            var parent = node.Parent;
            var child = node.LeftChild;

            if(parent != null)
            {
                if (node.IsLeftChild)
                {
                    parent.LeftChild = child;
                }
                else
                {
                    parent.RightChild = child;
                }
            }
            else
            {
                this.root = child;
                this.root.Parent = null;
            }

            // Left child of node becomes the right child of child
            node.LeftChild = child.RightChild;
            // Node becomes right child of the child
            child.RightChild = node;

            node.BalanceFactor -= 1 + Math.Max(child.BalanceFactor, 0);
            child.BalanceFactor -= 1 - Math.Min(node.BalanceFactor, 0);
        }

        private void RotateLeft(Node<T> node)
        {
            var parent = node.Parent;
            var child = node.RightChild;

            if(parent != null)
            {
                // Link parent with new node
                if (node.IsLeftChild)
                {
                    parent.LeftChild = child;
                }
                else
                {
                    parent.RightChild = child;
                }

            }
            else
            {
                // If no parent set new root
                this.root = child;
                this.root.Parent = null;
            }

            // Right child of node becomes left child of child
            node.RightChild = child.LeftChild;
            // Node becomes left child of the child
            child.LeftChild = node;

            node.BalanceFactor += 1 - Math.Min(child.BalanceFactor, 0);
            child.BalanceFactor += 1 + Math.Max(node.BalanceFactor, 0);
        }

        public bool Contains(T item)
        {
            var node = this.root;
            while(node != null)
            {
                if(node.Value.CompareTo(item) == 0)
                {
                    return true;
                }
                else if(node.Value.CompareTo(item) > 0)
                {
                    node = node.LeftChild;
                }
                else
                {
                    node = node.RightChild;
                }
            }

            return false;
        }

        public void ForeachDfs(Action<T> action)
        {
            if(this.Count == null)
            {
                return;
            }

            this.InOrderDfs(this.root, action);
        }

        private void InOrderDfs(Node<T> node, Action<T> action)
        {
            if (node.LeftChild != null)
            {
                this.InOrderDfs(node.LeftChild, action);
            }

            action(node.Value);

            if (node.RightChild != null)
            {
                this.InOrderDfs(node.RightChild, action);
            }
        }

        public void PrintInOrderDfs(Node<T> node)
        {
            if (node.LeftChild != null)
            {
                this.PrintInOrderDfs(node.LeftChild);
            }

            Console.WriteLine(string.Format("{0} - {1}", node.Index, node.Value));

            if (node.RightChild != null)
            {
                this.PrintInOrderDfs(node.RightChild);
            }
        }

        public IEnumerable<T> FindElementsInRange(T from, T to)
        {
            var elementsInRange = new List<T>();

            this.ForeachDfs(elementValue =>
            {
                if(elementValue.CompareTo(from) >= 0 && elementValue.CompareTo(to) <= 0)
                {
                    elementsInRange.Add(elementValue);
                }
            });

            return elementsInRange;
        }

        private void SetElementsIndex(Node<T> newNode)
        {
            if (newNode.IsLeftChild)
            {
                newNode.Index = newNode.Parent.Index;
            }

            if (newNode.IsRightChild)
            {
                newNode.Index = newNode.Parent.Index + 1;
            }

            this.ModifyIndex(this.root, newNode);
        }

        private void ModifyIndex(Node<T> currentNode, Node<T> newNode)
        {
            if(currentNode.LeftChild != null)
            {
                ModifyIndex(currentNode.LeftChild, newNode);
            }

            if(currentNode.Value.CompareTo(newNode.Value) > 0)
            {
                currentNode.Index += 1;
            }

            if (currentNode.RightChild != null)
            {
                ModifyIndex(currentNode.RightChild, newNode);
            }
        }
    }
}

namespace OrderedSetImplementation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class OrderedSet<T> : IEnumerable<T> where T : IComparable<T>
    {
        public OrderedSet()
        {
            this.Root = null;
            this.Count = 0;
        }

        public int Count { get; private set; }

        public Node<T> Root { get; set; }

        public void Add(T value)
        {
            // Check if value is null -> throw exception
            if (value == null)
            {
                throw new ArgumentNullException(
                    string.Format("Can not add null value to the collection."));
            }

            // Check if collection is empty -> set Root
            if (this.Count == 0)
            {
                this.Root = new Node<T>(value);
                this.Count++;
            }

            // Check if the value exist in the collection and insert it in the collection
            if (!this.Contains(value))
            {
                this.InsertNewNode(value);
            }
        }

        private void InsertNewNode(T value)
        {
            var currentNode = this.Root;
            while (currentNode != null)
            {
                // Check if value is smaller than the current node 
                if (currentNode.Value.CompareTo(value) > 0)
                {
                    // If left child is null -> set value
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = new Node<T>(value);
                        currentNode.LeftChild.Parent = currentNode;
                        break;
                    }
                    else
                    {
                        currentNode = currentNode.LeftChild;
                    }
                }
                else
                {
                    // In case value  is bigger than the current node and its raight child is null
                    if (currentNode.RightChild == null)
                    {
                        currentNode.RightChild = new Node<T>(value);
                        currentNode.RightChild.Parent = currentNode;
                        break;
                    }
                    else
                    {
                        currentNode = currentNode.RightChild;
                    }
                }
            }

            // Increase count
            this.Count++;
        }

        public void Remove(T value)
        {
            // Check iv value exist in the collection
            if (!this.Contains(value))
            {
                throw new ArgumentException(
                    string.Format("Element with value {0} does not exist in the collection.", value));
            }

            // Find node
            var nodeToRemove = this.Find(value);

            // Find if node is left or right child to its parent and set to this place its right child(the bigger)
            if (nodeToRemove.Parent == null)
            {
                this.Root = this.Root.RightChild;
            }
            else if (nodeToRemove.Value.CompareTo(nodeToRemove.Parent.Value) < 0)
            {
                nodeToRemove.Parent.LeftChild = nodeToRemove.RightChild;
            }
            else
            {
                nodeToRemove.Parent.RightChild = nodeToRemove.RightChild;
            }

            // Set the left child of the node to the last left child of the right child without left child
            var currentNode = nodeToRemove.RightChild;
            while(currentNode != null)
            {
                if (currentNode.LeftChild == null)
                {
                    currentNode.LeftChild = nodeToRemove.LeftChild;
                    break;
                }

                currentNode = currentNode.LeftChild;
            }

            // Unset node
            nodeToRemove = default(Node<T>);

            // Reduce count
            this.Count--;
        }

        private Node<T> Find(T value)
        {
            var currentNode = this.Root;
            while(currentNode != null)
            {
                if (currentNode.Value.CompareTo(value) > 0)
                {
                    currentNode = currentNode.LeftChild;
                }
                else if (currentNode.Value.CompareTo(value) < 0)
                {
                    currentNode = currentNode.RightChild;
                }
                else if(currentNode.Value.CompareTo(value) == 0)
                {
                    return currentNode;
                }
            }

            return null;
        }

        public bool Contains(T value)
        {
            var node = this.Find(value);
            return node != null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Root.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void EachInOrder(Action<T> action)
        {
            if(this.Root.LeftChild != null)
            {
                this.Root.LeftChild.EachInOrder(action);
            }

            action(this.Root.Value);

            if(this.Root.RightChild != null)
            {
                this.Root.EachInOrder(action);
            }
        }

        public void PrintInOrder()
        {
            if (this.Root.LeftChild != null)
            {
                foreach (var child in this.Root.LeftChild)
                {
                    Console.WriteLine(child);
                }
            }

            Console.WriteLine(this.Root.Value);

            if (this.Root.RightChild != null)
            {
                foreach (var child in this.Root.RightChild)
                {
                    Console.WriteLine(child);
                }
            }
        }
    }
}

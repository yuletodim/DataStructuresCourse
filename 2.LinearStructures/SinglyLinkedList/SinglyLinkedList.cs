namespace SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        private ListNode<T> head;
        private ListNode<T> lastNode;

        public int Count { get; private set; }

        private ListNode<T> this[int index]
        {
            get
            {
                if(index < 0 || index >= this.Count)
                {
                    throw new IndexOutOfRangeException(
                        string.Format("Index range of current list is [0..{0}]", this.Count-1));
                }

                int currentIndex = 0;
                var currentNode = this.head;

                while(currentNode != null)
                {
                    if(currentIndex == index)
                    {
                        break;
                    }

                    currentIndex++;
                    currentNode = currentNode.NextNode;
                }

                return currentNode;
            }
        }

        public void Add(T element)
        {
            var newNode = new ListNode<T>(element);
            if(this.Count == 0)
            {
                this.head = newNode;
                this.lastNode = newNode;
            }
            else
            {
                this.lastNode.NextNode = newNode;
                this.lastNode = newNode;
            }

            this.Count++;
        }

        public ListNode<T> Remove(int index)
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException(
                    "Cannot remove element from empty list.");
            }

            var elementToRemove = this[index];
            if (this.Count == 1)
            {
                this.head = null;
                this.lastNode = null;
            }
            else
            {
                if(index == 0)
                {
                    this.head = elementToRemove.NextNode;
                }
                else
                {
                    var prevElement = this[index - 1];
                    var nextNode = elementToRemove.NextNode;
                    prevElement.NextNode = nextNode;
                    if(nextNode == null)
                    {
                        this.lastNode = prevElement;
                    }
                }
            }

            this.Count--;
            return elementToRemove;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentElement = this.head;
            while(currentElement != null)
            {
                yield return currentElement.Value;
                currentElement = currentElement.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int FirstIndexOf(T item)
        {
            int currentIndex = 0;
            foreach (var element in this)
            {
                if (element.Equals(item))
                {
                    return currentIndex;
                }

                currentIndex++;
            }

            return -1;
        }

        public int LastIndexOf(T item)
        {
            int currentIndex = 0;
            int foundIndex = -1;

            foreach (var element in this)
            {
                if(element.Equals(item))
                {
                    foundIndex = currentIndex;
                }

                currentIndex++;
            }

            return foundIndex;
        }
    }
}

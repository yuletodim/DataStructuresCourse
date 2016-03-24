namespace LinkedStack
{
    using System;

    public class LinkedStack<T>
    {
        private LinkedNode<T> firstNode;

        public int Count { get; private set; }

        public void Push(T element)
        {
            if(this.Count == 0)
            {
                this.firstNode = new LinkedNode<T>(element);
            }
            else
            {
                var newNode = new LinkedNode<T>(element, this.firstNode);
                this.firstNode = newNode;
            }

            this.Count++;
        }

        public LinkedNode<T> Pop()
        {
            if(this.Count == 0)
            {
                throw new InvalidOperationException("Can not po from empty satck.");
            }

            var element = this.firstNode;
            this.firstNode = this.firstNode.NextNode;
            this.Count--;
            return element;
        }

        public T[] ToArray()
        {
            T[] stackAsArray = new T[this.Count];
            int index = 0;
            var currentElement = this.firstNode;
            while (currentElement != null)
            {
                stackAsArray[index] = currentElement.Value;
                index++;
                currentElement = currentElement.NextNode;
            }

            return stackAsArray;
        }

        // this is only for me -> training indexer
        public LinkedNode<T> this[int index]
        {
            get
            {
                if(index < 0 || index >= this.Count)
                {
                    throw new IndexOutOfRangeException(
                        String.Format("Index should be in the range [0..{0}]", this.Count - 1));
                }

                int currentIndex = 0;
                var currentNode = this.firstNode;
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
    }
}

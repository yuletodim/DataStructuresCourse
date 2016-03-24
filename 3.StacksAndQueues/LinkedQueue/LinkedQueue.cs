namespace LinkedQueue
{
    using System;

    public class LinkedQueue<T>
    {
        private LinkedNode<T> head;
        private LinkedNode<T> tail;

        public int Count { get; private set; }

        public void Enqueque(T element)
        {
            var newNode = new LinkedNode<T>(element);

            if (this.Count == 0)
            {                
                this.head = newNode;
                this.tail = newNode;
            }
            else
            {
                this.tail.NextNode = newNode;
                this.tail = newNode;
            }

            this.Count++;
        }

        public T Dequeue()
        {
            if(this.Count == 0)
            {
                throw new InvalidOperationException("Can not dequeque from epmty queue.");
            }
            var element = this.head;
            this.head = this.head.NextNode;
            this.Count--;
            return element.Value;
        }

        public T[] ToArray()
        {
            var queueAsArray = new T[this.Count];
            var index = 0;
            var currentElement = this.head;
            while(currentElement != null)
            {
                queueAsArray[index] = currentElement.Value;
                index++;
                currentElement = currentElement.NextNode;
            }

            return queueAsArray;
        }

        // this is only for me -> treaning indexer
        public T this[int index]
        {
            get
            {
                if(index < 0 || index >= this.Count)
                {
                    throw new IndexOutOfRangeException(
                        String.Format("Index should be in the range [0.. {0}]", this.Count - 1));
                }

                int currentIndex = 0;
                var currentElement = this.head;
                while(currentElement != null)
                {
                    if(index == currentIndex)
                    {
                        break;
                    }

                    currentIndex ++;
                    currentElement = currentElement.NextNode;
                }

                return currentElement.Value;
            }
        }
    }
}

namespace ArrayBasedStack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ArrayStack<T>
    {
        private T[] elements;

        private const int InitialCapacity = 16;

        public ArrayStack(int capacity = InitialCapacity)
        {
            this.elements = new T[capacity];
        }

        public int Count { get; private set; }

        public void Push(T element)
        {
            if(this.Count == this.elements.Length)
            {
                this.Grow();
            }

            this.elements[this.Count] = element;
            this.Count++;
        }

        public T Pop()
        {
            if(this.Count == 0)
            {
                throw new InvalidOperationException(
                    "Can not pop from empty stack.");
            }

            T element = this.elements[this.Count - 1];
            this.elements[this.Count - 1] = default(T);
            this.Count--;

            return element;
        }

        public T[] ToArray()
        {
            var result = new T[this.Count];
            Array.Copy(this.elements, result, this.Count);
            Array.Reverse(result);
            return result;
        }

        private void Grow()
        {
            var newElements = new T[2 * this.elements.Length];
            Array.Copy(this.elements, newElements, this.Count);
            this.elements = newElements;
        }
    }
}

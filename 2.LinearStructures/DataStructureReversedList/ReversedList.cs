namespace DataStructureReversedList
{
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IEnumerable<T>
    {
        private T[] list;
        private int count;
        private int capacity = 16;

        public ReversedList()
        {
            this.list = new T[16];
            this.count = 0;
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }
        }

        public T this[int index]
        {
            get
            {
                return this.list[index];
            }
        }

        public void Add(T element)
        {
            if(this.count == this.capacity)
            {
                T[] newList = new T[2 * this.capacity];
				this.capacity = 2 * this.capacity;

                this.count++;
                for (int i = this.count - 1; i > 0; i--)
                {
                    newList[i] = this.list[i - 1];
                }

                newList[0] = element;
                this.list = newList;
            }
            else
            {
                this.count++;
                for (int i = this.count - 1; i > 0; i--)
                {
                    this.list[i] = this.list[i - 1];
                }

                this.list[0] = element;
            }
        }

        public T Remove(int index)
        {
            T element = this.list[index];
            for (int i = index; i < this.count - 1; i++)
            {
                this.list[i] = this.list[i + 1];
            }

            // set default value to the last element -> null or 0
            this.list[count - 1] = default(T);
            this.count--;
            return element;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.count; i++)
            {
                yield return this.list[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

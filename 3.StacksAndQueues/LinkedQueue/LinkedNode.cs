namespace LinkedQueue
{
    public class LinkedNode<T>
    {
        public LinkedNode(T value)
        {
            this.Value = value;
        }

        public T Value { get; private set; }

        public LinkedNode<T> NextNode { get; set; }

        public LinkedNode<T> PrevNode { get; set; }
    }
}

namespace LinkedStack
{
    public class LinkedNode<T>
    {
        public LinkedNode(T value, LinkedNode<T> nextNode = null)
        {
            this.Value = value;
            this.NextNode = nextNode;
        }

        public T Value { get; private set; }

        public LinkedNode<T> NextNode { get; set; }
    }
}

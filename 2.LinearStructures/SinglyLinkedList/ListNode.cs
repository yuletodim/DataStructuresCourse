namespace SinglyLinkedList
{
    public class ListNode<T>
    {
        public ListNode()
        {

        }

        public ListNode(T value)
        {
            this.Value = value;
        }

        public T Value { get; private set; }

        public ListNode<T> NextNode { get; set; }
    }
}

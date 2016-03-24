namespace AvlTreeLab
{
    using System;

    public class Node<T> where T : IComparable<T>
    {
        private Node<T> leftChild;
        private Node<T> rightChild;

        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public int Index { get; set; }

        public Node<T> LeftChild
        {
            get
            {
                return this.leftChild;
            }

            set
            {
                // If there is child set its THIS node
                if(value != null)
                {
                    value.Parent = this;
                }

                this.leftChild = value;
            }
        }

        public Node<T> RightChild
        {
            get
            {
                return this.rightChild;
            }

            set
            {
                // If there is child set its THIS node
                if (value != null)
                {
                    value.Parent = this;
                }

                this.rightChild = value;
            }
        }

        public Node<T> Parent { get; set; }

        public int BalanceFactor { get; set; }

        public bool IsLeftChild
        {
            get
            {
                bool isLeftChild = true;

                if (this.Parent == null)
                {
                    isLeftChild = false;
                }
                else
                {
                    if (this.Value.CompareTo(this.Parent.Value) > 0)
                    {
                        isLeftChild = false;
                    }
                }

                return isLeftChild;
            }
        }
        
        public bool IsRightChild
        {
            get
            {
                bool isRightChild = true;

                if(this.Parent == null)
                {
                    isRightChild = false;
                }
                else
                {
                    if (this.Value.CompareTo(this.Parent.Value) < 0)
                    {
                        isRightChild = false;
                    }
                }

                return isRightChild;
            }
        }

        public int ChildrenCount
        {
            get
            {
                int count = 0;

                if((this.LeftChild != null && this.RightChild == null) ||
                    (this.LeftChild == null  && this.RightChild != null))
                {
                    count = 1;
                }
                else if(this.LeftChild != null && this.RightChild != null)
                {
                    count = 2;
                }

                return count;
            }
        }
        
        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}


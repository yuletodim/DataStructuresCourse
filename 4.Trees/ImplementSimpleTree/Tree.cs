namespace PlayWithTrees
{
    using System;
    using System.Collections.Generic;

    public class Tree<T>
    {
        public Tree(T value, Tree<T> parent = null, params Tree<T>[] children)
        {
            this.Value = value;
            this.Parent = parent;
            this.Children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.Children.Add(child);
            }
        }

        public T Value { get; set; }

        public Tree<T> Parent { get; set; }

        public IList<Tree<T>> Children { get; private set; }

        public void Print(int indent = 0)
        {
            System.Console.Write(new string(' ', 2 * indent));
            System.Console.WriteLine(this.Value);

            foreach (var child in this.Children)
            {
                child.Print(indent + 1);
            }
        }

        public void Each(Action<T> action)
        {
            action(this.Value);
            foreach (var child in this.Children)
            {
                child.Each(action);
            }
        }
    }
}

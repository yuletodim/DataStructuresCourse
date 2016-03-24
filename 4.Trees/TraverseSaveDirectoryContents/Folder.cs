namespace TraverseSaveDirectoryContents
{
    using System;

    public class Folder
    {
        private long size;

        public Folder(string name, File[] files = null, Folder[] childFolders = null)
        {
            this.Name = name;
            this.Files = files;
            this.ChildFolders = childFolders;
        }

        public string Name { get; private set; }

        public File[] Files { get; set; }

        public Folder[] ChildFolders { get; set; }

        public long Size
        {
            get
            {
                this.FindFolderSize();
                return this.size;
            }
        }

        public void Print(int indent = 0)
        {
            var bullet = new string(' ', 2 * indent);
            Console.WriteLine("{0}{1}", bullet, this.Name);

            foreach (var file in this.Files)
            {
                Console.WriteLine("{0}-- {1} -> {2} bytes", bullet, file.Name, file.Size);
            }

            foreach (var folder in this.ChildFolders)
            {
                folder.Print(indent + 1);
            }
        }

        private void FindFolderSize()
        {
            foreach (var file in this.Files)
            {
                this.size += (long)file.Size;
            }

            foreach (var childFolder in this.ChildFolders)
            {
                childFolder.FindFolderSize();
                this.size += childFolder.Size;
            }
        }
    }
}

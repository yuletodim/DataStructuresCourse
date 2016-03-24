namespace TraverseSaveDirectoryContents
{
    public class File
    {
        public File(string name, int size)
        {
            this.Name = name;
            this.Size = size;
        }

        public string Name { get; private set; }

        public int Size { get; private set; }
    }
}

namespace TraverseSaveDirectoryContents
{
    using System;
    using System.IO;

    public class TraverseSaveDirectoryContents
    {
        private const string folderPath = @"C:\Users\...\MyDocuments";

        public static void Main(string[] args)
        {
            // Create, fill and print folder "MyDocuments"
            Folder folder = new Folder("MyDocuments");
            FillFolder(folder, folderPath);
            folder.Print();

            // Find and print SUM of all subfolders in "MyDocuments"
            Console.WriteLine("\nSubfolders sizes:");
            foreach (var subFolder in folder.ChildFolders)
            {
                Console.WriteLine("{0} -> {1} bytes", subFolder.Name, subFolder.Size);
            }
        }

        private static void FillFolder(Folder folder, string currentFolderPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(currentFolderPath);

            var gotFiles = dirInfo.GetFiles();
            File[] files = new File[gotFiles.Length];
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = new File(gotFiles[i].Name, (int)gotFiles[i].Length);
            }

            folder.Files = files;

            var gotDirectories = dirInfo.GetDirectories();
            folder.ChildFolders = new Folder[gotDirectories.Length];
            for (int i = 0; i < gotDirectories.Length; i++)
            {
                folder.ChildFolders[i] = new Folder(gotDirectories[i].Name);
                var dirPath = gotDirectories[i].FullName;
                FillFolder(folder.ChildFolders[i], dirPath);
            }
        }
    }
}

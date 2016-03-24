namespace StringEditor
{
    using System;
    using Wintellect.PowerCollections;

    class StringEditor
    {
        static void Main(string[] args)
        {
            var text = new BigList<char>();
            bool isRunning = true;
            while (isRunning)
            {
                try
                {
                    string[] commandArgs = Console.ReadLine().Trim().Split(' ');
                    if (commandArgs[0] == "END")
                    {
                        break;
                    }
                
                    ExecuteCommand(text, commandArgs, isRunning);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR");
                }
            }
        }

        private static void ExecuteCommand(BigList<char> text, string[] commandArgs, bool isRunning)
        {
            var command = commandArgs[0];

            switch (command)
            {
                case "APPEND":
                    var newString = GetString(1, commandArgs);
                    AppendStringToText(text, newString);
                    break;
                case "INSERT":
                    newString = GetString(2, commandArgs);
                    InsertStringInText(text, int.Parse(commandArgs[1]), newString);
                    break;
                case "DELETE":
                    DeleteSubstringFromText(text, int.Parse(commandArgs[1]), int.Parse(commandArgs[2]));
                    break;
                case "REPLACE":
                    newString = GetString(3, commandArgs);
                    ReplaceSubstringInText(text, int.Parse(commandArgs[1]), int.Parse(commandArgs[2]), newString);
                    break;
                case "PRINT":
                    PrintText(text);
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }

        private static string GetString(int index, string[] commandArgs)
        {
            string resultString = commandArgs[index];
            for (int i = index + 1; i < commandArgs.Length; i++)
            {
                resultString += " " + commandArgs[i];
            }

            return resultString;
        }

        private static void AppendStringToText(BigList<char> text, string newText)
        {
            char[] newTextAsChars = newText.ToCharArray();
            text.AddRange(newTextAsChars);
            Console.WriteLine("OK");
        }

        private static void InsertStringInText(BigList<char> text, int position, string newString)
        {
            ValidateIndex(text, position);
            char[] newStringAsChars = newString.ToCharArray();
            text.InsertRange(position, newStringAsChars);
            Console.WriteLine("OK");
        }

        private static void DeleteSubstringFromText(BigList<char> text, int startIndex, int count)
        {
            ValidateIndex(text, startIndex);
            ValidateCount(text, count);
            text.RemoveRange(startIndex, count);
            Console.WriteLine("OK");
        }

        private static void ReplaceSubstringInText(BigList<char> text, int startIndex, int count, string newString )
        {
            ValidateIndex(text, startIndex);
            ValidateCount(text, count);
            char[] newStringAsChar = newString.ToCharArray();
            text.RemoveRange(startIndex, count);
            text.InsertRange(startIndex, newStringAsChar);
            Console.WriteLine("OK");
        }

        private static void PrintText(BigList<char> text)
        {
            foreach (var singleChar in text)
            {
                Console.Write(singleChar);
            }

            Console.WriteLine();
        }

        private static void ValidateIndex(BigList<char> text, int index)
        {
            if (index < 0 || index > text.Count - 1)
            {
                throw new IndexOutOfRangeException(
                    string.Format("Invalid position: {0}", index));
            }
        }

        private static void ValidateCount(BigList<char> text, int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("The count of the symbols can not be negative.");
            }
        }
    }
}

namespace FastSearchStringsInText
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    class FastSearchStringsInText
    {
        static void Main(string[] args)
        {
            var wantedStrings = ReadWantedStrings();
            var readText = ReadText();
            FindStringsCount(readText, wantedStrings);

            Console.WriteLine("\nResults:");
            foreach (var singleString in wantedStrings)
            {
                Console.WriteLine("{0} - {1}", singleString.Key, singleString.Value);
            }    
        }

        private static void FindStringsCount(BigList<char> readText, Dictionary<string, int> wantedStrings)
        {
            var keys = wantedStrings.Keys.ToList();
            foreach (var key in keys)
            {
                string wantedString = key.ToLower();

                var cloneList = readText.Clone();
                var index = cloneList.IndexOf(wantedString[0]);
                var stringLength = wantedString.Length;

                while (index != -1)
                {
                    var rangeToString = string.Join("", cloneList.GetRange(index, stringLength));

                    if (wantedString == rangeToString)
                    {
                        wantedStrings[key] += 1;
                    }

                    cloneList.RemoveAt(index);
                    index = cloneList.IndexOf(wantedString[0]);
                }
            }
        }

        private static Dictionary<string, int> ReadWantedStrings()
        {
            var wantedStrings = new Dictionary<string, int>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var newString = Console.ReadLine();
                wantedStrings.Add(newString, 0);
            }
            
            return wantedStrings;
        }

        private static BigList<char> ReadText()
        {
            var text = new BigList<char>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                char[] newLine = Console.ReadLine().ToLower().ToCharArray();
                text.AddRange(newLine);
            }

            return text;
        }
    }
}

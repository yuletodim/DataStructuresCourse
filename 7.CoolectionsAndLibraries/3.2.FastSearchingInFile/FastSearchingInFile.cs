namespace FastSearchingInFile
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Wintellect.PowerCollections;

    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var wantedStrings = ReadWantedStrings();
            stopWatch.Stop();
            Console.WriteLine("Time to read 1000 strings from file: {0}", stopWatch.Elapsed);

            stopWatch.Reset();
            stopWatch.Start();
            var readText = ReadText();
            stopWatch.Stop();
            Console.WriteLine("Time to read large file: {0} secs.", stopWatch.Elapsed);

            stopWatch.Reset();
            stopWatch.Start();
            FindStringsCount(readText, wantedStrings);
            stopWatch.Stop();
            Console.WriteLine("Time to find the strings in the file: {0}", stopWatch.Elapsed);

            stopWatch.Reset();
            stopWatch.Start();
            using (StreamWriter streamWriter = new StreamWriter(@"Result.txt"))
            {
                foreach (var singleString in wantedStrings)
                {
                    streamWriter.WriteLine(
                        string.Format("{0} - {1}", singleString.Key, singleString.Value));
                }
            }
            stopWatch.Stop();
            Console.WriteLine("Time to write results in file: {0}", stopWatch.Elapsed);
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

        private static BigList<char> ReadText()
        {
            var text = new BigList<char>();
            using (StreamReader streamReader = new StreamReader(@"100.txt"))
            {
                string line = streamReader.ReadLine().Trim();
                while(line != null)
                {
                    char[] lineAsChars = line.ToLower().ToCharArray();
                    text.AddRange(line);
                    line = streamReader.ReadLine();
                }
            }

            return text;
        }

        private static Dictionary<string, int> ReadWantedStrings()
        {
            var wantedStrings = new Dictionary<string, int>();
            using (StreamReader streamReader = new StreamReader(@"100.txt"))
            {
                var count = 0;
                while (count < 1000)
                {
                    string[] inputs = streamReader.ReadLine().Trim()
                        .Split(new char[] { ' ', ',', '.', '-', ';', '!', '?', ':'}, 
                            StringSplitOptions.RemoveEmptyEntries);
                    foreach (var input in inputs)
                    {
                        if (!wantedStrings.ContainsKey(input))
                        {
                            wantedStrings.Add(input, 0);
                            count++;
                        }
                    }
                }
            }

            return wantedStrings;
        }
    }
}

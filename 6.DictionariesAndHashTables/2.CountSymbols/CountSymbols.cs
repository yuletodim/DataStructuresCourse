namespace CountSymbols
{
    using System;
    using ImplementDictionary;

    class Program
    {
        static void Main(string[] args)
        {
            var sortedCharArray = ReadInput();
            var symbolsInDictionary = CountSymbols(sortedCharArray);
            PrintDictionary(symbolsInDictionary);
        }

        private static char[] ReadInput()
        {
            char[] symbols = Console.ReadLine().ToCharArray();
            Array.Sort(symbols);

            return symbols;
        }

        private static CustomDictionary<char, int> CountSymbols(char[] symbols)
        {
            var symbolsInDictionary = new CustomDictionary<char, int>();

            foreach (var symbol in symbols)
            {
                if (!symbolsInDictionary.ContainsKey(symbol))
                {
                    symbolsInDictionary[symbol] = 1;
                }
                else
                {
                    symbolsInDictionary[symbol]++;
                }
            }

            return symbolsInDictionary;
        }

        private static void PrintDictionary(CustomDictionary<char, int> symbols)
        {
            foreach (var symbol in symbols)
            {
                Console.WriteLine("{0} -> {1}", symbol.Key, symbol.Value);
            }
        }
    }
}

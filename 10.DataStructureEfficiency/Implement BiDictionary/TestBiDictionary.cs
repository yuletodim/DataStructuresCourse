namespace ImplementBiDictionary
{
    using System;
    class TestBiDictionary
    {
        static void Main(string[] args)
        {
            var distances = new BiDictionary<string, string, int>();
            distances.Add("Sofia", "Varna", 443);
            distances.Add("Sofia", "Varna", 468);
            distances.Add("Sofia", "Varna", 490);
            distances.Add("Sofia", "Plovdiv", 145);
            distances.Add("Sofia", "Bourgas", 383);
            distances.Add("Plovdiv", "Bourgas", 253);
            distances.Add("Plovdiv", "Bourgas", 292);

            Console.WriteLine("Data collected by 1st key: {0}", distances.CountKey1);
            Console.WriteLine("Data collected by 2nd key: {0}", distances.CountKey2);
            Console.WriteLine("Data collected by both keys: {0}", distances.CountKey12);

            var distancesFromSofia = distances.FindByKey1("Sofia");
            Console.WriteLine("Distances from Sofia: {0}", string.Join(", ", distancesFromSofia));

            var distancesToBourgas = distances.FindByKey2("Bourgas");
            Console.WriteLine("Distances to Burgas: {0}", string.Join(", ", distancesToBourgas));

            var distancesPlovdivBourgas = distances.Find("Plovdiv", "Bourgas");
            Console.WriteLine("Distances Plovdiv - Burgas: {0}", string.Join(", ", distancesPlovdivBourgas));

            var distancesRousseVarna = distances.Find("Rousse", "Varna");
            Console.WriteLine("Distancesfrom Ruse - Varna: {0}", string.Join(", ", distancesRousseVarna));

            var distancesSofiaVarna = distances.Find("Sofia", "Varna");
            Console.WriteLine("Distances Sofia - Varna: {0}", string.Join(", ", distancesSofiaVarna));

            bool removingSofiaVarna = distances.Remove("Sofia", "Varna");
            Console.WriteLine("\nRemoving Sofia - Varna: {0}", removingSofiaVarna);

            var distancesFromSofiaAgain = distances.FindByKey1("Sofia");
            Console.WriteLine("Distances from Sofia: {0}", string.Join(", ", distancesFromSofiaAgain));

            var distancesToVarna = distances.FindByKey2("Varna");
            Console.WriteLine("Distances to Varna: {0}", string.Join(", ", distancesToVarna));

            var distancesSofiaVarnaAgain = distances.Find("Sofia", "Varna");
            Console.WriteLine("Distances Sofia - Varna: {0}", string.Join(", ", distancesSofiaVarnaAgain));
        }
    }
}

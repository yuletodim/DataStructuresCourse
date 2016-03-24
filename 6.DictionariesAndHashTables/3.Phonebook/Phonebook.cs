namespace Phonebook
{
    using System;
    using System.Collections.Generic;
    using ImplementDictionary;

    class Phonebook
    {
        static void Main(string[] args)
        {
            var phonebook = ReadInput();
            var searchedContacts = CollectSearchedContacts();
            PerformSearch(phonebook, searchedContacts);
        }

        private static IEnumerable<string> CollectSearchedContacts()
        {
            var searchedContacts = new List<string>();
            var searchedContact = Console.ReadLine();
            do
            {
                searchedContacts.Add(searchedContact);
                searchedContact = Console.ReadLine();
            }
            while (searchedContact != string.Empty);

            return searchedContacts;
        }

        private static void PerformSearch(
            CustomDictionary<string, string> phonebook,
            IEnumerable<string> searchedContacts)
        {
            foreach (var contact in searchedContacts)
            {
                try
                {
                    var phoneNumber = phonebook.Get(contact);
                    Console.WriteLine("{0} -> {1}", contact, phoneNumber);
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine(
                        string.Format("Contact {0} does not exist.", contact));
                }
            }         
        }

        private static CustomDictionary<string, string> ReadInput()
        {
            var phonebook = new CustomDictionary<string, string>();
            var line = Console.ReadLine();
            while (line != "search")
            {
                string[] newContact = line.Split('-');
                if (newContact.Length == 2)
                {
                    phonebook.Add(newContact[0], newContact[1]);
                }

                line = Console.ReadLine();
            }

            return phonebook;
        }
    }
}

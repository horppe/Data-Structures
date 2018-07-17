using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sorting_a_Phonebook
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader("file.txt");
            SortedDictionary<string, SortedDictionary<string, string>> cities = new SortedDictionary<string, SortedDictionary<string, string>>();
            using (reader)
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    string[] words = line.Split('|');
                    string name = words[0].Trim();
                    string city = words[1].Trim();
                    string phone = words[2].Trim();
                    SortedDictionary<string, string> nameAndNo;
                    if (!cities.TryGetValue(city, out nameAndNo))
                    {
                        nameAndNo = new SortedDictionary<string, string>();
                        cities.Add(city, nameAndNo);
                    }
                    nameAndNo.Add(name, phone);
                }

                foreach (var city in cities)
                {
                    Console.WriteLine("In Town {0}:", city.Key);
                    foreach (var phoneBook in city.Value)
                    {
                        Console.WriteLine("\t{0}, {1}", phoneBook.Key, phoneBook.Value);
                    }
                    Console.WriteLine();
                }
            }

            Console.ReadKey();

        }
    }
}

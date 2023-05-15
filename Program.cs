using System;
using System.Collections.Generic;

namespace CovidModel
{
    class City
    {
        public string Name { get; set; }
        public int OutbreakLevel { get; set; }
        public List<int> Contacts { get; set; }

        public City(string name)
        {
            Name = name;
            OutbreakLevel = 0;
            Contacts = new List<int>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of cities: ");
            int numCities = int.Parse(Console.ReadLine());

            List<City> cities = new List<City>();
            for (int i = 0; i < numCities; i++)
            {
                Console.Write($"Enter the name of city {i}: ");
                string cityName = Console.ReadLine();
                cities.Add(new City(cityName));

                Console.Write($"Enter the number of cities that {cityName} contacts: ");
                int numContacts = int.Parse(Console.ReadLine());
                for (int j = 0; j < numContacts; j++)
                {
                    int contactId;
                    do
                    {
                        Console.Write($"Enter the ID of the city that {cityName} contacts ({j + 1}/{numContacts}): ");
                        contactId = int.Parse(Console.ReadLine());
                        if (contactId < 0 || contactId >= numCities || contactId == i)
                        {
                            Console.WriteLine("Invalid ID, please enter again.");
                        }
                    } while (contactId < 0 || contactId >= numCities || contactId == i);

                    cities[i].Contacts.Add(contactId);
                }
            }

            while (true)
            {
                Console.Write("Enter an event (Outbreak, Vaccinate, Lockdown, Spread, Exit): ");
                string eventName = Console.ReadLine();
                if (eventName == "Outbreak" || eventName == "Vaccinate" || eventName == "Lockdown")
                {
                    Console.Write("Enter the ID of the city where the event occurred: ");
                    int cityId = int.Parse(Console.ReadLine());

                    if (eventName == "Outbreak")
                    {
                        cities[cityId].OutbreakLevel = Math.Min(3, cities[cityId].OutbreakLevel + 2);
                        foreach (int contactId in cities[cityId].Contacts)
                        {
                            cities[contactId].OutbreakLevel = Math.Min(3, cities[contactId].OutbreakLevel + 1);
                        }
                    }
                    else if (eventName == "Vaccinate")
                    {
                        cities[cityId].OutbreakLevel = 0;
                    }
                    else // eventName == "Lockdown"
                    {
                        cities[cityId].OutbreakLevel = Math.Max(0, cities[cityId].OutbreakLevel - 1);
                        foreach (int contactId in cities[cityId].Contacts)
                        {
                            cities[contactId].OutbreakLevel = Math.Max(0, cities[contactId].OutbreakLevel - 1);
                        }
                    }
                }
                else if (eventName == "Spread")
                {
                    foreach (City city in cities)
                    {
                        bool hasHigherOutbreakContact = false;
                        foreach (int contactId in city.Contacts)
                        {
                            if (cities[contactId].OutbreakLevel > city.OutbreakLevel)
                            {
                                hasHigherOutbreakContact = true;
                                break;
                            }
                        }
                        if (hasHigherOutbreakContact)
                        {
                            city.OutbreakLevel = Math.Min(3, city.OutbreakLevel + 1);
                        }
                    }
                }
                else if (eventName == "Exit"); 
            }
        }
    }
}
                
                   

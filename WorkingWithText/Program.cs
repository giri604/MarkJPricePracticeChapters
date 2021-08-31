using System;
using static System.Console;

namespace WorkingWithText
{
    class Program
    {
        static void Main(string[] args)
        {
            string city = "London";
            WriteLine($"{city} is {city.Length} characters long.");
            WriteLine($"the first charater is {city[0]} and third is {city[2]}.");

            string cities = "Paris,Berlin,Madrid,New York";
            string[] citiesArray = cities.Split(',');

            foreach (string item in citiesArray)
            {
                WriteLine(item);
            }

            //string fullName = "Giri, Deepak";
            //int indexOfTheSpace = fullName.IndexOf(' ');

            //string firstName = fullName.Substring(startIndex: indexOfTheSpace + 1);
            //string lastName = fullName.Substring(startIndex: 0, length: indexOfTheSpace - 1);

            //WriteLine($"{firstName} {lastName}");

            string recombined = string.Join(" => ", citiesArray);
            WriteLine(recombined);

            string fruit = "Apples";
            decimal price = 0.39M;
            DateTime when = DateTime.Today;

            WriteLine($"{fruit} cost {price:C} on {when:dddd}.");
            WriteLine(string.Format("{0} cost {1:C} on {2:dddd}.",
                      fruit, price, when));
        }
    }
}

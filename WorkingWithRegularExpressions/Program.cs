using System;
using System.Text.RegularExpressions;
using static System.Console;

namespace WorkingWithRegularExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            //Write("Enter your age: ");
            //string input = ReadLine();

            //var ageChecker = new Regex(@"^\d$");

            //if(ageChecker.IsMatch(input))
            //{
            //    WriteLine("Thank you!");
            //}
            //else
            //{
            //    WriteLine($"This is not a valid age: {input}");
            //}

            //Complex split by Regex
            string films = "\"Monsters, Inc.\",\"I, Tonya\",\"Lock, Stock and Two Smoking Barrels\"";
            string[] fileDumb = films.Split(',');
            WriteLine("Dumb Attempt at splitting:");
            foreach (string film in fileDumb)
            {
                WriteLine(film);
            }
            var csv = new Regex("(?:^|,)(?=[^\"]|(\")?)\"?((?(1)[^\"]*|[^,\"]*))\"?(?=,|$)");
            MatchCollection filmSmart = csv.Matches(films);
            WriteLine("Smart Attempt at aplitting:");
            foreach(Match film in filmSmart)
            {
                WriteLine(film.Groups[2].Value);
            }


        }
    }
}

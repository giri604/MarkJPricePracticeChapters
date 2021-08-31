using System;
using System.Globalization;
using static System.Console;

namespace Internationalization
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo globalization = CultureInfo.CurrentCulture;
            CultureInfo localization = CultureInfo.CurrentCulture;
            WriteLine("The current Globalization culture is {0}; {1}",
                arg0: globalization.Name,
                arg1: globalization.DisplayName);

            WriteLine("The current Locatiozation culture is {0}, {1}",
                arg0: localization.Name,
                arg1: localization.DisplayName);
            WriteLine();

            WriteLine("en-US: English (United States)");
            WriteLine("da-DK: Danish(Denmark)");
            WriteLine("fr-CA: french(Canada)");
            Write("Enter an ISO culture code: ");
            string newCulture = ReadLine();
            if (!string.IsNullOrEmpty(newCulture))
            {
                var ci = new CultureInfo(newCulture);
                CultureInfo.CurrentCulture = ci;
                CultureInfo.CurrentUICulture = ci;
            }
            WriteLine();

            Write("Enter your name: ");
            string name = ReadLine();
            Write("Enter your Date of birth: ");
            string dob = ReadLine();
            Write("Enter your Salary: ");
            string salary = ReadLine();

            DateTime date = DateTime.Parse(dob);
            int minutes = (int)DateTime.Today.Subtract(date).TotalMinutes;
            decimal earns = decimal.Parse(salary);

            WriteLine("{0} was born on a {1:dddd}, is {2:N0} minutes old, and earns {3:C}",
                    name, date, minutes, earns);

        }
    }
}

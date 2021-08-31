using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Packt.Shared
{
    public class Person : IComparable<Person>
    {
        public string Name;
        public DateTime DateOfBirth;
        public List<Person> Children = new List<Person>();

        //methods
        public void WriteToConsole()
        {
            Console.WriteLine($"{Name} was born on a {DateOfBirth:dddd}.");
        }

        public static Person Procreate(Person p1, Person p2)
        {
            var baby = new Person
            { Name = $"Baby of {p1.Name} and {p2.Name}" };

            p1.Children.Add(baby);
            p2.Children.Add(baby);

            return baby;
        }

        public Person ProcreateWith(Person partner)
        {
            return Procreate(this, partner);
        }

        public static Person operator *(Person p1, Person p2)
        {
            return Person.Procreate(p1, p2);
        }

        //method with a local function
        public static int Factorial(int number)
        {
            if(number < 0)
            {
                throw new ArgumentException($"{nameof(number)} cannot be less than zero.");
            }
            return localFactorial(number);

            int localFactorial(int LocalNumber)
            {
                if(LocalNumber < 1)
                {
                    return 1;
                }
                return LocalNumber * localFactorial(LocalNumber - 1);
            }
        }

        //event delegate field
        public EventHandler Shout;

        //Data Field
        public int AngerLevel;

        //method
        public void poke()
        {
            AngerLevel++;
            if(AngerLevel >= 3)
            {
                //if something is listening
                if(Shout != null)
                {
                    //..then call the delegate
                    Shout(this, EventArgs.Empty);
                }
            }
        }

        public int CompareTo([AllowNull] Person other)
        {
            return Name.CompareTo(other.Name);
        }

        public void TimeTravel(DateTime when)
        {
            if(when <= DateOfBirth)
            {
                throw new PersonException("If you back in time to a date earlier than your Birth, then the universe will explode!");
            }
            else
            {
                Console.WriteLine($"Welcome to {when:yyyy}");
            }
        }
    }
}

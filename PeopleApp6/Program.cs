using System;
using Packt.Shared;

namespace PeopleAppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var harry = new Person { Name = "Harry" };
            var mary = new Person { Name = "Mary" };
            var jil = new Person { Name = "Jil" };

            //call instance method
            var baby1 = mary.ProcreateWith(harry);

            //call static method
            var baby2 = Person.Procreate(harry, jil);

            //call an operator
            var baby3 = harry * mary;

            Console.WriteLine($"{harry.Name} has {harry.Children.Count} children.");
            Console.WriteLine($"{mary.Name} has {mary.Children.Count} children.");
            Console.WriteLine($"{jil.Name} has {jil.Children.Count} children.");

            Console.WriteLine(format: "{0}'s first child is named \"{1}\".",
                arg0: harry.Name,
                arg1: harry.Children[0].Name);

            Console.WriteLine($"5! is {Person.Factorial(5)}");

            harry.Shout = Harry_Shout;

            harry.poke();
            harry.poke();
            harry.poke();
            harry.poke();


            Person[] people =
            {
            new Person { Name = "Simon"},
            new Person { Name = "Jenny"},
            new Person { Name = "Adam"},
            new Person { Name = "Richard"}
            };

            Console.WriteLine("Initial List of People");

            foreach(var person in people)
            {
                Console.WriteLine($"{person.Name}");
            }

            Console.WriteLine("Use Person's IComparable implementation to sort:");
            Array.Sort(people);
            foreach (var person in people)
            {
                Console.WriteLine($"{person.Name}");
            }

            Console.WriteLine("Use PersonComparer's Icomparer implementation to sort:");
            Array.Sort(people, new PersonComparer());
            foreach (var person in people)
            {
                Console.WriteLine($"{person.Name}");
            }
            //DvdPlayer player = new DvdPlayer();
            //player.Play();
            //player.Pause();

            Thing thing = new Thing();
            thing.Data = 42;
            Console.WriteLine($"Thing with an integer: {thing.Process(42)}");

            Thing thing1 = new Thing();
            thing1.Data = "Apple";
            Console.WriteLine($"Thing with a string: {thing1.Process("Apple")}");

            var gt1 = new GenericThing<int>();
            gt1.Data = 42;
            Console.WriteLine($"GenericThing with an integer: {gt1.Process(42)}");

            var gt2 = new GenericThing<string>();
            gt2.Data = "Apple";
            Console.WriteLine($"Generic with a string: {gt2.Process("Apple")}");

            string number1 = "4";
            Console.WriteLine($"Square is: {Squarer.Square<string>(number1)}");

            Console.WriteLine($"Square is: {Squarer.Square(3)}");


            var dv1 = new DisplacementVector(3, 5);
            var dv2 = new DisplacementVector(-2, 7);
            var dv3 = dv1 + dv2;
            Console.WriteLine($"({dv1.X}, {dv1.Y}) + ({dv2.X}, {dv2.Y}) = ({ dv3.X}, { dv3.Y})");

            //Inheriting, Extending, Hiding, Overriding, Preventing Inheritence and Overriding of Class

            Employee john = new Employee
            {
                Name = "John Jones",
                DateOfBirth = new DateTime(1990, 7, 28)
            };

            john.WriteToConsole();
            john.EmployeeCode = "JJ001";
            john.HireDate = new DateTime(2014, 11, 23);
            Console.WriteLine($"{john.Name} was hired on { john.HireDate:dd/MM/yy}");

            Employee aliceInEmployee = new Employee
            { Name = "Alice", EmployeeCode = "AA123" };
            Person aliceInPerson = aliceInEmployee;
            aliceInEmployee.WriteToConsole();
            aliceInPerson.WriteToConsole();
            Console.WriteLine(aliceInEmployee.ToString());
            Console.WriteLine(aliceInPerson.ToString());

            //Explicit casting, Avoiding casting exceptions
            if(aliceInPerson is Employee)
            {
                Console.WriteLine($"{nameof(aliceInPerson)} IS an Employee");
                Employee expilictAlice = (Employee)aliceInPerson;
            }
            Employee aliceAsEmployee = aliceInPerson as Employee;
            if (aliceAsEmployee != null)
            {
                Console.WriteLine($"{nameof(aliceInPerson)} AS an Emplyoyee");
            }

            //Inheriting and extending .NET types
            Person el = new Person();
            try
            {
                el.TimeTravel(new DateTime(1992, 12, 31));
                el.TimeTravel(new DateTime(1950, 12, 25));
            }
            catch (PersonException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Using static methods to reuse functionality
            string email1 = "pamela@test.com";
            string email2 = "ian&test.com";

            Console.WriteLine("{0} is a Valid e-mail address: {1}",
                arg0: email1,
                arg1: email1.IsValidEmail());
            Console.WriteLine("{0} is a Valid e-mail address: {1}",
               arg0: email2,
               arg1: email2.IsValidEmail());
        }

        private static void Harry_Shout(object sender, EventArgs e)
        {
            Person p = (Person)sender;
            Console.WriteLine($"{p.Name} is this angry : {p.AngerLevel}.");
        }

        
    }
}

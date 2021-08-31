using System;
using Packt.Shared;

namespace PeopleAppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var bob = new Person();
            bob.Name = "Bob Smith";
            bob.DateOfBirth = new DateTime(1965, 12, 22);
            var alice = new Person();
            alice.Name = "Alice Jones";
            alice.DateOfBirth = new DateTime(1998, 3, 7);
            Console.WriteLine(format: "{0} was born on {1:dddd, d MMMM yyyy}",
                                arg0: bob.Name,
                                arg1: bob.DateOfBirth);
            Console.WriteLine(format: "{0} was born on {1:dd MM yy}",
                                        arg0: alice.Name,
                                        arg1: alice.DateOfBirth);

            bob.FavoriteAncientWonder = WondersOfTheAncientWorld.StatueOfZeusAtOlympia;
            Console.WriteLine(format: "{0}'s favourite wonder of world is {1}. It's integer value is {2}.",
                                arg0: bob.Name,
                                arg1: bob.FavoriteAncientWonder,
                                arg2: (int)bob.FavoriteAncientWonder);

            bob.BucketList = WondersOfTheAncientWorld.HangingGardensOfBabylon | WondersOfTheAncientWorld.MausoleumAtHalicarnassus;

            //bob.BucketList = (WondersOfTheAncientWorld)18;
            Console.WriteLine($"{bob.Name}'s bucket list is {bob.BucketList}");

            bob.Children.Add(new Person { Name = "Alfred" });
            bob.Children.Add(new Person { Name = "Zoe" });
            Console.WriteLine($"{bob.Name} has {bob.Children.Count} children:");
            for (int child = 0; child < bob.Children.Count; child++)
            {
                Console.WriteLine($"    {bob.Children[child].Name}");
            }

            BankAccount.InterestRate = 0.012M;

            var jonseAccount = new BankAccount();
            jonseAccount.AccountName = "Mrs. Jones";
            jonseAccount.Balance = 2400;
            Console.WriteLine(format: "{0} earned {1:C} interest.",
                               arg0: jonseAccount.AccountName,
                               arg1: jonseAccount.Balance * BankAccount.InterestRate);

            var gerrierAccount = new BankAccount();
            gerrierAccount.AccountName = "Ms. Gerrier";
            gerrierAccount.Balance = 98;
            Console.WriteLine(format: "{0} earned {1:C} interest.",
                              arg0: gerrierAccount.AccountName,
                              arg1: gerrierAccount.Balance * BankAccount.InterestRate);

            (string, int) fruit = bob.GetFruit();
            Console.WriteLine($"{fruit.Item1}, {fruit.Item2} There are.");

            var fruiteNamed = bob.GetNamedFruit();
            Console.WriteLine($"There are {fruiteNamed.Number} {fruiteNamed.Name}.");

            var sam = new Person
            {
                Name = "Sam",
                DateOfBirth = new DateTime(1972, 1, 27)
            };

            Console.WriteLine(sam.Origin);
            Console.WriteLine(sam.Greeting);
            Console.WriteLine(sam.Age);

            sam.FavoriteIceCream = "Chocolate Fudge";
            Console.WriteLine($"Sam's Favourite ice-cream flavour is {sam.FavoriteIceCream}.");

            sam.FavouritePrimaryColor = "Red";
            Console.WriteLine($"Sam's Favourite color is {sam.FavouritePrimaryColor}.");

            sam.Children.Add(new Person { Name = "Charlie" });
            sam.Children.Add(new Person { Name = "Ella" });
            Console.WriteLine($"Sam's first child is {sam.Children[0].Name}");
            Console.WriteLine($"Sam's second child is {sam.Children[1].Name}");
            Console.WriteLine($"Sam's first child is {sam[0].Name}");
            Console.WriteLine($"Sam's second child is {sam[1].Name}");


        }
    }
}

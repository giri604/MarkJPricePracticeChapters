using System;
using Packt.Shared;
using static System.Console;

namespace HashingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Registering Alice with Pa$$w0rd");
            var alice = Protector.Register("Alice", "Pa$$w0rd");
            WriteLine($"Name: {alice.Name}");
            WriteLine($"Salt: {alice.Salt}");
            WriteLine("Password (salted and hashed): {0}",
                      arg0: alice.SaltedHashedPassword);
            WriteLine();

            Write("Enter a new user to register: ");
            string username = ReadLine();
            Write($"Enter a password for {username}: ");
            string password = ReadLine();
            var user = Protector.Register(username, password);
            WriteLine($"Name : {user.Name}");
            WriteLine($"Password: {user.Salt}");
            WriteLine("Password (salted and hashed): {0}",
                       arg0: user.SaltedHashedPassword);
            WriteLine();

            bool correctPassword = false;
            while (!correctPassword)
            {
                Write("Enter a username to log in: ");
                string loginUsername = ReadLine();
                Write("Enter a password to log in: ");
                string loginPassword = ReadLine();

                correctPassword = Protector.CheckPassword(username: loginUsername, password: loginPassword);

                if (correctPassword)
                {
                    WriteLine($"Correct! {loginUsername} has been logged");
                }
                else
                {
                    WriteLine("Invalid username or password. Try again");
                }
            }

            Write("Enter some text to sign: ");
            string data = ReadLine();
            var signature = Protector.GenerateSignature(data);
            WriteLine($"Signature: {signature}");
            WriteLine("Public key used to check signature:");
            WriteLine(Protector.PublicKey);
            if (Protector.ValidateSignature(data, signature))
            {
                WriteLine("Correct! Signature is valid.");
            }
            else
            {
                WriteLine("Invalid signature.");
            }
            // simulate a fake signature by replacing the
            // first character with an X
            var fakeSignature = signature.Replace(signature[0], 'X');
            if (Protector.ValidateSignature(data, fakeSignature))
            {
                WriteLine("Correct! Signature is valid.");
            }
            else
            {
                WriteLine($"Invalid signature: {fakeSignature}");
            }
        }
    }
}

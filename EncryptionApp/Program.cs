using System;
using System.Security.Cryptography;
using Packt.Shared;
using static System.Console;

namespace EncryptionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Write(value: "Enter  a message that you want to encrypt: ");
            string message = ReadLine();

            Write(value: "Enter a password: ");
            string password = ReadLine();

            string cryptoText = Protector.Encrypt(message, password);

            WriteLine($"Encrypted Text: {cryptoText}");
            Write("Enter your password: ");
            string password2 = ReadLine();

            try
            {
                string clearText = Protector.Decrypt(cryptoText, password2);
                WriteLine($"Decrypted text: {clearText}");
            }
            catch (CryptographicException ex)
            {
                WriteLine("{0}\nMore Details: {1}", arg0: ex.GetType().Name, arg1: ex.Message);
            }
        }
    }
}

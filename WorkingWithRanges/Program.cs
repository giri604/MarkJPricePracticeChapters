using System;
using static System.Console;

namespace WorkingWithRanges
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "Samantha Jones";
            int indexofSpace = name.IndexOf(' ');

            string firstName = name.Substring(startIndex: 0,length: indexofSpace);
            string lastName = name.Substring(startIndex: name.Length - (indexofSpace - 1));
            WriteLine($"First name: {firstName}, Last name: {lastName}");
            ReadOnlySpan<char> nameAsSpan = name.AsSpan();
            
            //var firstNameSpan = nameAsSpan[0..lengthOfFirst];

        }
    }
}

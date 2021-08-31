using System;
using System.Reflection;
using System.Linq;
using static System.Console;

namespace WorkingWithReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Assempbiles metadata");
            var assembly = Assembly.GetEntryAssembly();
            WriteLine($"    Full name: {assembly.FullName}");
            WriteLine($"    Location: {assembly.Location}");

            var attribute = assembly.GetCustomAttributes();
            WriteLine($"    Attributes: ");
            foreach(Attribute a in attribute)
            {
                WriteLine($"    {a.GetType()}");  
            }

            var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            WriteLine($"    Version: {version.InformationalVersion}");

            var company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
            WriteLine($"    Company: {company.Company}");

            WriteLine();
            WriteLine($"* Types:");
            Type[] types = assembly.GetTypes();

            foreach(Type type in types)
            {
                WriteLine();
                WriteLine($"Type: {type.FullName}");
                MemberInfo[] members = type.GetMembers();
                foreach(MemberInfo member in members)
                {
                    WriteLine("{0}, {1} ({2})",
                        arg0: member.MemberType,
                        arg1: member.Name,
                        arg2: member.DeclaringType.Name);

                    var coders = member.GetCustomAttributes<CoderAttribute>().OrderByDescending(c => c.LastModified);
                    foreach(CoderAttribute coder in coders)
                    {
                        WriteLine("--> Modifier by {0} on {1}",
                            arg0: coder.Coder,
                            arg1: coder.LastModified);
                    }
                }
            }
        }

        [Coder("Mark Price","30 August 2021")]
        [Coder("Deepak Giri","31 August 2021")]
        public static void DoStuff()
        {

        }
    }
}

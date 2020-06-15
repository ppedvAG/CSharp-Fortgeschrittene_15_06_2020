using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HalloReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\rulan\source\repos\ppedvAG\CSharp-Fortgeschrittene_15_06_2020\HalloGenerics\HalloGenerics\bin\Debug\HalloGenerics.exe";

            var ass = Assembly.LoadFrom(path);

            foreach (Type item in ass.GetTypes())
            {
                Console.WriteLine($"{item.FullName}");
                foreach (var member in item.GetMembers())
                {
                    Console.WriteLine($"\t{member.Name}");
                }
            }

            Type dings = ass.GetType("HalloGenerics.Program");
            object prg = Activator.CreateInstance(dings);
            MethodInfo mi = dings.GetMethod("ArrayListBeispiel");
            mi.Invoke(prg,null);


            Console.WriteLine("Ende");
            Console.ReadKey();
        }
    }
}

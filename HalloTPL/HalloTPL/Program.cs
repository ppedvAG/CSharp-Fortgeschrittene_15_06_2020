using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HalloTPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Hallo TPL ***");

            Parallel.For(0, 100000, i => Console.WriteLine(i));
            //Parallel.Invoke(Zähle, Zähle, Zähle, () => { ZeigeText("lala"); }, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle);
            var texte = new List<string>();
            foreach (var item in texte.Where(x => x.StartsWith("b")).AsParallel())
            { }


            Console.WriteLine("Ende");
            Console.ReadKey();
        }

        private static void Zähle()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}");
            }
        }

        static void ZeigeText(string txt)
        {
            Console.WriteLine(txt);
        }
    }
}

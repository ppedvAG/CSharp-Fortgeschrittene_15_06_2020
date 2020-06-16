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

            //Parallel.For(0, 100000, i => Console.WriteLine(i));
            //Parallel.Invoke(Zähle, Zähle, Zähle, () => { ZeigeText("lala"); }, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle);
            //var texte = new List<string>();
            //foreach (var item in texte.Where(x => x.StartsWith("b")).AsParallel())
            //{ }


            var t1 = new Task(() =>
            {
                Console.WriteLine("T1 gestartet");
                Thread.Sleep(1800);
                Console.WriteLine("T1 fertig");
                throw new FieldAccessException();
                //Zähle();
            });


            var t2 = new Task<long>(() =>
            {
                Console.WriteLine("T2 gestartet");
                Thread.Sleep(1200);
                Console.WriteLine("T2 fertig");
                //Zähle();
                while (true)
                {

                }
                return 34567834567;
            });


            var t1c = t1.ContinueWith(t => Console.WriteLine("T1 Continue"), TaskContinuationOptions.None);
            t1.ContinueWith(t => Console.WriteLine("T1 OK"), TaskContinuationOptions.OnlyOnRanToCompletion);
            t1.ContinueWith(t => Console.WriteLine($"T1 ERROR {t.Exception.InnerException.Message}"), TaskContinuationOptions.OnlyOnFaulted);

            t1.Start();
            t2.Start();
          

            if (t2.Wait(500))
                Console.WriteLine($"Result of T2: {t2.Result}");
            else
                Console.WriteLine("T2 nach 5 Sekunden nicht fertig");




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

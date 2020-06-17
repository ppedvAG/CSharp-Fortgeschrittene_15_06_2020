using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sprachfeatures
{
    class Program
    {
        private int myProperty;

        async static Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            #region tupel
            string txt = "sieben";
            string txt2 = "7";

            var tuple = new Tuple<int, string, decimal, bool>(100_000_000, "lala", 12.44m, true);

            var wert = GetZeug();
            (int value, string unit, var ex) = GetZeug();
            Console.WriteLine(value);
            #endregion

            #region tryparse
            //int txtAlsZahl;
            if (int.TryParse(txt2, out int txtAlsZahl))
            {
                Console.WriteLine($"Zahl ist ok: {txtAlsZahl}");
            }
            #endregion

            #region funktionen

            void LokaleFunktion()
            {
                Console.WriteLine("Lokal");
            }

            int zahllll = txt.Length < 7 ? 18 : throw new ExecutionEngineException();

            if (txt.Length > 7)
                zahllll = 18;
            else
                zahllll = 99;

            LokaleFunktion();
            LokaleFunktion();

            int w = 12;
            //int w2 = Verdoppelt(w);
            VerdoppeltRef(ref w);


            EineFunktion(11);
            EineFunktion(11, 112);
            EineFunktion(11, 112, "lala", 1.22);
            EineFunktion(11, dd: 8.99); //neu 7.2
            #endregion


            Console.WriteLine("####################### yield");
            foreach (string item in GetTexte())
            {
                Console.WriteLine(item);
            }

            await foreach (string item in GetTexteNEW_ab8_0())
            {
                Console.WriteLine(item);
            }

#nullable enable
            StringComparer sc = null;

            sc.Compare(1, 2);
#nullable disable

            Console.WriteLine("Ende");
            Console.ReadKey();
        }
       


        static async IAsyncEnumerable<string> GetTexteNEW_ab8_0()
        {
            //ohne yield
            //var texte = new List<string>();
            //texte.Add("Hallo");
            //texte.Add("Welt");
            //texte.Add("wie");
            //texte.Add("geht's?");
            //return texte;

            await Task.Delay(500);
            yield return "Hallo";
            await Task.Delay(500);
            yield return "Welt";
            await Task.Delay(500);
            yield return "Wie";
            await Task.Delay(500);
            yield return "geht's?";
        }

        static IEnumerable<string> GetTexte()
        {
            //ohne yield
            //var texte = new List<string>();
            //texte.Add("Hallo");
            //texte.Add("Welt");
            //texte.Add("wie");
            //texte.Add("geht's?");
            //return texte;

            Thread.Sleep(500);
            yield return "Hallo";
            Thread.Sleep(500);
            yield return "Welt";
            Thread.Sleep(500);
            yield return "Wie";
            Thread.Sleep(500);
            yield return "geht's?";
        }

        static int EineFunktion(int muss, int zahl = 45, string txt = default, double dd = 99.90)
        {
            return zahl;
        }

        // static ref int Verdoppelt(int zahl)
        // {
        //     //ref int lala = zahl * 2;
        //     //return ref lala;
        // }

        public int MyProperty
        {
            get => "wekljnwekljnf".Length > 7 ? 18 : throw new ExecutionEngineException();

            set => new ExecutionEngineException();
        }


        static void VerdoppeltRef(ref int zahl) => zahl *= 2;


        static (int wert, string einheit, ExecutionEngineException) GetZeug() => (6, "km", new ExecutionEngineException());


    }

    interface IZeug
    {
        internal void Gettext(string txt)
        {
            Console.WriteLine(txt);
        }
    }
}

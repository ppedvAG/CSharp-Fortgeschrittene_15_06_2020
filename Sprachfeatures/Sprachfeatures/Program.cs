using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sprachfeatures
{
    class Program
    {
        private int myProperty;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string txt = "sieben";
            string txt2 = "7";

            var tuple = new Tuple<int, string, decimal, bool>(100_000_000, "lala", 12.44m, true);

            var wert = GetZeug();
            (int value, string unit, var ex) = GetZeug();
            Console.WriteLine(value);

            //int txtAlsZahl;
            if (int.TryParse(txt2, out int txtAlsZahl))
            {
                Console.WriteLine($"Zahl ist ok: {txtAlsZahl}");
            }

            void LokaleFunktion()
            {
                Console.WriteLine("Lokal");
            }


            int zahllll = txt.Length > 7 ? 18 : throw new ExecutionEngineException();

            if (txt.Length > 7)
                zahllll = 18;
            else
                zahllll = 99;

            LokaleFunktion();
            LokaleFunktion();

            int w = 12;
            //int w2 = Verdoppelt(w);
            VerdoppeltRef(ref w);

            Console.WriteLine("Ende");
            Console.ReadKey();
        }

        // static ref int Verdoppelt(int zahl)
        // {
        //     //ref int lala = zahl * 2;
        //     //return ref lala;
        // }

        public int MyProperty
        {
            get=> "wekljnwekljnf".Length > 7 ? 18 : throw new ExecutionEngineException();

            set => new ExecutionEngineException();
        }

        static void VerdoppeltRef(ref int zahl) => zahl *= 2;


        static (int wert, string einheit, ExecutionEngineException) GetZeug() => (6, "km", new ExecutionEngineException());


    }
}

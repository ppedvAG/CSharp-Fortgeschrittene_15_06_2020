using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HalloGenerics
{
    [Serializable]
    public class Hund
    {
        public int Beine { get; set; }
    }


    public interface IObst
    {
        ISaft GetSaft();
    }


    public class Banane : IObst
    {
        public ISaft GetSaft()
        {
            return new BananenSaft();
        }
    }

    public interface ISaft { }

    public class BananenSaft : ISaft
    { }

    class Mixer<DingRein, Raus> where DingRein : IObst
                                 where Raus : ISaft
    {
        public Raus Mixe(DingRein rein)
        {
            Console.WriteLine("Wird GEMIXT");

            return (Raus)rein.GetSaft();
        }

        //public Banane Mixen(Banane obj)
        //{ return obj; }

        //public object Mixen(object obj)
        //{
        //    Console.WriteLine($"mixe {obj}");
        //    return obj;
        //}
    }
    public class ObstDict<Key> : Dictionary<Key, IObst>
    {

    }


    public class Program
    {
        static void Main(string[] args)
        {
            ArrayListBeispiel();

            var mixer = new Mixer<Banane, BananenSaft>();
            var saft = mixer.Mixe(new Banane());

            Lala<int>(4, 7, 12);
            Lala<string>("Käse", "Wurst", "Ei");
            Lala222(5, 6, 1);

            //var mixer2 = new Mixer<int>();
            //mixer2.Mixe(8);
            var meinObst = new ObstDict<bool>();
            meinObst.Add(true, new Banane());

            Console.WriteLine("Ende");
            Console.ReadLine();
        }

        static void Lala<Ding>(Ding eins, Ding zwei, Ding drei)
        {
            Console.WriteLine($"Die Drei: {eins},{zwei},{drei}");
        }
        static void Lala222(Object eins, Object zwei, Object drei)
        {
            Console.WriteLine($"Die Drei: {eins},{zwei},{drei}");
        }

        private static void ArrayListBeispiel()
        {
            ArrayList al = new ArrayList();
            al.Add(5);
            al.Add("Hallo Welt");
            //al.Add(new SqlCommand());
            al.Add(new Hund());


            foreach (object item in al)
            {
                //casting + typprüfung
                if (item is int)
                {
                    int zahl = (int)item;
                    Console.WriteLine($"Casting: {zahl}");
                }

                //boxing
                string text = item as string;
                if (text != null)
                    Console.WriteLine($"Boxing ok: {text}");

                //pattern-matching
                if (item is Hund wuff)
                {
                    Console.WriteLine($"Pattern matching: {wuff}");
                }
            }

            //using (var sw = new FileStream("dings.txt",FileMode.Create))
            //{
            //    var serial = new DataContractSerializer(typeof(ArrayList));
            //    serial.WriteObject(sw, al);
            //}
        }
    }
}

using System;

namespace HalloDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            var hiDele = new HalloDelegate();
            hiDele.TollesEvent += HiDele_TollesEvent;
            hiDele.TollesEvent += HiDele_TollesEvent;
            hiDele.TollesEvent -= HiDele_TollesEvent;
            hiDele.TollesEvent -= HiDele_TollesEvent;
            hiDele.TollesEvent += HiDele_TollesEvent;

            hiDele.TollesEvent += (o, s, i) => Console.WriteLine("HALLO EVENT");

            hiDele.OnTollesEvent();

            Console.WriteLine("Ende");
            Console.ReadLine();
        }

        private static void HiDele_TollesEvent(object arg1, string arg2, int arg3)
        {
            Console.WriteLine("TollesEvent wurde gefeuert");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace HalloDelegate
{

    delegate void EinfacherDelegate();
    delegate void DelegateMitPara(string txt);
    delegate long CalcDelegate(int a, int b);

    class HalloDelegate
    {

        public event Action<object, string, int> TollesEvent;

        public virtual void OnTollesEvent()
        {
            Console.WriteLine("OnTollesEvent");
            if (TollesEvent != null)
                TollesEvent.Invoke(this, "Hallo", 12);
        }

        public HalloDelegate()
        {
            EinfacherDelegate meinDele = EinfacheMethode;
            Action meinDeleAlsAction = EinfacheMethode;
            Action meinDeleAlsActionAno = delegate () { Console.WriteLine("Hallo Ano"); };
            Action meinDeleAlsActionAno2 = () => { Console.WriteLine("Hallo"); };
            Action meinDeleAlsActionAno3 = () => Console.WriteLine("Hallo");

            DelegateMitPara meinDelemitPara = MethodeMitPara;
            Action<string> meinDelemitParaAlsAction = MethodeMitPara;
            DelegateMitPara meinDelemitParaAno = (string name) => { Console.WriteLine($"Hallo {name}"); };
            Action<string> meinDelemitParaAno2 = (name) => Console.WriteLine($"Hallo {name}");
            Action<string> meinDelemitParaAno3 = x => Console.WriteLine($"Hallo {x}");

            CalcDelegate calcDele = Minus;
            Func<int, int, long> calcDeleFunc = Sum;
            CalcDelegate calcDeleAno = (int x, int y) => { return x + y; };
            CalcDelegate calcDeleAno2 = (x, y) => x + y;

            List<string> texten = new List<string>();
            texten.Where(x => x.StartsWith("b"));
            texten.Where(Filter);

            long re = calcDeleFunc.Invoke(6, 78);
            long result = calcDele.Invoke(56, 777);
        }

        private bool Filter(string arg)
        {
            if (arg.StartsWith("b"))
                return true;
            else
                return false;
        }

        private long Minus(int a, int b)
        {
            return a - b;
        }

        private long Sum(int a, int b)
        {
            return a + b;
        }

        void MethodeMitPara(string msg)
        {
            Console.WriteLine($"Hallo {msg}");
        }
        void EinfacheMethode()
        {
            Console.WriteLine("Hallo");
        }
    }
}

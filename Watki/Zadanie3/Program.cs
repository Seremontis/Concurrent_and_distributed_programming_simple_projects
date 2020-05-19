using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadanie3
{
    class Program
    {

        static List<Plywak> plywacy = new List<Plywak>();
        static readonly object obj = new object();
        static void Main(string[] args)
        {
            for (int i = 0; i < 25; i++)
            {
                plywacy.Add(new Plywak());
            }
            Thread t1 = new Thread(new ThreadStart(basenA));
            Thread t2 = new Thread(new ThreadStart(basenB));
            t1.Start();
            t2.Start();
            Console.ReadKey();
        }
        private static void basenA()
        {
            int licznik = 0;
            do
            {
                Monitor.Enter(obj);
                Thread.Sleep(750);
                int warunek = licznik + 10 < 25 ? licznik + 10 : 25;
                Monitor.Pulse(obj);
                for (int i = licznik; i < warunek; i++)
                {
                    plywacy[i].baseny[0] = true;
                    Console.WriteLine("Plywak" + i + " duży basen");
                }
                Console.WriteLine();

                Monitor.Exit(obj);
                licznik += 10;
            } while (licznik < 30);

        }
        private static void basenB()
        {
            int licznik = 0;

            do
            {
                Monitor.Enter(obj);
                Thread.Sleep(750);
                int warunek = licznik + 4 < 25 ? licznik + 4 : 24;
                if (!plywacy[warunek].baseny[0])
                    Monitor.Wait(obj);
                for (int i = licznik; i <= warunek; i++)
                {
                    plywacy[i].baseny[1] = true;
                    Console.WriteLine("Plywak " + i +" mały basen");
                }
                Console.WriteLine();
                Monitor.Exit(obj);
                licznik += 5;
            } while (licznik < 30);
        }
    }
}

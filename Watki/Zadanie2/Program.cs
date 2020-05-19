using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadanie2
{
    class Program
    {
        static List<Widz> widzs = new List<Widz>();
        static readonly object obj = new object();


        static void Main(string[] args)
        {
            for (int i = 0; i < 30; i++)
            {
                widzs.Add(new Widz());
            }
            Thread t1 = new Thread(new ThreadStart(salaA));
            Thread t2 = new Thread(new ThreadStart(salaB));
            Thread t3 = new Thread(new ThreadStart(salaC));
            t1.Start();
            t2.Start();
            t3.Start();

            Console.ReadKey();
        }
        private static void salaA()
        {
            int licznik = 0;
            do
            {             
                Monitor.Enter(obj);
                Thread.Sleep(500);
                int warunek = licznik + 4 < 30 ? licznik + 4 : 30;
                Monitor.Pulse(obj);
                for (int i=licznik; i < warunek ; i++)
                {
                    widzs[i].filmy[0] = true;
                    Console.WriteLine("Sala A widz" + i);
                }
                if (licznik == 0)
                    Console.WriteLine();

                Monitor.Exit(obj);                            
                licznik += 4;
            } while (licznik<30);

        }
        private static void salaB()
        {
            int licznik = 0;
           
            do
            {               
                Monitor.Enter(obj);
                Thread.Sleep(500);
                int warunek = licznik + 4 < 30 ? licznik + 4 : 29;
                if (!widzs[warunek].filmy[0])
                    Monitor.Wait(obj);
                for (int i = licznik; i <= warunek; i++)
                {                    
                    widzs[i].filmy[1] = true;
                    Console.WriteLine("Sala B widz" + i);
                }
                if (licznik == 0)
                    Console.WriteLine();
                Monitor.Exit(obj);               
                licznik+=4;
            } while (licznik < 30);
        }
        private static void salaC()
        {
            int licznik = 0;

            do
            {
                Monitor.Enter(obj);
                Thread.Sleep(500);
                int warunek = licznik + 4 < 30 ? licznik + 4 : 29;
                if (!widzs[warunek].filmy[1])
                    Monitor.Wait(obj);
                for (int i = licznik; i <= warunek; i++)
                {
                    widzs[i].filmy[2] = true;
                    Console.WriteLine("Sala C widz" + i);
                }
                Console.WriteLine();
                Monitor.Exit(obj);
                licznik += 4;
            } while (licznik < 30);
        }
    }
}

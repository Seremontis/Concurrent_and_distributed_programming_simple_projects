using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static Mutex g1 = new Mutex();
        static Mutex g2 = new Mutex();
        static Mutex g3 = new Mutex();
        static void Main(string[] args)
        {
            Thread[] t = new Thread[40];
            for (int i = 0; i < 40; i++)
                t[i] = new Thread(new ParameterizedThreadStart((x)=>ZolnierzBada(x)));
             for (int i = 0; i < 40; i++)
                t[i].Start(i);
            for (int i = 0; i < 40; i++)
                t[i].Join();

            Console.WriteLine("Koniec");
            Console.ReadKey();
        }

        private static void ZolnierzBada(object nr)
        {
            g1.WaitOne();
            Console.WriteLine("Gabinet 1 żołnierz {0}", (int)nr);
            Thread.Sleep(400);
            g1.ReleaseMutex();
            g2.WaitOne();
            Console.WriteLine("Gabinet 2 żołnierz {0}", (int)nr);
            Thread.Sleep(400);
            g2.ReleaseMutex();
            g3.WaitOne();
            Console.WriteLine("Gabinet 3 żołnierz {0}", (int)nr);
            Thread.Sleep(400);
            g3.ReleaseMutex();

        }
    }
}

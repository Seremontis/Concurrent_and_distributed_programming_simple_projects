using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static int bilans=500;
        private static object obj = new object();
        static void Main(string[] args)
        {
            Thread t1 = new Thread(Odejmij);
            Thread t2 = new Thread(Dodaj);

                t1.Start();
                t2.Start();
            Console.ReadKey();
        }
        private static void Dodaj()
        {
            Monitor.Enter(obj);
            for (int i = 0; i < 20; i++)
            {
                bilans += 100;
                while (bilans>=1000)
                {
                    Monitor.Wait(obj);
                }
                if (bilans > 0)
                    Monitor.Pulse(obj);
                Console.WriteLine("Dodano 100 Wartość {0}", bilans);
            }

            Monitor.Exit(obj);
        }
        private static void Odejmij()
        {
            Monitor.Enter(obj);
            for (int i = 0; i < 20; i++)
            {
                Monitor.Pulse(obj);
                while (bilans <= 0)
                    Monitor.Wait(obj);              
                bilans -= 100;

                Console.WriteLine("Odjęto 100 Wartość {0}", bilans);
            }
            Monitor.Exit(obj);
        }
    }
}


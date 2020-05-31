using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static Thread[] threads = new Thread[30];
        static Semaphore wazenie = new Semaphore(3, 3, "wazenie");
        static Semaphore okulista = new Semaphore(1, 1, "okulista");
        static Semaphore internista = new Semaphore(1, 1, "internista");
        static object obj = new object();
        static void Main(string[] args)
        {
            for (int i = 0; i < 30; i++)
            {
                threads[i] = new Thread(new ParameterizedThreadStart((value)=>
                {
                        wazenie.WaitOne();
                        Console.WriteLine("Ważenie żołnierz {0}", (int)value);
                        Thread.Sleep(300);
                        wazenie.Release();
                        okulista.WaitOne();
                        Console.WriteLine("Okulista żołnierz {0}", (int)value);
                        Thread.Sleep(300);
                        okulista.Release();
                        internista.WaitOne();
                        Console.WriteLine("Internista żołnierz {0}", (int)value);
                        Thread.Sleep(300);
                        internista.Release();               
                }));
                threads[i].Start(i);
                
            }
            for (int i = 0; i < 30; i++)
            {
                threads[i].Join();
            }
            Console.WriteLine("Koniec");
            Console.ReadKey();

        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static Random rand = new Random();

        static void Main(string[] args)
        {
            Task t1, t2,t3,t4;
            Action a1 = () =>
            {
                Console.WriteLine("Zadanie 1");
                Thread.Sleep(rand.Next(1000, 2000));
            };
            Action a2 = () =>
            {
                Console.WriteLine("Zadanie 2");
                Thread.Sleep(rand.Next(1000, 2000));
            };
            Action<Task> a3 = (x) =>
            {
                Console.WriteLine("Zadanie 3");
                Thread.Sleep(rand.Next(1000, 2000));
            };
            Action<Task> a4 = (x) =>
            {
                Console.WriteLine("Zadanie 4");
                Thread.Sleep(rand.Next(1000, 2000));
            };


            t1 = new Task(a1);
            t2 = new Task(a2);
            t3=t1.ContinueWith(a3);
            t4=t2.ContinueWith(a4);

            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2,t3,t4);
            Console.WriteLine("Koniec");
        }
    }
}

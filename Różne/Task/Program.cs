using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            int wartosc = 0;
            Barrier barrier = new Barrier(10, (x) => { Console.WriteLine("Faza {0} wartość {1}", x.CurrentPhaseNumber + 1, wartosc); wartosc = 0; });

            Task[] tasks = new Task[50];
            for (int y = 0; y < 50; y++)
            {
                tasks[y] = new Task(() =>
                      {
                          int value = (int)barrier.CurrentPhaseNumber + 1;
                          int a = 5 * (int)Math.Pow(value, 3);
                          int b = 2 * value;
                          int c = -7;
                          wartosc += (a + b + c);
                          barrier.SignalAndWait();

                      });

            };
            //for (int a = 0; a < 50; a++)
            //    tasks[a].Start();
            Parallel.For(0, 50, (z) => tasks[z].Start());
            Task.WaitAll(tasks);
            Console.ReadKey();
        }
    }
}

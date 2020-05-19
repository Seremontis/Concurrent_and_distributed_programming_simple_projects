using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Watki
{
    class Program
    {
        static readonly object obj = new object();
        static string[] arrayAsk=new string[5];
        static string[] arrayAnswer= new string[5];
        static void Main(string[] args)
        {
            Thread t1 = new Thread(new ThreadStart(ask));
            Thread t2 = new Thread(new ThreadStart(answer));
            t1.Start();
            t2.Start();

            Console.ReadKey();
        }

        private static void ask()
        {
            arrayAsk = new string[5];
            for (int i = 0; i < arrayAsk.Length; i++)
            {
                Monitor.Enter(obj);
                arrayAsk[i] = "pytanie" + i;
                Console.WriteLine(arrayAsk[i]);
                Monitor.Wait(obj);
                Monitor.Exit(obj);               
            }

        }

        private static void answer()
        {
            arrayAnswer = new string[5];
            for (int i = 0; i < arrayAnswer.Length; i++)
            {
                Monitor.Enter(obj);
                Monitor.Pulse(obj);
                arrayAnswer[i] = "odpowiedz" + i;
                Console.WriteLine(arrayAnswer[i]);
                Monitor.Exit(obj);
            }

        }
    }
}

using System;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace LogiZajecia4
{
    class Program
    {
        static bool trySave = false;
        static void Main(string[] args)
        {
            while (!trySave)
            {
                Zadanie();
                Thread.Sleep(500);
            }

            Console.ReadKey();
        }
        static void Zadanie()
        {
            using (var mutex = new Mutex(true, "test",out trySave))
            {
                if (!trySave)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Czekam");
                }
                mutex.WaitOne();
                Console.SetCursorPosition(0, 0);
                Console.Write("Wpisuje");


                string path = @"C:\Users\komp2\Documents\Logi.txt";
                using (StreamWriter sw = File.AppendText(path))
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        DateTime dateTime = DateTime.Now;
                        int process = Process.GetCurrentProcess().Id;
                        string output = String.Format("{0}; Id {1} ; {2}", i, process, dateTime);
                        sw.WriteLine(output);
                        Console.Write(".");
                        Thread.Sleep(500);
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("Koniec");
                mutex.ReleaseMutex();

            }
        }
    }
}


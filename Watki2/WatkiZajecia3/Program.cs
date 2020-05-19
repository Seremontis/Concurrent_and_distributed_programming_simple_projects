using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace WatkiZajecia3
{
    class Program
    {
        public static List<int> listInt = new List<int>();
        static Thread t1;
        static Thread t2;
        private static object obj = new object();
        static int progress = 0;
        static void Main(string[] args)
        {
            loadFile();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Wcisniej ESC aby zakończyć działanie programu");
            t1 = new Thread(new ThreadStart(returnVal));
            t1.IsBackground = true;
            t2 = new Thread(new ThreadStart(returnRow));
            t2.IsBackground = true;
            t1.Start();
            t2.Start();
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        static void loadFile()
        {
            using (var reader = new StreamReader(@"C:\liczby.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    int.TryParse(line, out int l);
                    listInt.Add(l);
                }
            }
        }

        static void returnVal()
        {
            int suma = 0;
            int wiersz = 3;
            for (int i = 0; i < listInt.Count; i++)
            {
                int wartosc = excelentNumb(listInt[i]);
                suma += wartosc;
                if (wartosc != 0)
                {
                    lock (obj)
                    {
                        Console.SetCursorPosition(0, wiersz++);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(wartosc + "(wiersz " + (i + 1) + ")");
                    }
                }
                Interlocked.Add(ref progress, 1);
            }
            Console.WriteLine("\nŁączny wynik " + suma);
        }

        static void returnRow()
        {
            double procent;
            int max = listInt.Count;
            for (int i = 0; i < max; i++)
            {
                lock (obj)
                {
                    procent = ((double)progress / 100000) * 100;
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Postęp: Nr rekordu " + progress + " z " + max + "(" + procent + "%)");
                }
            }

        }
        static int excelentNumb(int number)
        {
            int spr = 0;
            for (int i = 1; i <= number / 2; i++)
            {
                if (number % i == 0)
                {
                    spr += i;
                    if (spr == number)
                    {
                        return number;
                    }
                    else if (spr > number)
                        break;
                }
            }
            return 0;
        }
    }
}

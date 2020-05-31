using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static int[,] tablica = new int[1000, 2];
        static Random random = new Random();
        static void Main(string[] args)
        {
            Parallel.For(0, 1000-1, (x) => Losuj(x));
            Console.ReadKey();
        }
        private static void Losuj(int x) {
            for (int y = 0; y < tablica.GetLength(1); y++)
            {
                tablica[x, y] = random.Next(1, 250);
            }
            Wypisz(x);
        }

        private static void Wypisz(int x)
        {
            int mniejsza = tablica[x, 0] > tablica[x, 1] ? tablica[x, 1] : tablica[x, 0];
            bool flaga = true;
            for (int i = 2; i < mniejsza; i++)
            {
                if(tablica[x,0]%i==0 && tablica[x, 1] % i == 0)
                {
                    flaga = false;
                    break;
                }
            }
            if (flaga)
                Console.WriteLine("{0} I {1}", tablica[x, 0] ,tablica[x, 1]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async
{
    class Program
    {
        static Random random = new Random();
        static void Main(string[] args)
        {
            Wykonaj();
            Console.ReadKey();
        }

        private static async void Wykonaj()
        {
            var t1=Zadanie1();
            var t2=Zadanie2();
            await Task.WhenAll(t1,t2);
        }

        private static async Task Zadanie1()
        {
            List<Bliziacy> list = new List<Bliziacy>();
            int poprzednia = 2;
            await Task.Run(() =>
            {
                for (int i = 2; i < 1000; i++)
                {
                    if (CzyPierwsza(i))
                    {
                        if (i - poprzednia == 2)
                            list.Add(new Bliziacy() { l1 = poprzednia, l2 = i });
                        poprzednia = i;
                    }
                }
                Zapisz(list);
            });
            
        }
        private static async Task Zadanie2()
        {
            int[] tab = new int[10];
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Podaj liczby");
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] = Convert.ToInt32(Console.ReadLine());
            }
            await Task.Run(() =>
            {
                Array.Sort(tab);
                decimal mediana = decimal.Zero;
                if (tab.Length % 2 == 0)
                    mediana = (tab[(tab.Length / 2) - 1] + tab[tab.Length / 2]) / 2;
                else
                    mediana = tab[(tab.Length / 2) + 1];
                Console.WriteLine("Mediana wynosi {0}", mediana);
            });
        }

        private static void Zapisz(List<Bliziacy> bliziacies)
        {
            var filepath = "liczby.csv";
            using (StreamWriter writer = new StreamWriter(new FileStream(filepath,
                        FileMode.Create, FileAccess.Write)))
            {
                foreach (var item in bliziacies)
                {
                    writer.WriteLine("{0}, {1}", item.l1, item.l2);
                }
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Zapisano");
        }
        private static bool CzyPierwsza(int liczba)
        {
            int licznik = 0;
            for (int i = 1; i <= liczba; i++)
            {
                if (liczba % 2 == 0)
                    licznik++;
            }
            if (licznik > 2)
                return false;
            else
                return true;
        }

    }
    class Bliziacy
    {
        public int l1 { get; set; }
        public int l2 { get; set; }
    }
}

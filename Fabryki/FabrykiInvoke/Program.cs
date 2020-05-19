using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FabrykiInvoke
{
    class Program
    {
        private static List<string> listA;

        static void Main(string[] args)
        {
            ReadData();
            int start = System.Environment.TickCount;
            Parallel.Invoke(LiczSumeIloczynZnakow, GrupujISortuj, GrupujWgDlugosci, LiczbaImionWyzejWgAlfabetu) ;
            int stop = System.Environment.TickCount;


            int start2 = System.Environment.TickCount;
            LiczSumeIloczynZnakow();
            GrupujISortuj();
            GrupujWgDlugosci();
            LiczbaImionWyzejWgAlfabetu();
            int stop2 = System.Environment.TickCount;

            Console.WriteLine("Test 1");
            Console.WriteLine("Czas wykonania {0} ms", (stop - start).ToString("N0"));
            Console.WriteLine("Test 2");
            Console.WriteLine("Czas wykonania {0} ms", (stop2 - start2).ToString("N0"));
            
            Console.ReadKey();
        }
        private static void ReadData()
        {
            using (var reader = new StreamReader(@"C:\lista_imion.csv"))
            {
                listA = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    listA.Add(values[0]);
                }
                listA.RemoveAt(0);
            }
        }

        private static void LiczSumeIloczynZnakow()
        {
            int iloraz = 1;
            int suma = 0;
            int sumailorazow = 0;
            foreach (var item in listA)
            {
                foreach (var litera in item)
                {
                    iloraz *= (1 + JakiZnak(litera));
                }
            }
            foreach (var item in listA)
            {
                foreach (var litera in item)
                {
                    suma += JakiZnak(litera);
                }
            }
            foreach (var item in listA)
            {
                sumailorazow += LiczRekurencyjnie(item);
            }
        }
        private static int LiczRekurencyjnie(string slowo)
        {
            int liczba = 1;
            if (slowo.Length > 1)
                liczba = LiczRekurencyjnie(slowo.Substring(0, slowo.Length - 1));

            foreach (var item in slowo)
            {
                int z= JakiZnak(item) + 1;
                if (z % 2 == 0)
                {
                    z *= z;
                }
                liczba *= z;
            }
            return liczba;
        }
       
        private static void GrupujISortuj()
        {
            List<string> grupa;
 
            for (int i = 65; i <= 90; i++)
            {
                grupa = new List<string>();
                foreach (var item in listA)
                {
                    if (item[0] == (char)i)
                        grupa.Add(item);
                }
                grupa.Sort();
            }

        }

        private static void GrupujWgDlugosci()
        {

            List<string> imionaWgDlugosci;
            for (int i = 0; i < 15; i++)
            {
                imionaWgDlugosci = new List<string>();
                foreach (var item in listA)
                {
                    if (item.Count() == i)
                        imionaWgDlugosci.Add(item);
                }
                imionaWgDlugosci.Sort();
            }

        }

        private static void LiczbaImionWyzejWgAlfabetu()
        {
            foreach (var item in listA)
            {
                int licznik = 0;
                foreach (var item2 in listA)
                {
                    if (item.CompareTo(item2) > 0)
                        licznik++;
                }
            }
        }

        private static int JakiZnak(char litera)
        {
            if (litera >= 65 && litera <= 90)
                return litera - 65;
            else if (litera >= 97 && litera <= 122)
                return litera - 97;
            else
                return 0;
        }

    }
}

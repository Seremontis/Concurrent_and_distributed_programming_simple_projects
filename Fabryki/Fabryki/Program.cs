using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabryki
{
    class Program
    {
        private static List<string> listA;
        private static List<Obiekt> obiekty = new List<Obiekt>();

        static void Main(string[] args)
        {
            ReadData(); // dane z https://dane.gov.pl/dataset/1501,lista-imion-wystepujacych-w-rejestrze-pesel/resource/21490/table?page=1&per_page=20&q=&sort=

            //Zadanie równoległe
            int start = System.Environment.TickCount;
            Parallel.ForEach(listA, (item) =>
            {
                var obiekt = new Obiekt();
                obiekt.Imie = item;
                obiekt.Wystapienia = new Dictionary<char,int>();
                foreach (var litera in item)
                {
                    if (obiekt.Wystapienia.ContainsKey(litera))
                        obiekt.Wystapienia[litera] += 1;
                    else
                        obiekt.Wystapienia.Add(litera, 1);
                }
                obiekty.Add(obiekt);
                obiekty.Sort((x,y) =>  x.Imie.CompareTo(y.Imie));
            });

            int stop = System.Environment.TickCount;
            Console.WriteLine("Test 1");
            Console.WriteLine("Czas wykonania {0} ms", (stop - start).ToString("N0"));


            //Zadanie sekwencyjne
            obiekty = new List<Obiekt>();
            int start2 = System.Environment.TickCount;           
            foreach (var item in listA)
            {
                var obiekt = new Obiekt();
                obiekt.Imie = item;
                obiekt.Wystapienia = new Dictionary<char, int>();
                foreach (var litera in item)
                {
                    if (obiekt.Wystapienia.ContainsKey(litera))
                        obiekt.Wystapienia[litera] += 1;
                    else
                        obiekt.Wystapienia.Add(litera, 1);
                }
                obiekty.Add(obiekt);
                obiekty.Sort((x, y) => x.Imie.CompareTo(y.Imie));
            };
            int stop2 = System.Environment.TickCount;

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


    }
}

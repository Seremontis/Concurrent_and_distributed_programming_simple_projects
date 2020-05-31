using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        static Random random = new Random();
        static List<ListaLiczb> listaLiczbs = new List<ListaLiczb>();
        static void Main(string[] args)
        {
            for (int i = 0; i < 50; i++)
            {
                listaLiczbs.Add(Generuj(i));
            }
            Parallel.ForEach(listaLiczbs, (item) => Wypisz(item));

        }

        private static ListaLiczb Generuj(int z)
        {
            ListaLiczb listaLiczb = new ListaLiczb();
            listaLiczb.nrZbioru = z;
            List<int> lista = new List<int>();
            for (int i = 0; i < 15000; i++)
                lista.Add(random.Next(500));
            lista = sort(lista.ToArray());
            listaLiczb.liczby = lista;
            return listaLiczb;
        }
        private static void Wypisz(ListaLiczb lista)
        {
            var filepath = "zbior"+lista.nrZbioru+".csv";
            using (StreamWriter writer = new StreamWriter(new FileStream(filepath,
            FileMode.Create, FileAccess.Write)))
            {
                foreach (var item in lista.liczby)
                {
                    writer.WriteLine("{0}", item);
                }
            }
        }

        static List<int> sort(int[] tablica)
        {
            int n = tablica.Length;
            do
            {
                for (int i = 0; i < n - 1; i++)
                {
                    if (tablica[i] > tablica[i + 1])
                    {
                        int tmp = tablica[i];
                        tablica[i] = tablica[i + 1];
                        tablica[i + 1] = tmp;
                    }
                }
                n--;
            }
            while (n > 1);
            return tablica.ToList();
        }
    }
    class ListaLiczb
    {
        public int nrZbioru { get; set; }
        public List<int> liczby{ get; set; }
    }
}

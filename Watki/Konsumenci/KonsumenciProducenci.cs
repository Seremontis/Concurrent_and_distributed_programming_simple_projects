using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Konsumenci
{
    class KonsumenciProducenci
    {
        static Thread watekProducenta = null;
        static Thread watekKonsumenta = null;
        static int pojemnoscMagazynu = 5;
        static ConcurrentQueue<string> kolejka = new ConcurrentQueue<string>();
        static int nrTowaru=0;
        static readonly object obiekt = new object();    

        public void Pracuj()
        {
            ThreadStart prod = () =>
            {
                while (true)
                {
                    Monitor.Enter(obiekt);
                    Console.WriteLine(kolejka.Count());
                    if (kolejka.Count >= pojemnoscMagazynu)
                        Monitor.Wait(obiekt);
                    else
                    {
                        Monitor.Pulse(obiekt);
                        Console.WriteLine(kolejka.Count());
                        Console.WriteLine("test {0}",nrTowaru);                     
                        kolejka.Enqueue("Produkt " + (++nrTowaru));
                    }

                    Monitor.Exit(obiekt);
                }
                
            };

            ThreadStart kon = () =>
            {
                while (true)
                {
                    Monitor.Enter(obiekt);
                    if (kolejka.Count <= 0)
                    {
                        Console.WriteLine(kolejka.Count());
                        Console.WriteLine("Brak towaru");
                        Monitor.Wait(obiekt);
                    }
                    else
                    {
                        Monitor.Pulse(obiekt);
                        Console.WriteLine(kolejka.Count());
                        Console.WriteLine("Wydano towar");
                        kolejka.TryDequeue(out string output);
                    }
                    Monitor.Exit(obiekt);
                }
            };

            
            watekProducenta = new Thread(prod);
            watekKonsumenta = new Thread(kon);
            //ThreadPool.QueueUserWorkItem(prod); // zmienic typy metod anonimowych na WaitCallback i dodac parametr objct
            //ThreadPool.QueueUserWorkItem(kon);
            watekProducenta.IsBackground = true;
            watekKonsumenta.IsBackground = true;
            watekKonsumenta.Start();
            watekProducenta.Start();


            Console.ReadLine();
            Console.WriteLine("!!! Koniec !!!");
            wyswietlStanMagazynu();
        }
        private void wyswietlStanMagazynu()
        {
            foreach (string item in kolejka)
            {
                Console.WriteLine(item);
            }
        }
    }
}

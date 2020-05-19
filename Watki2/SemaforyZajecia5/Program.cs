using System;
using System.Threading;

namespace SemaforyZajecia5
{
    class Program
    {
        static void Main(string[] args)
        {            
            PiecuFilozofow fil = new PiecuFilozofow();
            fil.Zacznij();
            Thread.Sleep(10000);
            fil.Zakoncz();
            Console.WriteLine("Koniec");
            Console.ReadKey();
        }
    }

    class PiecuFilozofow
    {
        Thread[] t;
        private static Semaphore[] Widelec=new Semaphore[5];
        private static Semaphore lokaj;
        public PiecuFilozofow()
        {
            t = new Thread[5];
            lokaj = new Semaphore(0, 4, "test");
            for (int i = 0; i < 5; i++)
            {
                Widelec[i] = new Semaphore(0,1);
                t[i] = new Thread(new ParameterizedThreadStart(Pracuj));
            }
        }
        public void Pracuj(object text)
        {
            int i = (int)text;

            Console.WriteLine("Filozof " + i + " myśli");
            lock (lokaj)
            {
                lokaj.WaitOne(0);
                Widelec[i].WaitOne(0);
                Widelec[(i + 1) % 5].WaitOne(0);
                Console.WriteLine("Filozof " + i + " je");
                lokaj.Release();
                Widelec[i].Release();
                Widelec[(i + 1) % 5].Release();
            }

        }

        public void Zacznij()
        {
            for (int i = 0; i < 5; i++)
            {
                t[i].Start(i);
            }
        }
        public void Zakoncz()
        {
            for (int i = 0; i < 5; i++)
            {
                t[i].Abort();
            }
        }
    }
}

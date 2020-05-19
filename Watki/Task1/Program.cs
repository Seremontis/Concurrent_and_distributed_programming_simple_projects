using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        const int ileWatkow = 10;
        //static Barrier b = new Barrier(ileWatkow);
        //static Barrier b = new Barrier(ileWatkow, (Barrier b)=> { Console.WriteLine(); });  // Klasa Barrier umozliwia okreslenia dzialania jakie ma byc wykonane po kazdej fazie

        static void Main(string[] args)
        {
            Action metodaWatku = () =>
            {
                for (char znak = 'A'; znak <= 'G'; znak++)
                {
                    Console.Write(znak);
                    //b.SignalAndWait();
                }
            };
            Task[] tab = new Task[ileWatkow];
            for (int i = 0; i < ileWatkow; i++)
            {
                tab[i] =new Task(metodaWatku);
                tab[i].Start();
            }
            Console.ReadKey();
        }
    }
}

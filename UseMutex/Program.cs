using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UseMutex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start!");
            Console.ReadKey();
            Mutex mutex = Mutex.OpenExisting("mars");

            while (true)
            {
                Console.WriteLine("Press any key to stop mutex!");
                Console.ReadKey();
                Console.WriteLine("Waiting...");
                mutex.WaitOne();    // wait for unblock
                                    // and imediately block
                Console.WriteLine("Got a processor time!");
                Thread.Sleep(7000);
                mutex.ReleaseMutex(); // unblock

                Console.WriteLine("Continue...");
            }
        }
    }
}

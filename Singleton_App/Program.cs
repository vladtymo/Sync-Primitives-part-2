using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Singleton_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Mutex mutex = null;
            if (!Mutex.TryOpenExisting("Neptun", out mutex))
            {
                mutex = new Mutex(false, "Neptun");
            }
            else
            {
                Console.WriteLine("Another instance of this program was opened!");
                Console.WriteLine("Closing...");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }

            Console.WriteLine("Working...");
            Console.ReadKey();
        }
    }
}

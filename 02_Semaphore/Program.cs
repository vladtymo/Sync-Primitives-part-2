using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _02_Semaphore
{
    class Program
    {
        static void Main(string[] args)
        {
            Semaphore s = new Semaphore(3, 3);

            for (int i = 0; i < 6; ++i)
                ThreadPool.QueueUserWorkItem(SomeMethod, s);

            Console.ReadKey();
        }
        static void SomeMethod(object obj)
        {
            Semaphore s = obj as Semaphore;

            bool stop = false;
            while (!stop)
            {
                if (s.WaitOne(500))
                {
                    try
                    {
                        Console.WriteLine("Thread {0} got a lock", Thread.CurrentThread.ManagedThreadId);
                        Thread.Sleep(2000);
                    }
                    finally
                    {
                        stop = true;
                        Console.WriteLine("Thread {0} remove a lock", Thread.CurrentThread.ManagedThreadId);
                        s.Release(); 
                    }
                }
                else
                    Console.WriteLine("Timeout for thread {0} expired. Re-waiting...", Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _04_WaitHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Manual reset event: ");

            ManualResetEvent[] events = new ManualResetEvent[10];

            for (int i = 0; i < 10; ++i)
            {
                events[i] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(SomeMethod, events[i]);
            }

            // wait thread
            WaitHandle.WaitAny(events);
            Console.WriteLine("Some thread has done!");

            events[5].WaitOne();
            Console.WriteLine("5-th thread has done!");

            WaitHandle.WaitAll(events);
            Console.WriteLine("All threads have done!");

            Console.ReadKey();
        }
        static void SomeMethod(object obj)
        {
            EventWaitHandle ev = obj as EventWaitHandle;

            Console.WriteLine("Doing hard work...", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(new Random().Next(10000));
            Console.WriteLine("Done!", Thread.CurrentThread.ManagedThreadId);
            //ev.Reset(); // set to nonsignaled state
            ev.Set();     // set to signaled state
        }
    }
}

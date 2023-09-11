using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _03_Event
{
    class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine("Reset event: ");

            // signaled    - free
            // nonsignaled - busy

            // WaitOne() - still with signaled state
            //ManualResetEvent mre = new ManualResetEvent(true); // send signal

            // WaitOne() - auto sets to nonsignaled state
            //AutoResetEvent are = new AutoResetEvent(true); // initialState - is signaled state (free), false by default

            //for (int i = 0; i < 10; ++i)
            //    ThreadPool.QueueUserWorkItem(SomeMethod, mre);

            ///////////// Test Manual Reset
            ManualResetEvent resetEvent = new ManualResetEvent(true);
            ThreadPool.QueueUserWorkItem(ShowSecondsUntilMinutes, resetEvent);

            ConsoleKey key;
            do
            {
                key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Spacebar:
                        resetEvent.Reset();
                        break;
                    case ConsoleKey.Enter:
                        resetEvent.Set();
                        break;
                }
            } while (key != ConsoleKey.Escape);

            //AutoResetEvent resetEvent = new AutoResetEvent(false);
            //Mutex resetEvent = new Mutex(true);

            //ThreadPool.QueueUserWorkItem(M1, resetEvent);
            //ThreadPool.QueueUserWorkItem(M2, resetEvent);

            Console.WriteLine("Continue...");
            Console.ReadKey();
        }

        static void M1(object obj)
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine(i);
            }
            ((EventWaitHandle)obj).Set();
        }
        static void M2(object obj)
        {
            ((EventWaitHandle)obj).WaitOne();
            for (int i = 10; i >= 1; i--)
            {
                Console.WriteLine(i);
            }
        }

        static void ShowSecondsUntilMinutes(object obj)
        {
            EventWaitHandle ev = (EventWaitHandle)obj;

            int seconds = DateTime.Now.Second;
            while(seconds < 60)
            {
                ev.WaitOne();
                Console.WriteLine(seconds);
                ++seconds;
                Thread.Sleep(1000);
            }
        }
        static void SomeMethod(object obj)
        {
            EventWaitHandle ev = obj as EventWaitHandle;
            if (ev.WaitOne(1)) // wait
            {
                ev.Reset(); // set to nonsignaled
                Console.WriteLine("Thread {0} managed to slip past", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(100);
                ev.Set(); // unblock
            }
            else
            {
                Console.WriteLine("Thread {0} late", Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _01_Mutex
{
    /*
        ■ public virtual void Close(); – закрывает описатель объекта ядра;
        ■ public virtual bool WaitOne(); – ожидает сигнального состояния одного объекта ядра;
        ■ public virtual bool WaitOne(int millisecondsTimeout, bool exitContext); 
            – ожидает сигнального состояния одного объекта ядра пока не истечет таймаут;
        ■ public static int WaitAny(WaitHandle[] waitHandles); 
            – ожидает сигнального состояния любого объекта ядра из массива;
        ■ public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext);
            – ожидает сигнального состояния любого объекта ядра из массива пока не истечет таймаут;
        ■ public static bool WaitAll(WaitHandle[] waitHandles);
            – ожидает сигнального состояния всех объектов ядра из массива;
        ■ public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext); 
            – ожидает сигнального состояния всех объектов ядра из массива пока не истечет таймаут;
        ■ public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn); 
            – переводит объект ядра в сигнальное состояние и ждет сигнального состояния от другого объекта ядра;
        ■ public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, 
            int millisecondsTimeout, bool exitContext); 
            – переводит объект ядра в сигнальное состояние и ждет сигнального состояния от другого объекта ядра.
    */
    class Program
    {
        static void Main(string[] args)
        {
            Mutex mutexObj = new Mutex(false, "mars"); // singnal for default
            Console.WriteLine("Sync using Mutex class: ");

            LockCounter c = new LockCounter();
            Thread[] threads = new Thread[5];

            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(c.UpdateFields);
                threads[i].Start(mutexObj);
            }

            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();

            Console.WriteLine("Count: {0}\n\n", c.Count);
        }
    }
    class LockCounter
    {
        int count;
        public int Count
        {
            get { return count; }
        }

        public void UpdateFields(object obj)
        {
            Mutex m = obj as Mutex;

            for (int i = 0; i < 100_000; ++i)
            {
                m.WaitOne(); // wait and then block
                Console.WriteLine($"Thread id: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine("Count: " + count);
                ++count;
                Thread.Sleep(500);
                m.ReleaseMutex(); // set singnal state (unlock)
            }
        }
    }
}
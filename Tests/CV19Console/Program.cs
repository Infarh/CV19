using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace CV19Console
{
    class Program
    {
        private static bool __ThreadUpdate = true;

        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main theread";

            var clock_thread = new Thread(ThreadMethod);
            clock_thread.Name = "Other thread";
            clock_thread.IsBackground = true;
            clock_thread.Priority = ThreadPriority.AboveNormal;

            clock_thread.Start(42);

            //var count = 5;
            //var msg = "Hello World!";
            //var timeout = 150;

            //new Thread(() => PrintMethod(msg, count, timeout)) { IsBackground = true }.Start();



            //CheckThread();

            //for (var i = 0; i < 5; i++)
            //{
            //    Thread.Sleep(100);
            //    Console.WriteLine(i);
            //}

            var values = new List<int>();

            var threads = new Thread[10];
            object lock_object = new object();
            for (var i = 0; i < threads.Length; i++)
                threads[i] = new Thread(() =>
                {
                    for (var j = 0; j < 10; j++)
                    {
                        lock (lock_object)
                            values.Add(Thread.CurrentThread.ManagedThreadId);
                        Thread.Sleep(1);
                    }
                });

            Monitor.Enter(lock_object);
            try
            {
                
            }
            finally
            {
                Monitor.Exit(lock_object);
            }

            foreach (var thread in threads)
                thread.Start();


            if (!clock_thread.Join(100))
            {
                //clock_thread.Abort();     // Прерывает поток в любой точке процесса его выполнения
                clock_thread.Interrupt();
            }

            Console.ReadLine();
            Console.WriteLine(string.Join(",", values));

            Console.ReadLine();
        }

        private static void PrintMethod(string Message, int Count, int Timeout)
        {
            for (var i = 0; i < Count; i++)
            {
                Console.WriteLine(Message);
                Thread.Sleep(Timeout);
            }
        }

        private static void ThreadMethod(object parameter)
        {
            var value = (int)parameter;
            Console.WriteLine(value);

            CheckThread();

            while (__ThreadUpdate)
            {
                Thread.Sleep(100);
                Thread.SpinWait(1000);
                Console.Title = DateTime.Now.ToString();
            }
        }

        private static void CheckThread()
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine("id:{0} - {1}", thread.ManagedThreadId, thread.Name);
        }
    }
}

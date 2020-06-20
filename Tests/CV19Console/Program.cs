using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace CV19Console
{
    class Program
    {
        private static bool __ThreadUpdate = true;

        static void Main(string[] args)
        {
            WebServerTest.Run();
            return;

            //Thread.CurrentThread.Name = "Main theread";

            //var clock_thread = new Thread(ThreadMethod);
            //clock_thread.Name = "Other thread";
            //clock_thread.IsBackground = true;
            //clock_thread.Priority = ThreadPriority.AboveNormal;

            //clock_thread.Start(42);

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

            //var values = new List<int>();

            //var threads = new Thread[10];
            //object lock_object = new object();
            //for (var i = 0; i < threads.Length; i++)
            //    threads[i] = new Thread(() =>
            //    {
            //        for (var j = 0; j < 10; j++)
            //        {
            //            lock (lock_object)
            //                values.Add(Thread.CurrentThread.ManagedThreadId);
            //            Thread.Sleep(1);
            //        }
            //    });

            //Monitor.Enter(lock_object);
            //try
            //{
                
            //}
            //finally
            //{
            //    Monitor.Exit(lock_object);
            //}

            //foreach (var thread in threads)
            //    thread.Start();


            //if (!clock_thread.Join(100))
            //{
            //    //clock_thread.Abort();     // Прерывает поток в любой точке процесса его выполнения
            //    clock_thread.Interrupt();
            //}

            //Mutex mutex = new Mutex();
            //Semaphore semaphore = new Semaphore(0, 10);
            //semaphore.WaitOne();

            // действия в крит.секции

            //semaphore.Release();

            ManualResetEvent manual_reset_event = new ManualResetEvent(false);
            AutoResetEvent auto_reset_event = new AutoResetEvent(false);

            EventWaitHandle thread_guidance = auto_reset_event;

            var test_threads = new Thread[10];
            for (var i = 0; i < test_threads.Length; i++)
            {
                var local_i = i;
                test_threads[i] = new Thread(() =>
                {
                    Console.WriteLine("Поток id:{0} - стартовал", Thread.CurrentThread.ManagedThreadId);

                    thread_guidance.WaitOne();

                    Console.WriteLine("Value:{0}", local_i);
                    Console.WriteLine("Поток id:{0} - завершился", Thread.CurrentThread.ManagedThreadId);
                    //thread_guidance.Set();
                });
                test_threads[i].Start();
            }

            Console.WriteLine("Готов к запуску потоков");
            Console.ReadLine();

            thread_guidance.Set();
            thread_guidance.Reset();

            //Console.ReadLine();
            //Console.WriteLine(string.Join(",", values));

            Console.ReadLine();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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

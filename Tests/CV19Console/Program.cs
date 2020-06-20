using System;
using System.Globalization;
using System.Threading;

namespace CV19Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main theread";

            var thread = new Thread(ThreadMethod);
            thread.Name = "Other thread";
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.AboveNormal;

            thread.Start(42);

            CheckThread();

            for (var i = 0; i < 5; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }

        private static void ThreadMethod(object parameter)
        {
            var value = (long) parameter;
            Console.WriteLine(value);

            CheckThread();

            while (true)
            {
                Thread.Sleep(100);
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

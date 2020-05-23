using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SimpleWaitPulseDemo
{
    class Program
    {
        static object locker = new object();
        static Queue<int> numbers = new Queue<int>();
        static int[] vec = { 12, 123, 512, 21, 535, 6, 3, 74654, 233, 4, 1, 2, 3, 4, 5, 6, 7, 8, 8, 65 };

        public static void Main(string[] args)
        {
            new Thread(Write).Start();
            new Thread(Read).Start();
        }
        public static void Read()
        {
            while (true)
            {
                lock (locker)
                {
                    while (numbers.Count == 0)
                        Monitor.Wait(locker);
                    Console.WriteLine(numbers.Dequeue());
                }
                Thread.Sleep(1000);
            }
        }
        public static void Write()
        {
            for (int i=0; i<vec.Length;i++)
            {
                lock (locker)
                {
                    numbers.Enqueue(vec[i]);
                    Monitor.Pulse(locker);
                }
                Thread.Sleep(2000);
            }
        }
    }
}

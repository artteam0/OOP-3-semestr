using System;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Threading;
using System.ComponentModel.Design;

namespace lab13
{
    class Program
    {
        static Mutex consoleMutex = new Mutex();
        static ManualResetEvent pauseEvent = new ManualResetEvent(true);
        static object lockObject = new object();
        static bool evenTurn = false;
        static Timer timer;
        static void Main()
        {
            var allProcess = Process.GetCurrentProcess();
            Console.WriteLine($"ID: {allProcess.Id}");
            Console.WriteLine($"Name: {allProcess.ProcessName}");
            Console.WriteLine($"Priority: {allProcess.PriorityClass}");
            Console.WriteLine($"Start time: {allProcess.StartTime}");
            Console.WriteLine($"Current state: {allProcess.Responding}");
            Console.WriteLine($"All CPU time: {allProcess.TotalProcessorTime}");

            AppDomain domain = AppDomain.CurrentDomain;
            using (StreamWriter writer = new StreamWriter("task_2.txt"))
            {
                writer.WriteLine($"Имя домена: {domain.FriendlyName}");
                writer.WriteLine($"Базовый директорий: {domain.BaseDirectory}");
                writer.WriteLine("Выгруженные сборки: ");
                foreach (var assembly in domain.GetAssemblies())
                {
                    writer.WriteLine($"- {assembly.FullName}");
                }
            }

            ThirdTask();
            EvenOddNumbersWithMutex();
            Timer();

            Console.WriteLine("Нажмите любую клавишу для завершения программы...");
            Console.ReadKey();
            timer.Dispose();
        }

        static void ThirdTask()
        {
            Console.WriteLine("Введите n (для управления потоком): ");
            int n = int.Parse(Console.ReadLine());

            Thread thread = new Thread(() =>
            {
                using (StreamWriter writer = new StreamWriter("task_3.txt", true))
                {
                    for (int i = 2; i <= n; i++)
                    {
                        pauseEvent.WaitOne();
                        if (isPrime(i))
                        {
                            consoleMutex.WaitOne();
                            try
                            {
                                Console.WriteLine($"Значение: {i}");
                                writer.WriteLine(i);
                                Console.WriteLine($"Поток: {Thread.CurrentThread.Name}");
                                Console.WriteLine($"Идентификатор: {Thread.CurrentThread.ManagedThreadId}");
                                Console.WriteLine($"Приоритет: {Thread.CurrentThread.Priority}");
                                Console.WriteLine($"Состояние: {Thread.CurrentThread.ThreadState}");
                            }
                            finally
                            {
                                consoleMutex.ReleaseMutex();
                            }
                        }
                        Thread.Sleep(100);
                    }
                }
            });
            thread.Start();
            Thread.Sleep(750);
            pauseEvent.Reset();
            Console.WriteLine("Поток приостановлен");
            Thread.Sleep(750);
            pauseEvent.Set();
            Console.WriteLine("Поток возобновлен");
            thread.Join();
        }

        static bool isPrime(int i)
        {
            if (i < 2)
            {
                return false;
            }

            for (int j = 2; i <= Math.Sqrt(i); j++)
            {
                if (i % j == 0)
                {
                    return false;
                }
            }
            return true;
        }

        static void EvenOddNumbersWithMutex()
        {
            Console.Write("Введите n (для чётности - нечётности): ");
            int n = int.Parse(Console.ReadLine());

            Thread evenThread = new Thread(() => PrintEvenNumbers(n)) { Priority = ThreadPriority.Highest };
            Thread oddThread = new Thread(() => PrintOddNumbers(n)) { Priority = ThreadPriority.Lowest };

            evenThread.Start();
            evenThread.Join();

            oddThread.Start();
            oddThread.Join();

            evenThread = new Thread(() => PrintEvenNumbersWithSync(n));
            oddThread = new Thread(() => PrintOddNumbersWithSync(n));

            oddThread.Start();
            evenThread.Start();

            oddThread.Join();
            evenThread.Join();
        }

        static void PrintEvenNumbers(int n)
        {
            using (StreamWriter writer = new StreamWriter("task_4_1.txt"))
            {
                for (int i = 2; i <= n; i += 2)
                {
                    lock (lockObject)
                    {
                        writer.WriteLine(i);
                        Console.WriteLine(i);
                    }
                    Thread.Sleep(75);
                }
            }
        }

        static void PrintOddNumbers(int n)
        {
            using (StreamWriter writer = new StreamWriter("task_4_2.txt"))
            {
                for (int i = 1; i <= n; i += 2)
                {
                    lock (lockObject)
                    {
                        writer.WriteLine(i);
                        Console.WriteLine(i);
                    }
                    Thread.Sleep(125);
                }
            }
        }

        static void PrintEvenNumbersWithSync(int n)
        {
            for (int i = 2; i <= n; i += 2)
            {
                lock (lockObject)
                {
                    while (!evenTurn)
                    {
                        Monitor.Wait(lockObject);
                    }

                    Console.WriteLine($"Чередование (четное): {i}");
                    evenTurn = false;
                    Monitor.Pulse(lockObject);
                }
                Thread.Sleep(75);
            }
        }

        static void PrintOddNumbersWithSync(int n)
        {
            for (int i = 1; i <= n; i += 2)
            {
                lock (lockObject)
                {
                    while (evenTurn)
                    {
                        Monitor.Wait(lockObject);
                    }

                    Console.WriteLine($"Чередование (нечетное): {i}");
                    evenTurn = true;
                    Monitor.Pulse(lockObject);
                }
                Thread.Sleep(125);
            }
        }

        static void Timer()
        {
            timer = new Timer(state =>
            {
                string currTime = DateTime.Now.ToString("HH:mm:ss:fff");
                Console.WriteLine($"Текущее время: {currTime}");
                using (StreamWriter writer = new StreamWriter("Timer.txt", true))
                {
                    writer.WriteLine(currTime);
                }
            }, null, 0, 15);
        }
    }
}
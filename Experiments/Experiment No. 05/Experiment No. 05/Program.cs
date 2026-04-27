using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WriteLine("===== 1. Synchronous Execution =====");
        Stopwatch sw = new Stopwatch();
        sw.Start();

        SyncMethod1();
        SyncMethod2();

        sw.Stop();
        Console.WriteLine("Synchronous Time: " + sw.ElapsedMilliseconds + " ms");


        Console.WriteLine("\n===== 2. Asynchronous Execution =====");
        sw.Restart();

        Task t1 = AsyncMethod1();
        Task t2 = AsyncMethod2();

        Task.WaitAll(t1, t2);

        sw.Stop();
        Console.WriteLine("Asynchronous Time: " + sw.ElapsedMilliseconds + " ms");


        Console.WriteLine("\n===== 3. Thread.Sleep vs Task.Delay =====");

        Console.WriteLine("Using Thread.Sleep (Blocking)...");
        Thread.Sleep(2000);
        Console.WriteLine("After Thread.Sleep");

        Console.WriteLine("\nUsing Task.Delay (Non-Blocking)...");
        Task.Delay(2000).Wait();
        Console.WriteLine("After Task.Delay");


        Console.WriteLine("\n===== 4. Method Returning Task =====");
        Task<int> resultTask = GetNumberAsync();
        Console.WriteLine("Result from Task: " + resultTask.Result);
    }

    // 1. Synchronous Methods
    static void SyncMethod1()
    {
        Console.WriteLine("Sync Method 1 Start");
        Thread.Sleep(2000);
        Console.WriteLine("Sync Method 1 End");
    }

    static void SyncMethod2()
    {
        Console.WriteLine("Sync Method 2 Start");
        Thread.Sleep(2000);
        Console.WriteLine("Sync Method 2 End");
    }

    // 2. Asynchronous Methods
    static async Task AsyncMethod1()
    {
        Console.WriteLine("Async Method 1 Start");
        await Task.Delay(2000);
        Console.WriteLine("Async Method 1 End");
    }

    static async Task AsyncMethod2()
    {
        Console.WriteLine("Async Method 2 Start");
        await Task.Delay(2000);
        Console.WriteLine("Async Method 2 End");
    }

    // 4. Method Returning Task
    static async Task<int> GetNumberAsync()
    {
        await Task.Delay(1000);
        return 100;
    }
}
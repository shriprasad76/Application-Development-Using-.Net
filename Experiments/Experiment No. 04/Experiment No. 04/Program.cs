using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // 1. Delegate for Calculator
    public delegate int Calc(int a, int b);

    // Calculator methods
    static int Add(int a, int b) => a + b;
    static int Sub(int a, int b) => a - b;
    static int Mul(int a, int b) => a * b;
    static int Div(int a, int b) => a / b;

    // 2. Multicast Delegate
    public delegate void Show();

    static void Method1() => Console.WriteLine("Method 1 Executed");
    static void Method2() => Console.WriteLine("Method 2 Executed");

    static void Main()
    {
        Console.WriteLine("===== 1. Calculator using Delegate =====");
        Calc c;

        c = Add;
        Console.WriteLine("Add: " + c(10, 5));

        c = Sub;
        Console.WriteLine("Sub: " + c(10, 5));

        c = Mul;
        Console.WriteLine("Mul: " + c(10, 5));

        c = Div;
        Console.WriteLine("Div: " + c(10, 5));


        Console.WriteLine("\n===== 2. Multicast Delegate =====");
        Show s = Method1;
        s += Method2;
        s(); // Calls both methods


        Console.WriteLine("\n===== 3. Lambda Expression (Replace Method) =====");
        Func<int, int> square = x => x * x;
        Console.WriteLine("Square of 5: " + square(5));


        Console.WriteLine("\n===== 4. Sort List using Lambda =====");
        List<int> numbers = new List<int> { 5, 2, 8, 1, 3 };

        numbers.Sort((a, b) => a.CompareTo(b));

        Console.WriteLine("Sorted List:");
        foreach (var n in numbers)
        {
            Console.WriteLine(n);
        }


        Console.WriteLine("\n===== 5 & 6. LINQ + Lambda Usage =====");

        List<int> nums = new List<int> { 1, 2, 3, 4, 5 };

        // LINQ with Lambda
        var result = nums.Where(n => n > 2);

        Console.WriteLine("Numbers greater than 2:");
        foreach (var n in result)
        {
            Console.WriteLine(n);
        }

        Console.WriteLine("\n--- End of Program ---");
    }
}
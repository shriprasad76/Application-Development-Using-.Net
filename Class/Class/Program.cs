using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("===== 1. GENERIC METHOD =====");
        Display<int>(10);
        Display<string>("Hello Generics");


        Console.WriteLine("\n===== 2. GENERIC CLASS =====");
        Box<int> intBox = new Box<int>(100);
        intBox.Show();

        Box<string> strBox = new Box<string>("CSharp");
        strBox.Show();


        Console.WriteLine("\n===== 3. LAMBDA EXPRESSION EXAMPLES =====");

        // Example 1
        Func<int, int> square = x => x * x;
        Console.WriteLine("Square of 5: " + square(5));

        // Example 2
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        var result = numbers.Where(n => n > 2);

        Console.WriteLine("Numbers > 2:");
        foreach (var n in result)
        {
            Console.WriteLine(n);
        }


        Console.WriteLine("\n===== 4. GENERIC CLASS WITH LAMBDA =====");

        Pair<int, int> pair = new Pair<int, int>(10, 20);

        // Lambda to perform addition
        Func<int, int, int> add = (a, b) => a + b;

        Console.WriteLine("First Value: " + pair.First);
        Console.WriteLine("Second Value: " + pair.Second);
        Console.WriteLine("Sum using Lambda: " + add(pair.First, pair.Second));
    }

    // Generic Method
    static void Display<T>(T value)
    {
        Console.WriteLine("Value: " + value);
    }
}

// Generic Class
class Box<T>
{
    public T Value;

    public Box(T value)
    {
        Value = value;
    }

    public void Show()
    {
        Console.WriteLine("Stored Value: " + Value);
    }
}

// Generic Class storing two values
class Pair<T1, T2>
{
    public T1 First;
    public T2 Second;

    public Pair(T1 first, T2 second)
    {
        First = first;
        Second = second;
    }
}
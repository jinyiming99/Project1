using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
public class Test1
{
    public struct Class1
    {
        public long a1, a2, a3, a4, a5, a6, a7;
        public int num;
        public long b1, b2, b3, b4, b5, b6, b7;
    }
    const int loop = 1000000;
    [Test]
    public static void Test_Fun1()
    {

        Stopwatch watch = new Stopwatch();
        Class1[] arr = new Class1[2] {new Class1(), new Class1()};
        
        watch.Start();
        Thread thread1 = new Thread(() =>
        {
            for (int i = 0; i < loop; i++)
            {
                arr[0].num = i;
            }
        });
        thread1.Start();
        
        Thread thread2 = new Thread(() =>
        {
            for (int i = 0; i < loop; i++)
            {
                arr[1].num = i;
            }
        });

        
        thread2.Start();
        thread1.Join();
        thread2.Join();
        watch.Stop();
        Console.WriteLine(watch.ElapsedTicks);
    }

    [Test]
    public static void Test_Fun2()
    {
        Stopwatch watch = new Stopwatch();
        int arrLength = 100;
        Class1[] list = new Class1[loop];
        for (int i = 0; i < loop; i++)
        {
            list[i] = new Class1();
            list[i].num = 0;
        }
        watch.Start();
        for (int i = 0; i < loop; i++)
        {
            list[i].num++;
        }
        
        
        watch.Stop();
        Console.WriteLine(watch.ElapsedTicks);
    }
}

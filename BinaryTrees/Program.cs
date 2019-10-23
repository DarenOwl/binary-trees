using System;
using System.Collections.Generic;

namespace BinaryTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("first ignore: ");
            MeasureTime(25, 50);
            Console.Write("\nN\t RANGE\t\tTIME BINARY TREE\tTIME SORT() FUNC\n");
            MeasureTime(25, 50);
            // 1000 чисел
            MeasureTime(1000, 10000);
            MeasureTime(1000, 500);
            // 5000 чисел
            MeasureTime(5000, 10000);
            MeasureTime(5000, 500);
            // 10 000 чисел
            MeasureTime(10000, 10000);
            MeasureTime(10000, 500);
        }

        static void MeasureTime(int count, int range)
        {
            var tree = new Tree();
            var rand = new Random();
            List<int> list = new List<int>();

            for (int i = 0; i < count; i++)
            {
                int key = rand.Next(0, range);
                tree.Add(key);
                list.Add(key);
            }

            //tree.Print(PrintStyle.Better);

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var sortedList = tree.SortedKeys();
            watch.Stop();
            Console.Write(count + "\t" + range + "\t\t");
            Console.Write(watch.Elapsed);

            watch.Restart();
            list.Sort();
            watch.Stop();
            Console.Write("\t");
            Console.WriteLine(watch.Elapsed);

            //foreach (var item in sortedList)
            //{
            //    Console.Write(item);
            //    Console.Write("; ");
            //}
        }
    }
}

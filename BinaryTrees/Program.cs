using System;
using System.Collections.Generic;

namespace BinaryTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            //Print_Trees();
            //Print_Trees_fixed();
            //DisplayHeight_Random();
            DisplayDelete_Small(15,5);
            //Task1_Compare_Time();
        }

        static Tree GenerateTree(int range, int count)
        {
            var tree = new Tree();
            var rand = new Random();

            for (int i = 0; i < count; i++)
            {
                int key = rand.Next(0, range);
                tree.Add(key);
            }

            return tree;
        }

        static void DisplayHeight_Random()
        {
            var tree = GenerateTree(15, 5);
            tree.Print();
            Console.WriteLine("Height:{0}", tree.Height());
        }

        static void DisplayDelete_Small(int range, int count)
        {
            var tree = GenerateTree(range, count);
            tree.Print();
            var key = new Random().Next(range);           
            Console.WriteLine("I want to delete {0} key",key);
            tree.Delete(key);
            tree.Print();
            //Console.WriteLine("Height:{0}", tree.Height());
        }

        static void Task1_Compare_Time()
        {
            MeasureTime(25, 50);
            Console.Clear();
            Console.Write("\nCOUNT\tRANGE\tTIME:\tADD TIME\tBT SORT\tSORT() FUNC\n\n");
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

        static void Print_Trees()
        {
            var tree = new Tree();
            var rand = new Random();
            int[] t1 = new int[] { 1, 4, 5, 7, 12, 13, 15, 14, 20, 16, 26, 32, 34, 32, 33, 37, 38, 38, 40, 39, 39, 44, 43, 48, 46 };

            var count = 25;
            var range = 50;

            for (int i = 0; i < count; i++)
            {
                int key = rand.Next(0, range);
                tree.Add(key);
            }

            tree.Print();
        }

        static void Print_Trees_fixed()
        {
            var tree = new Tree();
            int[] keys = new int[] { 1, 4, 5, 7, 12, 13, 15, 14, 20, 16, 26, 32, 34, 32, 33, 37, 38, 38, 40, 39, 39, 44, 43, 48, 46 };
            int[] keys2 = new int[] { 37, 1, 40, 32, 39, 44, 13, 34, 38, 39, 43, 48, 46, 38, 7, 15, 32, 5, 12, 14, 20, 33, 4, 16, 26 };

            var count = keys.Length;

            for (int i = 0; i < count; i++)
            {
                tree.Add(keys2[i]);
            }

            tree.Print();
            foreach (var key in tree.SortedKeys())
            {
                Console.Write(key + ",");
            }
            Console.WriteLine("({0})", count);
        }

        static void MeasureTime(int count, int range)
        {
            var tree = new Tree();
            var rand = new Random();
            List<int> list = new List<int>();
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            for (int i = 0; i < count; i++)
            {
                int key = rand.Next(0, range);
                tree.Add(key);
                list.Add(key);
            }
            watch.Stop();
            Console.Write(count + "\t" + range + "\t  |\t");
            Console.Write(watch.Elapsed.TotalMilliseconds);
            //tree.Print(PrintStyle.Better);

            watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var sortedList = tree.SortedKeys();
            watch.Stop();
            Console.Write("\t\t");
            Console.Write(watch.Elapsed.TotalMilliseconds);

            watch.Restart();
            list.Sort();
            watch.Stop();
            Console.Write("\t\t");
            Console.WriteLine(watch.Elapsed.TotalMilliseconds);

            //foreach (var item in sortedList)
            //{
            //    Console.Write(item);
            //    Console.Write("; ");
            //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTrees
{
    public static class TreeExtension
    {
        public static void Print(this Tree tree, PrintStyle printStyle) // дерево может не вместиться в консоль, тогда будет ошибка
        {
            if (tree.Root == null)
            {
                Console.WriteLine("tree is empty");
                return;
            }

            var nodes = new Queue<TreeNode>();
            nodes.Enqueue(tree.Root);
            List<List<TreeNode>> levels = new List<List<TreeNode>>();
            int height = 0;
            int width = 1;
            while (true)
            {
                var isEnd = true;
                levels.Add(new List<TreeNode>());

                for (int i = 0; i < width; i++)
                {
                    var current = nodes.Dequeue();

                    if (current == null)
                    {
                        nodes.Enqueue(null);
                        nodes.Enqueue(null);
                        levels[height].Add(null);
                        continue;
                    }
                    else
                    {
                        levels[height].Add(current);

                        if (current.Left != null)
                        {
                            nodes.Enqueue(current.Left);
                            isEnd = false;
                        }
                        else nodes.Enqueue(null);

                        if (current.Right != null)
                        {
                            nodes.Enqueue(current.Right);
                            isEnd = false;
                        }
                        else nodes.Enqueue(null);
                    }
                }
                width *= 2;
                height++;
                if (isEnd) break;
            }

            Console.Clear();
            if (printStyle == PrintStyle.Horisontal)
                PrintHorisontal(tree, height, levels);
            else if (printStyle == PrintStyle.Vertical)
                PrintVertical(tree, height, levels);
            else if (printStyle == PrintStyle.Simple)
                PrintSimple(tree, height, levels);
            else if (height < 5)
                PrintHorisontal(tree, height, levels);
            else if (height < 9)
                PrintVertical(tree, height, levels);
            else
                PrintSimple(tree, height, levels);

        }

        static void PrintHorisontal(Tree tree, int height, List<List<TreeNode>> levels)
        {
            var length = tree.Max().ToString().Length;
            var maxHeight = height;

            foreach (var level in levels)
            {
                var count = 0;
                var shift = length * ((int)Math.Pow(2, height) - 1);

                bool isLeft = true;

                foreach (var item in level)
                {
                    if (item != null)
                    {
                        var left = shift + count * 2 * (shift + length);
                        var top = 2 * (maxHeight - height);
                        Console.SetCursorPosition(left, top);

                        Console.Write(item.Key);
                        if (isLeft && top > 0)
                        {
                            Console.SetCursorPosition(left + length, top - 1);
                            Console.Write('/');
                            for (int i = 0; i < shift; i++)
                                Console.Write('-');
                        }
                        else if (top > 0)
                        {
                            Console.SetCursorPosition(left - shift - 1, top - 1);
                            for (int i = 0; i < shift; i++)
                                Console.Write('-');
                            Console.Write('\\');
                        }
                    }
                    count++;
                    isLeft = !isLeft;
                }
                height--;
            }
            Console.SetCursorPosition(0, maxHeight*2);
        }

        static void PrintVertical(Tree tree, int height, List<List<TreeNode>> levels)
        {
            var length = tree.Max().ToString().Length;
            var maxHeight = height;

            var rightHeight = (int)Math.Pow(2, maxHeight - tree.RightHeight()-1)-1;

            foreach (var level in levels)
            {
                var count = 0;
                var shift = (int)Math.Pow(2, height-1) - 1;

                bool isLeft = false;

                foreach (var item in level.Reverse<TreeNode>())
                {
                    if (item != null)
                    {
                        var top = shift + count * 2 * (shift + 1) + 2 - rightHeight;
                        var left = (maxHeight - height)*(length + 1);
                        Console.SetCursorPosition(left, top);

                        Console.Write(item.Key);
                        if (isLeft && top >= length && left > 1)
                        {
                            Console.SetCursorPosition(left-1, top);
                            Console.Write('\\');
                            for (int i = 0; i < shift; i++)
                            {
                                top--;
                                Console.SetCursorPosition(left - 2, top);
                                Console.Write('|');
                            }
                        }
                        else if (left > 1)
                        {
                            Console.SetCursorPosition(left - 1, top);
                            Console.Write('/');
                            for (int i = 0; i < shift; i++)
                            {
                                top++;
                                Console.SetCursorPosition(left - 2, top);
                                Console.Write('|');
                            }
                        }
                    }
                    count++;
                    isLeft = !isLeft;
                }
                Console.SetCursorPosition(0, 0);
                height--;
            }
        }

        static void PrintSimple(Tree tree, int height, List<List<TreeNode>> levels)
        {
            foreach (var level in levels)
            {
                var isLeft = true;
                foreach (var item in level)
                {
                    if (item != null)
                    {
                        Console.Write(item.Key);
                        if (item.Parent == null) continue;
                        if (isLeft)
                            Console.Write("[l.");
                        else
                            Console.Write("[r.");
                        Console.Write(item.Parent.Key);
                        Console.Write("] ");
                    }
                    isLeft = !isLeft;
                }
                Console.WriteLine();
            }
        }

        static int RightHeight(this Tree tree)
        {
            var current = tree.Root;
            var height = 0;
            while (true)
            {
                if (current.Right == null) return height;
                else current = current.Right;
                height++;
            }
        }
    }
}

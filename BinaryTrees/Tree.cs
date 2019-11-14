using System.Collections.Generic;
using System;

namespace BinaryTrees
{
    public class TreeNode
    {
        public int Key;
        public TreeNode Left = null, Right = null, Parent = null;

        public TreeNode(int key)
        {
            Key = key;
        }
    }

    public class Tree
    {
        TreeNode root = null;
        public TreeNode Root { get { return root; } }

        public void Add(int key)
        {
            if (Root == null)
            {
                root = new TreeNode(key);
                return;
            }
            var current = Root;

            while (true)
            {
                if (key < current.Key)
                    if (current.Left == null)
                    {
                        var newNode = new TreeNode(key);
                        newNode.Parent = current;
                        current.Left = newNode;
                        return;
                    }
                    else
                        current = current.Left;
                else
                    if (current.Right == null)
                    {
                        var newNode = new TreeNode(key);
                        newNode.Parent = current;
                        current.Right = newNode;
                        return;
                    }
                else
                    current = current.Right;
            }
        }

        public List<int> SortedKeys()
        {
            var result = new List<int>();
            var tovisit = new Stack<TreeNode>();
            if (root != null)
                tovisit.Push(Root);

            bool branchVisited = false;

            while (tovisit.Count != 0)
            {
                var current = tovisit.Peek();
                if (current.Left != null && !branchVisited)
                    tovisit.Push(current.Left);
                else if (current.Right != null)
                {
                    result.Add(current.Key);
                    tovisit.Pop();
                    tovisit.Push(current.Right);
                    branchVisited = false;
                }
                else
                {
                    result.Add(current.Key);
                    tovisit.Pop();
                    branchVisited = true;
                }
            }
            return result;
        }

        public int Max()
        {
            var current = Root;
            while(true)
                if (current.Right == null) return current.Key;
                else current = current.Right;
        }

        public int Min()
        {
            var current = Root;
            while (true)
                if (current.Left == null) return current.Key;
                else current = current.Left;
        }

        public int Height()
        {
            var queue = new Queue<TreeNode>();
            if (root != null) queue.Enqueue(root);

            var levelWidth = 1;
            var height = 0;

            while (queue.Count != 0)
            {
                var levW = 0;
                for (int i = 0; i < levelWidth; i++)
                {
                    var current = queue.Dequeue();
                    if (current.Left != null)
                    {
                        queue.Enqueue(current.Left);
                        levW++;
                    }
                    if (current.Right != null)
                    {
                        queue.Enqueue(current.Right);
                        levW++;
                    }
                }
                height++;
                levelWidth = levW;
            }
            return height;
        }

        public void Delete(int key)
        {
            bool isDeleted = false;
            var current = Root;

            while (current != null)
                if (key < current.Key)
                        current = current.Left;
                else if (key > current.Key)
                        current = current.Right;
                else
                {
                    Delete(current);
                    current = current.Right;
                    isDeleted = true;
                }

            if (isDeleted)
                Console.WriteLine("The key {0} was deleted successfully!", key);
            else
                Console.WriteLine("The key {0} was not found", key);
        }

        void Delete(TreeNode current)
        {
            TreeNode newNode = null;

            if (current.Right != null)
            {
                newNode = current.Right;
                if (current.Left != null)
                {
                    var min = newNode;
                    while (min.Left != null)
                        min = min.Left;
                    min.Left = current.Left;
                }
            }
            else if (current.Left != null)
                newNode = current.Left;

            if (newNode != null)
                newNode.Parent = current.Parent;

            if (current.Parent == null)
                root = newNode;
            else
                if (current.Key < current.Parent.Key)
                    current.Parent.Left = newNode;
                else
                    current.Parent.Right = newNode;
        }
    }
}

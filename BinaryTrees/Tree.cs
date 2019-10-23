using System.Collections.Generic;

namespace BinaryTrees
{
    public class TreeNode
    {
        public int Key;
        public TreeNode Left, Right, Parent;

        public TreeNode(int key)
        {
            Key = key;
        }
    }

    public class Tree
    {
        TreeNode root;
        public TreeNode Root { get { return root; } }

        List<List<int>> levelToPrint = new List<List<int>>();

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
            {
                if (current.Right == null) return current.Key;
                else current = current.Right;
            }

        }
    }
}

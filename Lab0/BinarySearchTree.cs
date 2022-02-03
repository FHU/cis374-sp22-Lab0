using System;
using System.Collections.Generic;

namespace Lab0
{
    public class BinarySearchTree<T> : IBinarySearchTree<T>
    {
        public BinarySearchTreeNode<T> Root { get; set; }

        public int Count { get; private set; }

        public BinarySearchTree()
        {
            Root = null;
            Count = 0;
        }

        public bool IsEmpty => Root == null;

        public int Height
        {
            get
            {
                int height = 0;
                int depth = 0;

                if (IsEmpty)
                {
                    return height;
                }

                return GetHeightRecursive(height, Root, depth);
            }
        }

        private int GetHeightRecursive(int height, BinarySearchTreeNode<T> node, int depth)
        { 
            if (node.Left != null)
            {
                depth += 1;
                height = GetHeightRecursive(height, node.Left, depth);
                depth -= 1;
            }
            if (node.Right != null)
            {
                depth += 1;
                height = GetHeightRecursive(height, node.Right, depth);
                depth -= 1;
            }

            if (depth > height)
            {
                height = depth;
            }

            return height;
        }

        public int MinKey
        {
            get
            {
                if (IsEmpty)
                {
                    return default;
                }

                return MinKeyRecursive(Root);
            }
        }

        private int MinKeyRecursive(BinarySearchTreeNode<T> node)
        {
            if (node == null)
            {
                return default;
            }

            if (node.Left == null)
            {
                return node.Key;
            }

            return MinKeyRecursive(node.Left);
        }

        public Tuple<int, T> Min
        {
            get
            {
                if (IsEmpty)
                {
                    return default;
                }

                return MinNodeRecursive(Root);
            }
        }


        private Tuple<int, T> MinNodeRecursive(BinarySearchTreeNode<T> node)
        {
            if (node == null)
            {
                return null;
            }

            if (node.Left == null)
            {
                return Tuple.Create(node.Key, node.Value);
            }

            return MinNodeRecursive(node.Left);
        }

        public int MaxKey
        {
            get
            {
                if (IsEmpty)
                {
                    return default;
                }

                return MaxKeyRecursive(Root);
            }
        }

        private int MaxKeyRecursive(BinarySearchTreeNode<T> node)
        {
            if (node == null)
            {
                return default;
            }

            if (node.Right == null)
            {
                return node.Key;
            }

            return MaxKeyRecursive(node.Right);
        }

        public Tuple<int, T> Max
        {
            get
            {
                if (IsEmpty)
                {
                    return default;
                }

                return MaxRecursive(Root);
            }
        }

        private Tuple<int, T> MaxRecursive(BinarySearchTreeNode<T> node)
        {
            if (node.Right == null)
            {
                return Tuple.Create(node.Key, node.Value);
            }

            node = node.Right;

            return MaxRecursive(node);
        }

        public double MedianKey
        {
            get
            {
                if (IsEmpty)
                {
                    return default;
                }

                List<int> OrderedKeys = InOrderKeys;

                double Midpoint = Count / 2;

                if (Count % 2 == 0)
                {
                    int low = (int)(Midpoint - 0.5);
                    int high = (int)(Midpoint + 0.5);

                    return (double)(OrderedKeys[low] + OrderedKeys[high]) / 2;
                }
                else
                {
                    int MidInt = (int)Midpoint;

                    return OrderedKeys[MidInt];
                }
            }
        }

        public void Add(int key, T value) 
        {
            var node = new BinarySearchTreeNode<T>(key, value);

            Count++;

            if (IsEmpty)
            {
                Root = node;
            }
            else
            {
                AddRecursive(Root, node);
            }

        }

        private void AddRecursive(BinarySearchTreeNode<T> parent, BinarySearchTreeNode<T> node)
        {
            // If duplicate found
            // Do not add to BST
            if(node.Key == parent.Key)
            {
                // Correct Count
                Count--;
                return;
            }

            if( node.Key < parent.Key)
            {
                if (parent.Left == null)
                {
                    parent.Left = node;
                    node.Parent = parent;
                }
                else
                {
                    AddRecursive(parent.Left, node);
                }
            }
            else
            {
                if(parent.Right == null)
                {
                    parent.Right = node;
                    node.Parent = parent;
                }
                else
                {
                    AddRecursive(parent.Right, node);
                }

            }
        }

        public void Clear()
        {
            Root = null;
        }

        public bool Contains(int key)
        {
            if (IsEmpty)
            {
                return false;
            }

            BinarySearchTreeNode<T> node = Root;

            while (true)
            {
                if (key == node.Key)
                {
                    return true;
                }
                else if (key < node.Key)
                {
                    if (node.Left == null)
                    {
                        return false;
                    }
                    node = node.Left;
                }
                else
                {
                    if (node.Right == null)
                    {
                        return false;
                    }
                    node = node.Right;
                }
            }
        }

        public BinarySearchTreeNode<T> GetNode(int key)
        {
            return GetNodeRecursive(Root, key);
        }

        private BinarySearchTreeNode<T> GetNodeRecursive(BinarySearchTreeNode<T> node, int key)
        {
            if(node == null)
            {
                return null;
            }

            if( key == node.Key)
            {
                return node;
            }
            else if(key < node.Key)
            {
                return GetNodeRecursive(node.Left, key);
            }
            else
            {
                return GetNodeRecursive(node.Right, key);
            }
        }

        public List<int> InOrderKeys
        {
            get
            {
                List<int> keys = new List<int>();

                InOrderKeysRecursive(Root, keys);

                return keys;
            }
        }

        private void InOrderKeysRecursive(BinarySearchTreeNode<T> node, List<int> keys)
        {
            if (node == null)
            {
                return;
            }

            InOrderKeysRecursive(node.Left, keys);
            keys.Add(node.Key);
            InOrderKeysRecursive(node.Right, keys);
        }

        public List<int> PreOrderKeys
        {
            get
            {
                List<int> keys = new List<int>();

                PreOrderKeysRecursive(Root, keys);

                return keys;
            }
        }

        private void PreOrderKeysRecursive(BinarySearchTreeNode<T> node, List<int> keys)
        {
            if (node == null)
            {
                return;
            }

            keys.Add(node.Key);
            PreOrderKeysRecursive(node.Left, keys);
            PreOrderKeysRecursive(node.Right, keys);
        }

        public List<int> PostOrderKeys
        {
            get
            {
                List<int> keys = new List<int>();

                PostOrderKeysRecursive(Root, keys);

                return keys;
            }
        }

        private void PostOrderKeysRecursive(BinarySearchTreeNode<T> node, List<int> keys)
        {
            if (node == null)
            {
                return;
            }

            PostOrderKeysRecursive(node.Left, keys);
            PostOrderKeysRecursive(node.Right, keys);
            keys.Add(node.Key);
        }

        public BinarySearchTreeNode<T> Next(BinarySearchTreeNode<T> node)
        {
            if (IsEmpty || !Contains(node.Key))
            {
                return null;
            }

            List<int> keys = InOrderKeys;

            if (keys.IndexOf(node.Key) + 1 > keys.Count - 1)
            {
                return null;
            }

            int NextKey = keys[keys.IndexOf(node.Key) + 1];

            BinarySearchTreeNode<T> NextNode= GetNode(NextKey);

            return NextNode;
        }

        public BinarySearchTreeNode<T> Prev(BinarySearchTreeNode<T> node)
        {
            if (IsEmpty || !Contains(node.Key))
            {
                return null;
            }

            List<int> keys = InOrderKeys;

            if (keys.IndexOf(node.Key) - 1 < 0)
            {
                return null;
            }

            int PrevKey = keys[keys.IndexOf(node.Key) - 1];

            BinarySearchTreeNode<T> PrevNode = GetNode(PrevKey);

            return PrevNode;
        }

        public void Remove(int key)
        {
            BinarySearchTreeNode<T> parent = null;
            BinarySearchTreeNode<T> current = Root;

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    if (current.Left == null && current.Right == null)
                    {
                        if (parent == null)
                        {
                            Root = null;
                        }
                        else if (parent.Left == current)
                        {
                            parent.Left = null;
                        }
                        else
                        {
                            parent.Right = null;
                        }
                    }
                    else if (current.Right == null)
                    {
                        if (parent == null)
                        {
                            Root = current.Left;
                        }
                        else if (parent.Left == current)
                        {
                            parent.Left = current.Left;
                            parent.Left.Parent = parent;
                        }
                        else
                        {
                            parent.Right = current.Left;
                            parent.Right.Parent = parent;
                        }
                    }
                    else if (current.Left == null)
                    {
                        if (parent == null)
                        {
                            Root = current.Right;
                        }
                        else if (parent.Left == current)
                        {
                            parent.Left = current.Right;
                            parent.Left.Parent = parent;
                        }
                        else
                        {
                            parent.Right = current.Right;
                            parent.Right.Parent = parent;
                        }
                    }
                    else
                    {
                        BinarySearchTreeNode<T> successor = current.Right;

                        while (successor.Left != null)
                        {
                            successor = successor.Left;
                        }

                        int successorKey = successor.Key;
                        T successorValue = successor.Value;

                        Remove(successor.Key);

                        current.Key = successorKey;
                        current.Value = successorValue;
                    }

                    return;
                }
                else if (current.Key < key)
                {
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    parent = current;
                    current = current.Left;
                }
            }
        }

        public T Search(int key)
        {
            return GetNode(key).Value;
        }

        public void Update(int key, T value)
        {
            if(Contains(key))
            {
                // update value
                var node = GetNode(key);
                node.Value = value;
            }
        }

        public List<BinarySearchTreeNode<T>> RangeSearch(int min, int max)
        {
            if (min < InOrderKeys[0] || max > InOrderKeys[InOrderKeys.Count - 1])
            {
                return null;
            }

            List<BinarySearchTreeNode<T>> Nodes = new List<BinarySearchTreeNode<T>>();

            for (int i = min; i <= max; i++)
            {
                if (Contains(i))
                {
                   Nodes.Add(GetNode(i));
                }
            }

            return Nodes;
        }
    }
}

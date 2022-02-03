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

        // TODO
        public int Height
        {
            get
            {
                if (IsEmpty)
                {
                    return 0;
                }
                return HeightRecursive(Root);
            }
        }

        private int HeightRecursive(BinarySearchTreeNode<T> node)
        {
            if (node is null)
            {
                return -1;
            }
            int leftHeight = HeightRecursive(node.Left);
            int rightHeight = HeightRecursive(node.Right);
            return 1 + Math.Max(leftHeight, rightHeight);
        }

        // TODO
        public int MinKey
        {
            get
            {
                if (IsEmpty)
                {
                    return -1;
                }
                BinarySearchTreeNode<T> minNode = MinNodeRecursive(Root);

                if (minNode == null)
                {
                    return -1;
                }
                else
                {
                    return minNode.Key;
                }
            }
        }

        // TODO
        public Tuple<int, T> Min
        {
            get
            {
                if(IsEmpty)
                {
                    return null;
                }
                else
                {
                    BinarySearchTreeNode<T> minNode = MinNodeRecursive(Root);
                    return Tuple.Create(minNode.Key, minNode.Value);
                }
            }
        }

        private BinarySearchTreeNode<T> MinNodeRecursive(BinarySearchTreeNode<T> node)
        {
            if (node == null)
            {
                return null;
            }
            else if (node.Left == null)
            {
                return node;
            }
            else {
                return MinNodeRecursive(node.Left);
            }
        }

        // TODO
        public int MaxKey
        {
            get
            {
                if (IsEmpty)
                {
                    return -1;
                }
                BinarySearchTreeNode<T> maxNode = MaxNodeRecursive(Root);

                if (maxNode == null)
                {
                    return -1;
                }
                else
                {
                    return maxNode.Key;
                }
            }
        }

        // TODO
        public Tuple<int, T> Max {
            get
            {
                if (IsEmpty)
                {
                    return null;
                }
                BinarySearchTreeNode<T> maxNode = MaxNodeRecursive(Root);

                if (maxNode == null)
                {
                    return null;
                }
                else
                {
                    return Tuple.Create(maxNode.Key, maxNode.Value);
                }

            }
        }

        private BinarySearchTreeNode<T> MaxNodeRecursive(BinarySearchTreeNode<T> node)
        {
            if (node == null)
            {
                return null;
            }
            else if (node.Right == null)
            {
                return node;
            }

            return MinNodeRecursive(node.Right);
        }

        // TODO
        public double MedianKey
        {
            get
            {
                if (IsEmpty)
                {
                    return -1;
                }
                List<int> keys = InOrderKeys;
                if (keys.Count % 2 == 0)
                {
                    double middle1 = keys[keys.Count / 2];
                    double middle2 = keys[(keys.Count / 2) - 1];
                    return (middle1 + middle2) / 2;
                }
                double middle = keys[keys.Count / 2];
                return middle;
            }
        }

        // TODO

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
            // Duplicate found
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

        // TODO
        public bool Contains(int key)
        {
            if (GetNodeRecursive(Root, key) == null)
            {
                return false;
            }
            return true;
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


        // TODO
        public List<int> InOrderKeys {
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

        // TODO
        public List<int> PreOrderKeys { get
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

        // TODO
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

        // TODO
        public BinarySearchTreeNode<T> Next(BinarySearchTreeNode<T> node)
        {
            List<int> ordered = InOrderKeys;
            int number = ordered.IndexOf(node.Key);
            if (number == ordered.Count-1)
            {
                return null;
            }
            int key = ordered[number+1];
            return GetNode(key);
        }

        // TODO
        public BinarySearchTreeNode<T> Prev(BinarySearchTreeNode<T> node)
        {
            List<int> ordered = InOrderKeys;
            int number = ordered.IndexOf(node.Key);
            if (number == 0)
            {
                return null;
            }
            int key = ordered[number - 1];
            return GetNode(key);
        }

        // TODO
        public void Remove(int key)
        {
            if (Contains(key) == true)
            {
                BinarySearchTreeNode<T> node = GetNode(key);
                BinarySearchTreeNode<T> parent = node.Parent;

                if (node.Left == null && node.Right == null)
                {
                    if (parent.Left == node)
                    {
                        parent.Left = null;
                        node.Parent = null;
                    }
                    else if (parent.Right == node)
                    {
                        parent.Right = null;
                        node.Parent = null;
                    }
                }
                if (node.Left == null && node.Right != null)
                {
                    BinarySearchTreeNode<T> child = node.Right;
                    if (parent.Left == node)
                    {
                        parent.Left = child;
                        child.Parent = parent;
                    }
                    else if (parent.Right == node)
                    {
                        parent.Right = child;
                        child.Parent = parent;
                    }
                }
                if (node.Left != null && node.Right == null)
                {
                    BinarySearchTreeNode<T> child = node.Left;
                    if (parent.Left == node)
                    {
                        parent.Left = child;
                        child.Parent = parent;
                    }
                    else if (parent.Right == node)
                    {
                        parent.Right = child;
                        child.Parent = parent;
                    }
                }
            }

            
        }

        // TODO
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
            List<int> ordered = InOrderKeys;
            List<int> order = new List<int>();
            foreach (int key in ordered)
            {
                if((min <= key) && (max >= key))
                {
                    order.Add(key);
                }
                if(min == max)
                {
                    if(min == key)
                    {
                        BinarySearchTreeNode<T> node = GetNode(key);
                        List<BinarySearchTreeNode<T>> newone = new List<BinarySearchTreeNode<T>>();
                        newone.Add(node);
                        return newone;

                    }
                }
            }
            List<BinarySearchTreeNode<T>> range  = new List<BinarySearchTreeNode<T>>();
            foreach (int key in order)
            {
                if(Contains(key))
                {
                    BinarySearchTreeNode<T> node = GetNode(key);
                    range.Add(node);
                }
            }
            return range;
        }
    }
}

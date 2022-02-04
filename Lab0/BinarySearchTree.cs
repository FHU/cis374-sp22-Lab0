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
                return GetHeight(Root);
            }
        }

        private int GetHeight(BinarySearchTreeNode<T> hnode)
        {
            if (hnode == null)
                return -1;
            int leftHeight = GetHeight(hnode.Left);
            int rightHeight = GetHeight(hnode.Right);
            return 1 + Math.Max(leftHeight, rightHeight);
        }

        // TODO
        public int MinKey
        {
            get
            {
                BinarySearchTreeNode<T> node = Root;
                while (!node.Left.Equals(null))
                    node = node.Left;
                return node.Key;
            }
        }

        // TODO
        public Tuple<int, T> Min
        {
            get
            {
                if (IsEmpty)
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
            else
            {
                return MinNodeRecursive(node.Left);
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
            else
            {
                return MaxNodeRecursive(node.Left);
            }
        }

        // TODO
        public int MaxKey
        {
            get
            {
                BinarySearchTreeNode<T> node = Root;
                while (!node.Right.Equals(null))
                    node = node.Right;
                return node.Key;
            }
        }

        // TODO
        public Tuple<int, T> Max
        {
            get
            {
                if (IsEmpty)
                {
                    return null;
                }
                else
                {
                    BinarySearchTreeNode<T> maxNode = MaxNodeRecursive(Root);
                    return Tuple.Create(maxNode.Key, maxNode.Value);
                }
            }
        }

        // TODO
        public double MedianKey
        {
            get
            {
                if (Count % 2 != 0)
                    return GetNode(InOrderKeys[InOrderKeys.Count / 2]).Key;
                else
                    return (GetNode(InOrderKeys[InOrderKeys.Count / 2]).Key) - 0.5;
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
            // Duplicate found
            // Do not add to BST
            if (node.Key == parent.Key)
            {
                // Correct Count
                Count--;
                return;
            }

            if (node.Key < parent.Key)
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
                if (parent.Right == null)
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
            BinarySearchTreeNode<T> node = Root;
            while (!Prev(node).Equals(null))
            {
                if (node.Key == key)
                    return true;
                node = Prev(node);
            }
            node = Next(Root);
            while (!Next(node).Equals(null))
            {
                if (node.Key == key)
                    return true;
                node = Next(node);
            }
            return false;
        }

        public BinarySearchTreeNode<T> GetNode(int key)
        {
            return GetNodeRecursive(Root, key);
        }

        private BinarySearchTreeNode<T> GetNodeRecursive(BinarySearchTreeNode<T> node, int key)
        {
            if (node == null)
            {
                return null;
            }

            if (key == node.Key)
            {
                return node;
            }
            else if (key < node.Key)
            {
                return GetNodeRecursive(node.Left, key);
            }
            else
            {
                return GetNodeRecursive(node.Right, key);
            }
        }


        // TODO
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

        // TODO
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

        // TODO
        public List<int> PostOrderKeys { 
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
            return GetNode(node.Key + 1);
        }

        // TODO
        public BinarySearchTreeNode<T> Prev(BinarySearchTreeNode<T> node)
        {
            return GetNode(node.Key - 1);
        }

        // TODO
        public void Remove(int key)
        {
            BinarySearchTreeNode<T> node = Root;
            BinarySearchTreeNode<T> parent = node.Parent;
            while (!node.Equals(null))
            {
                if (node.Key == key)
                {
                    if (node.Left == null)
                    {
                        if (node.Right == null)
                        {
                            if (parent.Equals(null))
                                Root = null;
                            else if (parent.Left == node)
                                parent.Left = null;
                            else
                                parent.Right = null;
                        }
                        else
                        {
                            if (parent.Equals(null))
                                Root = node.Right;
                            else if (parent.Left == node)
                                parent.Left = node.Right;
                            else
                                parent.Right = node.Right;
                        }
                    }
                    else
                    {
                        if (node.Right == null)
                        {
                            if (parent.Equals(null))
                                Root = node.Left;
                            else if (parent.Left == node)
                                parent.Left = node.Left;
                            else
                                parent.Right = node.Right;
                        }
                        else
                        {
                            BinarySearchTreeNode<T> suc = node.Right;
                            while (!suc.Left.Equals(null))
                                suc = suc.Left;
                            T sucData = suc.Value;
                            Remove(suc.Key);
                            node = new BinarySearchTreeNode<T>(node.Key, sucData);
                        }
                    }
                    break;
                }
                else if (node.Key < key)
                {
                    parent = node;
                    node = node.Right;
                }
                else
                {
                    parent = node;
                    node = node.Left;
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
            if (Contains(key))
            {
                // update value
                var node = GetNode(key);
                node.Value = value;
            }
        }

        public List<BinarySearchTreeNode<T>> RangeSearch(int min, int max)
        {
            List<BinarySearchTreeNode<T>> rangeList = new();

            List<int> all = new();
            for (int i = min; i < (max + 1); i++)
                all.Add(i);
            foreach(int key in InOrderKeys)
            {
                if (all.Contains(key))
                    rangeList.Add(GetNode(key));
            }

            return rangeList;
        }
    }
}

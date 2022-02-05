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
        public int Height => BSTGetHeight(Root);
      

        public int BSTGetHeight(BinarySearchTreeNode<T> node)
        {
            if (IsEmpty)
            {
                return 0;

            }
            int leftHeight = BSTGetHeight(node.Left);
            int rightHeight = BSTGetHeight(node.Right);

            return 1 + Math.Max(leftHeight, rightHeight);

        }

        // TODO
        public int MinKey => GetMinKey(Root);

        public int GetMinKey(BinarySearchTreeNode<T> node)
        {
            if(node.Left == null)
            {
                return node.Key;
            }
            else
            {
                return GetMinKey(node.Left);
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

        // TODO
        public int MaxKey => throw new NotImplementedException();

        // TODO
        public Tuple<int, T> Max => throw new NotImplementedException();

        // TODO
        public double MedianKey => throw new NotImplementedException();

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
            BinarySearchTreeNode<T> node = GetNode(key);

            if (node.Key == key)
            {
                return true;
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
            int num = node.Key;
            while(GetNode(num) == null)
            {
                num++;

            }

            return GetNode(num);
            


        }

        // TODO
        public BinarySearchTreeNode<T> Prev(BinarySearchTreeNode<T> node)
        {
            int num = node.Key;
            while (GetNode(num) == null)
            {
                num--;

            }

            return GetNode(num);
        }

        // TODO
        public void Remove(int key)
        {
            throw new NotImplementedException();
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
            List<BinarySearchTreeNode<T>> list = new List<BinarySearchTreeNode<T>>();

            for (int i = min; i < (max + 1); i++)
            {
                if(GetNode(i) != null)
                {
                    list.Add(GetNode(i));
                }
            }

            return list;
        }
    }
}

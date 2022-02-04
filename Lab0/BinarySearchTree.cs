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
                if (IsEmpty)
                {
                    return -1;
                }
                else
                {
                    return HeightRecursive(Root);
                }
            }
        }

        private int HeightRecursive(BinarySearchTreeNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }
            else
            {
                var leftHeight = HeightRecursive(node.Left);
                var rightHeight = HeightRecursive(node.Right);
                return 1 + Math.Max(leftHeight, rightHeight);
            }
        }

        public int MinKey
        {
            get
            {
                if (IsEmpty)
                {
                    return -1;
                }
                else
                {
                    return MinKeyRecursive(Root);
                }
            }
        }

        private int MinKeyRecursive(BinarySearchTreeNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }
            else if (node.Left == null)
            {
                return node.Key;
            }
            else
            {
                return MinKeyRecursive(node.Left);
            }
        }

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

        public int MaxKey
        {
            get
            {
                if (IsEmpty)
                {
                    return -1;
                }
                else
                {
                    return MaxKeyRecursive(Root);
                }
            }
        }

        private int MaxKeyRecursive(BinarySearchTreeNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }
            else if (node.Right == null)
            {
                return node.Key;
            }
            else
            {
                return MaxKeyRecursive(node.Right);
            }
        }

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
                return MaxNodeRecursive(node.Right);
            }
        }

        public double MedianKey
        {
            get
            {
                var allKeys = InOrderKeys;

                if (IsEmpty)
                {
                    return -1;
                }
                else if (allKeys.Count % 2 == 0)
                {
                    var index = allKeys.Count / 2;
                    return (double)(allKeys[index] + allKeys[index - 1]) / 2;
                }
                else
                {
                    return allKeys[allKeys.Count / 2];
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

        public bool Contains(int key)
        {
            var node = GetNode(key);

            if (node == null)
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
            // https://www.geeksforgeeks.org/inorder-successor-in-binary-search-tree/
            // idk my brain wasn't working and I couldn't figure it out myself
            if (node.Right != null)
            {
                return MinNodeRecursive(node.Right);
            }

            // wouldn't you know it, I forgot the case where the next would be the root or a subtree root
            var parent = node.Parent;
            while (parent != null && node == parent.Right)
            {
                node = parent;
                parent = parent.Parent;
            }

            return parent;
        }

        public BinarySearchTreeNode<T> Prev(BinarySearchTreeNode<T> node)
        {
            if (node == null)
            {
                return null;
            }
            else if (node.Left != null)
            {
                return MaxNodeRecursive(node.Left);
            }
            else
            {
                return node.Parent;
            }
        }

        // https://learn.zybooks.com/zybook/FHUCIS273CaseyFall2021/chapter/5/section/6
        // RIP my functioning brain, this method took it all away
        public void Remove(int key)
        {
            var current = Root;
            var parent = Root.Parent;

            while (current != null) // search for node by key
            {
                if (current.Key == key) // node found, do the thing
                {
                    // case leaf
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
                    // case left child only
                    else if (current.Right == null)
                    {
                        if (parent == null)
                        {
                            Root = current.Left;
                            Root.Parent = null;
                        }
                        else if (parent.Left == current)
                        {
                            parent.Left = current.Left;
                            current.Left.Parent = parent;
                        }
                        else
                        {
                            parent.Right = current.Left;
                            current.Left.Parent = parent;
                        }
                    }
                    // case right child only
                    else if (current.Left == null)
                    {
                        if (parent == null)
                        {
                            Root = current.Right;
                            Root.Parent = null;
                        }
                        else if (parent.Left == current)
                        {
                            parent.Left = current.Right;
                            current.Right.Parent = parent;
                        }
                        else
                        {
                            parent.Right = current.Right;
                            current.Right.Parent = parent;
                        }
                    }
                    // case 2 children
                    else
                    {
                        // find successor
                        var sucNode = Next(current);

                        var sucCopy = sucNode;
                        Remove(sucNode.Key);
                        current.Key = sucCopy.Key;
                        current.Value = sucCopy.Value;
                    }

                    return; // found and removed
                }
                else if (current.Key < key) // search right subtree
                {
                    parent = current;
                    current = current.Right;
                }
                else // search left subtree
                {
                    parent = current;
                    current = current.Left;
                }
            }

            return; // not found, do nothing
        }

        public T Search(int key)
        {
            if (!Contains(key))
            {
                return default;
            }

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
            List<BinarySearchTreeNode<T>> nodes = new List<BinarySearchTreeNode<T>>();

            for (int i = min; i < max + 1; i++)
            {
                if (InOrderKeys.Contains(i))
                {
                    nodes.Add(GetNode(i));
                }
            }

            return nodes;
        }
    }
}

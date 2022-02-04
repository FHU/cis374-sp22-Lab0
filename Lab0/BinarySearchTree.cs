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
            Height = 0;
        }

        public bool IsEmpty => Root == null;

        // TODO
        public int Height {get; set;}
        private int TempHeight { get; set; }

        // TODO
        public int MinKey
        { get
            {
                BinarySearchTreeNode<T> currentNode = null;
                if (Root == null)
                {
                    return -1;
                }
                currentNode = Root;
                while (currentNode.Left != null)
                {
                    currentNode = currentNode.Left;
                }
                return currentNode.Key;
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

        public int MaxKey
        {
            get
            {
                BinarySearchTreeNode<T> currentNode = null;
                if (Root == null)
                {
                    return -1;
                }
                currentNode = Root;
                while (currentNode.Right != null)
                {
                    currentNode = currentNode.Right;
                }
                return currentNode.Key;
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

        // TODO
        public double MedianKey
        {
            get
            {
                if (InOrderKeys.Count == 1)
                {
                    return InOrderKeys[0];
                }
                if (InOrderKeys.Count % 2 == 0)
                {
                    //int count = InOrderKeys.Count / 2;
                    double sum = InOrderKeys[(InOrderKeys.Count / 2) - 1] + InOrderKeys[InOrderKeys.Count / 2];
                    return sum / 2;
                }
                else
                {
                    double sum = InOrderKeys[(InOrderKeys.Count+1 / 2) - 1];
                    return sum / 2;
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
                Height = 0;
            }
            else
            {
                TempHeight = 1;
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
                    if(TempHeight > Height)
                    {
                        Height = TempHeight;
                    }
                }
                else
                {
                    TempHeight++;
                    AddRecursive(parent.Left, node);
                }
            }
            else
            {
                if(parent.Right == null)
                {
                    parent.Right = node;
                    node.Parent = parent;
                    if (TempHeight > Height)
                    {
                        Height = TempHeight;
                    }
                }
                else
                {
                    TempHeight++;
                    AddRecursive(parent.Right, node);
                }

            }
        }

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        public bool Contains(int key)
        {
            if(GetNodeRecursive(Root, key) == null)
            {
                return false;
            }
            else
            {
                return true;
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



        public BinarySearchTreeNode<T> Next(BinarySearchTreeNode<T> node)
        {
            if (node.Right == null)
            {
                if (node.Parent.Key >= node.Key)
                {
                    return node.Parent;
                }
                else
                {
                    return NextRecursive(node.Parent);
                }
            }
            else
            {
                return node.Right;
            }
        }
        public BinarySearchTreeNode<T> NextRecursive(BinarySearchTreeNode<T> node)
        {
            if (node.Parent.Key >= node.Key)
            {
                return node.Parent;
            }
            else
            {
                return NextRecursive(node.Parent);
            }
        }

        public BinarySearchTreeNode<T> Prev(BinarySearchTreeNode<T> node)
        {
            if (node.Left == null)
            {
                if (node.Parent.Key <= node.Key)
                {
                    return node.Parent;
                }
                else
                {
                    return PrevRecursive(node.Parent);
                }
            }
            else
            {
                return node.Left;
            }
        }
        public BinarySearchTreeNode<T> PrevRecursive(BinarySearchTreeNode<T> node)
        {
            if (node == Root)
            {
                return null;
            }
            if (node.Parent.Key <= node.Key)
            {
                return node.Parent;
            }
            else
            {
                return PrevRecursive(node.Parent);
            }
        }


        // TODO
        public void Remove(int key)
        {
            Count--;
            BinarySearchTreeNode<T> node = GetNodeRecursive(Root, key);
            BinarySearchTreeNode<T> left = node.Left;
            BinarySearchTreeNode<T> right = node.Right;
            BinarySearchTreeNode<T> parent = node.Parent;
            if (node.Parent.Left == node)
            {
                if(node.Right != null)
                {
                    if (node.Left != null)
                    {
                        parent.Left = right;
                        right.Parent = node.Parent;
                        AddRecursive(right, left);
                    }
                    else
                    {
                        parent.Left = right;
                        right.Parent = node.Parent;
                    }
                }
                else
                {
                    if (node.Left != null)
                    {
                        parent.Left = left;
                        left.Parent = node.Parent;
                    }
                    else
                    {
                        parent.Left = null;
                    }
                }
            }
            else
            {
                if (node.Right != null)
                {
                    if (node.Left != null)
                    {

                        parent.Right = node.Right;
                        AddRecursive(right, left);
                    }
                    else
                    {
                        parent.Right = right;
                        right.Parent = node.Parent;
                    }
                }
                else
                {
                    if (node.Left != null)
                    {
                        parent.Right = left;
                        left.Parent = node.Parent;
                    }
                    else
                    {
                        parent.Right = null;
                    }
                }
            }
            TempHeight = 0;
            Height = 0;
            SetHeight(Root);
        }

        private void SetHeight(BinarySearchTreeNode<T> node)
        {
            if (node.Left != null)
            {
                TempHeight++;
                SetHeight(node.Left);
            }
            if (node.Right != null)
            {
                TempHeight++;
                SetHeight(node.Right);
            }
            if (TempHeight > Height)
            {
                Height = TempHeight;
            }
            TempHeight--;
            return;
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
            List<BinarySearchTreeNode<T>> Range = new List<BinarySearchTreeNode<T>>();
            RangeSearchRecursive(Root, Range, min, max);
            return Range;
        }
        private void RangeSearchRecursive(BinarySearchTreeNode<T> node, List<BinarySearchTreeNode<T>> Range, int min, int max)
        {
            if (node == null)
            {
                return;
            }

            RangeSearchRecursive(node.Left, Range, min, max);
            if (node.Key >= min & node.Key <= max)
            {
                Range.Add(node);
            }
            RangeSearchRecursive(node.Right, Range, min, max);
        }
    }
}

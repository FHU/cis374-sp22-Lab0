using System;
using System.Collections.Generic;

namespace Lab0
{
    public interface IBinarySearchTree <T>
    {
        public void Add(int key, T value);

        public void Update(int key, T value);

        public void Remove(int key);

        public bool Contains(int key);

        public T Search(int key);

        public bool IsEmpty { get; }

        public int Count { get; }

        public int Height { get; }

        public void Clear();

        /* Ordering/Sorting Methods/Properties (Advanced) */

        public int MinKey { get; }

        public Tuple<int,T> Min { get; }

        public int MaxKey { get; }

        public Tuple<int, T> Max { get; }

        public double MedianKey { get; }

        public BinarySearchTreeNode<T> Next(BinarySearchTreeNode<T> node);

        public BinarySearchTreeNode<T> Prev(BinarySearchTreeNode<T> node);

        public List<BinarySearchTreeNode<T>> RangeSearch(int min, int max);



    }
}

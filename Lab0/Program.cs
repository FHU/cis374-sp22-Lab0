using System;

namespace Lab0
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            for (int i = 0; i < 50; i++)
            {
                tree.Add(i, i + 1);
            }
            tree.Remove(20);
            Console.WriteLine(tree.Height); // 48
            Console.WriteLine(tree.GetNode(21).Parent); // 20
        }
    }
}

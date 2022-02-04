using System;
using System.Collections.Generic;

namespace Lab0
{
    class Program
    {
        static void Main(string[] args)
        {
            //next4Edge():

            //BinarySearchTree<int> tree = new BinarySearchTree<int>();
            //tree.Add(4, 5);
            //tree.Add(5, 5);
            ////Assert.IsNull(tree.Next(tree.GetNode(5)));
            //Console.WriteLine(tree.Prev(tree.GetNode(4)));

            ////Prev4Edge():

            //BinarySearchTree<int> tree = new BinarySearchTree<int>();
            //tree.Add(4, 5);
            //tree.Add(5, 5);
            ////Assert.IsNull(tree.Next(tree.GetNode(5)));
            //Console.WriteLine(tree.Prev(tree.GetNode(4)));

            ////Prev1:

            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            int index = 0;
            for (int i = 0; i < 50; i++)
            {
                index = (i + 13) % 50;
                tree.Add(index, index);
            }
            BinarySearchTreeNode<int> current = new BinarySearchTreeNode<int>(1, 1);
            for (int i = 1; i < 50; i++)
            {
                current = tree.GetNode(i);
                //Assert.AreEqual(tree.Search(i - 1), tree.Prev(current).Value);
                Console.Write(tree.Search(i - 1));
                Console.Write(" - ");
                Console.WriteLine(tree.Prev(current).Value);
            }
        }
    }
}

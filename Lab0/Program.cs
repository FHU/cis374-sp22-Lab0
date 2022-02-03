using System;

namespace Lab0
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            Random rand = new Random();
            int random = 0;
            random = rand.Next();
            tree.Add(8, random);
            random = rand.Next();
            tree.Add(10, random);
            random = rand.Next();
            tree.Add(6, random);
            random = rand.Next();
            tree.Add(5, random);
            random = rand.Next();
            tree.Add(22, random);
            random = rand.Next();
            tree.Add(26, random);
            random = rand.Next();
            tree.Add(7, random);
            random = rand.Next();
            random = rand.Next();
            tree.Add(23, random);

            Console.WriteLine(tree.Height);
        }
    }
}

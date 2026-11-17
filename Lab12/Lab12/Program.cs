using System;
using System.Text;

// ЛАБОРАТОРНА 12. Просте BST
// Набір чисел і порядок вставки не змінювати.
// Готових впорядкованих колекцій не брати — дерево будуємо самі.

class Node
{
    public int Value;
    public Node? Left;    // null, якщо нащадка немає
    public Node? Right;

    public Node(int value)
    {
        Value = value;
    }
}

class Program
{
    static Node? root = null;
    static int[] values = { 42, 18, 65, 9, 27, 55, 90, 33 };   // вставляти саме в цьому порядку
    static int compareCount = 0;   // лічильник порівнянь у Search

    // TODO 1: вставити число в дерево; якщо воно вже є — нічого не робити
    static Node Insert(Node? node, int key)
    {
        if (node == null)                 // базовий випадок: порожнє місце —
            return new Node(key);         // новий вузол стає листком

        if (key < node.Value)
            node.Left = Insert(node.Left, key);
        else if (key > node.Value)
            node.Right = Insert(node.Right, key);
        // key == node.Value: число вже є — нічого не робимо

        return node;
    }

    // TODO 2: повернути true, якщо число є в дереві; рахувати порівняння
    static bool Search(Node? node, int key)
    {
        if (node == null)                 // дійшли до порожнього місця — числа немає
            return false;

        compareCount++;                   // порівнюємо ключ із цим вузлом
        if (key == node.Value)
            return true;
        if (key < node.Value)
            return Search(node.Left, key);
        return Search(node.Right, key);
    }

    // TODO 3: надрукувати всі числа дерева за зростанням
    static void Inorder(Node? node)
    {
        if (node == null)
            return;
        Inorder(node.Left);               // спершу все менше
        Console.Write(node.Value + " ");  // потім сам вузол
        Inorder(node.Right);              // потім все більше
    }

    // допоміжні — для таблиці перевірки
    static int CountNodes(Node? node)
    {
        if (node == null) return 0;
        return 1 + CountNodes(node.Left) + CountNodes(node.Right);
    }

    static int Height(Node? node)
    {
        if (node == null) return 0;
        return 1 + Math.Max(Height(node.Left), Height(node.Right));
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        foreach (var v in values)
            root = Insert(root, v);

        Console.WriteLine("Вузлів у дереві: " + CountNodes(root));

        Console.Write("Симетричний обхід: ");
        Inorder(root);
        Console.WriteLine();

        compareCount = 0;
        Console.WriteLine("Search(55) = " + Search(root, 55) + ", порівнянь: " + compareCount);

        compareCount = 0;
        Console.WriteLine("Search(30) = " + Search(root, 30) + ", порівнянь: " + compareCount);

        Console.WriteLine("Глибина найдовшої гілки: " + Height(root));

        root = Insert(root, 27);   // повторна вставка
        Console.WriteLine("Вузлів після повторної вставки 27: " + CountNodes(root));
    }
}
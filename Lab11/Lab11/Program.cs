using System;
using System.Collections.Generic;
using System.Text;

// ЛАБОРАТОРНА 11. Обхід дерева
// Дерево не змінювати. Готових бібліотек для роботи з деревами не брати —
// обхід писати самостійно.

class Node
{
    public string Title;
    public List<Node> Children;

    public Node(string title, List<Node> children)
    {
        Title = title;
        Children = children;
    }
}

class Program
{
    static Node menu = new Node("Головне меню", new List<Node>
    {
        new Node("Файл", new List<Node> { new Node("Новий", new List<Node>()), new Node("Зберегти", new List<Node>()) }),
        new Node("Правка", new List<Node> { new Node("Копіювати", new List<Node>()) }),
        new Node("Вигляд", new List<Node> { new Node("Масштаб", new List<Node> { new Node("Збільшити", new List<Node>()), new Node("Зменшити", new List<Node>()) }) }),
        new Node("Довідка", new List<Node>()),
    });

    static int callCount = 0;   // лічильник викликів, збільшується на початку CountItems

    // TODO 1: вивести всю ієрархію з відступом за рівнем вкладеності (2 пробіли на рівень)
    static void PrintMenu(Node node, int level)
    {
        Console.WriteLine(new string(' ', level * 2) + node.Title);   // спершу сам вузол
        foreach (var child in node.Children)                          // потім його діти
            PrintMenu(child, level + 1);
    }

    // TODO 2: повернути кількість усіх пунктів меню, враховуючи сам вузол
    static int CountItems(Node node)
    {
        callCount++;                          // на початку виконання
        int total = 1;                        // сам вузол
        foreach (var child in node.Children)  // у вузла без дітей цикл не виконається жодного разу
            total += CountItems(child);
        return total;
    }

    // TODO 3: повернути true, якщо в дереві є пункт із такою назвою
    static bool Find(Node node, string name)
    {
        if (node.Title == name)               // перевіряємо КОЖЕН вузол, і з дітьми теж
            return true;
        foreach (var child in node.Children)
            if (Find(child, name))
                return true;
        return false;
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("--- PrintMenu ---");
        PrintMenu(menu, 0);

        Console.WriteLine("--- CountItems ---");
        Console.WriteLine("CountItems(menu) = " + CountItems(menu));
        Console.WriteLine("callCount = " + callCount);

        Console.WriteLine("--- Find ---");
        Console.WriteLine("Find(menu, \"Зберегти\") = " + Find(menu, "Зберегти"));
        Console.WriteLine("Find(menu, \"Масштаб\")  = " + Find(menu, "Масштаб"));
        Console.WriteLine("Find(menu, \"Довідка\")  = " + Find(menu, "Довідка"));
        Console.WriteLine("Find(menu, \"Експорт\")  = " + Find(menu, "Експорт"));
    }
}
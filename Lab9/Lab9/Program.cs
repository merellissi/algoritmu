using System;
using System.Collections.Generic;
using System.Text;

// ЛАБОРАТОРНА 9. Ключі хеш-колекцій
// Дані не змінювати. Дефект у коді один, і він у цьому класі.

class Product
{
    public string Code;   // артикул — саме він визначає товар
    public string Name;   // назва, може змінитися при перейменуванні

    public Product(string code, string name)
    {
        Code = code;
        Name = name;
    }

    // два товари однакові, якщо однаковий артикул
    public override bool Equals(object? obj)
    {
        return obj is Product other && Code == other.Code;
    }

    // ВИПРАВЛЕННЯ: хеш рахується з того самого поля, що й рівність — з артикулу.
    // Без цього методу HashSet і Dictionary клали однакові товари в різні "кошики"
    // і навіть не доходили до виклику Equals.
    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var items = new List<Product>
        {
            new Product("A-100", "Кабель USB-C"),
            new Product("B-205", "Миша бездротова"),
            new Product("A-100", "Кабель USB-C"),
            new Product("C-310", "Клавіатура"),
            new Product("D-415", "Монітор 24 дюйми"),
            new Product("B-205", "Миша бездротова"),
            new Product("E-520", "Навушники"),
            new Product("F-630", "Вебкамера"),
        };

        var unique = new HashSet<Product>();
        foreach (var p in items)
            unique.Add(p);   // мали б лишитися тільки унікальні артикули

        var stock = new Dictionary<Product, int>();
        stock[new Product("A-100", "Кабель USB-C")] = 17;
        stock[new Product("A-100", "Кабель USB-C")] = 25;   // той самий артикул, нове число

        // Діагностика
        var a = new Product("A-100", "Кабель USB-C");
        var b = new Product("A-100", "Кабель USB-C");

        Console.WriteLine("(1) a.Equals(b):         " + a.Equals(b));
        Console.WriteLine("(2) unique.Count:        " + unique.Count);
        Console.WriteLine("(3) unique.Contains(a):  " + unique.Contains(a));
        Console.WriteLine("(4) stock.Count:         " + stock.Count);
        Console.WriteLine("(5) stock.ContainsKey(a): " + stock.ContainsKey(a));
    }
}
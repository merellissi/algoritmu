using System;
using System.Collections.Generic;
using System.Text;

// ЛАБОРАТОРНА 8. Швидкий пошук ключів
// Дані не змінювати. Міняти тільки код фрагментів.

record Client(int Id, string Name);
record Order(int Number, int ClientId, int Sum);

class Program
{
    static List<Client> clients = new List<Client>   // id, прізвище
    {
        new Client(101, "Коваленко"),
        new Client(102, "Мельник"),
        new Client(103, "Шевчук"),
        new Client(104, "Бондар"),
        new Client(105, "Ткачук"),
        new Client(106, "Гриценко"),
    };

    static List<Order> orders = new List<Order>      // номер, id клієнта, сума
    {
        new Order(1, 103, 450), new Order(2, 101, 120),
        new Order(3, 105, 980), new Order(4, 103, 300),
        new Order(5, 102, 75),  new Order(6, 106, 1500),
        new Order(7, 101, 220), new Order(8, 103, 60),
    };

    static List<int> blocked = new List<int> { 102, 104 };   // заблоковані клієнти

    // ---------- Фрагмент 1. Ім'я клієнта для кожного замовлення ----------
    static void Fragment1()
    {
        // СТАРИЙ ВАРІАНТ: для кожного замовлення — повний перебір clients, O(n·m)
        // foreach (var order in orders)
        //     foreach (var client in clients)
        //         if (client.Id == order.ClientId)
        //             Console.WriteLine("Замовлення " + order.Number + " — " + client.Name);

        // НОВИЙ ВАРІАНТ: словник id -> клієнт будується ОДИН раз, далі пошук O(1)
        var byId = new Dictionary<int, Client>();
        foreach (var client in clients)
            byId[client.Id] = client;

        foreach (var order in orders)   // обходимо список orders — порядок збережено
            Console.WriteLine("Замовлення " + order.Number + " — " + byId[order.ClientId].Name);
    }

    // ---------- Фрагмент 2. Сума замовлень, крім заблокованих клієнтів ----------
    static void Fragment2()
    {
        // СТАРИЙ ВАРІАНТ: blocked — список, Contains щоразу перебирає його
        // int total = 0;
        // foreach (var order in orders)
        //     if (!blocked.Contains(order.ClientId))
        //         total = total + order.Sum;
        // Console.WriteLine("Сума прийнятих замовлень: " + total);

        // НОВИЙ ВАРІАНТ: множина заблокованих id, перевірка O(1)
        var blockedSet = new HashSet<int>(blocked);
        int total = 0;
        foreach (var order in orders)
            if (!blockedSet.Contains(order.ClientId))
                total = total + order.Sum;
        Console.WriteLine("Сума прийнятих замовлень: " + total);
    }

    // ---------- Фрагмент 3. Скільки замовлень у кожного клієнта ----------
    static void Fragment3()
    {
        // СТАРИЙ ВАРІАНТ: вкладений прохід по orders + пошук у списку counted, O(n²)
        // var counted = new List<int>();
        // foreach (var order in orders)
        // {
        //     if (counted.Contains(order.ClientId))
        //         continue;
        //     int count = 0;
        //     foreach (var other in orders)
        //         if (other.ClientId == order.ClientId)
        //             count = count + 1;
        //     counted.Add(order.ClientId);
        //     Console.WriteLine(order.ClientId + ": " + count);
        // }

        // НОВИЙ ВАРІАНТ: словник clientId -> кількість, один прохід O(n).
        // Порядок першої появи зберігає окремий список firstSeen.
        var counts = new Dictionary<int, int>();
        var firstSeen = new List<int>();
        foreach (var order in orders)
        {
            if (!counts.ContainsKey(order.ClientId))
            {
                counts[order.ClientId] = 0;
                firstSeen.Add(order.ClientId);
            }
            counts[order.ClientId] = counts[order.ClientId] + 1;
        }
        foreach (var clientId in firstSeen)   // обходимо список, а не словник
            Console.WriteLine(clientId + ": " + counts[clientId]);
    }

    // ---------- Фрагмент 4. Найбільше замовлення ----------
    // Без змін: один прохід O(n), пошуку за ключем тут немає.
    static void Fragment4()
    {
        var best = orders[0];
        foreach (var order in orders)
            if (order.Sum > best.Sum)
                best = order;
        Console.WriteLine("Найбільше замовлення: " + best.Sum);
    }

    // ---------- Фрагмент 5. Чийсь «оптимізований» варіант фрагмента 1 ----------
    // Без змін. Нова структура не потрібна: словник уже є, але він будується
    // ЗАНОВО всередині циклу для кожного замовлення — тобто знову O(n·m).
    // Правильно — винести побудову словника за цикл, а це і є новий Fragment1.
    static void Fragment5()
    {
        foreach (var order in orders)
        {
            var byId = new Dictionary<int, Client>();
            foreach (var client in clients)
                byId[client.Id] = client;
            Console.WriteLine("Замовлення " + order.Number + " — " + byId[order.ClientId].Name);
        }
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("--- Фрагмент 1 ---"); Fragment1();
        Console.WriteLine("--- Фрагмент 2 ---"); Fragment2();
        Console.WriteLine("--- Фрагмент 3 ---"); Fragment3();
        Console.WriteLine("--- Фрагмент 4 ---"); Fragment4();
        Console.WriteLine("--- Фрагмент 5 ---"); Fragment5();
    }
}
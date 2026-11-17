using System;
using System.Collections.Generic;
using System.Text;

// ЛАБОРАТОРНА 10. Пріоритетні задачі
// Дані не змінювати. Власну купу не реалізовувати —
// брати готову чергу з пріоритетом своєї мови.
// У C# це PriorityQueue<TElement, TPriority>: першим виходить НАЙМЕНШИЙ пріоритет.

record Request(string Name, int Priority, int Seq);
// Priority: 1 — критично, 2 — важливо, 3 — може почекати
// Seq: порядковий номер надходження

class Program
{
    static List<Request> incoming = new List<Request>   // у порядку надходження
    {
        new Request("Оновити підпис у листі", 3, 1),
        new Request("Сервер не відповідає", 1, 2),
        new Request("Не друкує принтер", 2, 3),
        new Request("Не працює термінал оплати", 1, 4),
        new Request("Змінити шпалери", 3, 5),
    };

    static Request late = new Request("Відвалився інтернет у відділі", 1, 6);   // для задачі 3

    static void Print(int number, Request r)
    {
        Console.WriteLine(number + ". " + r.Name + " (пріоритет " + r.Priority + ", seq " + r.Seq + ")");
    }

    // ---------- Задача 1. Обробити за пріоритетом ----------
    // Ключ — тільки пріоритет.
    static void ProcessAll(List<Request> incoming)
    {
        var queue = new PriorityQueue<Request, int>();
        foreach (var r in incoming)
            queue.Enqueue(r, r.Priority);

        int number = 1;
        while (queue.Count > 0)
        {
            var r = queue.Dequeue();
            Print(number, r);
            number++;
        }
    }

    // ---------- Задача 2. Зробити порядок передбачуваним ----------
    // Ключ — пара (Priority, Seq): спершу пріоритет, при рівності — хто раніше надійшов.
    static void ProcessStable(List<Request> incoming)
    {
        var queue = new PriorityQueue<Request, (int, int)>();
        foreach (var r in incoming)
            queue.Enqueue(r, (r.Priority, r.Seq));

        int number = 1;
        while (queue.Count > 0)
        {
            var r = queue.Dequeue();
            Print(number, r);
            number++;
        }
    }

    // ---------- Задача 3. Заявка, що надійшла посеред обробки ----------
    // Обробити дві заявки, потім покласти late і продовжити до кінця.
    // Ключ — такий самий складений, як у задачі 2.
    static void ProcessWithLate(List<Request> incoming, Request late)
    {
        var queue = new PriorityQueue<Request, (int, int)>();
        foreach (var r in incoming)
            queue.Enqueue(r, (r.Priority, r.Seq));

        int number = 1;
        for (int i = 0; i < 2; i++)          // обробили дві заявки
        {
            Print(number, queue.Dequeue());
            number++;
        }

        queue.Enqueue(late, (late.Priority, late.Seq));   // надійшла нова критична заявка

        while (queue.Count > 0)              // продовжуємо до кінця
        {
            Print(number, queue.Dequeue());
            number++;
        }
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("--- Задача 1 ---");
        ProcessAll(incoming);
        Console.WriteLine("--- Задача 2 ---");
        ProcessStable(incoming);
        Console.WriteLine("--- Задача 3 ---");
        ProcessWithLate(incoming, late);
    }
}
/* ВИВІД ПРОГРАМИ:
--- Задача 1 ---
1. Сервер не відповідає (пріоритет 1, seq 2)
2. Не працює термінал оплати (пріоритет 1, seq 4)
3. Не друкує принтер (пріоритет 2, seq 3)
4. Змінити шпалери (пріоритет 3, seq 5)
5. Оновити підпис у листі (пріоритет 3, seq 1)
   ^ рівні пріоритети 3 вийшли НЕ в порядку надходження — ключ лише Priority
--- Задача 2 ---
1. Сервер не відповідає (пріоритет 1, seq 2)
2. Не працює термінал оплати (пріоритет 1, seq 4)
3. Не друкує принтер (пріоритет 2, seq 3)
4. Оновити підпис у листі (пріоритет 3, seq 1)
5. Змінити шпалери (пріоритет 3, seq 5)
--- Задача 3 ---
1. Сервер не відповідає (пріоритет 1, seq 2)
2. Не працює термінал оплати (пріоритет 1, seq 4)
3. Відвалився інтернет у відділі (пріоритет 1, seq 6)   <- late, додана після 2-ї обробки
4. Не друкує принтер (пріоритет 2, seq 3)
5. Оновити підпис у листі (пріоритет 3, seq 1)
6. Змінити шпалери (пріоритет 3, seq 5)
*/
using System;
using System.Collections.Generic;
using System.Text;

// ЛАБОРАТОРНА 15. Порядок залежностей (топологічне сортування, алгоритм Кана)

class Program
{
    // ─── ГОТОВО: набір А — робочий навчальний план ─────────────────
    // before[X] — що треба скласти ПЕРЕД X
    // after[X]  — хто чекає на X (зворотний список, уже побудований)
    static Dictionary<string, List<string>> beforeA = new Dictionary<string, List<string>>
    {
        ["Вступ"]         = new List<string>(),
        ["Математика"]    = new List<string>(),
        ["ООП"]           = new List<string> { "Вступ" },
        ["Бази даних"]    = new List<string> { "Вступ" },
        ["Алгоритми"]     = new List<string> { "Вступ", "Математика" },
        ["Вебтехнології"] = new List<string> { "ООП", "Бази даних" },
        ["Курсовий"]      = new List<string> { "Алгоритми", "Вебтехнології" },
    };

    static Dictionary<string, List<string>> afterA = new Dictionary<string, List<string>>
    {
        ["Вступ"]         = new List<string> { "ООП", "Бази даних", "Алгоритми" },
        ["Математика"]    = new List<string> { "Алгоритми" },
        ["ООП"]           = new List<string> { "Вебтехнології" },
        ["Бази даних"]    = new List<string> { "Вебтехнології" },
        ["Алгоритми"]     = new List<string> { "Курсовий" },
        ["Вебтехнології"] = new List<string> { "Курсовий" },
        ["Курсовий"]      = new List<string>(),
    };

    // ─── ГОТОВО: набір Б — той самий план з однією зайвою вимогою ──
    // хтось помилково записав «Бази даних» після «Вебтехнологій»
    static Dictionary<string, List<string>> beforeB = MakeBeforeB();
    static Dictionary<string, List<string>> afterB = MakeAfterB();

    static Dictionary<string, List<string>> MakeBeforeB()
    {
        var copy = new Dictionary<string, List<string>>(beforeA);
        copy["Бази даних"] = new List<string> { "Вступ", "Вебтехнології" };
        return copy;
    }

    static Dictionary<string, List<string>> MakeAfterB()
    {
        var copy = new Dictionary<string, List<string>>(afterA);
        copy["Вебтехнології"] = new List<string> { "Курсовий", "Бази даних" };
        return copy;
    }

    // ─── ВАШ КОД: завершіть алгоритм ───────────────────────────────
    static List<string> TopoSort(Dictionary<string, List<string>> before,
                                 Dictionary<string, List<string>> after)
    {
        var incoming = new Dictionary<string, int>();
        // TODO 1: для кожного предмета — скільки предметів треба скласти перед ним
        foreach (var subject in before.Keys)
            incoming[subject] = before[subject].Count;

        var queue = new Queue<string>();
        // TODO 2: покласти в чергу всі предмети з лічильником 0
        foreach (var subject in before.Keys)
            if (incoming[subject] == 0)
                queue.Enqueue(subject);

        var order = new List<string>();
        while (queue.Count > 0)
        {
            string v = queue.Dequeue();
            order.Add(v);
            foreach (string next in after[v])
            {
                // TODO 3: одну передумову предмета next виконано
                incoming[next]--;
                if (incoming[next] == 0)
                    queue.Enqueue(next);
            }
        }
        return order;
    }

    // TODO 4: true, якщо в наборі залежностей є цикл
    static bool HasCycle(Dictionary<string, List<string>> before,
                         Dictionary<string, List<string>> after)
    {
        return TopoSort(before, after).Count < before.Count;
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var orderA = TopoSort(beforeA, afterA);
        Console.WriteLine("--- Набір А ---");
        Console.WriteLine("Предметів у результаті: " + orderA.Count);
        Console.WriteLine("Порядок: " + string.Join(" → ", orderA));
        Console.WriteLine("Вступ раніше за Вебтехнології: " +
            (orderA.IndexOf("Вступ") < orderA.IndexOf("Вебтехнології")));
        Console.WriteLine("HasCycle(А) = " + HasCycle(beforeA, afterA));

        var orderB = TopoSort(beforeB, afterB);
        Console.WriteLine("--- Набір Б ---");
        Console.WriteLine("Предметів у результаті: " + orderB.Count);
        Console.WriteLine("Порядок: " + string.Join(" → ", orderB));
        Console.WriteLine("HasCycle(Б) = " + HasCycle(beforeB, afterB));

        var missing = new List<string>();
        foreach (var subject in beforeB.Keys)
            if (!orderB.Contains(subject))
                missing.Add(subject);
        Console.WriteLine("Не потрапили в результат: " + string.Join(", ", missing));
    }
}
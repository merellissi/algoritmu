using System;
using System.Collections.Generic;
using System.Text;

// ЛАБОРАТОРНА 14. Досяжність у графі
// Обхід — у ширину (BFS) через чергу Queue.

class Program
{
    // ─── ГОТОВО: модель графа. Нічого тут не змінюйте ──────────────
    static Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>
    {
        ["Центр"]   = new List<string> { "Парк", "Ринок" },
        ["Парк"]    = new List<string> { "Центр", "Вокзал" },
        ["Ринок"]   = new List<string> { "Центр", "Вокзал", "Стадіон" },
        ["Вокзал"]  = new List<string> { "Парк", "Ринок", "Озеро" },
        ["Стадіон"] = new List<string> { "Ринок" },
        ["Озеро"]   = new List<string> { "Вокзал" },
        ["Гай"]     = new List<string> { "Міст" },
        ["Міст"]    = new List<string> { "Гай" },
    };

    // ─── ВАШ КОД ───────────────────────────────────────────────────

    // TODO 1: обійти граф від start і повернути множину відвіданих вершин.
    static HashSet<string> VisitAll(Dictionary<string, List<string>> graph, string start)
    {
        var visited = new HashSet<string>();
        var queue = new Queue<string>();

        visited.Add(start);
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            string current = queue.Dequeue();
            foreach (string next in graph[current])
            {
                if (!visited.Contains(next))   // без цієї перевірки обхід ходив би по колу
                {
                    visited.Add(next);
                    queue.Enqueue(next);
                }
            }
        }
        return visited;
    }

    // TODO 2: true, якщо з from можна дістатися до to; інакше false
    static bool Reachable(Dictionary<string, List<string>> graph, string from, string to)
    {
        return VisitAll(graph, from).Contains(to);
    }

    // TODO 3: шлях від from до to переліком вершин; порожній список, якщо шляху немає
    static List<string> FindPath(Dictionary<string, List<string>> graph, string from, string to)
    {
        var parent = new Dictionary<string, string>();   // з якої вершини прийшли в кожну
        var visited = new HashSet<string>();
        var queue = new Queue<string>();

        visited.Add(from);
        queue.Enqueue(from);

        while (queue.Count > 0)
        {
            string current = queue.Dequeue();
            foreach (string next in graph[current])
            {
                if (!visited.Contains(next))
                {
                    visited.Add(next);
                    parent[next] = current;            // позначка: в next прийшли з current
                    queue.Enqueue(next);
                }
            }
        }

        var path = new List<string>();
        if (!visited.Contains(to))                     // to недосяжна — порожній шлях
            return path;

        string step = to;
        path.Add(step);
        while (parent.ContainsKey(step))               // у from позначки немає — це кінець
        {
            step = parent[step];
            path.Add(step);
        }
        path.Reverse();                                // зібрали задом наперед — розвертаємо
        return path;
    }

    static string Show(List<string> path)
    {
        return path.Count == 0 ? "[] (порожній список)" : string.Join(" → ", path);
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var fromCenter = VisitAll(graph, "Центр");
        Console.WriteLine("VisitAll(Центр): " + fromCenter.Count + " вершин — " + string.Join(", ", fromCenter));

        Console.WriteLine("Reachable(Центр, Озеро) = " + Reachable(graph, "Центр", "Озеро"));
        Console.WriteLine("Reachable(Центр, Гай)   = " + Reachable(graph, "Центр", "Гай"));
        Console.WriteLine("Reachable(Гай, Міст)    = " + Reachable(graph, "Гай", "Міст"));

        var p1 = FindPath(graph, "Центр", "Озеро");
        Console.WriteLine("FindPath(Центр, Озеро):   " + Show(p1) + "  (ребер: " + (p1.Count - 1) + ")");

        var p2 = FindPath(graph, "Центр", "Стадіон");
        Console.WriteLine("FindPath(Центр, Стадіон): " + Show(p2) + "  (ребер: " + (p2.Count - 1) + ")");

        var p3 = FindPath(graph, "Центр", "Міст");
        Console.WriteLine("FindPath(Центр, Міст):    " + Show(p3));

        Console.WriteLine("Недосяжних із Центру: " + (graph.Count - fromCenter.Count));
    }
}
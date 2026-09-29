using System;

class Program
{
    static int comparisons = 0;

    static int LinearSearch(int[] items, int target)
    {
        for (int i = 0; i < items.Length; i++)
        {
            comparisons++;
            if (items[i] == target)
                return i;
        }
        return -1;
    }

    static int BinarySearch(int[] items, int target)
    {
        int low = 0;
        int high = items.Length - 1;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;
            comparisons++;                     
            if (items[mid] == target)
                return mid;
            if (items[mid] < target)
                low = mid + 1;
            else
                high = mid - 1;
        }
        return -1;
    }

    static void Report(string name, int[] items, int target)
    {
        comparisons = 0;
        int index = name == "linear" ? LinearSearch(items, target)
                                     : BinarySearch(items, target);
        Console.WriteLine($"{name}, шукаємо {target}: індекс {index}, порівнянь {comparisons}");
    }

    static void Main()
    {
        int[] data = { 42, 8, 60, 19, 3, 55, 12, 31, 68, 24, 49, 37, 71, 5, 27 };
        int[] sortedData = (int[])data.Clone();
        Array.Sort(sortedData);

        int[] targets = { 3, 71, 31, 1, 99, 50 };
        foreach (int t in targets)
        {
            Report("linear", sortedData, t);
            Report("binary", sortedData, t);
        }
        Report("linear", new[] { 42 }, 42);
        Report("binary", new[] { 42 }, 42);
        Report("linear", new int[0], 42);
        Report("binary", new int[0], 42);

     
        Report("binary", data, 55);
    }
}
using System;

class Task2
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        (int Month, double Amount)[] operations =
        {
            (1, 1500.50),
            (3, 2300.00),
            (1, 450.00),
            (12, 5000.00),
            (3, 700.00)
        };

        double[] monthlyTotals = new double[12];

        foreach (var operation in operations)
        {
            monthlyTotals[operation.Month - 1] += operation.Amount;
        }

        Console.WriteLine("Підсумки за місяцями:");

        for (int month = 0; month < 12; month++)
        {
            Console.WriteLine($"Місяць {month + 1}: {monthlyTotals[month]:F2} грн");
        }

        Console.ReadKey();
    }
}
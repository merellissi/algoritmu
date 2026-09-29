using System;
using System.Collections.Generic;

class Task1
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        List<string> logEntries = new List<string>();

        logEntries.Add("09:00: Сервер запущено");
        logEntries.Add("09:05: Підключення користувача");
        logEntries.Add("09:15: Запит до бази даних");
        logEntries.Add("09:30: Роботу завершено");

        Console.WriteLine("Журнал у зворотному порядку:");

        for (int i = logEntries.Count - 1; i >= 0; i--)
        {
            Console.WriteLine(logEntries[i]);
        }

        Console.ReadKey();
    }
}
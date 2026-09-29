using System;

class Task3
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        const int N = 5;

        string[] buffer = new string[N];

        int writeIndex = 0;
        int totalEvents = 0;

        string[] incomingEvents =
        {
            "Подія 1",
            "Подія 2",
            "Подія 3",
            "Подія 4",
            "Подія 5",
            "Подія 6",
            "Подія 7"
        };

        foreach (var evt in incomingEvents)
        {
            buffer[writeIndex] = evt;

            writeIndex = (writeIndex + 1) % N;

            totalEvents++;
        }

        int count = Math.Min(totalEvents, N);
        int startIndex = totalEvents < N ? 0 : writeIndex;

        Console.WriteLine("Останні збережені події:");

        for (int i = 0; i < count; i++)
        {
            int index = (startIndex + i) % N;

            Console.WriteLine(buffer[index]);
        }

        Console.ReadKey();
    }
}
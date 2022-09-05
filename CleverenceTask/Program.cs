using System.Collections.Concurrent;
using CleverenceTask;

const int READERS = 20; // Кол-во читателей
const int WRITERS = 5;  // Кол-во писателей
var random = Random.Shared;
var console = new ConcurrentQueue<string>();

var threads = new List<Thread>(READERS + WRITERS);  // Список в котором будут жить потоки читателей и писателей

for (int i = 0; i < READERS; i++)
{
    threads.Add(new Thread(Reader));
}

for (int i = 0; i < WRITERS; i++)
{
    threads.Add(new Thread(Writer));
}

threads.Add(new Thread(Printer));

foreach (var thread in threads)
{
    thread.Start();
}

// Функция которая читает, имитирует читателя
void Reader()
{
    while (true)
    {
        var count = MockServer.GetCount();
        console.Enqueue($"[READER|{Thread.CurrentThread.ManagedThreadId,5}]: {count}");
        Thread.Sleep(200);
    }
}

// Функция которая пишет, имитирует писателя
void Writer()
{
    while (true)
    {
        var value = random.Next(10);
        MockServer.AddToCount(value);
        console.Enqueue($"[WRITER|{Thread.CurrentThread.ManagedThreadId,5}]: +{value}");
        Thread.Sleep(50);
    }
}

// Вспомогательная функция для вывода в консоль
void Printer()
{
    while (true)
    {
        if (console.TryDequeue(out var message))
        {
            Console.WriteLine(message);
        }
    }
}

// Вызов полу-асинхронного вызова
var eventHandler = new EventHandler(MyHandler);
var asyncCaller = new AsyncCaller(eventHandler);
if (asyncCaller.Invoke(250, null, EventArgs.Empty))
{
    Console.WriteLine("Событие вызволось успешно");
}
else
{
    Console.WriteLine("Событие провалилось");
}

void MyHandler(object? sender, EventArgs eventArgs)
{
    Console.WriteLine("Начало События");
    Thread.Sleep(200);
    Console.WriteLine("Конец События");
}
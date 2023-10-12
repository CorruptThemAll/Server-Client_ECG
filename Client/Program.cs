using System.Collections.Concurrent;

namespace ECGPro;

internal class Program
{
    static void Main(string[] args)
    {
        var queue = new BlockingCollection<DataContainer>();
        ECGReadingProducer ecgReadingProducer = new(queue, new ECGSensor());
        ECGReadingClientSocket ecgReadingConsumer = new(queue);
        Log log = new(ecgReadingConsumer);
        ECGContainer ecgContainer = new(ecgReadingConsumer, new Processing());
        ecgReadingConsumer.Attach(log);
        ecgReadingConsumer.Attach(ecgContainer);
        Thread producerThread = new(ecgReadingProducer.ReadSample);
        Thread consumerThread = new(ecgReadingConsumer.Consume);
        producerThread.Start();
        consumerThread.Start();

        while (true)
        {
            if (Console.ReadKey().Key == ConsoleKey.P)
            {
                ecgContainer.Process();   
            }
            else if (Console.ReadKey().Key == ConsoleKey.Q)
            {
                producerThread.Join();
                consumerThread.Join();
                break;
            }
        }
    }
}
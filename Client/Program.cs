using System.Collections.Concurrent;

namespace ECGPro;

internal class Program
{
    static void Main(string[] args)
    {
        var queue = new BlockingCollection<DataContainer>();
        ECGSensor sensor = new ECGSensor();
        var _ecgproducer = new ECGReadingProducer(queue,sensor);
        var _ecgClient = new ECGReadingClientSocket(queue);

        Thread t1 = new Thread(_ecgproducer.Run);
        Thread t2 = new Thread(_ecgClient.Run);

        t1.Start();
        t2.Start();

        while (true)
        {
            if (Console.ReadKey().Key == ConsoleKey.P)
            {
                Console.WriteLine("Does nothing");
            }
            else if (Console.ReadKey().Key == ConsoleKey.Q)
            {
                break;
            }
        }
    }
}
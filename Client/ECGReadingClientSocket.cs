using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ECGPro;

public class ECGReadingClientSocket : Subject
{
    private readonly BlockingCollection<DataContainer> _queue;

    public ECGReadingClientSocket(BlockingCollection<DataContainer> queue)
    {
        _queue = queue;
    }

    public void Run()
    {
        IPAddress address = IPAddress.Parse("127.0.0.1");
        IPEndPoint ipEndPoint = new IPEndPoint(address, 2001);

        using Socket socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        socket.Connect(ipEndPoint);

        while (!_queue.IsCompleted)
        {
            try
            {
                var sample = _queue.Take();
                string jsonObject = JsonSerializer.Serialize(sample);
                var message = Encoding.UTF8.GetBytes(jsonObject);
                Console.WriteLine(message);
                
                socket.Send(message, SocketFlags.None);
            }
            catch (InvalidOperationException)
            {

            }
            
        }
        socket.Shutdown(SocketShutdown.Both);
    }
    
    public void Consume()
    {
        while (!_queue.IsCompleted)
        {
            if (_queue.TryTake(out var data))
            {
                //Samle = data.Sample;
                Notify();
            }
        }
    }
}
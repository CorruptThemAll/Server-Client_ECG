using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using ECGPro;

namespace Server;

public class ECGReadingServer
{
    private readonly BlockingCollection<DataContainer> _queue;
    public bool ShallStop { get; set; } = false;

    public ECGReadingServer(BlockingCollection<DataContainer> queue)
    {
        _queue = queue;
    }

    public void Run() => RunServer();

    public void RunServer()
    {
        IPAddress ipAddress = IPAddress.Any;
        IPEndPoint localEndPoint = new(ipAddress, 2001);
        using Socket listener = new(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        Socket handler;
        try
        {
            listener.Bind(localEndPoint);
            Console.WriteLine($"Listening on: {ipAddress}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Could not bind to: {ipAddress}", ex);
        }
        try
        {
            listener.Listen();
            handler = listener.Accept();
        }
        catch (Exception e)
        {
            throw new Exception("Could not listen", e);
        }

        while (!ShallStop)
        {
            byte[] buffer = new byte[1024];
            int numberOfBytesRecieved = handler.Receive(buffer, SocketFlags.None);
            if (numberOfBytesRecieved > 0)
            {
                string recievedData = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRecieved);
                Console.WriteLine($"Recieved: {recievedData}");
                try
                {
                    DataContainer dataContainer = JsonSerializer.Deserialize<DataContainer>(recievedData);
                    if (dataContainer != null)
                    {
                        _queue.Add(dataContainer);
                    }
                }
                catch (JsonException e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        listener.Close();
        _queue.CompleteAdding();
    }
}
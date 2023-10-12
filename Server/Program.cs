using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ECGPro;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BlockingCollection<DataContainer> _data = new BlockingCollection<DataContainer>();
            ECGReadingServer _ecgServer = new ECGReadingServer(_data);
            ECGReader _ecgReader = new(_data);
            ECGContainer _container = new ECGContainer(_ecgReader, new Processing());
            Log _log = new Log(_ecgReader);

            Thread t1 = new(_ecgServer.Run);
            Thread t2 = new(_ecgReader.Run);

            t1.Start();
            t2.Start();

            while(true)
            {

                Thread.Sleep(100);
            }

            _ecgServer.ShallStop = true;

            Console.WriteLine("stopped");
        }
    }
}
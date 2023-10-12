using System.Collections.Concurrent;

namespace ECGPro;

public class ECGReadingProducer
{
    private readonly BlockingCollection<DataContainer> _queue;
    private readonly ECGSensor ecgSensor;

    public ECGReadingProducer(BlockingCollection<DataContainer> queue, ECGSensor ecgSensor)
    {
        _queue = queue;
        this.ecgSensor = ecgSensor;
    }

    public void Run() => ReadSample();

    public void ReadSample()
    {
        var count = 500;
        while (count > 0)
        {
            var sample = ecgSensor.GenerateSample();
            var dataContainer = new DataContainer();
            dataContainer.Sample = sample;
            _queue.TryAdd(dataContainer);
            Thread.Sleep(2);
            //count--;
        }
        _queue.CompleteAdding();
    }
}
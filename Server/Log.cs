using Server;

namespace ECGPro;

public class Log : IObserver
{
    private readonly ECGReader _ecgReading;

    public Log(ECGReader ecgReading)
    {
        _ecgReading = ecgReading;
    }

    public void Update()
    {
        Console.WriteLine(_ecgReading.Sample);
    }
}
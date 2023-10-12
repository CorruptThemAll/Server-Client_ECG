namespace ECGPro;

public class Log : IObserver
{
    private readonly ECGReadingConsumer _ecgReading;

    public Log(ECGReadingConsumer ecgReading)
    {
        _ecgReading = ecgReading;
    }

    public void Update()
    {
        Console.WriteLine(_ecgReading.Sample);
    }
}
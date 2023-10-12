namespace ECGPro;

public class Log : IObserver
{
    private readonly ECGReadingClientSocket _ecgReading;

    public Log(ECGReadingClientSocket ecgReading)
    {
        _ecgReading = ecgReading;
    }

    public void Update()
    {
        Console.WriteLine(_ecgReading.Sample);
    }
}
namespace ECGPro;

public class ECGContainer : IObserver
{
    public List<int> ECGData { get; private set; } = new List<int>();
    private readonly ECGReadingConsumer _ecgReading; 
    private readonly IProcessing _processing;

    public ECGContainer(ECGReadingConsumer ecgReading, IProcessing processing)
    {
        _ecgReading = ecgReading;
        _processing = processing;
    }

    public void Update()
    {
        ECGData.Add(_ecgReading.Sample);
        if (ECGData.Count > 120)
        {
            _processing.Process(ECGData);
            ECGData.Clear();
        }
    }

    public void Process() => _processing.Process(ECGData);
}
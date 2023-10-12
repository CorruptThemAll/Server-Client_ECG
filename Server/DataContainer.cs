namespace ECGPro;

public class DataContainer
{
    private int _sample;
    private readonly object _lock = new();
    public int Sample
    {
        get
        {
            lock (_lock)
            {
                return _sample;
            }
        }
        set
        {
            lock (_lock)
            {
                _sample = value;
            }
        }
    }
}

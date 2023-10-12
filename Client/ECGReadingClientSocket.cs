using System.Collections.Concurrent;

namespace ECGPro;

public class ECGReadingClientSocket : Subject
{
    private readonly BlockingCollection<DataContainer> _queue;
    public int Sample { get; private set; }

    public ECGReadingClientSocket(BlockingCollection<DataContainer> queue)
    {
        _queue = queue;
    }

    public void Consume()
    {
        while (!_queue.IsCompleted)
        {
            if (_queue.TryTake(out var data))
            {
                Sample = data.Sample;
                Notify();
            }
        }
    }
}
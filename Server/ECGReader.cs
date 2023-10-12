using System.Collections.Concurrent;
using ECGPro;

namespace Server;

public class ECGReader : Subject
{
    public int Sample { get; private set; }

    private readonly BlockingCollection<DataContainer> _queue;

    public ECGReader(BlockingCollection<DataContainer> queue)
    {
        _queue = queue;
    }

    public void Run() => Consume();

    public void Consume()
    {
        while (!_queue.IsCompleted)
        {
            if (_queue.TryTake(out var data))
            {
                Sample = data.Sample;
            }
        }
    }
}
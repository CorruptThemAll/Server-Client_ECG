using System.Collections.Concurrent;

namespace ECGPro;

public abstract class Subject
{
    protected List<IObserver> observers = new List<IObserver>();

    public void Attach(IObserver obs)
    {
        observers.Add(obs);
    }

    public void Detach(IObserver obs)
    {
        observers.Remove(obs);
    }

    public void Notify()
    {
        observers.ForEach(o => o.Update());
    }
}
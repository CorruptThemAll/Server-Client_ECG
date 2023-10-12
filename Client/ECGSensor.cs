using System.Collections.Concurrent;

namespace ECGPro;

public class ECGSensor : Subject
{
    Random genRandom = new ();

    public int GenerateSample()
    {
        var sample = genRandom.Next(0, 50);
        return sample;
    }
}
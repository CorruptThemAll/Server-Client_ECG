namespace ECGPro;

public class Processing : IProcessing
{
    public void Process(List<int> samples)
    {
        if (samples.Count > 0)
        {
            Console.WriteLine("Processing... . .. .. " + samples.Average() + " Average value");
        }
    }
}
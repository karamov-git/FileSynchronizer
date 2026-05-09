namespace Confgirations;

public abstract class Configuration
{
    public SynchronizerType SynchronizerType { get; set; }
    public TimeSpan Period { get; set; }
    public string SyncPath { get; set; }
}
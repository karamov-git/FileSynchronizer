using System;

namespace SynchronizationManager;

public interface ISynchronizer
{
    public Synchronization Run(Configuration configuration);
}

public class Synchronizer : ISynchronizer
{
    public Synchronization Run(Configuration configuration)
    {
        throw new NotImplementedException();
    }
}
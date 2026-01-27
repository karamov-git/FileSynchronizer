using System;
using System.Collections.Generic;

namespace SynchronizationManager;

public interface ISynchronizationSource
{
    IEnumerable<Configuration> GetConfigurations();
}

public class FileSynchronizationSource : ISynchronizationSource
{
    public IEnumerable<Configuration> GetConfigurations()
    {
        throw new NotImplementedException();
    }
}
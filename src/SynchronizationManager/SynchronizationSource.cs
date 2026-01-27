using System;
using System.Collections.Generic;

namespace SynchronizationManager;

public interface ISynchronizationSource
{
    IEnumerable<Configuration> GetConfigurations();
}

public class SynchronizationSource : ISynchronizationSource
{
    public IEnumerable<Configuration> GetConfigurations()
    {
        throw new NotImplementedException();
    }
}
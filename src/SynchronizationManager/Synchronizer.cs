using System;
using System.Threading;
using Confgirations;
using Microsoft.Extensions.Logging;
using Synchronizations;

namespace SynchronizationManager;

public interface ISynchronizer
{
    public Synchronization Run(Configuration configuration, ILogger logger, CancellationToken cancellationToken);
}

public class Synchronizer : ISynchronizer
{
    public Synchronization Run(Configuration configuration, ILogger logger, CancellationToken cancellationToken)
    {
        switch (configuration.SynchronizerType)
        {
            case SynchronizerType.Git:
                return GitSynchronizer.Start(configuration as GitConfiguration ?? throw new InvalidOperationException(), logger, cancellationToken);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
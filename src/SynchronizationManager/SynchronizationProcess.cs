using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SynchronizationManager;

public class SynchronizationProcess(ISynchronizationSource synchronizationSource, ILogger logger, ISynchronizer synchronizer)
{
    public async Task Start(CancellationToken cancellationToken)
    {
        var configurations = synchronizationSource.GetConfigurations();

        var process = new List<Synchronization>();

        foreach (var configuration in configurations)
        {
            var synchronization = synchronizer.Run(configuration);
            process.Add(synchronization);
        }

        while (cancellationToken.IsCancellationRequested == false)
        {
            logger.LogSynchronization(process);
        }
    }
}
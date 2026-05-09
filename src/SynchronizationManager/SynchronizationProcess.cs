using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Synchronizations;

namespace SynchronizationManager;

public class SynchronizationProcess(IConfigurationSource configurationSource, ILogger logger, ISynchronizer synchronizer)
{
    public async Task Start(CancellationToken cancellationToken)
    {
        var configurations = configurationSource.GetConfigurations();

        var process = new List<Synchronization>();

        foreach (var configuration in configurations)
        {
            var synchronization = synchronizer.Run(configuration, logger, cancellationToken);
            process.Add(synchronization);
        }

        while (cancellationToken.IsCancellationRequested == false)
        {
            logger.LogSynchronization(process);
        }
    }
}
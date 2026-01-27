using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SynchronizationManager;

public class SynchronizationProcess
{
    private readonly ISynchronizationSource _synchronizationSource;
    private readonly ISynchronizer _synchronizer;
    private readonly ILogger _logger;

    public SynchronizationProcess(ISynchronizationSource synchronizationSource, ILogger logger, ISynchronizer synchronizer)
    {
        _synchronizationSource = synchronizationSource;
        _logger = logger;
        _synchronizer = synchronizer;
    }

    public async Task Start(CancellationToken cancellationToken)
    {
        var configurations = _synchronizationSource.GetConfigurations();

        var process = new List<Synchronization>();

        foreach (var configuration in configurations)
        {
            var synchronization = _synchronizer.Run(configuration);
            process.Add(synchronization);
        }

        while (cancellationToken.IsCancellationRequested == false)
        {
            _logger.LogSynchronization(process);
        }
    }
}
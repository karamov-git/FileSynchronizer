using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Synchronizations;

namespace SynchronizationManager;

public static class LoggerExtension
{
    public static void LogSynchronization(this ILogger logger, IEnumerable<Synchronization> synchronizations)
    {
        throw new NotImplementedException();
    }
}
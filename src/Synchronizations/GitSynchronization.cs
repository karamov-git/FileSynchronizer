using LibGit2Sharp;
using Microsoft.Extensions.Logging;

namespace Synchronizations;

public class GitSynchronization(ILogger logger, Repository repository, CancellationToken cancellationToken, GitCommitAuthor author, TimeSpan period)
    : Synchronization, IDisposable
{
    private Task? _process;

    public void Start()
    {
        _process = Task.Run(async () =>
        {
            while (cancellationToken.IsCancellationRequested == false)
            {
                await Task.Delay(period, cancellationToken);
                try
                {
                    await Sync();
                }
                catch (Exception e)
                {
                    logger.LogError("[{0}] Can't sync, error: {1}", SynchronizationId, e);
                    throw;
                }
            }
        }, cancellationToken);
    }


    private Task Sync()
    {
        Commands.Stage(repository, "*");
        if (repository.RetrieveStatus().IsDirty == false)
        {
            logger.LogInformation("[{0}] Not changes", SynchronizationId);
            return Task.CompletedTask;
        }

        var signature = new Signature(author.Name, author.Email, DateTimeOffset.UtcNow);
        repository.Commit("git_syncer_commit", signature, signature, new CommitOptions());
        repository.Network.Push(repository.Head, new PushOptions());
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _process?.Dispose();
        repository.Dispose();
    }
}
using System;
using System.IO;
using System.Threading;
using LibGit2Sharp;

namespace SynchronizationManager;

public interface ISynchronizer
{
    public Synchronization Run(Configuration configuration, CancellationToken cancellationToken);
}

public class Synchronizer : ISynchronizer
{
    public Synchronization Run(Configuration configuration, CancellationToken cancellationToken)
    {
        switch (configuration.SynchronizerType)
        {
            case SynchronizerType.Git:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public class GitSynchronizer
{
    public Synchronization Start(GitConfiguration gitConfiguration)
    {
        if (Directory.Exists(gitConfiguration.SyncPath) == false)
        {
            return Synchronization.Fail($"Can't found directory {gitConfiguration.SyncPath}");
        }

        Repository repo;
        try
        {
            repo = GetRepo(gitConfiguration.SyncPath, gitConfiguration.GitSettings);
        }
        catch (Exception e)
        {
            return Synchronization.Fail($"Can't open repo. Error {e}");
        }
    }


    private static Repository GetRepo(string repoLocalPath, GitSettings gitSettings)
    {
        Repository repo;

        if (Repository.IsValid(repoLocalPath) == false)
        {
            repo = new Repository(repoLocalPath);
        }
        else
        {
            var cloneOption = new CloneOptions();
            cloneOption.CredentialsProvider = (url, fromUrl, types) => new SshUserKeyCredentials()
            {
                Username = gitSettings.GitUserName,
                PrivateKey = gitSettings.PathToPrivateKey,
                PublicKey = gitSettings.PathToPublicKey,
                Passphrase = gitSettings.Passphrase
            };

            var path = Repository.Clone(gitSettings.SshSourceUrl, repoLocalPath, cloneOption);
            repo = new Repository(path);
        }

        return repo;
    }
}
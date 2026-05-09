using Confgirations;
using LibGit2Sharp;
using Microsoft.Extensions.Logging;

namespace Synchronizations;

public static class GitSynchronizer
{
    public static Synchronization Start(GitConfiguration gitConfiguration, ILogger logger, CancellationToken cancellationToken)
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

        var sync = new GitSynchronization(logger, repo, cancellationToken, new GitCommitAuthor()
        {
            Email = gitConfiguration.GitSettings.Email,
            Name = gitConfiguration.GitSettings.GitUserName
        }, gitConfiguration.Period);
        sync.Start();

        return sync;
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
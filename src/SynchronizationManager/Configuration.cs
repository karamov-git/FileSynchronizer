using System;

namespace SynchronizationManager;

public abstract class Configuration
{
    public SynchronizerType SynchronizerType { get; set; }
    public TimeSpan Period { get; set; }
    public string SyncPath { get; set; }
}

public class GitConfiguration : Configuration
{
    public GitSettings GitSettings { get; set; }
}

public class GitSettings
{
    public string SshSourceUrl { get; set; }
    public string GitUserName { get; set; }
    public string PathToPublicKey { get; set; }
    public string PathToPrivateKey { get; set; }
    public string Passphrase;
}

public enum SynchronizerType
{
    Git
}
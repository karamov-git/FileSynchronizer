namespace Confgirations;

public class GitSettings
{
    public string SshSourceUrl { get; set; }
    public string GitUserName { get; set; }
    public string PathToPublicKey { get; set; }
    public string PathToPrivateKey { get; set; }
    public string Email { get; set; }

    public string Passphrase;
}
namespace Synchronizations;

public class Synchronization
{
    protected string SynchronizationId { get; set; }
    public static Synchronization Fail(string messageError)
    {
        throw new System.NotImplementedException();
    }

    public string? LastErrorMessage { get; private set; } 

    protected void Fail(Exception ex)
    {
        
    }
    
}

public class GitCommitAuthor
{
    public string Name { get; set; }
    public string Email { get; set; }
}
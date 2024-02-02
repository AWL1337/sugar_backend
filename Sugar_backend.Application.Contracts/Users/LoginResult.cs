namespace Sugar_backend.Application.Contracts.Users;

public abstract record LoginResult
{
    private LoginResult() { }

    public sealed record Success : LoginResult;

    public sealed record NotFound : LoginResult;
    
    public sealed record Failure : LoginResult;
}
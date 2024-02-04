namespace Sugar_backend.Application.Contract.Users;

public abstract record LoginResult
{
    private LoginResult() { }

    public sealed record Success : LoginResult;

    public sealed record NotFound : LoginResult;
    
    public sealed record Failure : LoginResult;
}
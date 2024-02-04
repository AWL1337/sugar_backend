namespace Sugar_backend.Application.Models.Users;

public record User(long Id, string login, UserInfo Info)
{
    public User() : this(0, "", new UserInfo()) { }
}
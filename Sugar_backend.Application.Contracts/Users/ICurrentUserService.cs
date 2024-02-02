using Sugar_backend.Application.Models.Users;

namespace Sugar_backend.Application.Contracts.Users;

public interface ICurrentUserService
{
    User? User { get; }
}
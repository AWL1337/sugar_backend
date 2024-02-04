using Sugar_backend.Application.Models.Users;

namespace Sugar_backend.Application.Contract.Users;

public interface ICurrentUserService
{
    User? User { get; }
}
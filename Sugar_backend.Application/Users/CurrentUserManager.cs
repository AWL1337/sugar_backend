using Sugar_backend.Application.Contract.Users;
using Sugar_backend.Application.Models.Users;

namespace Sugar_backend.Application.Users;

public class CurrentUserManager : ICurrentUserService
{
    public User? User { get; set; }
}
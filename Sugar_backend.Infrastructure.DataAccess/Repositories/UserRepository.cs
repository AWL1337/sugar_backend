using Sugar_backend.Application.Abstractions.Repositories;
using Sugar_backend.Application.Models.Users;

namespace Sugar_backend.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    public User? AddUser(string login, string password, string name, DateTime birthday, Gender gender, int weight,
        int carbohydrateRatio, int breadUnit)
    {
        throw new NotImplementedException();
    }

    public void DeleteUserById(long id)
    {
        throw new NotImplementedException();
    }

    public User FindUserByLogin(string login)
    {
        throw new NotImplementedException();
    }

    public bool ChangeName(string login, string newName)
    {
        throw new NotImplementedException();
    }

    public bool ChangeBirthday(string login, DateTime newBirthday)
    {
        throw new NotImplementedException();
    }

    public bool ChangeGender(string login, Gender newGender)
    {
        throw new NotImplementedException();
    }

    public bool ChangeWeight(string login, int newWeight)
    {
        throw new NotImplementedException();
    }

    public bool ChangeCarbohydrateRatio(string login, int newCarbohydrateRatio)
    {
        throw new NotImplementedException();
    }

    public bool ChangeBreadUnit(string login, int newBreadUnit)
    {
        throw new NotImplementedException();
    }
}
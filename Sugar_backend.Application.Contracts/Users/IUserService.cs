using System.Collections.ObjectModel;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Models.Users;

namespace Sugar_backend.Application.Contract.Users;

public interface IUserService
{
    LoginResult Login(string login, string password);

    LoginResult Registration(
        string login,
        string password,
        string name,
        DateTime birthday,
        Gender gender,
        int weight,
        int height,
        int carbohydrateRatio,
        int breadUnit);
    
    bool ChangeName(string login, string newName);
    bool ChangeBirthday(string login, DateTime newBirthday);
    bool ChangeGender(string login, Gender newGender);
    bool ChangeWeight(string login, int newWeight);
    bool ChangeCarbohydrateRatio(string login, int newCarbohydrateRatio);
    bool ChangeBreadUnit(string login, int newBreadUnit);

    void DeleteUserById(long id);
}
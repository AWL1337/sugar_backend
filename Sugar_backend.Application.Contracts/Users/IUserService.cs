using System.Collections.ObjectModel;
using Sugar_backend.Application.Models.Users;
using Sugar_backend.Application.Abstractions.Repositories;

namespace Sugar_backend.Application.Contracts.Users;

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

    KeyValuePair<Collection<DateTime>, Collection<int>>? GetStatistic(INoteRepository repository);
    
    bool ChangeName(string newName);
    bool ChangeBirthday(DateTime newBirthday);
    bool ChangeGender(Gender newGender);
    bool ChangeWeight(int newWeight);
    bool ChangeCarbohydrateRatio(int newCarbohydrateRatio);
    bool ChangeBreadUnit(int newBreadUnit);
}
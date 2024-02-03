using System.Collections.ObjectModel;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Abstractions.Repositories;
using Sugar_backend.Application.Contracts.Users;
using Sugar_backend.Application.Models.Users;

namespace Sugar_backend.Application.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly CurrentUserManager _currentUserManager;

    public UserService(IUserRepository repository, CurrentUserManager currentUserManager)
    {
        _repository = repository;
        _currentUserManager = currentUserManager;
    }
    
    public LoginResult Login(string login, string password)
    {
        var user = _repository.FindUserByLogin(login);

        if (user is null)
        {
            return new LoginResult.NotFound();
        }

        _currentUserManager.User = user;
        return new LoginResult.Success();
    }

    public LoginResult Registration(string login, string password, string name, DateTime birthday, Gender gender, int weight,
        int height, int carbohydrateRatio, int breadUnit)
    {
        var user = _repository.FindUserByLogin(login);

        if (user is not null)
        {
            return new LoginResult.Failure();
        }

        _repository.AddUser(login, password, name, birthday, gender, weight,height, carbohydrateRatio, breadUnit);
        return new LoginResult.Success();
    }

    public KeyValuePair<Collection<DateTime>, Collection<int>>? GetStatistic(INoteRepository repository)
    {
        var allNotes = repository.GetAllNotes(_currentUserManager.User.login);

        if (allNotes is null)
            return null;
        
        Collection<DateTime> allDatesXOrdinat =  new();
        Collection<int> allSugarLevelYOrdinat =  new();
        foreach (var note in allNotes)
        {
            allDatesXOrdinat.Add(note.Date);
            allSugarLevelYOrdinat.Add(note.SugarLevel);
        }

        return new KeyValuePair<Collection<DateTime>, Collection<int>>(allDatesXOrdinat, allSugarLevelYOrdinat);
    }

    public bool ChangeName(string newName)
    {
        return _repository.ChangeName(_currentUserManager.User.login, newName);
    }

    public bool ChangeBirthday(DateTime newBirthday)
    {
        return _repository.ChangeBirthday(_currentUserManager.User.login, newBirthday);
    }

    public bool ChangeGender(Gender newGender)
    {
        return _repository.ChangeGender(_currentUserManager.User.login, newGender);
    }

    public bool ChangeWeight(int newWeight)
    {
        return _repository.ChangeWeight(_currentUserManager.User.login, newWeight);
    }

    public bool ChangeCarbohydrateRatio(int newCarbohydrateRatio)
    {
        return _repository.ChangeCarbohydrateRatio(_currentUserManager.User.login, newCarbohydrateRatio);
    }

    public bool ChangeBreadUnit(int newBreadUnit)
    {
        return _repository.ChangeBreadUnit(_currentUserManager.User.login, newBreadUnit);
    }
}
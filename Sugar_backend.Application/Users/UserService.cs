using System.Collections.ObjectModel;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Contract.Users;
using Sugar_backend.Application.Models.Users;
using Sugar_backend.Infrastructure.DataAccess.Repositories;

namespace Sugar_backend.Application.Users;

public class UserService(IUserRepository repository)
    : IUserService
{
    public LoginResult Login(string login, string password)
    {
        var user = repository.FindUser(login, password);

        if (user is null)
        {
            return new LoginResult.NotFound();
        }

        return new LoginResult.Success(user);
    }

    public LoginResult Registration(string login, string password, string name, DateTime birthday, Gender gender,
        int weight,
        int height, int carbohydrateRatio, int breadUnit)
    {
        var user = repository.FindUserByLogin(login);

        if (user is not null)
        {
            return new LoginResult.Failure();
        }

        repository.AddUser(login, password, name, birthday, gender, weight, height, carbohydrateRatio, breadUnit);
        return new LoginResult.Success(user);
    }

    public KeyValuePair<Collection<DateTime>, Collection<int>>? GetStatistic(string login)
    {
        var allNotes = NoteRepository.Instance?.GetAllNotes(login);

        if (allNotes is null)
            return new KeyValuePair<Collection<DateTime>, Collection<int>>();

        Collection<DateTime> allDatesXOrdinat = new();
        Collection<int> allSugarLevelYOrdinat = new();
        foreach (var note in allNotes)
        {
            allDatesXOrdinat.Add(note.Date);
            allSugarLevelYOrdinat.Add(note.SugarLevel);
        }

        return new KeyValuePair<Collection<DateTime>, Collection<int>>(allDatesXOrdinat, allSugarLevelYOrdinat);
    }

    public bool ChangeName(string login, string newName)
    {
        return repository.ChangeName(login, newName);
    }

    public bool ChangeBirthday(string login, DateTime newBirthday)
    {
        return repository.ChangeBirthday(login, newBirthday);
    }

    public bool ChangeGender(string login, Gender newGender)
    {
        return repository.ChangeGender(login, newGender);
    }

    public bool ChangeWeight(string login, int newWeight)
    {
        return repository.ChangeWeight(login, newWeight);
    }

    public bool ChangeCarbohydrateRatio(string login, int newCarbohydrateRatio)
    {
        return repository.ChangeCarbohydrateRatio(login, newCarbohydrateRatio);
    }

    public bool ChangeBreadUnit(string login, int newBreadUnit)
    {
        return repository.ChangeBreadUnit(login, newBreadUnit);
    }

    public void DeleteUserById(long id)
    {
        repository.DeleteUserById(id);
    }
}
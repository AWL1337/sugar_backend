using Sugar_backend.Application.Models.Users;

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
        int carbohydrateRatio, 
        int breadUnit);

    void GetStatistic();
    
    bool ChangeName(string newName);
    bool ChangeBirthday(DateTime newBirthday);
    bool ChangeGender(Gender newGender);
    bool ChangeWeight(int newWeight);
    bool ChangeCarbohydrateRatio(int newCarbohydrateRatio);
    bool ChangeBreadUnit(int newBreadUnit);

    void AddNote();

    void AddNewProduct(string name, int carbs);
}
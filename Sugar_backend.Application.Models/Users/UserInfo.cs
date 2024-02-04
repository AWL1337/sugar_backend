namespace Sugar_backend.Application.Models.Users;

public record UserInfo(
    string name, 
    DateTime birthday, 
    Gender gender, 
    int weight, 
    int carbohydrateRatio, 
    int grainUnit
)
{
    public UserInfo() : this("", DateTime.Now, Gender.Male, 0, 0, 0) { }
}
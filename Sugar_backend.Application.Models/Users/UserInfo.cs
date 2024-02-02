namespace Sugar_backend.Application.Models.Users;

public record UserInfo(
    string name, 
    DateTime birthday, 
    Gender gender, 
    int weight, 
    int carbohydrateRatio, 
    int grainUnit
    );
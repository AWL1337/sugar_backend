using Microsoft.AspNetCore.Mvc;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Models.Users;

namespace Controllers;

[ApiController]
[Route($"api/users")]
public class UserController(IUserRepository users)
{
    private IUserRepository Users { get; } = users;

    [HttpGet]
    public JsonResult GetUser(string login, string password)
    {
        return new JsonResult(users.FindUser(login, password));
    }

    [HttpPost]
    public JsonResult PostUser(
        string login, 
        string password,  
        string name, 
        DateTime birthday, 
        Gender gender, 
        int weight,
        int height,
        int carbohydrateRatio, 
        int breadUnit
        )
    {
        return new JsonResult(users.AddUser(login, password, name, birthday, gender, weight, height, carbohydrateRatio,
            breadUnit));
    }

    [HttpPut]
    public JsonResult PutChangeName(string login, string newName)
    {
        return new JsonResult(users.ChangeName(login, newName));
    }

    [HttpPut]
    public JsonResult PutChangeBirthday(string login, DateTime newBirthday)
    {
        return new JsonResult(users.ChangeBirthday(login, newBirthday));
    }
    
    [HttpPut]
    public JsonResult PutChangeGender(string login, Gender newGender)
    {
        return new JsonResult(users.ChangeGender(login, newGender));
    }
    
    [HttpPut]
    public JsonResult PutChangeWeight(string login, int newWeight)
    {
        return new JsonResult(users.ChangeWeight(login, newWeight));
    }
    
    [HttpPut]
    public JsonResult PutChangeCarbohydrateRatio(string login, int newCarbohydrateRatio)
    {
        return new JsonResult(users.ChangeCarbohydrateRatio(login, newCarbohydrateRatio));
    }
    
    [HttpPut]
    public JsonResult PutChangeBreadUnit(string login, int newBreadUnit)
    {
        return new JsonResult(users.ChangeBreadUnit(login, newBreadUnit));
    }

    [HttpDelete]
    public void DeleteUserById(long id)
    {
        users.DeleteUserById(id);
    }
}
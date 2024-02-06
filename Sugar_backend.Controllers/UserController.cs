using Microsoft.AspNetCore.Mvc;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Contract.Users;
using Sugar_backend.Application.Models.Users;

namespace Controllers;

[ApiController]
[Route($"api/users")]
public class UserController(IUserService userService) : Controller
{
    [HttpGet("{login}/{password}")]
    public JsonResult GetUser(string login, string password)
    {
        return new JsonResult(userService.Login(login, password));
    }

    [HttpGet("statistics/{login}")]
    public JsonResult GetStatistic(string login)
    {
        return new JsonResult(userService.GetStatistic(login));
    }

    [HttpPost]
    public JsonResult PostNewUser(
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
        return new JsonResult(userService.Registration(login, password, name, birthday, gender, weight, height,
            carbohydrateRatio,
            breadUnit));
    }

    [HttpPut("changeName/{login}")]
    public JsonResult PutChangeName(string login, string newName)
    {
        return new JsonResult(userService.ChangeName(login, newName));
    }

    [HttpPut("changeBirthday/{login}")]
    public JsonResult PutChangeBirthday(string login, DateTime newBirthday)
    {
        return new JsonResult(userService.ChangeBirthday(login, newBirthday));
    }

    [HttpPut("changeGender/{login}")]
    public JsonResult PutChangeGender(string login, Gender newGender)
    {
        return new JsonResult(userService.ChangeGender(login, newGender));
    }

    [HttpPut("changeWeight/{login}")]
    public JsonResult PutChangeWeight(string login, int newWeight)
    {
        return new JsonResult(userService.ChangeWeight(login, newWeight));
    }

    [HttpPut("changeCarbohydrateRatio/{login}")]
    public JsonResult PutChangeCarbohydrateRatio(string login, int newCarbohydrateRatio)
    {
        return new JsonResult(userService.ChangeCarbohydrateRatio(login, newCarbohydrateRatio));
    }

    [HttpPut("changeBreadUnit/{login}")]
    public JsonResult PutChangeBreadUnit(string login, int newBreadUnit)
    {
        return new JsonResult(userService.ChangeBreadUnit(login, newBreadUnit));
    }

    [HttpDelete("delete/{id}")]
    public void DeleteUserById(long id)
    {
        userService.DeleteUserById(id);
    }
}
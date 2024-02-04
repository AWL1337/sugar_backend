﻿using Microsoft.AspNetCore.Mvc;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Models.Users;

namespace Controllers;

[ApiController]
[Route($"api/users")]
public class UserController(IUserRepository userRepository)
{
    private IUserRepository _userRepository { get; } = userRepository;

    [HttpGet]
    public JsonResult GetUser(string login, string password)
    {
        return new JsonResult(_userRepository.FindUser(login, password));
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
        return new JsonResult(_userRepository.AddUser(login, password, name, birthday, gender, weight, height, carbohydrateRatio,
            breadUnit));
    }

    [HttpPut]
    public JsonResult PutChangeName(string login, string newName)
    {
        return new JsonResult(_userRepository.ChangeName(login, newName));
    }

    [HttpPut]
    public JsonResult PutChangeBirthday(string login, DateTime newBirthday)
    {
        return new JsonResult(_userRepository.ChangeBirthday(login, newBirthday));
    }
    
    [HttpPut]
    public JsonResult PutChangeGender(string login, Gender newGender)
    {
        return new JsonResult(_userRepository.ChangeGender(login, newGender));
    }
    
    [HttpPut]
    public JsonResult PutChangeWeight(string login, int newWeight)
    {
        return new JsonResult(_userRepository.ChangeWeight(login, newWeight));
    }
    
    [HttpPut]
    public JsonResult PutChangeCarbohydrateRatio(string login, int newCarbohydrateRatio)
    {
        return new JsonResult(_userRepository.ChangeCarbohydrateRatio(login, newCarbohydrateRatio));
    }
    
    [HttpPut]
    public JsonResult PutChangeBreadUnit(string login, int newBreadUnit)
    {
        return new JsonResult(_userRepository.ChangeBreadUnit(login, newBreadUnit));
    }

    [HttpDelete]
    public void DeleteUserById(long id)
    {
        _userRepository.DeleteUserById(id);
    }
}
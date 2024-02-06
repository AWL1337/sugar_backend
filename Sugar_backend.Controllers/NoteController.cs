using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Contract.Notes;
using Sugar_backend.Application.Models.Notes;

namespace Controllers;

[ApiController]
[Route("api/note")]
public class NoteController(INoteService noteService) : Controller
{
    [HttpGet("getAllNotes/{login}")]
    public JsonResult GetAllNotes(string login)
    {
        return new JsonResult(noteService.GetAllNotes(login));
    }

    [HttpGet("carbsAmount/{dateTime}/{login}")]
    public JsonResult GetNoteCarbsAmount(DateTime dateTime, string login)
    {
        return new JsonResult(noteService.GetNoteCarbsAmount(dateTime, login));
    }

    [HttpGet("notebyDate/{login}/{dateTime}")]
    public JsonResult GetNoteByDate(string login, DateTime dateTime)
    {
        return new JsonResult(noteService.GetNoteByDate(login, dateTime));
    }

    [HttpPost]
    public void PostCreateNote(
        long userId,
        NoteType type,
        DateTime date,
        int sugarLevel,
        Collection<NoteProduct> products)
    {
        noteService.CreateNote(userId, type, date, sugarLevel, products);
    }

    [HttpDelete("delete/{login}/{date}")]
    public void DeleteNote(string login, DateTime date)
    {
        noteService.DeleteNote(login, date);
    }
}
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Models.Notes;

namespace Controllers;

[ApiController]
[Route("api/note")]
public class NoteController(INoteRepository noteRepository)
{
    [HttpGet]
    public JsonResult GetAllNotes(string login)
    {
        return new JsonResult(noteRepository.GetAllNotes(login));
    }

    [HttpGet]
    public JsonResult GetNoteCarbsAmount(DateTime dateTime, string login)
    {
        return new JsonResult(noteRepository.GetNoteCarbsAmount(dateTime, login));
    }

    [HttpGet]
    public JsonResult GetNoteByDate(string login, DateTime dateTime)
    {
        return new JsonResult(noteRepository.GetNoteByDate(login, dateTime));
    }

    [HttpPost]
    public void PostNote(
        long userId,
        NoteType type,
        DateTime date,
        int sugarLevel,
        Collection<NoteProduct> products)
    {
        noteRepository.AddNote(userId, type, date, sugarLevel, products);
    }

    [HttpDelete]
    public void DeleteNote(string login, DateTime date)
    {
        noteRepository.DeleteNote(login, date);
    }
}
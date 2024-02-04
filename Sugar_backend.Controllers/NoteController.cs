using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Sugar_backend.Application.Abstractions.Repositories;
using Sugar_backend.Application.Models.Notes;

namespace Controllers;

[ApiController]
[Route("api/note")]
public class NoteController(INoteRepository noteRepository)
{
    private INoteRepository _noteRepository { get; } = noteRepository;

    [HttpGet]
    public JsonResult GetAllNotes(string login)
    {
        return new JsonResult(_noteRepository.GetAllNotes(login));
    }

    [HttpGet]
    public JsonResult GetNoteCarbsAmount(DateTime dateTime, string login)
    {
        return new JsonResult(_noteRepository.GetNoteCarbsAmount(dateTime, login));
    }

    [HttpGet]
    public JsonResult GetNoteByDate(string login, DateTime dateTime)
    {
        return new JsonResult(_noteRepository.GetNoteByDate(login, dateTime));
    }

    [HttpPost]
    public void PostNote(
        long userId,
        NoteType type,
        DateTime date,
        int sugarLevel,
        Collection<NoteProduct> products)
    {
        _noteRepository.AddNote(userId, type, date, sugarLevel,products);
    }

    [HttpDelete]
    public void DeleteNote(string login, DateTime date)
    {
        _noteRepository.DeleteNote(login, date);
    }
}
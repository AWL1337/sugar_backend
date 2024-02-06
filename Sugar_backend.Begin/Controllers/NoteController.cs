using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Sugar_backend.Application.Contract.Notes;
using Sugar_backend.Application.Models.Notes;

namespace begin.Controllers;

[ApiController]
[Route("api/note")]
public class NoteController(INoteService noteService) : Controller
{
    [HttpGet("getAllNotes/{login}")]
    public JsonResult GetAllNotes([FromBody]long userId)
    {
        return new JsonResult(noteService.GetAllNotes(userId));
    }
    
    [HttpGet("statistics/{login}")]
    public JsonResult GetStatistic([FromBody]long userId)
    {
        return new JsonResult(noteService.GetStatistic(userId));
    }

    [HttpGet("carbsAmount/{dateTime}/{login}")]
    public JsonResult GetNoteCarbsAmount([FromBody]DateTime dateTime, long userId)
    {
        return new JsonResult(noteService.GetNoteCarbsAmount(dateTime, userId));
    }

    [HttpGet("notebyDate/{login}/{dateTime}")]
    public JsonResult GetNoteByDate([FromBody]long userId, DateTime dateTime)
    {
        return new JsonResult(noteService.GetNoteByDate(userId, dateTime));
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
    public void DeleteNote([FromBody]long userId, DateTime date)
    {
        noteService.DeleteNote(userId, date);
    }
}
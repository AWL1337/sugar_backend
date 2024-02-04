using System.Collections.ObjectModel;
using Sugar_backend.Application.Abstractions.Repositories;
using Sugar_backend.Application.Contracts.Notes;
using Sugar_backend.Application.Models.Notes;
using Sugar_backend.Application.Models.Users;

namespace Sugar_backend.Application.Notes;

public class NoteService : INoteService
{
    private readonly INoteRepository _repository;
    
    public NoteService(INoteRepository repository)
    {
        _repository = repository;
    }
    
    public IEnumerable<Note> getAllNotes(string login)
    {
        return _repository.GetAllNotes(login);
    }

    public Note GetNoteByDate(string login, DateTime dateTime)
    {
        return _repository.GetNoteByDate(login, dateTime);
    }
    

    public int GetNotesInsulin(DateTime dateTime, UserInfo userInfo, string login)
    {
        var carbs = GetNoteCarbsAmount(dateTime, login);

        return (carbs / userInfo.grainUnit) / userInfo.carbohydrateRatio;
    }

    public int GetNoteCarbsAmount(DateTime dateTime, string login)
    {
       return _repository.GetNoteCarbsAmount(dateTime, login);
    }
    
    public void CreateNote(long login, NoteType type, DateTime date, int sugarLevel, Collection<NoteProduct> products)
    {
        _repository.AddNote(login, type, date, sugarLevel, products);
    }
}
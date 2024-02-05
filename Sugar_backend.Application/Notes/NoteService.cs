using System.Collections.ObjectModel;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Contract.Notes;
using Sugar_backend.Application.Models.Notes;
using Sugar_backend.Application.Models.Users;
using Sugar_backend.Infrastructure.DataAccess.Repositories;

namespace Sugar_backend.Application.Notes;

public class NoteService(INoteRepository repository) : INoteService
{
    public IEnumerable<Note> GetAllNotes(string login)
    {
        return NoteRepository.GetAllNotes(login);
    }

    public Note? GetNoteByDate(string login, DateTime dateTime) => repository.GetNoteByDate(login, dateTime);


    public int GetNotesInsulin(DateTime dateTime, UserInfo userInfo, string login)
    {
        var carbs = GetNoteCarbsAmount(dateTime, login);

        return (carbs / userInfo.grainUnit) / userInfo.carbohydrateRatio;
    }

    public int GetNoteCarbsAmount(DateTime dateTime, string login)
    {
       return repository.GetNoteCarbsAmount(dateTime, login);
    }
    
    public void CreateNote(long login, NoteType type, DateTime date, int sugarLevel, Collection<NoteProduct> products)
    {
        repository.AddNote(login, type, date, sugarLevel, products);
    }

    public void DeleteNote(string login, DateTime date)
    {
        repository.DeleteNote(login, date);
    }
}
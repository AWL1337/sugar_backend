using System.Collections.ObjectModel;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Contract.Notes;
using Sugar_backend.Application.Models.Notes;
using Sugar_backend.Application.Models.Users;
using Sugar_backend.Infrastructure.DataAccess.Repositories;

namespace Sugar_backend.Application.Notes;

public class NoteService(INoteRepository repository) : INoteService
{
    public IEnumerable<Note> GetAllNotes(long userId)
    {
        return repository.GetAllNotes(userId);
    }
    
    public KeyValuePair<Collection<DateTime>, Collection<int>>? GetStatistic(long userId)
    {
        var allNotes = repository.GetAllNotes(userId);

        if (allNotes is null)
            return new KeyValuePair<Collection<DateTime>, Collection<int>>();

        Collection<DateTime> allDatesXOrdinat = new();
        Collection<int> allSugarLevelYOrdinat = new();
        foreach (var note in allNotes)
        {
            allDatesXOrdinat.Add(note.Date);
            allSugarLevelYOrdinat.Add(note.SugarLevel);
        }

        return new KeyValuePair<Collection<DateTime>, Collection<int>>(allDatesXOrdinat, allSugarLevelYOrdinat);
    }

    public Note? GetNoteByDate(long userId, DateTime dateTime) => repository.GetNoteByDate(userId, dateTime);


    public int GetNotesInsulin(DateTime dateTime, UserInfo userInfo, long userId)
    {
        var carbs = GetNoteCarbsAmount(dateTime, userId);

        return (carbs / userInfo.grainUnit) / userInfo.carbohydrateRatio;
    }

    public int GetNoteCarbsAmount(DateTime dateTime, long userId)
    {
       return repository.GetNoteCarbsAmount(dateTime, userId);
    }
    
    public void CreateNote(long login, NoteType type, DateTime date, int sugarLevel, Collection<NoteProduct> products)
    {
        repository.AddNote(login, type, date, sugarLevel, products);
    }

    public void DeleteNote(long userId, DateTime date)
    {
        repository.DeleteNote(userId, date);
    }
}
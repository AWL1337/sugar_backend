using System.Collections.ObjectModel;
using Sugar_backend.Application.Models.Notes;

namespace Sugar_backend.Application.Abstraction.Repositories;

public interface INoteRepository
{
    static extern IEnumerable<Note> GetAllNotes(string login);
    int GetNoteCarbsAmount(DateTime dateTime, string login);
    
    Note? GetNoteByDate(string login, DateTime dateTime);
    
    void DeleteNote(string login, DateTime date);
    
    void AddNote(
        long userId, 
        NoteType type,
        DateTime date,
        int sugarLevel,
        Collection<NoteProduct> products);
}
using System.Collections.ObjectModel;
using Sugar_backend.Application.Models.Notes;

namespace Sugar_backend.Application.Abstraction.Repositories;

public interface INoteRepository
{
    IEnumerable<Note> GetAllNotes(long userId);
    int GetNoteCarbsAmount(DateTime dateTime, long userId);
    
    Note? GetNoteByDate(long userId, DateTime dateTime);
    
    void DeleteNote(long userId, DateTime date);
    
    void AddNote(
        long userId, 
        NoteType type,
        DateTime date,
        int sugarLevel,
        Collection<NoteProduct> products);
}
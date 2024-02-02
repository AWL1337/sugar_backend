using System.Collections.ObjectModel;
using Sugar_backend.Application.Models.Notes;

namespace Sugar_backend.Application.Contracts.Notes;

public interface INoteService
{
    IEnumerable<Note> getAllNotes();

    Note GetNoteByDate(long userId, DateTime dateTime);

    void CreateNote(
        long userId, 
        NoteType type,
        DateTime date,
        int sugarLevel,
        Collection<NoteProduct> products);
}
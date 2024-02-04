using System.Collections.ObjectModel;
using Sugar_backend.Application.Models.Notes;
using Sugar_backend.Application.Models.Users;

namespace Sugar_backend.Application.Contract.Notes;

public interface INoteService
{
    IEnumerable<Note> GetAllNotes(string login);

    Note? GetNoteByDate(string login, DateTime dateTime);
    int GetNotesInsulin(DateTime dateTime, UserInfo userInfo, string login);

    int GetNoteCarbsAmount(DateTime dateTime, string login);

    void CreateNote(
        long userId, 
        NoteType type,
        DateTime date,
        int sugarLevel,
        Collection<NoteProduct> products);
}
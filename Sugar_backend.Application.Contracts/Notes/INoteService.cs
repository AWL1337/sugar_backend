using System.Collections.ObjectModel;
using Sugar_backend.Application.Models.Notes;
using Sugar_backend.Application.Models.Users;

namespace Sugar_backend.Application.Contract.Notes;

public interface INoteService
{
    IEnumerable<Note> GetAllNotes(long userId);

    Note? GetNoteByDate(long userId, DateTime dateTime);
    int GetNotesInsulin(DateTime dateTime, UserInfo userInfo, long userId);

    KeyValuePair<Collection<DateTime>, Collection<int>>? GetStatistic(long userId);

    int GetNoteCarbsAmount(DateTime dateTime, long userId);

    void CreateNote(
        long userId,
        NoteType type,
        DateTime date,
        int sugarLevel,
        Collection<NoteProduct> products);

    void DeleteNote(long userId, DateTime date);
}
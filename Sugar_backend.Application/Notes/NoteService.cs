using System.Collections.ObjectModel;
using Sugar_backend.Application.Abstractions.Repositories;
using Sugar_backend.Application.Contracts.Notes;
using Sugar_backend.Application.Models.Notes;

namespace Sugar_backend.Application.Notes;

public class NoteService : INoteService
{
    private readonly INoteRepository _repository;
    
    public NoteService(INoteRepository repository)
    {
        _repository = repository;
    }
    
    public IEnumerable<Note> getAllNotes()
    {
        return _repository.GetAllNotes();
    }

    public Note GetNoteByDate(long userId, DateTime dateTime)
    {
        return _repository.GetNoteByDate(userId, dateTime);
    }

    public void CreateNote(long userId, NoteType type, DateTime date, int sugarLevel, Collection<NoteProduct> products)
    {
        _repository.AddNote(userId, type, date, sugarLevel, products);
    }
}
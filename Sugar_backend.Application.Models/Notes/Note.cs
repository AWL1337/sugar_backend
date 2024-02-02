using System.Collections.ObjectModel;

namespace Sugar_backend.Application.Models.Notes;

public record Note(
    long userId,
    NoteType Type, 
    DateTime Date, 
    int SugarLevel, 
    Collection<NoteProduct> Products
    );
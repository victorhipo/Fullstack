using System;

namespace TechNotes.domain.Notes;

public interface INoteRepository
{
    Task<List<Note>>GetAllNotesAsync();
    Task<Note> CreateNoteAsync(Note note);
}

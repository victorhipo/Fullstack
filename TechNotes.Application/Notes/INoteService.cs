using System;
using TechNotes.domain.Notes;

namespace TechNotes.Application.Notes;

public interface INoteService
{
    List<Note> GetAllNotes();
}

using System;
using Mapster;
using MediatR;
using TechNotes.domain.Notes;

namespace TechNotes.Application.Notes.UpdateNote;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, NoteResponse?>
{
    private readonly INoteRepository _noteRepository;


    public UpdateNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<NoteResponse?> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var noteToUpdate = request.Adapt<Note>();
        var UpdateNote = await _noteRepository.UpdateNoteAsync(noteToUpdate);
        if(UpdateNote is null) return null;
        return UpdateNote.Adapt<NoteResponse>();
    }
}

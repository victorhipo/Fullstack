using System;
using Mapster;
using MediatR;
using TechNotes.domain.Notes;

namespace TechNotes.Application.Notes.CreateNote;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteResponse>
{
    private readonly INoteRepository _noteRepository;

    public CreateNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    public async Task<NoteResponse> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        //* Mapeamos la nota de forma manual
        // var newNote = new Note
        // {
        //     Title = request.Title,
        //     Content = request.Content,
        //     PublishedAt = request.PublishedAt,
        //     IsPublished = request.IsPublished
        // };
        //* Mapeamos la nota con mapster
        var NewNote = request.Adapt<Note>();
        var notes = await _noteRepository.CreateNoteAsync(NewNote);
        return notes.Adapt<NoteResponse>();
    }
}

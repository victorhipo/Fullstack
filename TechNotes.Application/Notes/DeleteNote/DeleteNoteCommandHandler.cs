using System;
using MediatR;
using MediatR.Pipeline;
using TechNotes.domain.Notes;

namespace TechNotes.Application.Notes.DeleteNote;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, bool>
{
    private INoteRepository _noteRepository;

    public DeleteNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        return await _noteRepository.DeleteNoteAsync(request.Id);
    }
}

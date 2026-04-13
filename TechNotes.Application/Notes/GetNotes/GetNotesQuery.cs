using System;
using MediatR;
using TechNotes.domain.Notes;

namespace TechNotes.Application.Notes.GetNotes;

public class GetNotesQuery:IRequest<List<Note>>
{
    

}

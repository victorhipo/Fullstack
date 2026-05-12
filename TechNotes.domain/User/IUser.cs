using System;
using TechNotes.domain.Notes;

namespace TechNotes.domain.User;

public interface IUser
{
    public string Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public List<Note> Notes { get; set; }
}

using System;
using Microsoft.AspNetCore.Identity;
using TechNotes.domain.Notes;
using TechNotes.domain.User;

namespace TechNotes.Infrastructure.Authetication;

public class User : IdentityUser, IUser
{
    public List<Note> Notes { get; set;  } = new List<Note>();
}

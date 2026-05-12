using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechNotes.domain.Notes;
using TechNotes.Infrastructure.Authetication;

namespace TechNotes.Infrastructure;

public class ApplicationDbContext: IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }
    
    public DbSet<Note> Notes{ get; set; }
}

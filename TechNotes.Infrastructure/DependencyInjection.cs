using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechNotes.domain.Notes;
using TechNotes.Infrastructure.Repositories;

namespace TechNotes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("TechNotes.Infraestructure")
            )
        );
        services.AddScoped<INoteRepository, NoteRepository>();
        return services;
    }
}

using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechNotes.domain.Notes;
using TechNotes.Infrastructure.Authetication;
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
        AddAuthentication(services);
        return services;
    }
    private static void AddAuthentication(IServiceCollection services)
    {
        services.AddScoped<TechNotes.Application.Authentication.IAuthenticationService, TechNotes.Infrastructure.Authentication.AuthenticationService>();
        services.AddScoped<AuthenticationStateProvider,ServerAuthenticationStateProvider>();
        services.AddCascadingAuthenticationState();
        services.AddAuthorization();
        services.AddAuthentication( options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = IdentityConstants.ExternalScheme;
            
        }).AddIdentityCookies();
        services.AddIdentityCore<User>().AddEntityFrameworkStores<ApplicationDbContext>().AddSignInManager().AddDefaultTokenProviders();
    }
}

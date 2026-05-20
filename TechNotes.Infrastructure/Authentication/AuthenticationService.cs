using System;
using Microsoft.AspNetCore.Identity;
using TechNotes.Application.Authentication;
using TechNotes.Infrastructure.Authetication;

namespace TechNotes.Infrastructure.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userInManager;

    public AuthenticationService(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userInManager = userManager;

    }

    public async Task<bool> LoginUserAsync(string userName, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(userName, password,isPersistent: false, lockoutOnFailure: false);
        return result.Succeeded;
    }

    public async Task<RegisterUserResponse> RegisterUserAsync(string userName, string email, string password)
    {
        var user = new User
        {
            UserName = userName,
            Email = email,
            EmailConfirmed = true
        };
        var result = await _userInManager.CreateAsync(user, password);
        return new RegisterUserResponse
        {
            Succeded = result.Succeeded,
            Errors = result.Errors.Select(e => e.Description).ToList()
        };
    }
}
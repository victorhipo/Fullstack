using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechNotes.Infrastructure.Authetication;

namespace TechNotes.Components;

[Route("account")]
public class AcountController: Controller
{
    private readonly SignInManager<User> _signinManager;
    private readonly UserManager<User> _userManager;

    public AcountController(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signinManager = signInManager;
        _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpPost("external-login")]
    public IActionResult ExternalLogin(string provider)
    {
        var redirectUrl =  Url.Action(nameof(HandleExternalCallBack));
        var properties = _signinManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }
    [AllowAnonymous]
    [HttpGet("external-callback")]
    public async Task<IActionResult> HandleExternalCallBack()
    {
        var info = await _signinManager.GetExternalLoginInfoAsync();
        if(info == null) return RedirectWithError("Error obteniendo información de google.");
        var result = await _signinManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
        if(result.Succeeded) return Redirect("/notes");
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        if(string.IsNullOrEmpty(email)) return RedirectWithError("No se obtuvo el email del usuario.");
        var user = await _userManager.FindByEmailAsync(email) ?? new User
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true 
        };
        await _userManager.CreateAsync(user);
        await _userManager.AddLoginAsync(user, info);
        await _signinManager.SignInAsync(user, isPersistent: false);
        return Redirect("/notes");
    }
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signinManager.SignOutAsync();
        return Redirect("/notes");
    }
    private IActionResult RedirectWithError(string message)
    {
        var encoded = Uri.EscapeDataString(message);
        return Redirect($"register?error={encoded}");
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace TechNotes.Features.Users;

public class RegisterUserModel
{  
    [Required(ErrorMessage = "Nombre de usurio requerido.")]
    public string UserName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email requerido.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contraseña requerida.")]
    public string Password { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Confirmación de contraseña requerida.")]
    [Compare("Password", ErrorMessage = "La contraseña no coincide.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

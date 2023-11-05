#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace SesionTaller.Models;

public class Login{
    [Required(ErrorMessage = "The Name is required")]
    [MinLength(2, ErrorMessage = "The Name must be at least 2 characters.")]
    public string Name {get; set;}
}


using System.ComponentModel.DataAnnotations;

namespace Models.DTOs;

public class SignUpDto
{
    [Required(ErrorMessage = "The username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "The username is required")]
    public string Password { get; set; }
}
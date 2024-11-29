using System.ComponentModel.DataAnnotations;

namespace Models.DTO;

public class SignUpDto
{
    [Required(ErrorMessage = "The username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "The password is required")]
    [StringLength(10, MinimumLength = 4, 
                ErrorMessage = "The password must have a minimun of 4 characters and a maximum of 10 characters")]
    public string Password { get; set; }

    [Required]
    public string Lastname { get; set; }

    [Required]
    public string Firstname { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string  Role { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Models.Entities;

public class Specialty
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(60, MinimumLength = 1, ErrorMessage = "The Name of Specialty must have at minimum 1 and maximum 60 characters")]
    public string NameSpecialty { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "The Description must have at minimum 1 and maximum 100 characters")]
    public string Description { get; set; }

    public bool State { get; set; }
    
}
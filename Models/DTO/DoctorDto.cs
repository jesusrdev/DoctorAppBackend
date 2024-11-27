using System.ComponentModel.DataAnnotations;

namespace Models.DTO;

public class DoctorDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Lastname is required")]
    [StringLength(60, MinimumLength = 1,
        ErrorMessage = "The lastname must have at minimum 1 and maximum 60 characters")]
    public string Lastname { get; set; }

    [Required(ErrorMessage = "Firstname is required")]
    [StringLength(60, MinimumLength = 1,
        ErrorMessage = "The firstname must have at minimum 1 and maximum 60 characters")]
    public string Firstname { get; set; }

    [Required(ErrorMessage = "Direction is required")]
    [StringLength(100, MinimumLength = 1,
        ErrorMessage = "The direction must have at minimum 1 and maximum 100 characters")]
    public string Direction { get; set; }

    [MaxLength(40)] public string Phone { get; set; }

    [Required(ErrorMessage = "Genre is required")]
    public char Genre { get; set; }

    [Required(ErrorMessage = "Specialty is Required")]
    public int SpecialtyId { get; set; }

    public string? NameSpecialty { get; set; }

    public int State { get; set; }
}
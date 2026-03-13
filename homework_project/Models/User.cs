using System.ComponentModel.DataAnnotations;

namespace homework_project.Models;


public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ім'я є обов'язковим")]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Прізвище є обов'язковим")]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email є обов'язковим")]
    [EmailAddress(ErrorMessage = "Невірний формат Email")]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Телефон є обов'язковим")]
    [Phone(ErrorMessage = "Невірний формат телефону")]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Дата народження є обов'язковою")]
    public DateOnly DateOfBirth { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

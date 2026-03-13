using System.ComponentModel.DataAnnotations;

namespace homework_project.DTOs;

public class UserDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    public string FullName { get; set; } = string.Empty;

    public int Age { get; set; }
}

public class CreateUserDto
{
    [Required(ErrorMessage = "Ім'я є обов'язковим")]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Прізвище є обов'язковим")]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email є обов'язковим")]
    [EmailAddress(ErrorMessage = "Невірний формат Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Телефон є обов'язковим")]
    [Phone(ErrorMessage = "Невірний формат телефону")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Дата народження є обов'язковою")]
    public DateOnly DateOfBirth { get; set; }
}

public class UpdateUserDto
{
    [Required(ErrorMessage = "Ім'я є обов'язковим")]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Прізвище є обов'язковим")]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email є обов'язковим")]
    [EmailAddress(ErrorMessage = "Невірний формат Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Телефон є обов'язковим")]
    [Phone(ErrorMessage = "Невірний формат телефону")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Дата народження є обов'язковою")]
    public DateOnly DateOfBirth { get; set; }
}

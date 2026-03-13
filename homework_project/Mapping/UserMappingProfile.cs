using AutoMapper;
using homework_project.DTOs;
using homework_project.Models;

namespace homework_project.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.FullName,
                       opt  => opt.MapFrom(src => $"{src.Name} {src.LastName}"))
            .ForMember(dest => dest.Age,
                       opt  => opt.MapFrom(src => CalculateAge(src.DateOfBirth)));

        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.Id,        opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.Id,        opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
    }

    /// <summary>
    /// Обчислює вік у роках на основі дати народження
    /// </summary>
    private static int CalculateAge(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age   = today.Year - dateOfBirth.Year;

        // Якщо день народження ще не настав цього року — віднімаємо 1
        if (dateOfBirth > today.AddYears(-age))
            age--;

        return age;
    }
}

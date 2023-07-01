using AutoMapper;
using WebApp.Database;
using WebApp.Model;

namespace WebApp.Mapping
{
    public class CustomExampleClassMappingProfile : Profile
    {
        public static Dictionary<string, string> ServiceQueryMappings = new Dictionary<string, string>()
        {
            { nameof(CustomExampleClassDto.CustomId), nameof(ExampleClass.Id)},
            { nameof(CustomExampleClassDto.CustomName), nameof(ExampleClass.Name)},
            { nameof(CustomExampleClassDto.CustomIsConfirmed), nameof(ExampleClass.IsConfirmed)},
            { nameof(CustomExampleClassDto.CustomEmail), nameof(ExampleClass.Email)},
            { nameof(CustomExampleClassDto.CustomBirthDate), nameof(ExampleClass.BirthDate)},
        };

        public CustomExampleClassMappingProfile()
        {
            CreateMap<ExampleClass, CustomExampleClassDto>()
                .ForMember(x => x.CustomId, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.CustomName, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.CustomIsConfirmed, y => y.MapFrom(z => z.IsConfirmed))
                .ForMember(x => x.CustomEmail, y => y.MapFrom(z => z.Email))
                .ForMember(x => x.CustomBirthDate, y => y.MapFrom(z => z.BirthDate))
                .ReverseMap();
        }
    }
}
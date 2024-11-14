using AutoMapper;
using Models.DTO;
using Models.Entities;

namespace Utils;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Specialty, SpecialtyDto>()
            .ForMember(d => d.State, 
                m => m.MapFrom(
                    o => o.State == true ? 1 : 0));
    }
}
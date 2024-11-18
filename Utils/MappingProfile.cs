using AutoMapper;
using Models.DTO;
using Models.Entities;

namespace Utils;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Specialty, SpecialtyDto>()
            .ForMember(s => s.State, 
                m => m.MapFrom(
                    s => s.State == true ? 1 : 0));
        
        CreateMap<Doctor, DoctorDto>()
            .ForMember(d => d.State, 
                m => m.MapFrom(
                    o => o.State == true ? 1 : 0))
            .ForMember(d => d.NameSpecialty,
                m => m.MapFrom(
                        d => d.Specialty.NameSpecialty
                    ));
    }
}
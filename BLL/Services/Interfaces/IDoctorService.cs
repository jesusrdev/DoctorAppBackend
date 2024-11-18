using Models.DTO;

namespace BLL.Services.Interfaces;

public interface IDoctorService
{
    Task<IEnumerable<DoctorDto>> GetAll();

    Task<DoctorDto> Add(DoctorDto modelDto);

    Task Update(DoctorDto modelDto);

    Task Remove(int id);
}
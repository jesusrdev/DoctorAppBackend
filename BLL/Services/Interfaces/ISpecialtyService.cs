using Models.DTO;

namespace BLL.Services.Interfaces;

public interface ISpecialtyService
{
    Task<IEnumerable<SpecialtyDto>> GetAll();

    Task<SpecialtyDto> Add(SpecialtyDto modelDto);

    Task Update(SpecialtyDto modelDto);

    Task Remove(int id);
}
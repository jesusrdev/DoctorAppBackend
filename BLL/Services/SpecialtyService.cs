using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepository;
using Models.DTO;
using Models.Entities;

namespace BLL.Services;

public class SpecialtyService : ISpecialtyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SpecialtyService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SpecialtyDto> Add(SpecialtyDto modelDto)
    {
        try
        {
            Specialty specialty = new Specialty()
            {
                NameSpecialty = modelDto.NameSpecialty,
                Description = modelDto.Description,
                State = modelDto.State == 1,
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            await _unitOfWork.Specialty.Add(specialty);
            await _unitOfWork.Save();

            if (specialty.Id == 0) throw new TaskCanceledException("The specialty could not be created");

            return _mapper.Map<SpecialtyDto>(specialty);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<IEnumerable<SpecialtyDto>> GetAll()
    {
        try
        {
            var list = await _unitOfWork.Specialty.GetAll(
                orderBy: s => s.OrderBy(s => s.NameSpecialty)
            );

            return _mapper.Map<IEnumerable<SpecialtyDto>>(list);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<IEnumerable<SpecialtyDto>> GetActive()
    {
        try
        {
            var list = await _unitOfWork.Specialty.GetAll(
                orderBy: s => s.OrderBy(s => s.NameSpecialty),
                filter: s => s.State
            );

            return _mapper.Map<IEnumerable<SpecialtyDto>>(list);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task Update(SpecialtyDto modelDto)
    {
        try
        {
            var specialtyDb = await _unitOfWork.Specialty.GetFirst(s => s.Id == modelDto.Id);

            if (specialtyDb == null) throw new TaskCanceledException("The specialty doesn't exist");

            specialtyDb.NameSpecialty = modelDto.NameSpecialty;
            specialtyDb.Description = modelDto.Description;
            specialtyDb.State = modelDto.State == 1;

            _unitOfWork.Specialty.Update(specialtyDb);
            await _unitOfWork.Save();
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task Remove(int id)
    {
        try
        {
            var specialtyDb = await _unitOfWork.Specialty.GetFirst(s => s.Id == id);

            if (specialtyDb == null) throw new TaskCanceledException("The specialty doesn't exist");

            _unitOfWork.Specialty.Remove(specialtyDb);
            await _unitOfWork.Save();
        }
        catch (Exception e)
        {
            throw;
        }
    }
}
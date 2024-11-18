using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepository;
using Models.DTO;
using Models.Entities;

namespace BLL.Services;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DoctorDto> Add(DoctorDto modelDto)
    {
        try
        {
            Doctor doctor = new Doctor()
            {
                Lastname = modelDto.Lastname,
                Firstname = modelDto.Firstname,
                Direction = modelDto.Direction,
                Phone = modelDto.Phone,
                Genre = modelDto.Genre,
                State = modelDto.State == 1,
                SpecialtyId = modelDto.SpecialtyId,
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };

            await _unitOfWork.Doctor.Add(doctor);
            await _unitOfWork.Save();

            if (doctor.Id == 0) throw new TaskCanceledException("The doctor could not be created");

            return _mapper.Map<DoctorDto>(doctor);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<IEnumerable<DoctorDto>> GetAll()
    {
        try
        {
            var list = await _unitOfWork.Doctor.GetAll(
                orderBy: d => d.OrderBy(d => d.Lastname)
            );

            return _mapper.Map<IEnumerable<DoctorDto>>(list);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task Update(DoctorDto modelDto)
    {
        try
        {
            var doctorDb = await _unitOfWork.Doctor.GetFirst(s => s.Id == modelDto.Id);

            if (doctorDb == null) throw new TaskCanceledException("The doctor doesn't exist");

            doctorDb.Lastname = modelDto.Lastname;
            doctorDb.Firstname = modelDto.Firstname;
            doctorDb.Direction = modelDto.Direction;
            doctorDb.Phone = modelDto.Phone;
            doctorDb.Genre = modelDto.Genre;
            doctorDb.State = modelDto.State == 1;
            doctorDb.SpecialtyId = modelDto.SpecialtyId;
            doctorDb.UpdateDate = DateTime.Now;

            _unitOfWork.Doctor.Update(doctorDb);
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
            var doctorDb = await _unitOfWork.Doctor.GetFirst(d => d.Id == id);

            if (doctorDb == null) throw new TaskCanceledException("The doctor doesn't exist");

            _unitOfWork.Doctor.Remove(doctorDb);
            await _unitOfWork.Save();
        }
        catch (Exception e)
        {
            throw;
        }
    }
}
using Models.Entities;

namespace Data.Interfaces.IRepository;

public interface IDoctorRepository : IGenericRepository<Doctor>
{
    void Update(Doctor doctor);
}
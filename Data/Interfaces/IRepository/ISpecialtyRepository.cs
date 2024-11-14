using Models.Entities;

namespace Data.Interfaces.IRepository;

public interface ISpecialtyRepository : IGenericRepository<Specialty>
{
    void Update(Specialty specialty);
}
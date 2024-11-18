namespace Data.Interfaces.IRepository;

public interface IUnitOfWork : IDisposable
{
    ISpecialtyRepository Specialty { get; }
    IDoctorRepository Doctor { get; }

    Task Save();
}